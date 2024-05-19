using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Tests.Serialization.Integration;

public sealed class IntegrationTests
{
    [Fact]
    public void Sitemap_Serialize_Deserialize()
    {
        // arrange
        var sitemap = new Sitemap(
            new[] {new SitemapNode("https://example.com/", DateTime.UtcNow, ChangeFrequency.Daily, 0.5m)});
        var serializer = new XmlSerializer();
        var xml = serializer.Serialize(sitemap);

        // act
        var result = serializer.Deserialize(xml);

        // assert
        result.Should().BeEquivalentTo(sitemap);
    }

    [Fact]
    public void Sitemap_Deserialize_Serialize()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://www.example.com/</loc><lastmod>2005-01-01</lastmod><changefreq>monthly</changefreq><priority>0.8</priority></url></urlset>";
        var serializer = new XmlSerializer();
        var sitemap = serializer.Deserialize(Xml);

        // act
        var result = serializer.Serialize(sitemap);

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(Xml);
    }
}