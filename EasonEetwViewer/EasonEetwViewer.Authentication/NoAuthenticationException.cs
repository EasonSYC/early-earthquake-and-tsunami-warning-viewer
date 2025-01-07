namespace EasonEetwViewer.Authentication;
public class NoAuthenticationException : Exception
{
    public NoAuthenticationException() { }

    public NoAuthenticationException(string message)
        : base(message) { }

    public NoAuthenticationException(string message, Exception inner)
        : base(message, inner) { }
}
