
using FluentValidation;

namespace GameDatabase.Domain.AggregatesModel.GameAggregate;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
    }
}

