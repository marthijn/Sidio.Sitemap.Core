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

    [Fact]
    public void Create_WhenUrlIsValidWithSingleVideoContent_NodeCreated()
    {
        // arrange
        const string Url = "http://www.example.com";
        var videoContent = new VideoContent(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>());

        // act
        var node = SitemapVideoNode.Create(Url, videoContent);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.Videos.Should().Contain(videoContent);
    }

    [Fact]
    public void Create_WhenUrlIsNullWithSingleVideoContent_NodeNotCreated()
    {
        // arrange
        var videoContent = new VideoContent(
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>());

        // act
        var node = SitemapVideoNode.Create(null, videoContent);

        // assert
        node.Should().BeNull();
    }

    [Fact]
    public void Create_WhenUrlIsValidWithMultipleVideoContent_NodeCreated()
    {
        // arrange
        const string Url = "http://www.example.com";
        var videoContent = new List<VideoContent>
        {
            new VideoContent(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>())
        };

        // act
        var node = SitemapVideoNode.Create(Url, videoContent);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.Videos.Should().BeEquivalentTo(videoContent);
    }

    [Fact]
    public void Create_WhenUrlIsNullWithMultipleVideoContent_NodeNotCreated()
    {
        // arrange
        var videoContent = new List<VideoContent>
        {
            new VideoContent(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>())
        };

        // act
        var node = SitemapVideoNode.Create(null, videoContent);

        // assert
        node.Should().BeNull();
    }

    [Fact]
    public void Equality_WithSameVideosDifferentOrder_ShouldBeTrue()
    {
        // arrange
        const string Url = "http://www.example.com";
        var video1 = new VideoContent("https://example.com/thumb1.jpg", "video1", "description1", "https://example.com/video1.mp4", null);
        var video2 = new VideoContent("https://example.com/thumb2.jpg", "video2", "description2", "https://example.com/video2.mp4", null);

        var node1 = new SitemapVideoNode(Url, new[] { video1, video2 });
        var node2 = new SitemapVideoNode(Url, new[] { video2, video1 });

        // act & assert
        (node1 == node2).Should().BeTrue();
    }
}