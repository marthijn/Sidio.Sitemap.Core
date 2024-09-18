using System.Text.RegularExpressions;

namespace Sidio.Sitemap.Core.Serialization;

internal static partial class StringExtensions
{
    public static string? GetHref(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

#if NET7_0_OR_GREATER
        var regex = HrefRegex();
#else
        var regex = new Regex(@"href=""([^""]*)""");
#endif
        return regex.IsMatch(value) ? regex.Match(value).Groups[1].Value : null;
    }

#if NET7_0_OR_GREATER
    [GeneratedRegex(@"href=""([^""]*)""")]
    private static partial Regex HrefRegex();
#endif
}