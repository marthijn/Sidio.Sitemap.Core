namespace Sitemap.Core.Serialization;

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
    /// Serializes the specified sitemap.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="string"/> representing the serialized sitemap.</returns>
    Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default);

    /// <summary>
    /// Serializes the specified sitemap index.
    /// </summary>
    /// <param name="sitemapIndex"></param>
    /// <returns></returns>
    string Serialize(SitemapIndex sitemapIndex);
}