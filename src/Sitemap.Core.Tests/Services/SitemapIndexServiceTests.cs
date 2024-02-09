using Sitemap.Core.Serialization;
using Sitemap.Core.Services;

namespace Sitemap.Core.Tests.Services;

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
}