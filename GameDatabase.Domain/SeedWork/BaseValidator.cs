using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace GameDatabase.Domain.SeedWork;

public class BaseValidator
{
    #region constructors

    public BaseValidator()
    {
        Erros = new List<ValidationFailure>();
    }

    #endregion

    #region properties

    public List<ValidationFailure> Erros { get; }

    #endregion

    private List<ValidationFailure> GetValidation<T>(T obj)
    {
        var list = new List<ValidationFailure>();

        if (obj == null)
            return list;

        var properties = obj.GetType().GetProperties(BindingFlags.Public |
                                                     BindingFlags.Instance).Where(o => o != null).ToList();

        properties.ForEach(p =>
        {
            if (p.PropertyType == typeof(ValidationFailure))
                list.AddRange(obj as List<ValidationFailure> ?? throw new InvalidOperationException());
            else if (p.PropertyType.IsClass && p.PropertyType.Namespace != "System")
                list.AddRange(GetValidation(p.GetValue(obj, null)));
        });

        return list;
    }

    private void SetValidation(ValidationResult failures)
    {
        Erros.AddRange(failures.Errors);
    }

    public void ValidateNow<T>(AbstractValidator<T> validator, T instance)
    {
        var falhaValidacao = validator.Validate(instance);
        SetValidation(falhaValidacao);
    }

    public bool IsValid()
    {
        var erros = GetValidation(this);
        if (erros.Count > 0)
        {
            Erros.Clear();
            Erros.AddRange(erros);
            return false;
        }

        return true;
    }
}