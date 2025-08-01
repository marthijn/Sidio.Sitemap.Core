﻿namespace Sidio.Sitemap.Core.Tests;

public sealed class SitemapNodeTests
{
    private readonly Fixture _fixture = new ();

    [Theory]
    [InlineData("https://www.example.com")]
    [InlineData("HTTPS://www.example.com")]
    [InlineData("http://www.example.com")]
    public void Construct_WithValidArguments_SitemapNodeConstructed(string url)
    {
        // arrange
        const decimal Priority = 0.5m;
        var lastModified = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();

        // act
        var sitemapNode = new SitemapNode(url, lastModified, changeFrequency, Priority);

        // assert
        sitemapNode.Url.Should().Be(url);
        sitemapNode.LastModified.Should().Be(lastModified);
        sitemapNode.ChangeFrequency.Should().Be(changeFrequency);
        sitemapNode.Priority.Should().Be(Priority);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? url)
    {
        // act
        var sitemapNodeAction = () => new SitemapNode(url!);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [InlineData(-0.001)]
    [InlineData(1.001)]
    public void Construct_WithInvalidPriority_ThrowException(decimal priority)
    {
        // arrange
        const string Url = "https://example.com";

        // act
        var sitemapNodeAction = () => new SitemapNode(Url, priority: priority);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>().WithMessage("*priority*");
    }

    [Fact]
    public void Create_WhenUrlIsValid_SitemapNodeCreated()
    {
        // arrange
        const string Url = "https://example.com";
        var dateTime = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        const decimal Priority = 0.5m;

        // act
        var node = SitemapNode.Create(Url, dateTime, changeFrequency, Priority);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.LastModified.Should().Be(dateTime);
        node.ChangeFrequency.Should().Be(changeFrequency);
        node.Priority.Should().Be(Priority);
    }

    [Fact]
    public void Create_WhenUrlIsNull_SitemapNodeNotCreated()
    {
        // arrange & act
        var node = SitemapIndexNode.Create(null);

        // assert
        node.Should().BeNull();
    }

    [Fact]
    public void SitemapNode_Equality()
    {
        // arrange
        var url = "https://example.com";
        var lastModified = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        const decimal Priority = 0.5m;

        var node1 = new SitemapNode(url, lastModified, changeFrequency, Priority);
        var node2 = new SitemapNode(url, lastModified, changeFrequency, Priority);

        // act & assert
        (node1 == node2).Should().BeTrue();
    }
}