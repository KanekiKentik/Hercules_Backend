public class InvalidTokenException : Exception
{
    public InvalidTokenException() : base() {}
    public InvalidTokenException(string message) : base(message) {}
}