public record JwtOptions
{
    public required string SecretKey { get; set; }
    public required int LifespanHours { get; set; }
}