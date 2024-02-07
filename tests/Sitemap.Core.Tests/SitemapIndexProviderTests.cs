namespace Sitemap.Core.Tests;

public sealed class SitemapIndexProviderTests
{
    [Fact]
    public void Construct_WithSitemapIndex_ShouldContainSitemapIndex()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();

        // act
        var sitemapProvider = new SitemapIndexProvider(sitemapIndex);

        // assert
        sitemapProvider.Should().NotBeNull();
        sitemapProvider.SitemapIndex.Should().Be(sitemapIndex);
    }

    [Fact]
    public void Serialize_ReturnsSerializedSitemapIndex()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var sitemapProvider = new SitemapIndexProvider(sitemapIndex);

        // act
        var result = sitemapProvider.Serialize();

        // assert
        result.Should().NotBeNullOrEmpty();
    }
}