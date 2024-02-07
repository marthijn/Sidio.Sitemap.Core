using System.Globalization;
using System.Text;
using System.Xml;

namespace Sitemap.Core.Serialization;

/// <summary>
/// The XML sitemap serializer.
/// </summary>
public sealed class XmlSerializer : ISitemapSerializer
{
    internal const int MaxSitemapSizeInMegaBytes = 50;

    private const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    private const string SitemapDateFormat = "yyyy-MM-dd";

    /// <inheritdoc />
    public string Serialize(Sitemap sitemap)
    {
        using var stringWriter = new Utf8StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, Settings);
        SerializeSitemap(xmlWriter, sitemap);
        xmlWriter.Close();

        var result = stringWriter.ToString();
        var size = Encoding.UTF8.GetByteCount(result);

        if (size > MaxSitemapSizeInMegaBytes * 1024 * 1024)
        {
            throw new InvalidOperationException($"The sitemap is too large. It must be less than {MaxSitemapSizeInMegaBytes} MB but is {size / 1024 / 1024} MB.");
        }

        return result;
    }

    /// <inheritdoc />
    public Task<string> SerializeAsync(Sitemap sitemap, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => Serialize(sitemap), cancellationToken);
    }

    /// <inheritdoc />
    public void Serialize(Sitemap sitemap, Stream output)
    {
        using var xmlWriter = XmlWriter.Create(output, Settings);
        SerializeSitemap(xmlWriter, sitemap);
        xmlWriter.Close();
    }

    /// <inheritdoc />
    public string Serialize(SitemapIndex sitemapIndex)
    {
        using var stringWriter = new Utf8StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, Settings);
        SerializeSitemapIndex(xmlWriter, sitemapIndex);
        xmlWriter.Close();

        var result = stringWriter.ToString();
        return result;
    }

    private static XmlWriterSettings Settings =>
        new ()
            {
                Encoding = new UTF8Encoding(true), Indent = false, OmitXmlDeclaration = false, NewLineHandling = NewLineHandling.None,
            };

    private static void SerializeSitemap(XmlWriter writer, Sitemap sitemap)
    {
        writer.WriteStartDocument(false);
        writer.WriteStartElement(null, "urlset", SitemapNamespace);

        foreach (var n in sitemap.Nodes)
        {
            SerializeNode(writer, n);
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private static void SerializeNode(XmlWriter writer, SitemapNode node)
    {
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", node.Url);
        if (node.LastModified.HasValue)
        {
            writer.WriteElementString("lastmod", node.LastModified.Value.ToString(SitemapDateFormat));
        }

        if (node.ChangeFrequency.HasValue)
        {
            writer.WriteElementString("changefreq", node.ChangeFrequency.Value.ToString().ToLower());
        }

        if (node.Priority.HasValue)
        {
            writer.WriteElementString("priority", node.Priority.Value.ToString("F1", new CultureInfo("en-US")));
        }

        writer.WriteEndElement();
    }

    private static void SerializeSitemapIndex(XmlWriter writer, SitemapIndex sitemapIndex)
    {
        writer.WriteStartDocument(false);
        writer.WriteStartElement(null, "sitemapindex", SitemapNamespace);

        foreach (var n in sitemapIndex.Nodes)
        {
            SerializeSitemapIndexNode(writer, n);
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private static void SerializeSitemapIndexNode(XmlWriter writer, SitemapIndexNode node)
    {
        writer.WriteStartElement("sitemap");
        writer.WriteElementString("loc", node.Url);
        if (node.LastModified.HasValue)
        {
            writer.WriteElementString("lastmod", node.LastModified.Value.ToString(SitemapDateFormat));
        }

        writer.WriteEndElement();
    }

    private static string EscapeUrl(string value)
    {
        return string.IsNullOrEmpty(value) ? value : value.Replace("'", "\\&apos;").Replace("\"", "\\&quot;");
    }
}