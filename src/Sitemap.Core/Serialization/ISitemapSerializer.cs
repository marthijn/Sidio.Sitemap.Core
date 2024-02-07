﻿namespace Sitemap.Core.Serialization;

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
    /// Serializes the specified sitemap to a stream.
    /// </summary>
    /// <param name="sitemap">The sitemap.</param>
    /// <param name="output">The output stream.</param>
    void Serialize(Sitemap sitemap, Stream output);

    /// <summary>
    /// Serializes the specified sitemap index.
    /// </summary>
    /// <param name="sitemapIndex"></param>
    /// <returns></returns>
    string Serialize(SitemapIndex sitemapIndex);
}