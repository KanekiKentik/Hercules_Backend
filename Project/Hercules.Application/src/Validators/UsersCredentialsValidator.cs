using FluentValidation;
public class UserCredentialsValidator : AbstractValidator<UserCredentialsDTO>
{
    public UserCredentialsValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .Must(u => u.Length.IsBetween(UserEntity.MinUsernameLength, UserEntity.MaxUsernameLength))
                .WithMessage($"Username length must be between {UserEntity.MinUsernameLength} and {UserEntity.MaxUsernameLength}")
            .Matches(UserEntity.UsernameRegex);

        RuleFor(u => u.Password)
            .NotEmpty()
            .Must(p => p.Length.IsBetween(UserEntity.MinPasswordLength, UserEntity.MaxPasswordLength))
                .WithMessage($"Password length must be between {UserEntity.MinPasswordLength} and {UserEntity.MaxPasswordLength}");
    }
}