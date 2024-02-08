namespace Sitemap.Core;

/// <summary>
/// Represents a node in a sitemap.
/// </summary>
public sealed record SitemapNode
{
    /// <summary>
    /// The URL value must be less than 2,048 characters.
    /// </summary>
    internal const int UrlMaxLength = 2047;

    private decimal? _priority;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="lastModified">The date of last modification of the page.</param>
    /// <param name="changeFrequency">How frequently the page is likely to change. This value provides general information to search engines and may not correlate exactly to how often they crawl the page.</param>
    /// <param name="priority">The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public SitemapNode(string? url, DateTime? lastModified = null, ChangeFrequency? changeFrequency = null, decimal? priority = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (url.Length > UrlMaxLength)
        {
            throw new ArgumentException($"{nameof(url)} exceeds the maximum length of {UrlMaxLength} characters.", nameof(url));
        }

        Url = url;
        ChangeFrequency = changeFrequency;
        Priority = priority;
        LastModified = lastModified;
    }

    /// <summary>
    /// Gets the URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.
    /// </summary>
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
        set
        {
            if (value is < 0 or > 1)
            {
                throw new ArgumentException($"{nameof(Priority)} must be a value between 0.0 and 1.0.", nameof(value));
            }

            _priority = value;
        }
    }
}