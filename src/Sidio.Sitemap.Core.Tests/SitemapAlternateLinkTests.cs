namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapAlternateLinkTests
{
    [Theory]
    [InlineData("en")]
    [InlineData("en-US")]
    [InlineData("x-default")]
    public void Construct_WithValidArguments_SitemapNodeConstructed(string hrefLang)
    {
        // act
        var sitemapAlternateLink = new SitemapAlternateLink(hrefLang, "http://example.com/");

        // assert
        sitemapAlternateLink.Should().NotBeNull();
    }

    [Theory]
    [InlineData("englisch")]
    [InlineData("en_US")]
    public void Construct_WithInvalidHrefLang_ThrowsArgumentException(string hrefLang)
    {
        // act
        Action act = () => _ = new SitemapAlternateLink(hrefLang, "http://example.com/");

        // assert
        act.Should().Throw<ArgumentException>().WithMessage("*hreflang*");
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeTrue()
    {
        // arrange
        var link1 = new SitemapAlternateLink("en", "http://example.com/", "test1");
        var link2 = new SitemapAlternateLink("en", "http://example.com/", "test1");

        // act & assert
        (link1 == link2).Should().BeTrue();
    }
}