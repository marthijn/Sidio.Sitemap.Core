using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Tests.Serialization;

public sealed class StringExtensionsTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData("href=\"\"", "")]
    [InlineData("href=\"https://example.com\"", "https://example.com")]
    [InlineData("href=\"https://example.com\" rel=\"nofollow\"", "https://example.com")]
    [InlineData(" target=\"_blank\" href=\"https://example.com\" rel=\"nofollow\"", "https://example.com")]
    public void GetHref_WithInput_ReturnsExpected(string? input, string? expected)
    {
        // act
        var result = input.GetHref();

        // assert
        result.Should().Be(expected);
    }
}