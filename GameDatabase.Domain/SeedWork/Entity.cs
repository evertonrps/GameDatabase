namespace GameDatabase.Domain.SeedWork;

public abstract class Entity<T> : BaseValidator
{
    public string Id { get; protected set; }

    //    public abstract bool IsValid();

    //  public ValidationResult ValidationResult { get; protected set; }
}