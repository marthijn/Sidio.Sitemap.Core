using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class PublicationTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct_WithValidArguments_PublicationConstructed()
    {
        // arrange
        var name = _fixture.Create<string>();
        var language = _fixture.Create<string>();

        // act
        var sitemapNode = new Publication(name, language);

        // assert
        sitemapNode.Name.Should().Be(name);
        sitemapNode.Language.Should().Be(language);
    }

    [Theory]
    [InlineData("", "NL")]
    [InlineData(" ", "NL")]
    [InlineData(null, "NL")]
    [InlineData("name", "")]
    [InlineData("name", " ")]
    [InlineData("name", null)]
    public void Construct_WithEmptyUrl_ThrowException(string? name, string? language)
    {
        // act
        var sitemapNodeAction = () => new Publication(name!, language!);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }
}