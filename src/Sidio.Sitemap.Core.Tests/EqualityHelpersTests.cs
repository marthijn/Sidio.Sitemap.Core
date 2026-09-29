namespace Sidio.Sitemap.Core.Tests;

public sealed class EqualityHelpersTests
{
    [Fact]
    public void UnorderedEquals_BothNull_ReturnsTrue()
    {
        // act
        var result = EqualityHelpers.UnorderedEquals<string>(null, null);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UnorderedEquals_OneNull_ReturnsFalse()
    {
        // arrange
        var values = new[] {"a", "b"};

        // act
        var result = EqualityHelpers.UnorderedEquals(values, null);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void UnorderedEquals_SameItemsDifferentOrder_ReturnsTrue()
    {
        // arrange
        var left = new[] {"a", "b", "c"};
        var right = new[] {"c", "a", "b"};

        // act
        var result = EqualityHelpers.UnorderedEquals(left, right);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void UnorderedEquals_DifferentItemMultiplicity_ReturnsFalse()
    {
        // arrange
        var left = new[] {"a", "a", "b"};
        var right = new[] {"a", "b", "b"};

        // act
        var result = EqualityHelpers.UnorderedEquals(left, right);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetUnorderedHashCode_Null_ReturnsZero()
    {
        // act
        var hash = EqualityHelpers.GetUnorderedHashCode<string>(null);

        // assert
        hash.Should().Be(0);
    }

    [Fact]
    public void GetUnorderedHashCode_SameItemsDifferentOrder_ReturnsSameHash()
    {
        // arrange
        var left = new[] {"a", "b", "c"};
        var right = new[] {"c", "a", "b"};

        // act
        var leftHash = EqualityHelpers.GetUnorderedHashCode(left);
        var rightHash = EqualityHelpers.GetUnorderedHashCode(right);

        // assert
        leftHash.Should().Be(rightHash);
    }

    [Fact]
    public void GetUnorderedHashCode_SameInput_IsDeterministic()
    {
        // arrange
        var values = new[] {"a", "a", "b"};

        // act
        var firstHash = EqualityHelpers.GetUnorderedHashCode(values);
        var secondHash = EqualityHelpers.GetUnorderedHashCode(values);

        // assert
        firstHash.Should().Be(secondHash);
    }
}
