namespace Sitemap.Core.Validation;

internal sealed class UrlValidator
{
    /// <summary>
    /// The URL value must be less than 2,048 characters.
    /// </summary>
    internal const int UrlMaxLength = 2047;

    private readonly Uri? _baseUri;

    public UrlValidator(IBaseUrlProvider? baseUrlProvider = null)
    {
        if (baseUrlProvider is not null)
        {
            if (!baseUrlProvider.BaseUrl.IsAbsoluteUri)
            {
                throw new InvalidUrlException(null, baseUrlProvider.BaseUrl, "The base URL is not a valid absolute URL.");
            }

            if (baseUrlProvider.BaseUrl.Scheme != Uri.UriSchemeHttp && baseUrlProvider.BaseUrl.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidUrlException(null, baseUrlProvider.BaseUrl, "The base URL scheme must be HTTP or HTTPS.");
            }

            _baseUri = baseUrlProvider.BaseUrl;
        }
    }

    /// <summary>
    /// Validates a URL, and returns an absolute URI.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when an argument is invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the URL is relative but the base URL is empty.</exception>
    public Uri Validate(string? url)
    {
        if (url == null)
        {
            throw new ArgumentException("The URL cannot be null.", nameof(url));
        }

        var tmpUrl = EnsureRelativeUrl(url);
        if (Uri.TryCreate(tmpUrl, UriKind.Absolute, out var absoluteUri))
        {
            if (absoluteUri.ToString().Length > UrlMaxLength)
            {
                throw new InvalidUrlException(absoluteUri, _baseUri, $"{nameof(url)} exceeds the maximum length of {UrlMaxLength} characters.");
            }

            return absoluteUri;
        }

        if (!Uri.TryCreate(tmpUrl, UriKind.Relative, out var uri))
        {
            throw new InvalidUrlException(uri, _baseUri, "The URL is not a valid absolute or relative URL.");
        }

        if ( _baseUri is null)
        {
            throw new InvalidUrlException(uri, _baseUri, "The base URL cannot be null because the given URL is relative.");
        }

        var result = new Uri(_baseUri, new Uri(uri.ToString(), UriKind.Relative));

        if (result.ToString().Length > UrlMaxLength)
        {
            throw new InvalidUrlException(absoluteUri, _baseUri, $"{nameof(url)} exceeds the maximum length of {UrlMaxLength} characters.");
        }

        return result;
    }

    private static string? EnsureRelativeUrl(string? url)
    {
        // fix for this issue on Ubuntu: https://github.com/dotnet/runtime/issues/22718
        if (string.IsNullOrWhiteSpace(url))
        {
            return url;
        }

        if (url.StartsWith("/", StringComparison.Ordinal))
        {
            return url.Length > 1 ? url[1..] : string.Empty;
        }

        return url;
    }
}