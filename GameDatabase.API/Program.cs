using System.Reflection;
using GameDatabase.API.Schema.Formatters;
using GameDatabase.API.Schema.Mutations;
using GameDatabase.IoC;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1, 0);
    v.ReportApiVersions = true;
    v.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(e =>
{
    e.GroupNameFormat = "'v'VVV";
    e.SubstituteApiVersionInUrl = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
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

app.UseGlobalExceptionHandler(x =>
{
    x.ContentType = "application/json";
    x.ResponseBody(s=> JsonConvert.SerializeObject(new
    {
        Message = s.Message ?? $"An error ocurred whilst processing your request"
    }));
    
    x.Map<UnauthorizedAccessException>().ToStatusCode(StatusCodes.Status401Unauthorized)
        .WithBody((ex, context) => JsonConvert.SerializeObject(new
        {
            Message = ex.Message
        }));
});

//GraphQL
app.MapGraphQL();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();