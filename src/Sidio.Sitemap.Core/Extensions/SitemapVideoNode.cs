using System.Diagnostics.CodeAnalysis;

namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents a node in a sitemap with videos.
/// </summary>
public sealed class SitemapVideoNode : ISitemapNode, IEquatable<SitemapVideoNode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapVideoNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="videos">One or more videos.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapVideoNode(string url, IEnumerable<VideoContent> videos)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        if (videos == null)
        {
            throw new ArgumentNullException(nameof(videos));
        }

        Url = url;
        Videos = videos.ToList();

        if (Videos.Count == 0)
        {
            throw new ArgumentException($"A {nameof(SitemapVideoNode)} must contain at least one video.", nameof(videos));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapVideoNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="videoContent">A video.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapVideoNode(string url, VideoContent videoContent)
        : this(url, new[] { videoContent })
    {
        if (videoContent == null)
        {
            throw new ArgumentNullException(nameof(videoContent));
        }
    }

    /// <inheritdoc />
    public string Url { get; }

    /// <summary>
    /// Gets the videos.
    /// </summary>
    public IReadOnlyCollection<VideoContent> Videos { get; }

    /// <inheritdoc />
    public DateTime? LastModified { get; init; }

    /// <inheritdoc />
    public bool Equals(SitemapVideoNode? other)
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
               EqualityHelpers.UnorderedEquals(Videos, other.Videos) &&
               LastModified == other.LastModified;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as SitemapVideoNode);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Url);
            hashCode = (hashCode * 31) + EqualityHelpers.GetUnorderedHashCode(Videos);
            hashCode = (hashCode * 31) + LastModified.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="SitemapVideoNode"/> objects have the same value.
    /// </summary>
    public static bool operator ==(SitemapVideoNode? left, SitemapVideoNode? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SitemapVideoNode"/> objects have different values.
    /// </summary>
    public static bool operator !=(SitemapVideoNode? left, SitemapVideoNode? right) => !Equals(left, right);

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapVideoNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="videos">One or more videos.</param>
    /// <returns>A <see cref="SitemapVideoNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapVideoNode? Create(string? url, IEnumerable<VideoContent> videos)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, videos);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapVideoNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="videoContent">A video.</param>
    /// <returns>A <see cref="SitemapVideoNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapVideoNode? Create(string? url, VideoContent videoContent)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, videoContent);
    }
}