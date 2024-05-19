using Sidio.Sitemap.Core.Extensions;
using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Tests.Serialization;

public sealed partial class XmlSerializerTests
{
    [Fact]
    public void Deserialize_GivenValidXml_ReturnsSitemapObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://www.example.com/</loc><lastmod>2005-01-01</lastmod><changefreq>monthly</changefreq><priority>0.8</priority></url></urlset>";
        var serializer = new XmlSerializer();

        // act
        var result = serializer.Deserialize(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(1);

        var node = result.Nodes[0] as SitemapNode;
        node.Should().NotBeNull();
        node!.Url.Should().Be("http://www.example.com/");
        node.LastModified.Should().Be(new DateTime(2005, 1, 1));
        node.ChangeFrequency.Should().Be(ChangeFrequency.Monthly);
        node.Priority.Should().Be(0.8m);
    }

    [Fact]
    public void DeserializeIndex_GivenValidXml_ReturnsSitemapIndexObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><sitemap><loc>https://www.example.com/sitemap1.xml.gz</loc><lastmod>2005-01-01</lastmod></sitemap><sitemap><loc>https://www.example.com/sitemap2.xml.gz</loc></sitemap></sitemapindex>";
        var serializer = new XmlSerializer();

        // act
        var result = serializer.DeserializeIndex(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(2);

        result.Nodes.Should().Contain(x => x.Url == "https://www.example.com/sitemap1.xml.gz");
        result.Nodes.Should().Contain(x => x.Url == "https://www.example.com/sitemap2.xml.gz");
    }

    [Fact]
    public async Task DeserializeAsync_GivenValidXml_ReturnsSitemapObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://www.example.com/</loc><lastmod>2005-01-01</lastmod><changefreq>monthly</changefreq><priority>0.8</priority></url></urlset>";
        var serializer = new XmlSerializer();

        // act
        var result = await serializer.DeserializeAsync(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(1);

        var node = result.Nodes[0] as SitemapNode;
        node.Should().NotBeNull();
        node!.Url.Should().Be("http://www.example.com/");
        node.LastModified.Should().Be(new DateTime(2005, 1, 1));
        node.ChangeFrequency.Should().Be(ChangeFrequency.Monthly);
        node.Priority.Should().Be(0.8m);
    }

    [Fact]
    public async Task DeserializeIndexAsync_GivenValidXml_ReturnsSitemapIndexObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><sitemap><loc>https://www.example.com/sitemap1.xml.gz</loc><lastmod>2005-01-01</lastmod></sitemap><sitemap><loc>https://www.example.com/sitemap2.xml.gz</loc></sitemap></sitemapindex>";
        var serializer = new XmlSerializer();

        // act
        var result = await serializer.DeserializeIndexAsync(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(2);

        result.Nodes.Should().Contain(x => x.Url == "https://www.example.com/sitemap1.xml.gz");
        result.Nodes.Should().Contain(x => x.Url == "https://www.example.com/sitemap2.xml.gz");
    }

    [Fact]
    public void Deserialize_GivenValidImageSitemapXml_ReturnsSitemapObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:image=\"http://www.google.com/schemas/sitemap-image/1.1\"><url><loc>https://example.com/sample1.html</loc><image:image><image:loc>https://example.com/image.jpg</image:loc></image:image><image:image><image:loc>https://example.com/photo.jpg</image:loc></image:image></url><url><loc>https://example.com/sample2.html</loc><image:image><image:loc>https://example.com/picture.jpg</image:loc></image:image></url></urlset>";
        var serializer = new XmlSerializer();

        // act
        var result = serializer.Deserialize(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(2);

        var imageNode = result.Nodes.Single(x => x.Url == "https://example.com/sample1.html") as SitemapImageNode;
        imageNode.Should().NotBeNull();
        imageNode!.Images.Should().HaveCount(2);
    }

    [Fact]
    public void Deserialize_GivenValidNewsSitemapXml_ReturnsSitemapObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:news=\"http://www.google.com/schemas/sitemap-news/0.9\"><url><loc>http://www.example.org/business/article55.html</loc><news:news><news:publication><news:name>The Example Times</news:name><news:language>en</news:language></news:publication><news:publication_date>2008-12-23</news:publication_date><news:title>Companies A, B in Merger Talks</news:title></news:news></url></urlset>";
        var serializer = new XmlSerializer();

