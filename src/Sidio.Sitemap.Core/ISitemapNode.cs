namespace Sidio.Sitemap.Core;

/// <summary>
/// Represents a node in a sitemap.
/// </summary>
public interface ISitemapNode
{
    /// <summary>
    /// Gets the URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.
    /// </summary>
    public string Url { get; }
}