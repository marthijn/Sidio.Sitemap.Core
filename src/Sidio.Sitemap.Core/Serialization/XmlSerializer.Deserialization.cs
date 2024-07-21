using System.Globalization;
using System.Xml.Linq;
using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Serialization;

public sealed partial class XmlSerializer
{
    /// <inheritdoc />
    public Sitemap Deserialize(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
        {
            throw new ArgumentNullException(nameof(xml));
        }

        var doc = XDocument.Parse(xml);
        XNamespace ns = SitemapNamespace;
        XNamespace imageNs = SitemapNamespaceImage;
        XNamespace newsNs = SitemapNamespaceNews;
        XNamespace videoNs = SitemapNamespaceVideo;

        var sitemap = new Sitemap();
        foreach (var element in doc.Root?.Elements(ns + "url") ?? [])
        {
            var loc = element.Element(ns + "loc")?.Value;
            var lastmod = element.Element(ns + "lastmod")?.Value;
            var changefreq = element.Element(ns + "changefreq")?.Value;
            var priority = element.Element(ns + "priority")?.Value;

            // image extensions
            var images = element.Elements(imageNs + "image").Select(
                x => new ImageLocation(
                    x.Element(imageNs + "loc")?.Value ??
                    throw new SitemapXmlDeserializationException("loc cannot be empty", x))).ToList();

            // news extensions
            var news = element.Element(newsNs + "news");

            // video extensions
            var videos = element.Elements(videoNs + "video").ToList();

            if (images.Count != 0)
            {
                sitemap.Add(new SitemapImageNode(loc ?? throw new SitemapXmlDeserializationException("loc cannot be empty", element), images));
            }
            else if (news != null)
            {
                sitemap.Add(ParseNewsNode(news, loc ?? throw new SitemapXmlDeserializationException("loc cannot be empty", element), newsNs));
            }
            else if (videos.Count != 0)
            {
                sitemap.Add(ParseVideoNode(videos, loc ?? throw new SitemapXmlDeserializationException("loc cannot be empty", element), videoNs));
            }
            else
            {
                sitemap.Add(
                    new SitemapNode(
                        loc ?? throw new SitemapXmlDeserializationException("URL is required for sitemap node.", element),
                        lastmod != null ? DateTime.Parse(lastmod) : null,
                        changefreq != null ? Enum.Parse(typeof(ChangeFrequency), changefreq, true) as ChangeFrequency? : null,
                        priority != null ? decimal.Parse(priority, SitemapCulture) : null));
            }
        }

        return sitemap;
    }

