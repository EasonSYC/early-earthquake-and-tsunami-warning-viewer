using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Authentication.Abstractions;
/// <summary>
/// Represents options for OAuth2.
/// </summary>
public sealed record OAuth2Options
{
    /// <summary>
    /// The string to be displayed on the webpage after successful authentication.
    /// </summary>
    public required string WebPageString { get; init; }
    /// <summary>
    /// The path to redirect to after successful authentication.
    /// </summary>
    public required string RedirectPath { get; init; }
    /// <summary>
    /// The base URI of HTTP requrests.
    /// </summary>
    public required string BaseUri { get; init; }
    /// <summary>
    /// The Host for the requests.
    /// </summary>
    public required string Host { get; init; }
    /// <summary>
    /// The Cliend ID to be used.
    /// </summary>
    public required string ClientId { get; init; }
    /// <summary>
    /// The scopes that are specified for the authentication.
    /// </summary>
    public required IEnumerable<string> Scopes { get; init; }
}
