using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class SitemapImageNodeTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_SitemapImageLocationConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";
        var imageLocation = new SitemapImageLocation(Url);

        // act
        var sitemapNode = new SitemapImageNode(Url, imageLocation);

        // assert
        sitemapNode.Url.Should().Be(Url);
        sitemapNode.Images.Should().HaveCount(1);
        sitemapNode.Images.Should().Contain(imageLocation);
    }

    [Fact]
    public void Construct_WithValidArguments_MultipleImages_SitemapImageLocationConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";
        var imageLocations = _fixture.CreateMany<SitemapImageLocation>().ToList();

        // act
        var sitemapNode = new SitemapImageNode(Url, imageLocations);

        // assert
        sitemapNode.Url.Should().Be(Url);
        sitemapNode.Images.Should().HaveCount(imageLocations.Count);
        sitemapNode.Images.Should().Contain(imageLocations);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? url)
    {
        // act
        var sitemapNodeAction = () => new SitemapImageNode(url!, new SitemapImageLocation("http://www.example.com"));

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Construct_WithoutImages_ThrowException()
    {
        // act
        var sitemapNodeAction = () => new SitemapImageNode("http://www.example.com", []);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Construct_WithoutTooManyImages_ThrowException()
    {
        // act
        var sitemapNodeAction = () => new SitemapImageNode("http://www.example.com", new List<SitemapImageLocation>(1001).ToArray());

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }
}