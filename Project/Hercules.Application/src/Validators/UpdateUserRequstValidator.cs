using FluentValidation;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(u => u.Password.Length)
            .InclusiveBetween(UserEntity.MinPasswordLength, UserEntity.MaxPasswordLength);

        RuleFor(u => u.Username)
            .Matches(UserEntity.UsernameRegex);
        
        RuleFor(u => u.Username.Length)
            .InclusiveBetween(UserEntity.MinUsernameLength, UserEntity.MaxUsernameLength);
    }
}