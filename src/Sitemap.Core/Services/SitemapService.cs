using Sitemap.Core.Serialization;

namespace Sitemap.Core.Services;

/// <summary>
/// The sitemap service.
/// </summary>
public sealed class SitemapService : ISitemapService
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapService"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public SitemapService(ISitemapSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(serializer);
        _serializer = serializer;
    }

    /// <inheritdoc />
    public string Serialize(Sitemap sitemap)
    {
        ArgumentNullException.ThrowIfNull(sitemap);
        return _serializer.Serialize(sitemap);
    }

    /// <inheritdoc />
    public Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sitemap);
        return _serializer.SerializeAsync(sitemap, cancellationToken);
    }
}