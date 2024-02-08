namespace Sitemap.Core.Tests;

public sealed class SitemapProviderTests
{
    [Fact]
    public void Construct_WithSitemap_ShouldContainSitemap()
    {
        // arrange
        var sitemap = new Sitemap();

        // act
        var sitemapProvider = new SitemapProvider(sitemap);

        // assert
        sitemapProvider.Should().NotBeNull();
        sitemapProvider.Sitemap.Should().Be(sitemap);
    }

    [Fact]
    public void Serialize_ReturnsSerializedSitemap()
    {
        // arrange
        var sitemap = new Sitemap();
        var sitemapProvider = new SitemapProvider(sitemap);

        // act
        var result = sitemapProvider.Serialize();

        // assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task SerializeAsync_ReturnsSerializedSitemap()
    {
        // arrange
        var sitemap = new Sitemap();
        var sitemapProvider = new SitemapProvider(sitemap);

        // act
        var result = await sitemapProvider.SerializeAsync();

        // assert
        result.Should().NotBeNullOrEmpty();
    }
}