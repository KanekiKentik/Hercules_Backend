public static class UserMapper
{
    public static UserResponse ToResponse(this UserEntity user)
    {
        return new (user.Id, user.Username, user.Privilege.ToString(), user.RegistrationDate);
    }
}