namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapIndexNodeTests
{
    [Fact]
    public void Create_WhenUrlIsValid_SitemapIndexNodeCreated()
    {
        // arrange
        const string Url = "sitemap.xml";
        var dateTime = DateTime.UtcNow;

        // act
        var node = SitemapIndexNode.Create(Url, dateTime);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.LastModified.Should().Be(dateTime);
    }

    [Fact]
    public void Create_WhenUrlIsNull_SitemapIndexNodeNotCreated()
    {
        // arrange & act
        var node = SitemapIndexNode.Create(null);

        // assert
        node.Should().BeNull();
    }
}