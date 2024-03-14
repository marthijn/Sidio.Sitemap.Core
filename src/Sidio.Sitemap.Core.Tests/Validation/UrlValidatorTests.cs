using Sidio.Sitemap.Core.Validation;

namespace Sidio.Sitemap.Core.Tests.Validation;

public sealed class UrlValidatorTests
{
    [Fact]
    public void Construct_WithInvalidBaseUrl_ThrowException()
    {
        // act
        var action = () => new UrlValidator(new InvalidBaseUrlProvider());

        // assert
        action.Should().ThrowExactly<InvalidUrlException>();
    }

    [Fact]
    public void Construct_WithSchemeInvalidBaseUrl_ThrowException()
    {
        // act
        var action = () => new UrlValidator(new InvalidSchemeBaseUrlProvider());

        // assert
        action.Should().ThrowExactly<InvalidUrlException>().WithMessage("*https*");
    }

    [Fact]
    public void Validate_WithAbsoluteUrl_ReturnsUri()
    {
        // arrange
        const string Url = "https://example.com/sitemap.xml";
        var validator = new UrlValidator();

        // act
        var result = validator.Validate(Url);

        // assert
        result.ToString().Should().Be(Url);
    }

    [Theory]
    [InlineData("/sitemap.xml")]
    [InlineData("sitemap.xml")]
    public void Validate_WithRelativeUrl_ReturnsUri(string url)
    {
        // arrange
        var validator = new UrlValidator(new TestBaseUrlProvider());

        // act
        var result = validator.Validate(url);

        // assert
        result.ToString().Should().Be("https://example.com/sitemap.xml");
    }

    [Theory]
    [InlineData("/")]
    [InlineData("")]
    public void Validate_WithEmptyRelativeUrl_ReturnsUri(string url)
    {
        // arrange
        var validator = new UrlValidator(new TestBaseUrlProvider());

        // act
        var result = validator.Validate(url);

        // assert
        result.ToString().Should().Be("https://example.com/");
    }

    [Fact]
    public void Validate_WithRelativeUrlAndEmptyBaseUrl_ThrowException()
    {
        // arrange
        const string Url = "sitemap.xml";
        var validator = new UrlValidator();

        // act
        var action = () => validator.Validate(Url);

        // assert
        action.Should().ThrowExactly<InvalidUrlException>();
    }

    [Fact]
    public void Construct_MaximumUrlLength_DoesNotThrowException()
    {
        // arrange
        var url = "https://example.com/";
        url += string.Join(string.Empty, Enumerable.Range(0, UrlValidator.UrlMaxLength - url.Length).Select(_ => 'a'));
        var validator = new UrlValidator();

        // act
        var action = () => validator.Validate(url);

        // assert
        action.Should().NotThrow();
    }

    [Fact]
    public void Construct_UrlTooLong_ThrowException()
    {
        // arrange
        var url = "https://example.com/";
        url += string.Join(string.Empty, Enumerable.Range(0, UrlValidator.UrlMaxLength - url.Length + 1).Select(_ => 'a'));
        var validator = new UrlValidator();

        // act
        var action = () => validator.Validate(url);

        // assert
        action.Should().ThrowExactly<InvalidUrlException>().WithMessage($"*{UrlValidator.UrlMaxLength}*");
    }

    [Fact]
    public void Construct_WithBaseUrlProviderUrlTooLong_ThrowException()
    {
        // arrange
        var url = string.Join(string.Empty, Enumerable.Range(0, UrlValidator.UrlMaxLength).Select(_ => 'a'));
        var validator = new UrlValidator(new TestBaseUrlProvider());

        // act
        var action = () => validator.Validate(url);

        // assert
        action.Should().ThrowExactly<InvalidUrlException>().WithMessage($"*{UrlValidator.UrlMaxLength}*");
    }

    private sealed class TestBaseUrlProvider : IBaseUrlProvider
    {
        public Uri BaseUrl => new ("https://example.com", UriKind.Absolute);
    }

    private sealed class InvalidBaseUrlProvider : IBaseUrlProvider
    {
        public Uri BaseUrl => new("/example", UriKind.Relative);
    }

    private sealed class InvalidSchemeBaseUrlProvider : IBaseUrlProvider
    {
        public Uri BaseUrl => new("ftp://example.com", UriKind.Absolute);
    }
}