namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents the location of an image in a <see cref="SitemapImageNode"/>.
/// </summary>
public sealed record ImageLocation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageLocation"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    public ImageLocation(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        Url = url;
    }

    /// <summary>
    /// Gets the image URL.
    /// </summary>
    public string Url { get; }
}