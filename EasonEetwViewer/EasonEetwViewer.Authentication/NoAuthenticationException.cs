using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Authentication;
public class NoAuthenticationException : Exception
{
    public NoAuthenticationException() { }

    public NoAuthenticationException(string message)
        : base(message) { }

    public NoAuthenticationException(string message, Exception inner)
        : base(message, inner) { }
}
