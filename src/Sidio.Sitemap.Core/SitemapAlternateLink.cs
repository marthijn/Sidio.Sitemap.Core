namespace Sidio.Sitemap.Core;

/// <summary>
/// Represents an HTML link element for specifying localized versions of a URL (hreflang)
/// within a sitemap, conforming to the XHTML namespace.
/// </summary>
public sealed record SitemapAlternateLink
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapAlternateLink"/> class.
    /// </summary>
    /// <param name="hrefLang">The language and optional region code (e.g., "en", "en-us") for the localized version.</param>
    /// <param name="href">The fully qualified absolute URL of the localized version of the page.</param>
    /// <param name="rel">The relationship of the linked document. Defaults to "alternate".</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="hrefLang"/> or <paramref name="href"/> is null, empty, or consists only of white-space characters.</exception>
    public SitemapAlternateLink(string hrefLang, string href, string rel = "alternate")
    {
        if (!IsValidHreflang(hrefLang))
        {
            throw new ArgumentException(
                $"The value '{hrefLang}' is not a valid hreflang. Expected formats: 'x-default', 2-letter ISO code (e.g., 'en'), or 5-character language-region code (e.g., 'en-US').",
                nameof(hrefLang));
        }

        if (string.IsNullOrWhiteSpace(href))
        {
            throw new ArgumentException($"{nameof(href)} cannot be null or empty.", nameof(href));
        }

        HrefLang = hrefLang;
        Href = href;
        Rel = rel;
    }

    /// <summary>
    /// Gets or sets the relationship of the linked document.
    /// For sitemaps, this must always be set to "alternate".
    /// </summary>
    public string Rel { get; }

    /// <summary>
    /// Gets or sets the language and optional region code of the variant.
    /// Follows the ISO 639-1 format for languages and ISO 3166-1 Alpha-2 for regions (e.g., "en-us").
    /// Use "x-default" for unmatched languages.
    /// </summary>
    public string HrefLang { get; }

    /// <summary>
    /// Gets or sets the fully qualified absolute URL of the localized version.
    /// </summary>
    public string Href { get; }

    private static bool IsValidHreflang(string? hreflang)
    {
        if (string.IsNullOrWhiteSpace(hreflang))
        {
            return false;
        }

        if (hreflang!.Equals("x-default", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return hreflang.Length switch
        {
            2 => char.IsLetter(hreflang[0]) && char.IsLetter(hreflang[1]),
            5 => char.IsLetter(hreflang[0]) && char.IsLetter(hreflang[1]) && hreflang[2] == '-' &&
                 char.IsLetter(hreflang[3]) && char.IsLetter(hreflang[4]),
            _ => false
        };
    }
}