﻿using System.Globalization;
using System.Text;
using System.Xml;
using Sidio.Sitemap.Core.Extensions;
using Sidio.Sitemap.Core.Validation;

namespace Sidio.Sitemap.Core.Serialization;

/// <summary>
/// The XML sitemap serializer.
/// </summary>
public sealed partial class XmlSerializer : ISitemapSerializer
{
    private readonly UrlValidator _urlValidator;

    internal const int MaxSitemapSizeInMegaBytes = 50;

    private const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    private const string SitemapNamespaceImage = "http://www.google.com/schemas/sitemap-image/1.1";

    private const string SitemapNamespaceNews = "http://www.google.com/schemas/sitemap-news/0.9";

    private const string SitemapNamespaceVideo = "http://www.google.com/schemas/sitemap-video/1.1";

    private const string SitemapDateFormat = "yyyy-MM-dd";

    private static readonly CultureInfo SitemapCulture = new ("en-US");

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

    /// <inheritdoc />
    public Task<string> SerializeAsync(SitemapIndex sitemapIndex, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => Serialize(sitemapIndex), cancellationToken);
    }

    private static XmlWriterSettings Settings =>
        new ()
            {
                Encoding = new UTF8Encoding(true), Indent = false, OmitXmlDeclaration = false, NewLineHandling = NewLineHandling.None,
            };

    private static void WriteNamespaces(XmlWriter writer, Sitemap sitemap)
    {
        if (sitemap.HasImageNodes())
        {
            writer.WriteAttributeString("xmlns", "image", null, SitemapNamespaceImage);
        }

        if (sitemap.HasNewsNodes())
        {
            writer.WriteAttributeString("xmlns", "news", null, SitemapNamespaceNews);
        }

        if (sitemap.HasVideoNodes())
        {
            writer.WriteAttributeString("xmlns", "video", null, SitemapNamespaceVideo);
        }
    }

    private void SerializeSitemap(XmlWriter writer, Sitemap sitemap)
    {
        writer.WriteStartDocument(false);

        if (!string.IsNullOrWhiteSpace(sitemap.Stylesheet))
        {
            writer.WriteProcessingInstruction("xml-stylesheet", $"type=\"text/xsl\" href=\"{sitemap.Stylesheet}\"");
        }

        writer.WriteStartElement(null, "urlset", SitemapNamespace);
        WriteNamespaces(writer, sitemap);

        foreach (var n in sitemap.Nodes)
        {
            switch (n)
            {
                case SitemapNode regularNode:
                    SerializeNode(writer, regularNode);
                    break;
                case SitemapImageNode imageNode:
                    SerializeNode(writer, imageNode);
                    break;
                case SitemapNewsNode newsNode:
                    SerializeNode(writer, newsNode);
                    break;
                case SitemapVideoNode videoNode:
                    SerializeNode(writer, videoNode);
                    break;
                default:
                    throw new NotSupportedException($"The node type {n.GetType()} is not supported.");
            }
        }

        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private void SerializeNode(XmlWriter writer, SitemapNode node)
    {
        var url = _urlValidator.Validate(node.Url);
        writer.WriteStartElement("url");
        writer.WriteElementStringEscaped("loc", url.ToString());
        if (node.LastModified.HasValue)
        {
            writer.WriteElementStringEscaped("lastmod", node.LastModified.Value.ToString(SitemapDateFormat));
        }

        if (node.ChangeFrequency.HasValue)
        {
            writer.WriteElementStringEscaped("changefreq", node.ChangeFrequency.Value.ToString().ToLower());
        }

        if (node.Priority.HasValue)
        {
            writer.WriteElementStringEscaped("priority", node.Priority.Value.ToString("F1", SitemapCulture));
        }

        writer.WriteEndElement();
    }

    private void SerializeSitemapIndex(XmlWriter writer, SitemapIndex sitemapIndex)
    {
        writer.WriteStartDocument(false);

        if (!string.IsNullOrWhiteSpace(sitemapIndex.Stylesheet))
        {
            writer.WriteProcessingInstruction("xml-stylesheet", $"type=\"text/xsl\" href=\"{sitemapIndex.Stylesheet}\"");
        }

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
        writer.WriteElementStringEscaped("loc", url.ToString());
        if (node.LastModified.HasValue)
        {
            writer.WriteElementString("lastmod", node.LastModified.Value.ToString(SitemapDateFormat));
        }

        writer.WriteEndElement();
    }
}