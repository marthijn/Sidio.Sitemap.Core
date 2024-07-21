using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Services;

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
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
    }

    /// <inheritdoc />
    public string Serialize(SitemapIndex sitemapIndex)
    {
        if (sitemapIndex == null)
        {
            throw new ArgumentNullException(nameof(sitemapIndex));
        }

        return _serializer.Serialize(sitemapIndex);
    }

    /// <inheritdoc />
    public Task<string> SerializeAsync(SitemapIndex sitemapIndex, CancellationToken cancellationToken = default)
    {
        if (sitemapIndex == null)
        {
            throw new ArgumentNullException(nameof(sitemapIndex));
        }

        return _serializer.SerializeAsync(sitemapIndex, cancellationToken);
    }
}