using Sidio.Sitemap.Core.Extensions;
using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Tests.Serialization;

public sealed class XmlSerializerTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Serialize_WithSitemap_ReturnsXml()
    {
        // arrange
        const string Url = "https://example.com/?id=1&name=example&gt=>&lt=<&quotes=";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        sitemap.Add(new SitemapNode(Url, now, changeFrequency, 0.32m));
        var serializer = new XmlSerializer();

        var expectedUrl = EscapeUrl(Url);

        // act
        var result = serializer.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{expectedUrl}</loc><lastmod>{now:yyyy-MM-dd}</lastmod><changefreq>{changeFrequency.ToString().ToLower()}</changefreq><priority>0.3</priority></url></urlset>");
    }

    [Fact]
    public void Serialize_WithSitemapContainsImageNodes_ReturnsXml()
    {
        // arrange
        const string Url = "https://example.com/?id=1&name=example&gt=>&lt=<&quotes=";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        sitemap.Add(new SitemapNode(Url, now, changeFrequency, 0.32m));
        sitemap.Add(new SitemapImageNode(Url, new ImageLocation(Url)));
        var serializer = new XmlSerializer();

        var expectedUrl = EscapeUrl(Url);

        // act
        var result = serializer.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><urlset xmlns:image=\"http://www.google.com/schemas/sitemap-image/1.1\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{expectedUrl}</loc><lastmod>{now:yyyy-MM-dd}</lastmod><changefreq>{changeFrequency.ToString().ToLower()}</changefreq><priority>0.3</priority></url><url><loc>{expectedUrl}</loc><image:image><image:loc>{expectedUrl}</image:loc></image:image></url></urlset>");
    }

    [Fact]
    public void Serialize_WithSitemapContainsNewsNodes_ReturnsXml()
    {
        // arrange
        const string Url = "https://example.com/?id=1&name=example&gt=>&lt=<&quotes=";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();

        var name = _fixture.Create<string>();
        var title = _fixture.Create<string>();
        var language = _fixture.Create<string>();
        var publicationDate = _fixture.Create<DateTimeOffset>();

        sitemap.Add(new SitemapNode(Url, now, changeFrequency, 0.32m));
        sitemap.Add(new SitemapNewsNode(Url, title, name, language, publicationDate));
        var serializer = new XmlSerializer();

        var expectedUrl = EscapeUrl(Url);

        // act
        var result = serializer.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><urlset xmlns:news=\"http://www.google.com/schemas/sitemap-news/0.9\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{expectedUrl}</loc><lastmod>{now:yyyy-MM-dd}</lastmod><changefreq>{changeFrequency.ToString().ToLower()}</changefreq><priority>0.3</priority></url><url><loc>{expectedUrl}</loc><news:news><news:publication><news:name>{name}</news:name><news:language>{language}</news:language></news:publication><news:publication_date>{publicationDate:yyyy-MM-ddTHH:mm:ssK}</news:publication_date><news:title>{title}</news:title></news:news></url></urlset>");
    }

    [Fact]
    public void Serialize_SitemapTooLarge_ThrowException()
    {
        // arrange
        const string Url = "https://example.com/";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        var longUrlPart = string.Join(string.Empty, Enumerable.Range(0, 1500).Select(_ => 'a'));
        sitemap.Add(Enumerable.Range(0, Sitemap.MaxNodes).Select(x => new SitemapNode(Url + x + "/" + longUrlPart, now, changeFrequency, 0.32m)));
        var serializer = new XmlSerializer();

        // act
        var action = () =>serializer.Serialize(sitemap);

        // assert
        action.Should().ThrowExactly<InvalidOperationException>().WithMessage($"*{XmlSerializer.MaxSitemapSizeInMegaBytes}*");
    }

    [Fact]
    public async Task SerializeAsync_WithSitemap_ReturnsXml()
    {
        // arrange
        const string Url = "https://example.com/?id=1&name=example&gt=>&lt=<&quotes=";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();
        sitemap.Add(new SitemapNode(Url, now, changeFrequency, 0.32m));
        var serializer = new XmlSerializer();
        var expectedUrl = EscapeUrl(Url);

        // act
        var result = await serializer.SerializeAsync(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{expectedUrl}</loc><lastmod>{now:yyyy-MM-dd}</lastmod><changefreq>{changeFrequency.ToString().ToLower()}</changefreq><priority>0.3</priority></url></urlset>");
    }

    [Fact]
    public void Serialize_WithSitemapIndex_ReturnsXml()
    {
        // arrange
        var now = DateTime.UtcNow;
        var siteMapIndex = new SitemapIndex(new List<SitemapIndexNode>
        {
            new("https://example.com/sitemap1.xml", now),
            new("https://example.com/sitemap2.xml", now),
        });

        // act
        var result = new XmlSerializer().Serialize(siteMapIndex);

        // assert
        result.Should().NotBeNull();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><sitemap><loc>https://example.com/sitemap1.xml</loc><lastmod>{now:yyyy-MM-dd}</lastmod></sitemap><sitemap><loc>https://example.com/sitemap2.xml</loc><lastmod>{now:yyyy-MM-dd}</lastmod></sitemap></sitemapindex>");
    }

    [Fact]
    public async Task SerializeAsync_WithSitemapIndex_ReturnsXml()
    {
        // arrange
        var now = DateTime.UtcNow;
        var siteMapIndex = new SitemapIndex(new List<SitemapIndexNode>
                                                {
                                                    new("https://example.com/sitemap1.xml", now),
                                                    new("https://example.com/sitemap2.xml", now),
                                                });
        var serializer = new XmlSerializer();

        // act
        var result = await serializer.SerializeAsync(siteMapIndex);

        // assert
        result.Should().NotBeNull();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><sitemap><loc>https://example.com/sitemap1.xml</loc><lastmod>{now:yyyy-MM-dd}</lastmod></sitemap><sitemap><loc>https://example.com/sitemap2.xml</loc><lastmod>{now:yyyy-MM-dd}</lastmod></sitemap></sitemapindex>");
    }

    private static string EscapeUrl(string url)
    {
        return url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
    }
}