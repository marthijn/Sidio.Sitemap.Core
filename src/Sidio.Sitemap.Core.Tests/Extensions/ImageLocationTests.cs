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

    [Fact]
    public void Equality_WithSameValues_ShouldBeTrue()
    {
        // arrange
        var location1 = new ImageLocation("http://www.example.com/image.jpg");
        var location2 = new ImageLocation("http://www.example.com/image.jpg");

        // act & assert
        (location1 == location2).Should().BeTrue();
    }
}