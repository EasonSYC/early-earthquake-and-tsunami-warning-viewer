using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Events;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// The base view model for all pages.
/// </summary>
/// <param name="authenticator">The authenticator to be used.</param>
/// <param name="apiCaller">The API caller to be used.</param>
/// <param name="telegramRetriever">The telegram retriever to be used.</param>
/// <param name="timeProvider">The time provider to be used.</param>
/// <param name="logger">The logger to be used.</param>
internal abstract partial class PageViewModelBase(
    IAuthenticationHelper authenticator,
    IApiCaller apiCaller,
    ITelegramRetriever telegramRetriever,
    ITimeProvider timeProvider,
    ILogger<PageViewModelBase> logger) : ViewModelBase(logger)
{
    /// <summary>
    /// The authenticator to be used.
    /// </summary>
    protected readonly IAuthenticationHelper _authenticator = authenticator;
    /// <summary>
    /// The API caller to be used.
    /// </summary>
    protected readonly IApiCaller _apiCaller = apiCaller;
    /// <summary>
    /// The telegram retriever to be used.
    /// </summary>
    protected readonly ITelegramRetriever _telegramRetriever = telegramRetriever;
    /// <summary>
    /// The time provider to be used.
    /// </summary>
    protected readonly ITimeProvider _timeProvider = timeProvider;
}
