using System.Globalization;
using System.Text;
using System.Xml;
using Sitemap.Core.Validation;

namespace Sitemap.Core.Serialization;

/// <summary>
/// The XML sitemap serializer.
/// </summary>
public sealed class XmlSerializer : ISitemapSerializer
{
    private readonly UrlValidator _urlValidator;

    internal const int MaxSitemapSizeInMegaBytes = 50;

    private const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    private const string SitemapDateFormat = "yyyy-MM-dd";

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlSerializer"/> class.
    /// </summary>
    /// <param name="baseUrlProvider">The base URL provider.</param>
    public XmlSerializer(IBaseUrlProvider? baseUrlProvider = null)
    {
        _urlValidator = new UrlValidator(baseUrlProvider);
    }

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

    private void SerializeSitemap(XmlWriter writer, Sitemap sitemap)
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

    private void SerializeNode(XmlWriter writer, SitemapNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementString("loc", url.ToString());
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

    private void SerializeSitemapIndex(XmlWriter writer, SitemapIndex sitemapIndex)
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

    private void SerializeSitemapIndexNode(XmlWriter writer, SitemapIndexNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("sitemap");
        writer.WriteElementString("loc", url.ToString());
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