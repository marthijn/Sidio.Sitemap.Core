using Sitemap.Core.Serialization;

namespace Sitemap.Core.Services;

/// <summary>
/// The sitemap index service.
/// </summary>
public sealed class SitemapIndexService : ISitemapIndexService
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexService"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public SitemapIndexService(ISitemapSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <inheritdoc />
    public string Serialize(SitemapIndex sitemapIndex)
    {
        return _serializer.Serialize(sitemapIndex);
    }
}