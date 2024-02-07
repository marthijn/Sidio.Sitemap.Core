using Sitemap.Core.Serialization;

namespace Sitemap.Core;

/// <summary>
/// The sitemap index provider.
/// </summary>
public sealed class SitemapIndexProvider
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexProvider"/> class.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    public SitemapIndexProvider(SitemapIndex sitemapIndex)
        : this(sitemapIndex, new XmlSerializer())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexProvider"/> class.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <param name="serializer">The serializer.</param>
    public SitemapIndexProvider(SitemapIndex sitemapIndex, ISitemapSerializer serializer)
    {
        SitemapIndex = sitemapIndex;
        _serializer = serializer;
    }

    /// <summary>
    /// Gets the sitemap index.
    /// </summary>
    public SitemapIndex SitemapIndex { get; }

    /// <summary>
    /// Serializes the sitemap index to a string.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the sitemap index.</returns>
    public string Serialize()
    {
        return _serializer.Serialize(SitemapIndex);
    }
}