public record UserResponse
{
    public int UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Privilege { get; init; }
    public DateTime RegistrationDate { get; init; } = default;

    public UserResponse(int userId, string username, string privilege, DateTime registrationDate)
        => (UserId, Username, Privilege, RegistrationDate) = (userId, username, privilege, registrationDate);
}