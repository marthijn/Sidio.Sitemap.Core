namespace Sitemap.Core;

/// <summary>
/// The sitemap serializable interface.
/// </summary>
public interface ISitemapSerializable
{
    /// <summary>
    /// Serializes the sitemap (or sitemap index) to a string.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the sitemap.</returns>
    string Serialize();
}