namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapAlternateLinkTests
{
    [Theory]
    [InlineData("en")]
    [InlineData("en-US")]
    [InlineData("x-default")]
    public void Construct_WithValidArguments_SitemapNodeConstructed(string hrefLang)
    {
        var sitemapAlternateLink = new SitemapAlternateLink(hrefLang, "http://example.com/");
        sitemapAlternateLink.Should().NotBeNull();
    }

    [Theory]
    [InlineData("englisch")]
    [InlineData("en_US")]
    public void Construct_WithInvalidHrefLang_ThrowsArgumentException(string hrefLang)
    {
        // act
        Action act = () => new SitemapAlternateLink(hrefLang, "http://example.com/");

        // assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("*hreflang*");
    }
}