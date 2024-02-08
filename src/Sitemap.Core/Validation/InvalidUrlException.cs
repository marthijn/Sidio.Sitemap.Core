namespace Sitemap.Core.Validation;

/// <summary>
/// The exception that is thrown when a URL is invalid.
/// </summary>
public class InvalidUrlException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="message">The exception message.</param>
    public InvalidUrlException(Uri? url, string? baseUrl, string? message)
        : base(message)
    {
        Url = url;
        BaseUrl = baseUrl;
    }

    /// <summary>
    /// Gets the URL.
    /// </summary>
    public Uri? Url { get; }

    /// <summary>
    /// Gets the base URL.
    /// </summary>
    public string? BaseUrl { get; }
}