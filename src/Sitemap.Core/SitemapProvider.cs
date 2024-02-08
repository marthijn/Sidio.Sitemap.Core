using Sitemap.Core.Serialization;

namespace Sitemap.Core;

/// <summary>
/// The sitemap provider.
/// </summary>
public class SitemapProvider : ISitemapSerializableAsync
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
    /// The default serializer is <see cref="XmlSerializer"/>.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="baseUrlProvider">The base URL provider. When no provider is given, al the URLs in the sitemap must be absolute.</param>
    public SitemapProvider(Sitemap sitemap, IBaseUrlProvider? baseUrlProvider = null)
        : this(sitemap, new XmlSerializer(baseUrlProvider))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="serializer">The serializer.</param>
    public SitemapProvider(Sitemap sitemap, ISitemapSerializer serializer)
    {
        Sitemap = sitemap;
        _serializer = serializer;
    }

    /// <summary>
    /// Gets the sitemap.
    /// </summary>
    public Sitemap Sitemap { get; }

    /// <inheritdoc />
    public string Serialize()
    {
        return _serializer.Serialize(Sitemap);
    }

    /// <inheritdoc />
    public Task<string> SerializeAsync(CancellationToken cancellationToken = default)
    {
        return _serializer.SerializeAsync(Sitemap, cancellationToken);
    }
}