namespace Sidio.Sitemap.Core.Services;

/// <summary>
/// The sitemap service interface.
/// </summary>
public interface ISitemapService
{
    /// <summary>
    /// Serializes the specified sitemap.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <returns>A <see cref="string"/> representing the sitemap.</returns>
    string Serialize(Sitemap sitemap);

    /// <summary>
    /// Serializes the specified sitemap asynchronously.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default);
}