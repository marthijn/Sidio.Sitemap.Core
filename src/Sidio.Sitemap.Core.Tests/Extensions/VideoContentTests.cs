using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class VideoContentTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_VideoContentConstructed()
    {
        // arrange
        var thumbnailUrl = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        var duration = _fixture.Create<int>();
        var expirationDate = _fixture.Create<DateTimeOffset>();
        const decimal Rating = 1.0m;
        var viewCount = _fixture.Create<int>();
        var publicationDate = _fixture.Create<DateTimeOffset>();
        var familyFriendly = _fixture.Create<bool>();
        var videoRestriction = _fixture.Create<VideoRestriction>();
        var videoPlatform = _fixture.Create<VideoPlatform>();
        var requiresSubscription = _fixture.Create<bool>();
        var uploader = _fixture.Create<VideoUploader>();
        var live = _fixture.Create<bool>();
        var tags = _fixture.CreateMany<string>(5).ToList();

        // act
        var video = new VideoContent(thumbnailUrl, title, description, contentUrl, playerUrl)
                           {
                               Duration = duration,
                               ExpirationDate = expirationDate,
                               Rating = Rating,
                               ViewCount = viewCount,
                               PublicationDate = publicationDate,
                               FamilyFriendly = familyFriendly,
                               Restriction = videoRestriction,
                               Platform = videoPlatform,
                               RequiresSubscription = requiresSubscription,
                               Uploader = uploader,
                               Live = live,
                               Tags = tags,
                           };

        // assert
        video.ThumbnailUrl.Should().Be(thumbnailUrl);
        video.Title.Should().Be(title);
        video.Description.Should().Be(description);
        video.ContentUrl.Should().Be(contentUrl);
        video.PlayerUrl.Should().Be(playerUrl);

        video.Duration.Should().Be(duration);
        video.ExpirationDate.Should().Be(expirationDate);
        video.Rating.Should().Be(Rating);
        video.ViewCount.Should().Be(viewCount);
        video.PublicationDate.Should().Be(publicationDate);
        video.FamilyFriendly.Should().Be(familyFriendly);
        video.Restriction.Should().Be(videoRestriction);
        video.Platform.Should().Be(videoPlatform);
        video.RequiresSubscription.Should().Be(requiresSubscription);
        video.Uploader.Should().Be(uploader);
        video.Live.Should().Be(live);
        video.Tags.Should().BeEquivalentTo(tags);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithInvalidThumbnailUrl_ThrowException(string? url)
    {
        // arrange
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(url!, title, description, contentUrl, playerUrl);

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*ThumbnailUrl*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithInvalidTitle_ThrowException(string? title)
    {
        // arrange
        var url = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(url, title!, description, contentUrl, playerUrl);

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Title*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithInvalidDescription_ThrowException(string? description)
    {
        // arrange
        var url = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(url, title, description!, contentUrl, playerUrl);

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Description*");
    }

    [Fact]
    public void Construct_WithTooLongDescription_ThrowException()
    {
        // arrange
        var url = _fixture.Create<string>();
        var description = new string(Enumerable.Range(0, 2049).Select(_ => _fixture.Create<char>()).ToArray());
        var title = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(url, title, description, contentUrl, playerUrl);

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Description*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithInvalidContentAndPlayerUrl_ThrowException(string? url)
    {
        // arrange
        var thumbnailUrl = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(thumbnailUrl, title, description, url, url);

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*ContentUrl*PlayerUrl*");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(28801)]
    public void Construct_WithInvalidDuration_ThrowException(int duration)
    {
        // arrange
        var thumbnailUrl = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(thumbnailUrl, title, description, contentUrl, playerUrl) { Duration = duration };

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Duration*");
    }

    [Theory]
    [InlineData(-0.0000001)]
    [InlineData(5.0000001)]
    public void Construct_WithInvalidRating_ThrowException(decimal rating)
    {
        // arrange
        var thumbnailUrl = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();

        // act
        var action = () => new VideoContent(thumbnailUrl, title, description, contentUrl, playerUrl) { Rating = rating };

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Rating*");
    }

    [Fact]
    public void Construct_WithTooManyTags_ThrowException()
    {
        // arrange
        var thumbnailUrl = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var contentUrl = _fixture.Create<string>();
        var playerUrl = _fixture.Create<string>();
        var tags = _fixture.CreateMany<string>(33).ToList();

        // act
        var action = () => new VideoContent(thumbnailUrl, title, description, contentUrl, playerUrl) { Tags = tags };

        // assert
        action.Should().ThrowExactly<ArgumentException>().WithMessage("*Tags*");
    }
}