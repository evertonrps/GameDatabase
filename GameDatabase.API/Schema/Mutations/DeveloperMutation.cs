using GameDatabase.Domain.AggregatesModel.GameAggregate;
using GameDatabase.Domain.Interfaces.Services;
using HotChocolate.Language;

namespace GameDatabase.API.Schema.Mutations;

[ExtendObjectType(OperationType.Mutation)]
public class DeveloperMutation
{
    private readonly IDeveloperService _developerService;

    public DeveloperMutation(IDeveloperService developerService)
    {
        _developerService = developerService;
    }
    
    public async Task<MutationResult<Developer>> CreateDeveloper(string name, DateTime founded, string site)
    {
        var errors = new List<object>();
        if (founded > DateTime.Today)
        {
            errors.Add(new InvaliMydDateError(founded.ToString()));
        }

        if (errors.Count > 0)
        {
            return new MutationResult<Developer>(errors);
        }
      return  await _developerService.CreateDeveloper(Developer.Factory(name, founded, site));
    }
}


public record InvaliMydDateError
{
    public string Message { get; set; }
    public InvaliMydDateError(string date)
    {
        Message = $"Invalid date time {date}";
    }
}