namespace Sitemap.Core.Validation;

internal sealed class UrlValidator
{
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
    /// Validates a URL.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when an argument is invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the URL is relative but the base URL is empty.</exception>
    public Uri Validate(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("The URL cannot be null or empty.", nameof(url));
        }

        if (Uri.TryCreate(url, UriKind.Absolute, out var absoluteUri))
        {
            return absoluteUri;
        }

        if (!Uri.TryCreate(url, UriKind.Relative, out var uri))
        {
            throw new InvalidUrlException(uri, _baseUri, "The URL is not a valid absolute or relative URL.");
        }

        if ( _baseUri is null)
        {
            throw new InvalidUrlException(uri, _baseUri, "The base URL cannot be null because the given URL is relative.");
        }

        return new Uri(_baseUri, new Uri(uri.ToString(), UriKind.Relative));
    }
}