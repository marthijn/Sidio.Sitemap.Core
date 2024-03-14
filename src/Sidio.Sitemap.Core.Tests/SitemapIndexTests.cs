namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapIndexTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithNodes_ShouldContainNodes()
    {
        // arrange
        var nodes = _fixture.CreateMany<SitemapIndexNode>().ToList();

        // act
        var sitemapIndex = new SitemapIndex(nodes);

        // assert
        sitemapIndex.Nodes.Should().BeEquivalentTo(nodes);
    }

    [Fact]
    public void Add_Array_WithNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = _fixture.CreateMany<SitemapIndexNode>().ToList();

        // act
        sitemapIndex.Add(nodes.ToArray());

        // assert
        sitemapIndex.Nodes.Should().BeEquivalentTo(nodes);
    }

    [Fact]
    public void Add_Enumerable_WithNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = _fixture.CreateMany<SitemapIndexNode>().ToList();

        // act
        sitemapIndex.Add(nodes);

        // assert
        sitemapIndex.Nodes.Should().BeEquivalentTo(nodes);
    }
}