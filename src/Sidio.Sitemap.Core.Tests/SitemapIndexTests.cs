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
        sitemapIndex.Stylesheet.Should().BeNull();
    }

    [Fact]
    public void Construct_WithStylesheet_ShouldHaveStylesheet()
    {
        // arrange
        var styleSheet = _fixture.Create<string>();

        // act
        var sitemapIndex = new SitemapIndex(styleSheet);

        // assert
        sitemapIndex.Nodes.Should().BeEmpty();
        sitemapIndex.Stylesheet.Should().Be(styleSheet);
    }

    [Fact]
    public void Add_Array_WithNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = _fixture.CreateMany<SitemapIndexNode>().ToList();

        // act
        var result = sitemapIndex.Add(nodes.ToArray());

        // assert
        sitemapIndex.Nodes.Should().BeEquivalentTo(nodes);
        result.Should().Be(nodes.Count);
    }

    [Fact]
    public void Add_Array_WithNullableNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = new List<SitemapIndexNode?> {null, null};

        // act
        var result = sitemapIndex.Add(nodes.ToArray());

        // assert
        sitemapIndex.Nodes.Should().BeEmpty();
        result.Should().Be(0);
    }

    [Fact]
    public void Add_Enumerable_WithNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = _fixture.CreateMany<SitemapIndexNode>().ToList();

        // act
        var result = sitemapIndex.Add(nodes);

        // assert
        sitemapIndex.Nodes.Should().BeEquivalentTo(nodes);
        result.Should().Be(nodes.Count);
    }

    [Fact]
    public void Add_Enumerable_WithNullableNodes_ShouldContainNodes()
    {
        // arrange
        var sitemapIndex = new SitemapIndex();
        var nodes = new List<SitemapIndexNode?> {null, null};

        // act
        var result = sitemapIndex.Add(nodes);

        // assert
        sitemapIndex.Nodes.Should().BeEmpty();
        result.Should().Be(0);
    }
}