        // act
        var result = serializer.Deserialize(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(1);

        var newsNode = result.Nodes[0] as SitemapNewsNode;
        newsNode.Should().NotBeNull();
        newsNode!.Url.Should().Be("http://www.example.org/business/article55.html");
        newsNode.Title.Should().Be("Companies A, B in Merger Talks");
        newsNode.PublicationDate.Should().Be(new DateTime(2008, 12, 23));
        newsNode.Publication.Language.Should().Be("en");
        newsNode.Publication.Name.Should().Be("The Example Times");
    }

    [Fact]
    public void Deserialize_GivenValidVideoSitemapXml_ReturnsSitemapObject()
    {
        // arrange
        const string Xml =
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:video=\"http://www.google.com/schemas/sitemap-video/1.1\"><url><loc>https://www.example.com/videos/some_video_landing_page.html</loc><video:video><video:thumbnail_loc>https://www.example.com/thumbs/123.jpg</video:thumbnail_loc><video:title>Grilling steaks for summer</video:title><video:description>Alkis shows you how to get perfectly done steaks every time</video:description><video:content_loc>http://streamserver.example.com/video123.mp4</video:content_loc><video:player_loc>https://www.example.com/videoplayer.php?video=123</video:player_loc><video:duration>600</video:duration><video:expiration_date>2021-11-05T19:20:30+08:00</video:expiration_date><video:rating>4.2</video:rating><video:view_count>12345</video:view_count><video:publication_date>2007-11-05T19:20:30+08:00</video:publication_date><video:family_friendly>yes</video:family_friendly><video:restriction relationship=\"allow\">IE GB US CA</video:restriction><video:price currency=\"EUR\">1.99</video:price><video:requires_subscription>yes</video:requires_subscription><video:uploader info=\"https://www.example.com/users/grillymcgrillerson\">GrillyMcGrillerson</video:uploader><video:live>no</video:live></video:video><video:video><video:thumbnail_loc>https://www.example.com/thumbs/345.jpg</video:thumbnail_loc><video:title>Grilling steaks for winter</video:title><video:description>In the freezing cold, Roman shows you how to get perfectly done steaks every time.</video:description><video:content_loc>http://streamserver.example.com/video345.mp4</video:content_loc><video:player_loc>https://www.example.com/videoplayer.php?video=345</video:player_loc></video:video></url></urlset>";
        var serializer = new XmlSerializer();

        // act
        var result = serializer.Deserialize(Xml);

        // assert
        result.Should().NotBeNull();
        result.Nodes.Should().HaveCount(1);

        var videoNode = result.Nodes[0] as SitemapVideoNode;
        videoNode.Should().NotBeNull();
        videoNode!.Videos.Should().HaveCount(2);

        var firstVideoNode = videoNode.Videos.Single(x => x.ThumbnailUrl == "https://www.example.com/thumbs/123.jpg");
        firstVideoNode.Title.Should().Be("Grilling steaks for summer");
        firstVideoNode.Description.Should().Be("Alkis shows you how to get perfectly done steaks every time");
        firstVideoNode.ContentUrl.Should().Be("http://streamserver.example.com/video123.mp4");
        firstVideoNode.PlayerUrl.Should().Be("https://www.example.com/videoplayer.php?video=123");
        firstVideoNode.Duration.Should().Be(600);
        firstVideoNode.ExpirationDate.Should().Be(new DateTimeOffset(2021, 11, 5, 19, 20, 30, TimeSpan.FromHours(8)));
        firstVideoNode.Rating.Should().Be(4.2m);
        firstVideoNode.ViewCount.Should().Be(12345);
        firstVideoNode.PublicationDate.Should().Be(new DateTimeOffset(2007, 11, 5, 19, 20, 30, TimeSpan.FromHours(8)));
        firstVideoNode.FamilyFriendly.Should().BeTrue();
        firstVideoNode.Restriction.Should().NotBeNull();
        firstVideoNode.Restriction!.Relationship.Should().Be(Relationship.Allow);
        firstVideoNode.Restriction.Restriction.Should().Be("IE GB US CA");
        firstVideoNode.RequiresSubscription.Should().BeTrue();
        firstVideoNode.Uploader!.Name.Should().Be("GrillyMcGrillerson");
        firstVideoNode.Uploader.Info.Should().Be("https://www.example.com/users/grillymcgrillerson");
        firstVideoNode.Live.Should().BeFalse();
    }
}