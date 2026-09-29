namespace Sidio.Sitemap.Core;

/// <summary>
/// Represents a node in a sitemap.
/// </summary>
public interface ISitemapNode
{
    /// <summary>
    /// Gets the URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash,
    /// if your web server requires it. This value must be less than 2,048 characters.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets the date of last modification of the page.
    /// </summary>
    /// <remarks>The <c>lastmod</c> element is not officially supported for the extension nodes.</remarks>
    public DateTime? LastModified { get; }
}