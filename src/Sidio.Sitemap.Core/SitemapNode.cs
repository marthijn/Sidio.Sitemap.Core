namespace Sidio.Sitemap.Core;

/// <summary>
/// Represents a node in a sitemap.
/// </summary>
public sealed record SitemapNode : ISitemapNode
{
    private readonly decimal? _priority;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="lastModified">The date of last modification of the page.</param>
    /// <param name="changeFrequency">How frequently the page is likely to change. This value provides general information to search engines and may not correlate exactly to how often they crawl the page.</param>
    /// <param name="priority">The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapNode(string url, DateTime? lastModified = null, ChangeFrequency? changeFrequency = null, decimal? priority = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        Url = url;
        ChangeFrequency = changeFrequency;
        Priority = priority;
        LastModified = lastModified;
    }

    /// <inheritdoc />
    public string Url { get; }

    /// <summary>
    /// Gets or sets the date of last modification of the page.
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Gets or sets the frequency the page is likely to change. This value provides general information to search engines and may not correlate exactly to how often they crawl the page.
    /// </summary>
    public ChangeFrequency? ChangeFrequency { get; set; }

    /// <summary>
    /// Gets or sets the priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the provided priority has an invalid value.</exception>
    public decimal? Priority
    {
        get => _priority;
        init
        {
            if (value is < 0 or > 1)
            {
                throw new ArgumentException($"{nameof(Priority)} must be a value between 0.0 and 1.0.", nameof(value));
            }

            _priority = value;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="lastModified">The date of last modification of the page.</param>
    /// <param name="changeFrequency">How frequently the page is likely to change. This value provides general information to search engines and may not correlate exactly to how often they crawl the page.</param>
    /// <param name="priority">The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.</param>
    /// <returns>A <see cref="SitemapNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapNode? Create(
        string? url,
        DateTime? lastModified = null,
        ChangeFrequency? changeFrequency = null,
        decimal? priority = null)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, lastModified, changeFrequency, priority);
    }
}