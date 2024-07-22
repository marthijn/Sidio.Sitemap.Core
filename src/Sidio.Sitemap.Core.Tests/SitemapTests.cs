namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapTests
{
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