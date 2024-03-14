using Sidio.Sitemap.Core.Serialization;
using Sidio.Sitemap.Core.Services;

namespace Sidio.Sitemap.Core.Tests.Services;

public sealed class SitemapIndexServiceTests
{
    [Fact]
    public void Serialize_ReturnsSerializedSitemapIndex()
    {
        // arrange
        var sitemap = new SitemapIndex();
        var sitemapProvider = new SitemapIndexService(new XmlSerializer());

        // act
        var result = sitemapProvider.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task SerializeAsync_ReturnsSerializedSitemapIndex()
    {
        // arrange
        var sitemap = new SitemapIndex();
        var sitemapProvider = new SitemapIndexService(new XmlSerializer());

        // act
        var result = await sitemapProvider.SerializeAsync(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
    }
}