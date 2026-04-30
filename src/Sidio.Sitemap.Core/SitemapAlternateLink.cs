namespace Sidio.Sitemap.Core
{
    /// <summary>
    /// Represents an HTML link element for specifying localized versions of a URL (hreflang) 
    /// within a sitemap, conforming to the XHTML namespace.
    /// </summary>
    public class SitemapAlternateLink
    {
        /// <summary>
        /// Gets or sets the relationship of the linked document. 
        /// For sitemaps, this must always be set to "alternate".
        /// </summary>
        public string Rel { get; set; } = "alternate";

        /// <summary>
        /// Gets or sets the language and optional region code of the variant. 
        /// Follows the ISO 639-1 format for languages and ISO 3166-1 Alpha-2 for regions (e.g., "en-us"). 
        /// Use "x-default" for unmatched languages.
        /// </summary>
        public string? HrefLang { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified absolute URL of the localized version.
        /// </summary>
        public string? Href { get; set; }
    }
}
