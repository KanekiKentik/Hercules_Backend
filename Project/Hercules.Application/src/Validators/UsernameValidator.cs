using FluentValidation;

public class UsernameRequestValidator : AbstractValidator<UsernameRequest>
{
    public UsernameRequestValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .Must(u => u.Length.IsBetween(UserEntity.MinUsernameLength, UserEntity.MaxUsernameLength))
                .WithMessage($"Username length must be between {UserEntity.MinUsernameLength} and {UserEntity.MaxUsernameLength}")
            .Matches(UserEntity.UsernameRegex);
    }
}