namespace Sitemap.Core;

/// <summary>
/// The sitemap async serializable interface.
/// </summary>
public interface ISitemapSerializableAsync : ISitemapSerializable
{
    /// <summary>
    /// Serializes the sitemap (or sitemap index) to a string asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    Task<string> SerializeAsync(CancellationToken cancellationToken = default);
}