    /// <inheritdoc />
    public Task<Sitemap> DeserializeAsync(string xml, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => Deserialize(xml), cancellationToken);
    }

    /// <inheritdoc />
    public SitemapIndex DeserializeIndex(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
        {
            throw new ArgumentNullException(nameof(xml));
        }

        var doc = XDocument.Parse(xml);
        XNamespace ns = SitemapNamespace;

        var sitemapIndex = new SitemapIndex();
        foreach (var element in doc.Root?.Elements(ns + "sitemap") ?? [])
        {
            var loc = element.Element(ns + "loc")?.Value;
            var lastmod = element.Element(ns + "lastmod")?.Value;

            sitemapIndex.Add(
                new SitemapIndexNode(
                    loc ??  throw new SitemapXmlDeserializationException("loc cannot be empty", element),
                    lastmod != null ? DateTime.Parse(lastmod) : null));
        }

        return sitemapIndex;
    }

    /// <inheritdoc />
    public Task<SitemapIndex> DeserializeIndexAsync(string xml, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => DeserializeIndex(xml), cancellationToken);
    }

    private static SitemapVideoNode ParseVideoNode(IEnumerable<XElement> nodes, string url, XNamespace ns)
    {
        var parsedNodes = nodes.Select(x => ParseVideoContent(x, ns));
        return new SitemapVideoNode(url, parsedNodes);
    }

    private static VideoContent ParseVideoContent(XElement node, XNamespace ns)
    {
        var thumbnail = node.Element(ns + "thumbnail_loc")?.Value??
                        throw new SitemapXmlDeserializationException(
                            "Thumbnail location is required for video sitemap node.", node);
        var title = node.Element(ns + "title")?.Value??
                    throw new SitemapXmlDeserializationException(
                        "Title is required for video sitemap node.", node);
        var description = node.Element(ns + "description")?.Value??
                          throw new SitemapXmlDeserializationException(
                              "Description is required for video sitemap node.", node);
        var content = node.Element(ns + "content_loc")?.Value??
                      throw new SitemapXmlDeserializationException(
                          "Content location is required for video sitemap node.", node);
        var player = node.Element(ns + "player_loc")?.Value??
                     throw new SitemapXmlDeserializationException(
                         "Player location is required for video sitemap node.", node);
        var duration = node.Element(ns + "duration")?.Value;
        var expirationDate = node.Element(ns + "expiration_date")?.Value;
        var rating = node.Element(ns + "rating")?.Value;
        var publicationDate = node.Element(ns + "publication_date")?.Value;
        var familyFriendly = node.Element(ns + "family_friendly")?.Value;
        var restriction = node.Element(ns + "restriction")?.Value;
        var restrictionRelationship = node.Element(ns + "restriction")?.Attribute("relationship")?.Value;
        var requiresSubscription = node.Element(ns + "requires_subscription")?.Value;
        var uploader = node.Element(ns + "uploader")?.Value;
        var uploaderInfo = node.Element(ns + "uploader")?.Attribute("info")?.Value;
        var live = node.Element(ns + "live")?.Value;
        var tags = node.Elements(ns + "tag").Select(x => x.Value);
        var viewCount = node.Element(ns + "view_count")?.Value;
        var platform = node.Element(ns + "platform")?.Value;
        var platformRelationship = node.Element(ns + "platform")?.Attribute("relationship")?.Value;

        return new VideoContent(thumbnail, title, description, content, player)
        {
            Duration = duration != null ? int.Parse(duration) : null,
            ExpirationDate = expirationDate != null ? DateTimeOffset.Parse(expirationDate) : null,
            Rating = rating != null ? decimal.Parse(rating, SitemapCulture) : null,
            Live = live != null ? ParseBool(live, node) : null,
            FamilyFriendly = familyFriendly != null ? ParseBool(familyFriendly, node) : null,
            PublicationDate = publicationDate != null
                ? DateTimeOffset.Parse(publicationDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
                : null,
            RequiresSubscription = requiresSubscription != null ? ParseBool(requiresSubscription, node) : null,
            Uploader = uploader != null ? new VideoUploader(uploader, uploaderInfo) : null,
            Restriction = restriction != null
                ? new VideoRestriction(
                    restriction,
                    (Relationship)Enum.Parse(typeof(Relationship),
                        restrictionRelationship ?? throw new SitemapXmlDeserializationException(
                            "Relationship is required when a restriction is provided.",
                            node),
                        true))
                : null,
            Tags = tags.ToList(),
            ViewCount = viewCount != null ? int.Parse(viewCount) : null,
            Platform = platform != null
                ? new VideoPlatform(
                    (VideoPlatformType)Enum.Parse(typeof (VideoPlatformType), platform, true),
                    (Relationship)Enum.Parse(typeof(Relationship),
                        platformRelationship ?? throw new SitemapXmlDeserializationException(
                            "Relationship is required when a platform is provided.",
                            node),
                        true))
                : null,
        };
    }

    private static SitemapNewsNode ParseNewsNode(XElement node, string url, XNamespace ns)
    {
        var publicationName = node.Element(ns + "publication")?.Element(ns + "name")?.Value ??
                              throw new SitemapXmlDeserializationException(
                                  "Publication name is required for news sitemap node.", node);
        var publicationLanguage = node.Element(ns + "publication")?.Element(ns + "language")?.Value ??
                                  throw new SitemapXmlDeserializationException(
                                      "Publication language is required for news sitemap node.", node);
        var publicationDate = node.Element(ns + "publication_date")?.Value ??
                              throw new SitemapXmlDeserializationException(
                                  "Publication date is required for news sitemap node.", node);
        var title = node.Element(ns + "title")?.Value ??
                    throw new SitemapXmlDeserializationException("Title is required for news sitemap node.", node);

        return new SitemapNewsNode(
            url,
            title,
            new Publication(publicationName, publicationLanguage),
            DateTimeOffset.Parse(publicationDate));
    }

    private static bool ParseBool(string value, XElement element)
    {
        return value.ToLower() switch
        {
            "yes" => true,
            "no" => false,
            _ => throw new SitemapXmlDeserializationException(
                $"Value '{value}' is not a valid boolean value. Expected 'yes' or 'no'.", element),
        };
    }
}