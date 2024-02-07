using Sitemap.Core.Serialization;

namespace Sitemap.Core;

/// <summary>
/// The sitemap provider.
/// </summary>
public sealed class SitemapProvider
{
    private readonly ISitemapSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapProvider"/> class.
    /// The default serializer is <see cref="XmlSerializer"/>.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    public SitemapProvider(Sitemap sitemap)
        : this(sitemap, new XmlSerializer())
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

    /// <summary>
    /// Serializes the sitemap to a string.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the sitemap.</returns>
    public string Serialize()
    {
        return _serializer.Serialize(Sitemap);
    }

    /// <summary>
    /// Serializes the sitemap to a string asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="string"/> representing the sitemap.</returns>
    public Task<string> SerializeAsync(CancellationToken cancellationToken = default)
    {
        return _serializer.SerializeAsync(Sitemap, cancellationToken);
    }
}