using System.Globalization;
using System.Text;
using System.Xml;
using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Serialization;

public sealed partial class XmlSerializer
{
    private static string? BoolToSitemapValue(bool? value)
    {
        if (value == null)
        {
            return null;
        }

        return value.Value ? "yes" : "no";
    }

    private static string RelationshipToSitemapValue(Relationship value)
    {
        return value switch
        {
            Relationship.Allow => "allow",
            Relationship.Deny => "deny",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
        };
    }

    private static string PlatformToSitemapValue(VideoPlatformType platform)
    {
        var sb = new StringBuilder();
        if (platform.HasFlag(VideoPlatformType.Mobile))
        {
            sb.Append("mobile ");
        }

        if (platform.HasFlag(VideoPlatformType.Web))
        {
            sb.Append("web ");
        }

        if (platform.HasFlag(VideoPlatformType.Tv))
        {
            sb.Append("tv ");
        }

        return sb.ToString().Trim();
    }

    private void SerializeNode(XmlWriter writer, SitemapImageNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", url.ToString());

        foreach(var imageLocationNode in node.Images)
        {
            var imageUrl = _urlValidator.Validate(imageLocationNode.Url);
            writer.WriteStartElement("image", "image", null);
            writer.WriteElementString("image", "loc", null, imageUrl.ToString());
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    private void SerializeNode(XmlWriter writer, SitemapNewsNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", url.ToString());

        writer.WriteStartElement("news", "news", null);

        writer.WriteStartElement("news", "publication", null);
        writer.WriteElementString("news", "name", null, node.Publication.Name);
        writer.WriteElementString("news", "language", null, node.Publication.Language);
        writer.WriteEndElement();

        writer.WriteElementString("news", "publication_date", null, node.PublicationDate.ToString("yyyy-MM-ddTHH:mm:ssK"));
        writer.WriteElementString("news", "title", null, node.Title);

        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    private void SerializeNode(XmlWriter writer, SitemapVideoNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", url.ToString());

        foreach (var n in node.Videos)
        {
            SerializeNode(writer, n);
        }

        writer.WriteEndElement();
    }

    private void SerializeNode(XmlWriter writer, VideoContent node)
    {
        const string VideoPrefix = "video";
        writer.WriteStartElement(VideoPrefix, "video", null);
        writer.WriteElementString(VideoPrefix, "thumbnail_loc", null, _urlValidator.Validate(node.ThumbnailUrl).ToString());
        writer.WriteElementString(VideoPrefix, "title", null, node.Title);
        writer.WriteElementString(VideoPrefix, "description", null, node.Description);

        if (!string.IsNullOrEmpty(node.ContentUrl))
        {
            writer.WriteElementString(VideoPrefix, "content_loc", null, _urlValidator.Validate(node.ContentUrl).ToString());
        }

        if (!string.IsNullOrEmpty(node.PlayerUrl))
        {
            writer.WriteElementString(VideoPrefix, "player_loc", null, _urlValidator.Validate(node.PlayerUrl).ToString());
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "duration", node.Duration);
        writer.WriteElementStringIfNotNull(VideoPrefix, "expiration_date", node.ExpirationDate?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        writer.WriteElementStringIfNotNull(VideoPrefix, "rating", node.Rating?.ToString("0.0", new CultureInfo("en-US")));
        writer.WriteElementStringIfNotNull(VideoPrefix, "view_count", node.ViewCount);

        if (node.Restriction != null)
        {
            writer.WriteStartElement(VideoPrefix, "restriction", null);
            writer.WriteAttributeString("relationship", RelationshipToSitemapValue(node.Restriction.Relationship));
            writer.WriteValue(node.Restriction.Restriction);
            writer.WriteEndElement();
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "publication_date", node.PublicationDate?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        writer.WriteElementStringIfNotNull(VideoPrefix, "family_friendly", BoolToSitemapValue(node.FamilyFriendly));

        if (node.Platform != null)
        {
            writer.WriteStartElement(VideoPrefix, "platform", null);
            writer.WriteAttributeString("relationship", RelationshipToSitemapValue(node.Platform.Relationship));
            writer.WriteValue(PlatformToSitemapValue(node.Platform.Platform));
            writer.WriteEndElement();
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "requires_subscription", BoolToSitemapValue(node.RequiresSubscription));

        if (node.Uploader != null)
        {
            writer.WriteStartElement(VideoPrefix, "uploader", null);
            if (!string.IsNullOrEmpty(node.Uploader.Info))
            {
                writer.WriteAttributeString("info", _urlValidator.Validate(node.Uploader.Info).ToString());
            }

            writer.WriteValue(node.Uploader.Name);
            writer.WriteEndElement();
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "live", BoolToSitemapValue(node.Live));

        foreach (var t in node.Tags)
        {
            writer.WriteElementString(VideoPrefix, "tag", null, t);
        }

        writer.WriteEndElement();
    }
}