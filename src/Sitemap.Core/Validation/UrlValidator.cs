namespace Sitemap.Core.Validation;

internal sealed class UrlValidator
{
    private readonly IBaseUrlProvider? _baseUrlProvider;

    private readonly Uri? _baseUri;

    public UrlValidator(IBaseUrlProvider? baseUrlProvider = null)
    {
        _baseUrlProvider = baseUrlProvider;

        if (_baseUrlProvider is not null)
        {
            if (!Uri.TryCreate(_baseUrlProvider.BaseUrl, UriKind.Absolute, out var baseUri))
            {
                throw new InvalidUrlException(null, _baseUrlProvider.BaseUrl, "The base URL is not a valid absolute URL.");
            }

            if (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidUrlException(null, _baseUrlProvider.BaseUrl, "The base URL scheme must be HTTP or HTTPS.");
            }

            _baseUri = baseUri;
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
            throw new InvalidUrlException(uri, _baseUrlProvider?.BaseUrl, "The URL is not a valid absolute or relative URL.");
        }

        if (_baseUrlProvider is null || _baseUri is null)
        {
            throw new InvalidUrlException(uri, _baseUrlProvider?.BaseUrl, "The base URL provider cannot be null because the given URL is relative.");
        }

        return new Uri(_baseUri, uri);
    }
}