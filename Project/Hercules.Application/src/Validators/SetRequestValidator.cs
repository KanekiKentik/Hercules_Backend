using FluentValidation;

public class SetRequestValidator : AbstractValidator<SetRequest>
{
    public SetRequestValidator()
    {
        RuleFor(s => s.Weight)
            .InclusiveBetween(SetEntity.MinWeight, SetEntity.MaxWeight);

        RuleFor(s => s.Reps)
            .InclusiveBetween(SetEntity.MinReps, SetEntity.MaxReps);
    }
}