namespace Sitemap.Core.Services;

/// <summary>
/// The sitemap index service interface.
/// </summary>
public interface ISitemapIndexService
{
    /// <summary>
    /// Serializes the specified sitemap index.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <returns>A <see cref="string"/> representing the sitemap index.</returns>
    string Serialize(SitemapIndex sitemapIndex);

    /// <summary>
    /// Serializes the specified sitemap asynchronously.
    /// </summary>
    /// <param name="sitemapIndex">The sitemap index.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task<string> SerializeAsync(SitemapIndex sitemapIndex, CancellationToken cancellationToken = default);
}