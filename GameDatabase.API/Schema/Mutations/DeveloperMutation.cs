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

    [Error(typeof(MyCustomErrorA))]
    [Error(typeof(MyCustomErrorB))]
    public async Task<MutationResult<Developer>> CreateDeveloper(string name, DateTime founded, string site)
    {
        var errors = new List<object>();
        if (founded > DateTime.Today) errors.Add(new MyCustomErrorA("Data inválida"));

        if (name == "string") errors.Add(new MyCustomErrorB("Name invalido"));

        if (errors.Count > 0) return new MutationResult<Developer>(errors);
        return await _developerService.CreateDeveloper(Developer.Factory(name, founded, site));
    }
}

public class DomainExceptionA
{
    public DomainExceptionA(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

public class DomainExceptionB
{
    public DomainExceptionB(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

public class UserNameTakenError
{
}

public class CreateUserErrorFactory
    : IPayloadErrorFactory<MyCustomError, DomainExceptionA>
        , IPayloadErrorFactory<MyCustomError, DomainExceptionB>
{
    DomainExceptionA IPayloadErrorFactory<MyCustomError, DomainExceptionA>.CreateErrorFrom(MyCustomError exception)
    {
        return new DomainExceptionA(exception.Message);
    }

    DomainExceptionB IPayloadErrorFactory<MyCustomError, DomainExceptionB>.CreateErrorFrom(MyCustomError exception)
    {
        return new DomainExceptionB(exception.Message);
    }

    public MyCustomError CreateErrorFrom(DomainExceptionA ex)
    {
        return new MyCustomErrorA(ex.Message);
    }

    public MyCustomError CreateErrorFrom(DomainExceptionB ex)
    {
        return new MyCustomErrorB(ex.Message);
    }
}

public class MyCustomErrorB : MyCustomError
{
    public MyCustomErrorB(string message)
    {
        Message = message;
    }

    public override string Message { get; }
}

public class MyCustomErrorA(string message) : MyCustomError
{
    public override string Message { get; } = message;
}

public class MyCustomError : Exception
{
}