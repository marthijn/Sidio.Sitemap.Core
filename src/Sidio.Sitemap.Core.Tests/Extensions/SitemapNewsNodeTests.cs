﻿using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Tests.Extensions;

public sealed class SitemapNewsNodeTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Construct1_WithValidArguments_SitemapNewsNodeConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";
        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var sitemapNode = new SitemapNewsNode(Url, title, name, language, publicationDate);

        // assert
        sitemapNode.Url.Should().Be(Url);
        sitemapNode.Title.Should().Be(title);
        sitemapNode.Publication.Name.Should().Be(name);
        sitemapNode.Publication.Language.Should().Be(language);
        sitemapNode.PublicationDate.Should().Be(publicationDate);
    }

    [Fact]
    public void Construct2_WithValidArguments_SitemapNewsNodeConstructed()
    {
        // arrange
        const string Url = "http://www.example.com";
        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var sitemapNode = new SitemapNewsNode(Url, title, new Publication(name, language), publicationDate);

        // assert
        sitemapNode.Url.Should().Be(Url);
        sitemapNode.Title.Should().Be(title);
        sitemapNode.Publication.Name.Should().Be(name);
        sitemapNode.Publication.Language.Should().Be(language);
        sitemapNode.PublicationDate.Should().Be(publicationDate);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyUrl_ThrowException(string? url)
    {
        // arrange
        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var sitemapNodeAction = () => new SitemapNewsNode(url!, title, name, language, publicationDate);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Construct_WithEmptyTitle_ThrowException(string? title)
    {
        // arrange
        var url = _fixture.Create<string>();
        var name = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var sitemapNodeAction = () => new SitemapNewsNode(url, title!, name, language, publicationDate);

        // assert
        sitemapNodeAction.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Create_WhenUrlIsValidWithLanguage_NodeCreated()
    {
        // arrange
        const string Url = "http://www.example.com";
        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var node = SitemapNewsNode.Create(Url, title, name, language, publicationDate);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.Publication.Should().NotBeNull();
        node.Title.Should().Be(title);
        node.PublicationDate.Should().Be(publicationDate);
    }

    [Fact]
    public void Create_WhenUrlIsValidWithLanguage_NodeNotCreated()
    {
        // arrange
        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        // act
        var node = SitemapNewsNode.Create(null, title, name, language, publicationDate);

        // assert
        node.Should().BeNull();
    }

    [Fact]
    public void Create_WhenUrlIsValidWithPublication_NodeCreated()
    {
        // arrange
        const string Url = "http://www.example.com";
        var title = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();
        var publication = _fixture.Create<Publication>();

        // act
        var node = SitemapNewsNode.Create(Url, title ,publication, publicationDate);

        // assert
        node.Should().NotBeNull();
        node.Url.Should().Be(Url);
        node.Publication.Should().NotBeNull();
        node.Title.Should().Be(title);
        node.PublicationDate.Should().Be(publicationDate);
    }

    [Fact]
    public void Create_WhenUrlIsValidWitPublication_NodeNotCreated()
    {
        // arrange
        var title = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();
        var publication = _fixture.Create<Publication>();

        // act
        var node = SitemapNewsNode.Create(null, title ,publication, publicationDate);

        // assert
        node.Should().BeNull();
    }
}