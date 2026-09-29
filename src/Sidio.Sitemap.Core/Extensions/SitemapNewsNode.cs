namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents a node in a sitemap with news.
/// </summary>
public sealed class SitemapNewsNode : ISitemapNode, IEquatable<SitemapNewsNode>
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

    /// <inheritdoc />
    public DateTime? LastModified { get; init; }

    /// <inheritdoc />
    public bool Equals(SitemapNewsNode? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Url, other.Url, StringComparison.Ordinal) &&
               string.Equals(Title, other.Title, StringComparison.Ordinal) &&
               Equals(Publication, other.Publication) &&
               PublicationDate.Equals(other.PublicationDate) &&
               LastModified == other.LastModified;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as SitemapNewsNode);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Url);
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Title);
            hashCode = (hashCode * 31) + Publication.GetHashCode();
            hashCode = (hashCode * 31) + PublicationDate.GetHashCode();
            hashCode = (hashCode * 31) + LastModified.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="SitemapNewsNode"/> objects have the same value.
    /// </summary>
    public static bool operator ==(SitemapNewsNode? left, SitemapNewsNode? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SitemapNewsNode"/> objects have different values.
    /// </summary>
    public static bool operator !=(SitemapNewsNode? left, SitemapNewsNode? right) => !Equals(left, right);

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