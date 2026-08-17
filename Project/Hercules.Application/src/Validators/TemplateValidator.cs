using FluentValidation;

public class TemplateRequestValidator : AbstractValidator<TemplateRequest>
{
    public TemplateRequestValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty();

        RuleFor(t => t.Name.Length)
            .InclusiveBetween(TemplateEntity.MinNameLength, TemplateEntity.MaxNameLength);

        RuleFor(t => t.ExerciseIds)
            .NotEmpty();
    }
}