using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class VideoUploaderTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_VideoRestrictionConstructed()
    {
        // arrange
        var name = _fixture.Create<string>();
        var info = _fixture.Create<string>();

        // act
        var uploader = new VideoUploader(name, info);

        // assert
        uploader.Name.Should().Be(name);
        uploader.Info.Should().Be(info);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyName_ThrowException(string? name)
    {
        // act
        var action = () => new VideoUploader(name!);

        // assert
        action.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Construct_WithInvalidName_ThrowException()
    {
        // arrange
        var name = new string(Enumerable.Range(0, 256).Select(_ => _fixture.Create<char>()).ToArray());

        // act
        var action = () => new VideoUploader(name);

        // assert
        action.Should().ThrowExactly<ArgumentException>();
    }
}