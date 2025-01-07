using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Authentication;
internal class OAuthInvalidException : Exception
{
    public OAuthInvalidException() { }

    public OAuthInvalidException(string message)
        : base(message) { }

    public OAuthInvalidException(string message, Exception inner)
        : base(message, inner) { }
}
