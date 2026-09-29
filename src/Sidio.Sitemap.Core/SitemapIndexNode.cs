namespace Sidio.Sitemap.Core;

/// <summary>
/// This class represents a sitemap index node.
/// </summary>
public sealed class SitemapIndexNode : IEquatable<SitemapIndexNode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndexNode"/> class.
    /// </summary>
    /// <param name="url">The location of the sitemap.</param>
    /// <param name="lastModified">Identifies the time that the corresponding Sitemap file was modified.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapIndexNode(string url, DateTime? lastModified = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        Url = url;
        LastModified = lastModified;
    }

    /// <summary>
    /// Gets the Url which identifies the location of the Sitemap.
    /// This location can be a Sitemap, an Atom file, RSS file or a simple text file.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Gets the time that the corresponding Sitemap file was modified. It does not correspond to the time that any of the pages listed in that Sitemap were changed.
    /// </summary>
    public DateTime? LastModified { get; init; }

    /// <inheritdoc />
    public bool Equals(SitemapIndexNode? other)
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
               LastModified == other.LastModified;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as SitemapIndexNode);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Url);
            hashCode = (hashCode * 31) + LastModified.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="SitemapIndexNode"/> objects have the same value.
    /// </summary>
    public static bool operator ==(SitemapIndexNode? left, SitemapIndexNode? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SitemapIndexNode"/> objects have different values.
    /// </summary>
    public static bool operator !=(SitemapIndexNode? left, SitemapIndexNode? right) => !Equals(left, right);

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapIndexNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The location of the sitemap.</param>
    /// <param name="lastModified">Identifies the time that the corresponding Sitemap file was modified.</param>
    /// <returns>A <see cref="SitemapIndexNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapIndexNode? Create(string? url, DateTime? lastModified = null)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, lastModified);
    }
}