using System.Security.Claims;

public interface ICurrentUser
{
    public int UserId { get; }
    public ClaimsPrincipal User { get; }
}