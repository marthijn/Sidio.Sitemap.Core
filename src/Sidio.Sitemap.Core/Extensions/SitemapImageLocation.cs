namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents the location of an image in a sitemap.
/// </summary>
public sealed record SitemapImageLocation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageLocation"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    public SitemapImageLocation(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        Url = url;
    }

    /// <summary>
    /// Gets the image URL.
    /// </summary>
    public string Url { get; }
}