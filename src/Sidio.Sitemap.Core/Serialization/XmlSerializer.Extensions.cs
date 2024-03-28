using System.Globalization;
using System.Text;
using System.Xml;
using Sidio.Sitemap.Core.Extensions;

namespace Sidio.Sitemap.Core.Serialization;

public sealed partial class XmlSerializer
{
    private const string ExtensionsDateFormat = "yyyy-MM-ddTHH:mm:ssK";

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
        writer.WriteElementStringEscaped("loc", url.ToString());

        foreach(var imageLocationNode in node.Images)
        {
            var imageUrl = _urlValidator.Validate(imageLocationNode.Url);
            writer.WriteStartElement("image", "image", null);
            writer.WriteElementStringEscaped("image", "loc", imageUrl.ToString());
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    private void SerializeNode(XmlWriter writer, SitemapNewsNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementStringEscaped("loc", url.ToString());

        writer.WriteStartElement("news", "news", null);

        writer.WriteStartElement("news", "publication", null);
        writer.WriteElementStringEscaped("news", "name", node.Publication.Name);
        writer.WriteElementStringEscaped("news", "language", node.Publication.Language);
        writer.WriteEndElement();

        writer.WriteElementStringEscaped("news", "publication_date", node.PublicationDate.ToString(ExtensionsDateFormat));
        writer.WriteElementStringEscaped("news", "title", node.Title);

        writer.WriteEndElement();

        writer.WriteEndElement();
    }

    private void SerializeNode(XmlWriter writer, SitemapVideoNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementStringEscaped("loc", url.ToString());

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
        writer.WriteElementStringEscaped(VideoPrefix, "thumbnail_loc", _urlValidator.Validate(node.ThumbnailUrl).ToString());
        writer.WriteElementStringEscaped(VideoPrefix, "title", node.Title);
        writer.WriteElementStringEscaped(VideoPrefix, "description", node.Description);

        if (!string.IsNullOrEmpty(node.ContentUrl))
        {
            writer.WriteElementStringEscaped(VideoPrefix, "content_loc", _urlValidator.Validate(node.ContentUrl).ToString());
        }

        if (!string.IsNullOrEmpty(node.PlayerUrl))
        {
            writer.WriteElementStringEscaped(VideoPrefix, "player_loc", _urlValidator.Validate(node.PlayerUrl).ToString());
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "duration", node.Duration);
        writer.WriteElementStringIfNotNull(VideoPrefix, "expiration_date", node.ExpirationDate?.ToString(ExtensionsDateFormat));
        writer.WriteElementStringIfNotNull(VideoPrefix, "rating", node.Rating?.ToString("0.0", new CultureInfo("en-US")));
        writer.WriteElementStringIfNotNull(VideoPrefix, "view_count", node.ViewCount);

        if (node.Restriction != null)
        {
            writer.WriteStartElement(VideoPrefix, "restriction", null);
            writer.WriteAttributeString("relationship", RelationshipToSitemapValue(node.Restriction.Relationship));
            writer.WriteValue(node.Restriction.Restriction);
            writer.WriteEndElement();
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "publication_date", node.PublicationDate?.ToString(ExtensionsDateFormat));
        writer.WriteElementStringIfNotNull(VideoPrefix, "family_friendly", BoolToSitemapValue(node.FamilyFriendly));

        if (node.Platform != null)
        {
            writer.WriteStartElement(VideoPrefix, "platform", null);
            writer.WriteAttributeString("relationship", RelationshipToSitemapValue(node.Platform.Relationship));
            writer.WriteValue(PlatformToSitemapValue(node.Platform.Platform));
            writer.WriteEndElement();
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "requires_subscription", BoolToSitemapValue(node.RequiresSubscription));

        if (node.Uploader != null && !string.IsNullOrWhiteSpace(node.Uploader.Name))
        {
            var infoAttribute = string.Empty;
            if (!string.IsNullOrEmpty(node.Uploader.Info))
            {
                infoAttribute = $" info=\"{XmlWriterExtensions.EscapeValue(_urlValidator.Validate(node.Uploader.Info).ToString())}\"";
            }

            writer.WriteRaw($"<{VideoPrefix}:uploader{infoAttribute}>{XmlWriterExtensions.EscapeValue(node.Uploader.Name)}</{VideoPrefix}:uploader>");
        }

        writer.WriteElementStringIfNotNull(VideoPrefix, "live", BoolToSitemapValue(node.Live));

        foreach (var t in node.Tags)
        {
            writer.WriteElementStringEscaped(VideoPrefix, "tag", t);
        }

        writer.WriteEndElement();
    }
}