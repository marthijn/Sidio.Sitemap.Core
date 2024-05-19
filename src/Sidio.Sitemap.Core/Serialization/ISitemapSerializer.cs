namespace Sidio.Sitemap.Core.Serialization;

/// <summary>
/// The site map serializer interface.
/// </summary>
public interface ISitemapSerializer
{
    /// <summary>
    /// Serializes the specified sitemap.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <returns>A <see cref="string"/> representing the serialized sitemap.</returns>
    string Serialize(Sitemap sitemap);

    /// <summary>
    /// Serializes the specified sitemap asynchronous.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the serialized sitemap.</returns>
    Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default);

    /// <summary>
    /// Serializes the specified sitemap index.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <returns>A <see cref="string"/> representing the serialized sitemap index.</returns>
    string Serialize(SitemapIndex sitemapIndex);

    /// <summary>
    /// Serializes the specified sitemap index asynchronous.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the serialized sitemap index.</returns>
    Task<string> SerializeAsync(SitemapIndex sitemapIndex, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deserializes the specified XML to a <see cref="Sitemap"/>.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns>A <see cref="Sitemap"/>.</returns>
    Sitemap Deserialize(string xml);

    /// <summary>
    /// Deserializes the specified XML to a <see cref="Sitemap"/>.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Sitemap"/>.</returns>
    Task<Sitemap> DeserializeAsync(string xml, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deserializes the specified XML to a <see cref="SitemapIndex"/>.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns>A <see cref="SitemapIndex"/>.</returns>
    SitemapIndex DeserializeIndex(string xml);

    /// <summary>
    /// Deserializes the specified XML to a <see cref="SitemapIndex"/>.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="SitemapIndex"/>.</returns>
    Task<SitemapIndex> DeserializeIndexAsync(string xml, CancellationToken cancellationToken = default);
}