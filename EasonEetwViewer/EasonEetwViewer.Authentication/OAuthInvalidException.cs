namespace EasonEetwViewer.Authentication;
public class OAuthInvalidException : Exception
{
    public OAuthInvalidException() { }

    public OAuthInvalidException(string message)
        : base(message) { }

    public OAuthInvalidException(string message, Exception inner)
        : base(message, inner) { }
}
