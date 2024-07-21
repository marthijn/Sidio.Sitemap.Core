using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Services;

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
        _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
    }

    /// <inheritdoc />
    public string Serialize(Sitemap sitemap)
    {
        if (sitemap == null)
        {
            throw new ArgumentNullException(nameof(sitemap));
        }

        return _serializer.Serialize(sitemap);
    }

    /// <inheritdoc />
    public Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default)
    {
        if (sitemap == null)
        {
            throw new ArgumentNullException(nameof(sitemap));
        }

        return _serializer.SerializeAsync(sitemap, cancellationToken);
    }
}