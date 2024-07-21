using System.Xml.Linq;

namespace Sidio.Sitemap.Core.Serialization;

/// <summary>
/// The exception that is thrown when an error occurs during sitemap XML deserialization.
/// </summary>
[Serializable]
public sealed class SitemapXmlDeserializationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapXmlDeserializationException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="element">The element.</param>
    public SitemapXmlDeserializationException(string message, XElement element) : base(message)
    {
        Element = element;
    }

    /// <summary>
    /// Gets the element.
    /// </summary>
    public XElement Element { get; }
}