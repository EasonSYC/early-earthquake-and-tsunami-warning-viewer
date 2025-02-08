using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Authentication.Exceptions;
/// <summary>
/// Represents security errors that occurs during OAuth2 key change.
/// </summary>
public class OAuthSecurityException : Exception
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthSecurityException"/> class.
    /// </summary>
    public OAuthSecurityException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthSecurityException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OAuthSecurityException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthSecurityException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public OAuthSecurityException(string message, Exception inner)
        : base(message, inner) { }
}
