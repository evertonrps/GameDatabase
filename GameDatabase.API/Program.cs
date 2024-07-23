using System.Reflection;
using Asp.Versioning;
using GameDatabase.API.Filters;
using GameDatabase.API.Schema.Formatters;
using GameDatabase.API.Schema.Mutations;
using GameDatabase.IoC;
using Microsoft.OpenApi.Models;
using Serilog;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
}).AddApiExplorer(e =>
{
    e.GroupNameFormat = "'v'VVV";
    e.SubstituteApiVersionInUrl = true;
});
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo{ Title = "Game Database", Version = "v1"});
    s.SwaggerDoc("v2", new OpenApiInfo{ Title = "Game Database", Version = "v2"});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
    
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
BootStrapper.ConfigureServices(builder.Services);
builder.Host.UseSerilog(Log.Logger);

//GraphiQL
builder.Services.AddScoped<DeveloperMutation>();
builder.Services.AddHttpResponseFormatter<CustomHttpResponseFormatter>();

//GraphQL Config
builder.Services.AddGraphQLServer()
    .AddAPITypes()
    .AddQueryType()
    .AddMutationConventions()
    .AddMutationType();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(_ => { });

//GraphQL
app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();