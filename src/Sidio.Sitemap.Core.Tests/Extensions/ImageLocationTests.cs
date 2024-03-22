using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class ImageLocationTests
{
    [Fact]
    public void Construct_WithValidArguments_ImageLocationConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";

        // act
        var sitemapNode = new ImageLocation(Url);

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
        var sitemapNodeAction = () => new ImageLocation(url!);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }
}