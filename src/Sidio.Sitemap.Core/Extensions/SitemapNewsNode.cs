namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents a node in a sitemap with news.
/// </summary>
public sealed record SitemapNewsNode : ISitemapNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapNewsNode"/> class.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="publication">The publication details</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapNewsNode(string url, string title, Publication publication, DateTimeOffset publicationDate)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException($"{nameof(title)} cannot be null or empty.", nameof(title));
        }

        Url = url;
        Title = title;
        Publication = publication ?? throw new ArgumentNullException(nameof(publication));
        PublicationDate = publicationDate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapNewsNode"/> class.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="name">The name of the news publication.</param>
    /// <param name="language">The language.</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapNewsNode(string url, string title, string name, string language, DateTimeOffset publicationDate)
        : this(url, title, new Publication(name, language), publicationDate)
    {
    }

    /// <inheritdoc />
    public string Url { get; }

    /// <summary>
    /// Gets the publication title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the publication details.
    /// </summary>
    public Publication Publication { get; }

    /// <summary>
    /// Gets the publication date.
    /// </summary>
    public DateTimeOffset PublicationDate { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapNewsNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="publication">The publication details</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <returns>A <see cref="SitemapNewsNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapNewsNode? Create(
        string? url,
        string title,
        Publication publication,
        DateTimeOffset publicationDate)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, title, publication, publicationDate);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapNewsNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The url.</param>
    /// <param name="title">The title.</param>
    /// <param name="name">The name of the news publication.</param>
    /// <param name="language">The language.</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <returns>A <see cref="SitemapNewsNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapNewsNode? Create(
        string? url,
        string title,
        string name,
        string language,
        DateTimeOffset publicationDate)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, title, name, language, publicationDate);
    }
}