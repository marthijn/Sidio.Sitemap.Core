namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Construct_WithNodes_ShouldContainNodes()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = Enumerable.Range(0, 100).Select(_ => new SitemapNode(Url)).ToList();

        // act
        var sitemap = new Sitemap(nodes);

        // assert
        sitemap.Nodes.Should().BeEquivalentTo(nodes);
        sitemap.Stylesheet.Should().BeNull();
    }

    [Fact]
    public void Construct_WithTooManyNodes_ThrowException()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = Enumerable.Range(0, Sitemap.MaxNodes + 1).Select(_ => new SitemapNode(Url));

        // act
        var sitemapNodeAction = () => new Sitemap(nodes);

        // assert
        sitemapNodeAction.Should().ThrowExactly<InvalidOperationException>().WithMessage($"*{Sitemap.MaxNodes}*");
    }

    [Fact]
    public void Construct_WithStylesheet_ShouldHaveStylesheet()
    {
        // arrange
        var styleSheet = _fixture.Create<string>();

        // act
        var sitemap = new Sitemap(styleSheet);

        // assert
        sitemap.Nodes.Should().BeEmpty();
        sitemap.Stylesheet.Should().Be(styleSheet);
    }

    [Fact]
    public void Construct_WithNodesAndStylesheet_ShouldHaveStylesheet()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = Enumerable.Range(0, 100).Select(_ => new SitemapNode(Url)).ToList();
        var styleSheet = _fixture.Create<string>();

        // act
        var sitemap = new Sitemap(nodes, styleSheet);

        // assert
        sitemap.Nodes.Should().BeEquivalentTo(nodes);
        sitemap.Stylesheet.Should().Be(styleSheet);
    }

    [Fact]
    public void Construct_WhenNodesIsNull_ThrowException()
    {
        // arrange
        List<SitemapNode>? nodes = null;

        // act
        var action = () => new Sitemap(nodes!);

        // assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Add_NodesIsNull_ThrowException()
    {
        // arrange
        var sitemap = new Sitemap();
        List<SitemapNode>? nodes = null;

        // act
        var sitemapNodeAction = () => sitemap.Add(nodes!);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Add_NodesArrayIsNull_ThrowException()
    {
        // arrange
        var sitemap = new Sitemap();
        ISitemapNode[] nodes = null!;

        // act
        var sitemapNodeAction = () => sitemap.Add(nodes);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void AddNodes_Enumerable_WithTooManyNodes_ThrowException()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = Enumerable.Range(0, Sitemap.MaxNodes + 1).Select(_ => new SitemapNode(Url));
        var sitemap = new Sitemap();

        // act
        var sitemapNodeAction = () => sitemap.Add(nodes);

        // assert
        sitemapNodeAction.Should().ThrowExactly<InvalidOperationException>().WithMessage($"*{Sitemap.MaxNodes}*");
    }

    [Fact]
    public void AddNodes_Array_WithTooManyNodes_ThrowException()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = Enumerable.Range(0, Sitemap.MaxNodes + 1).Select(_ => new SitemapNode(Url));
        var sitemap = new Sitemap();

        // act
        var sitemapNodeAction = () => sitemap.Add(nodes.Cast<ISitemapNode>().ToArray());

        // assert
        sitemapNodeAction.Should().ThrowExactly<InvalidOperationException>().WithMessage($"*{Sitemap.MaxNodes}*");
    }

    [Fact]
    public void AddNodes_Enumerable_WithNodes_ShouldContainNodes()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = new List<SitemapNode>
        {
            new(Url),
            new(Url)
        };

        var sitemap = new Sitemap();

        // act
        var result = sitemap.Add(nodes);

        // assert
        result.Should().Be(nodes.Count);
        sitemap.Nodes.Should().BeEquivalentTo(nodes);
    }

    [Fact]
    public void AddNodes_Enumerable_WithNullableNodes_ShouldContainNodes()
    {
        // arrange
        var nodes = new List<SitemapNode?>
        {
            null, null
        };

        var sitemap = new Sitemap();

        // act
        var result = sitemap.Add(nodes);

        // assert
        result.Should().Be(0);
        sitemap.Nodes.Should().BeEmpty();
    }

    [Fact]
    public void AddNodes_Array_WithNodes_ShouldContainNodes()
    {
        // arrange
        const string Url = "https://example.com";
        var nodes = new List<SitemapNode>
        {
            new(Url),
            new(Url)
        }.Cast<ISitemapNode>().ToArray();

        var sitemap = new Sitemap();

        // act
        var result = sitemap.Add(nodes);

        // assert
        result.Should().Be(nodes.Length);
        sitemap.Nodes.Should().BeEquivalentTo(nodes);
    }

    [Fact]
    public void AddNodes_Array_WithNullableNodes_ShouldContainNodes()
    {
        // arrange
        var nodes = new List<SitemapNode?>
        {
            null, null
        }.Cast<ISitemapNode>().ToArray();

        var sitemap = new Sitemap();

        // act
        var result = sitemap.Add(nodes);

        // assert
        result.Should().Be(0);
        sitemap.Nodes.Should().BeEmpty();
    }
}