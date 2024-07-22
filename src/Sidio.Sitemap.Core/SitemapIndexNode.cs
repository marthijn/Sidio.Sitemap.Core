namespace Sidio.Sitemap.Core;

/// <summary>
/// This record represents a sitemap index node.
/// </summary>
public sealed record SitemapIndexNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexNode"/> class.
    /// </summary>
    /// <param name="url">The location of the sitemap.</param>
    /// <param name="lastModified">Identifies the time that the corresponding Sitemap file was modified.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapIndexNode(string url, DateTime? lastModified = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        Url = url;
        LastModified = lastModified;
    }

    /// <summary>
    /// Gets the Url which identifies the location of the Sitemap.
    /// This location can be a Sitemap, an Atom file, RSS file or a simple text file.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets or sets the time that the corresponding Sitemap file was modified. It does not correspond to the time that any of the pages listed in that Sitemap were changed.
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapIndexNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The location of the sitemap.</param>
    /// <param name="lastModified">Identifies the time that the corresponding Sitemap file was modified.</param>
    /// <returns>A <see cref="SitemapIndexNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapIndexNode? Create(string? url, DateTime? lastModified = null)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, lastModified);
    }
}