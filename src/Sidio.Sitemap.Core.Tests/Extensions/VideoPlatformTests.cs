using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class VideoPlatformTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_VideoPlatformConstructed()
    {
        // arrange
        var platformType = _fixture.Create<VideoPlatformType>();
        var relationship = _fixture.Create<Relationship>();

        // act
        var platform = new VideoPlatform(platformType, relationship);

        // assert
        platform.Platform.Should().Be(platformType);
        platform.Relationship.Should().Be(relationship);
    }
    
    [Fact]
    public void Construct_WithMultiplePlatformTypes_VideoPlatformConstructed()
    {
        // arrange
        const VideoPlatformType PlatformType = VideoPlatformType.Mobile | VideoPlatformType.Tv;
        var relationship = _fixture.Create<Relationship>();

        // act
        var platform = new VideoPlatform(PlatformType, relationship);

        // assert
        platform.Platform.Should().Be(PlatformType);
        platform.Relationship.Should().Be(relationship);

        platform.Platform.HasFlag(VideoPlatformType.Mobile).Should().BeTrue();
        platform.Platform.HasFlag(VideoPlatformType.Tv).Should().BeTrue();
        platform.Platform.HasFlag(VideoPlatformType.Web).Should().BeFalse();
    }
}