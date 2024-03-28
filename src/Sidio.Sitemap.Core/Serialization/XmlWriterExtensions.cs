using System.Xml;

namespace Sidio.Sitemap.Core.Serialization;

internal static class XmlWriterExtensions
{
    public static void WriteElementStringIfNotNull(this XmlWriter writer, string prefix, string localName, object? value)
    {
        if (value is not null)
        {
            writer.WriteElementStringEscaped(prefix, localName, value.ToString());
        }
    }

    public static void WriteElementStringEscaped(this XmlWriter writer, string localName, string? value)
    {
        ArgumentNullException.ThrowIfNull(localName);
        var escapedValue = EscapeValue(value);
        writer.WriteRaw($"<{localName}>{escapedValue}</{localName}>");
    }

    public static void WriteElementStringEscaped(this XmlWriter writer, string prefix, string localName, string? value)
    {
        ArgumentNullException.ThrowIfNull(prefix);
        ArgumentNullException.ThrowIfNull(localName);
        var escapedValue = EscapeValue(value);
        writer.WriteRaw($"<{prefix}:{localName}>{escapedValue}</{prefix}:{localName}>");
    }

    internal static string? EscapeValue(string? value)
    {
        return string.IsNullOrEmpty(value) ? value : value
                   .Replace("&", "&amp;")
                   .Replace("<", "&lt;")
                   .Replace(">", "&gt;")
                   .Replace("'", "&apos;")
                   .Replace("\"", "&quot;");
    }
}