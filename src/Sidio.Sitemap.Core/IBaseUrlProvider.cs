namespace Sidio.Sitemap.Core;

/// <summary>
/// The base URL provider.
/// </summary>
public interface IBaseUrlProvider
{
    /// <summary>
    /// Gets the base URL.
    /// </summary>
    Uri BaseUrl { get; }
}