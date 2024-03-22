using Sidio.Sitemap.Core.Extensions;
using Sidio.Sitemap.Core.Serialization;

namespace Sidio.Sitemap.Core.Tests.Serialization;

public sealed partial class XmlSerializerTests
{
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
    public void Serialize_WithSitemapContainsVideoNodes_ReturnsXml()
    {
        // arrange
        const string Url = "https://example.com/?id=1&name=example&gt=>&lt=<&quotes=";
        var sitemap = new Sitemap();
        var now = DateTime.UtcNow;
        var changeFrequency = _fixture.Create<ChangeFrequency>();

        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();

        var duration = _fixture.Create<int>();
        const decimal Rating = 1.1m;
        var viewCount = _fixture.Create<int>();
        var familyFriendly = _fixture.Create<bool>();
        var videoRestriction = new VideoRestriction(_fixture.Create<string>(), Relationship.Deny);
        var videoPlatform = new VideoPlatform(VideoPlatformType.Web, Relationship.Allow);
        var requiresSubscription = _fixture.Create<bool>();
        var uploader = new VideoUploader(_fixture.Create<string>(), Url);
        var live = _fixture.Create<bool>();
        var tags = _fixture.CreateMany<string>(1).ToList();

        var video = new VideoContent(Url, title, description, Url, Url)
                        {
                            Duration = duration,
                            ExpirationDate = now,
                            Rating = Rating,
                            ViewCount = viewCount,
                            PublicationDate = now,
                            FamilyFriendly = familyFriendly,
                            Restriction = videoRestriction,
                            Platform = videoPlatform,
                            RequiresSubscription = requiresSubscription,
                            Uploader = uploader,
                            Live = live,
                            Tags = tags,
                        };

        sitemap.Add(new SitemapNode(Url, now, changeFrequency, 0.32m));
        sitemap.Add(new SitemapVideoNode(Url, video));
        var serializer = new XmlSerializer();

        var expectedUrl = EscapeUrl(Url);

        // act
        var result = serializer.Serialize(sitemap);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be(
            $"<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><urlset xmlns:video=\"http://www.google.com/schemas/sitemap-video/1.1\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{expectedUrl}</loc><lastmod>2024-03-22</lastmod><changefreq>hourly</changefreq><priority>0.3</priority></url><url><loc>{expectedUrl}</loc><video:video><video:thumbnail_loc>{expectedUrl}</video:thumbnail_loc><video:title>{title}</video:title><video:description>{description}</video:description><video:content_loc>{expectedUrl}</video:content_loc><video:player_loc>{expectedUrl}</video:player_loc><video:duration>{duration}</video:duration><video:expiration_date>{video.ExpirationDate:yyyy-MM-ddTHH:mm:ssK}</video:expiration_date><video:rating>1.1</video:rating><video:view_count>{viewCount}</video:view_count><video:restriction relationship=\"deny\">{videoRestriction.Restriction}</video:restriction><video:publication_date>{video.PublicationDate:yyyy-MM-ddTHH:mm:ssK}</video:publication_date><video:family_friendly>{BoolToSitemap(familyFriendly)}</video:family_friendly><video:platform relationship=\"allow\">web</video:platform><video:requires_subscription>{BoolToSitemap(requiresSubscription)}</video:requires_subscription><video:uploader info=\"{expectedUrl}\">{uploader.Name}</video:uploader><video:live>{BoolToSitemap(live)}</video:live><video:tag>{tags.First()}</video:tag></video:video></url></urlset>");
    }

    private static string BoolToSitemap(bool value) => value ? "yes" : "no";
}