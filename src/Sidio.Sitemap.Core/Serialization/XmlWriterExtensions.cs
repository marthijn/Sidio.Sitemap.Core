using System.Xml;

namespace Sidio.Sitemap.Core.Serialization;

internal static class XmlWriterExtensions
{
    public static void WriteElementStringIfNotNull(this XmlWriter writer, string prefix, string localName, object? value)
    {
        if (value is not null)
        {
            writer.WriteElementString(prefix, localName, null, value.ToString());
        }
    }
}