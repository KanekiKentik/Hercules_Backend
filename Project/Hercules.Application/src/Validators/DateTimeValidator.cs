using FluentValidation;

public class DateTimeRequestValidator : AbstractValidator<DateTimeRequest>
{
    public DateTimeRequestValidator()
    {
        RuleFor(w => w.DateTime)
            .LessThan(DateTimeOffset.UtcNow).WithMessage("DateTime cannot be from future")
            .GreaterThan(DateTimeOffset.UtcNow.AddMinutes(-3)).WithMessage("DateTime cannot be from that long ago");
    }
}