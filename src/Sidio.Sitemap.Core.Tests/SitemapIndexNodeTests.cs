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

    [Fact]
    public void SitemapIndexNode_Equality()
    {
        // arrange
        var url = "https://example.com/sitemap.xml";
        var lastModified = DateTime.UtcNow;

        var node1 = new SitemapIndexNode(url, lastModified);
        var node2 = new SitemapIndexNode(url, lastModified);

        // act & assert
        (node1 == node2).Should().BeTrue();
    }
}