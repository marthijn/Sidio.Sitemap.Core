﻿using Sitemap.Core.Serialization;
using Sitemap.Core.Services;

namespace Sitemap.Core.Tests.Services;

public sealed class SitemapServiceTest
{
    [Fact]
    public void Serialize_ReturnsSerializedSitemap()
    {
        // arrange
        var sitemap = new Sitemap();
        var sitemapProvider = new SitemapService(new XmlSerializer());

        // act
        var result = sitemapProvider.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task SerializeAsync_ReturnsSerializedSitemap()
    {
        // arrange
        var sitemap = new Sitemap();
        var sitemapProvider = new SitemapService(new XmlSerializer());

        // act
        var result = await sitemapProvider.SerializeAsync(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
    }
}