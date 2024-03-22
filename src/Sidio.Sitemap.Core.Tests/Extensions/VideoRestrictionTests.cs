using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class VideoRestrictionTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_VideoRestrictionConstructed()
    {
        // arrange
        var restrictions = _fixture.Create<string>();
        var relationship = _fixture.Create<Relationship>();

        // act
        var restriction = new VideoRestriction(restrictions, relationship);

        // assert
        restriction.Restriction.Should().Be(restrictions);
        restriction.Relationship.Should().Be(relationship);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? restrictions)
    {
        // arrange
        var relationship = _fixture.Create<Relationship>();

        // act
        var restrictionAction = () => new VideoRestriction(restrictions!, relationship);

        // assert
        restrictionAction.Should().ThrowExactly<ArgumentException>();
    }
}