using FluentValidation;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class DeveloperValidator : AbstractValidator<Developer>
{
    public DeveloperValidator()
    {
        RuleFor(developer => developer.Name).NotNull().NotEmpty();
        RuleFor(developer => developer.Founded).NotNull().LessThan(DateTime.Today);
    }
}