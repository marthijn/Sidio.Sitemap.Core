namespace Sitemap.Core;

/// <summary>
/// The base URL provider.
/// </summary>
public interface IBaseUrlProvider
{
    /// <summary>
    /// Gets the base URL.
    /// </summary>
    string BaseUrl { get; }
}