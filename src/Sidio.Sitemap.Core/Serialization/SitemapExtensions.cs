using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Serialization;

internal static class SitemapExtensions
{
    public static bool HasImageNodes(this Sitemap sitemap) => sitemap.Nodes.Any(node => node is SitemapImageNode);

    public static bool HasNewsNodes(this Sitemap sitemap) => sitemap.Nodes.Any(node => node is SitemapNewsNode);

    public static bool HasVideoNodes(this Sitemap sitemap) => sitemap.Nodes.Any(node => node is SitemapVideoNode);
}