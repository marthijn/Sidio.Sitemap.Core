using System.Text;

namespace Sidio.Sitemap.Core.Serialization;

internal sealed class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}