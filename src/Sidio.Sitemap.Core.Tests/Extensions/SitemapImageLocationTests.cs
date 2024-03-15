using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class SitemapImageLocationTests
{
    [Fact]
    public void Construct_WithValidArguments_SitemapImageLocationConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";

        // act
        var sitemapNode = new SitemapImageLocation(Url);

        // assert
        sitemapNode.Url.Should().Be(Url);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? url)
    {
        // act
        var sitemapNodeAction = () => new SitemapImageLocation(url!);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentNullException>();
    }
}