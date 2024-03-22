using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class SitemapVideoNodeTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_SitemapImageNodeConstructed()
    {
        // arrange
        const string Url = "https://www.example.com";
        var videos = Enumerable.Range(0, 5)
            .Select(_ => new VideoContent(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>()))
            .ToList();

        // act
        var sitemapNode = new SitemapVideoNode(Url, videos);

        // assert
        sitemapNode.Url.Should().Be(Url);
        sitemapNode.Videos.Should().HaveCount(videos.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? url)
    {
        // act
        var sitemapNodeAction = () => new SitemapVideoNode(
            url!,
            new VideoContent(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>()));

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>().WithMessage("*url*");
    }

    [Fact]
    public void Construct_WithoutVideos_ThrowException()
    {
        // arrange
        var url = _fixture.Create<string>();

        // act
        var sitemapNodeAction = () => new SitemapVideoNode(url, new List<VideoContent>());

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>().WithMessage("*at least*");
    }
}