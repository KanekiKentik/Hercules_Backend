using System.Data;
using FluentValidation;

public class WorkoutSearchFilterValidator : AbstractValidator<WorkoutSearchFilter>
{
    public WorkoutSearchFilterValidator()
    {
        RuleFor(w => w.DateFrom)
            .LessThan(DateTimeOffset.UtcNow)
            .GreaterThan(DateTimeOffset.Parse(ValidationConstants.MinimalTime));

        RuleFor(w => w.DateTo)
            .GreaterThan(DateTimeOffset.Parse(ValidationConstants.MinimalTime));

        RuleFor(w => w.DateTo)
            .GreaterThan(w => w.DateFrom);
    }
}