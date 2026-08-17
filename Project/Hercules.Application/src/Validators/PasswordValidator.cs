using FluentValidation;

public class PasswordRequestValidator : AbstractValidator<PasswordRequest>
{
    public PasswordRequestValidator()
    {
        RuleFor(p => p.Password)
            .NotEmpty()
            .Must(p => p.Length.IsBetween(UserEntity.MinPasswordLength, UserEntity.MaxPasswordLength))
                .WithMessage($"Password length must be between {UserEntity.MinPasswordLength} and {UserEntity.MaxPasswordLength}");
    }
}