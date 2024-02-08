using Sitemap.Core.Serialization;

namespace Sitemap.Core;

/// <summary>
/// The sitemap index provider.
/// </summary>
public class SitemapIndexProvider : ISitemapSerializable
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexProvider"/> class.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <param name="baseUrlProvider">The base URL provider. When no provider is given, al the URLs in the sitemap index must be absolute.</param>
    public SitemapIndexProvider(SitemapIndex sitemapIndex, IBaseUrlProvider? baseUrlProvider = null)
        : this(sitemapIndex, new XmlSerializer(baseUrlProvider))
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

    /// <inheritdoc />
    public string Serialize()
    {
        return _serializer.Serialize(SitemapIndex);
    }
}