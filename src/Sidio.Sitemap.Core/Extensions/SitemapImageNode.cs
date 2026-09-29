namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents a node in a sitemap with images.
/// </summary>
public sealed class SitemapImageNode : ISitemapNode, IEquatable<SitemapImageNode>
{
    private const int MaxImages = 1000;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocations">One or more image locations.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapImageNode(string url, IEnumerable<ImageLocation> imageLocations)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        if (imageLocations == null)
        {
            throw new ArgumentNullException(nameof(imageLocations));
        }

        Url = url;
        Images = imageLocations.ToList();

        switch (Images.Count)
        {
            case 0:
                throw new ArgumentException($"A {nameof(SitemapImageNode)} must contain at least one image location.", nameof(imageLocations));
            case > MaxImages:
                throw new ArgumentException($"A {nameof(SitemapImageNode)} must contain at most {MaxImages} image locations.", nameof(imageLocations));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocation">An image locations.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapImageNode(string url, ImageLocation imageLocation)
        : this(url, new[] { imageLocation })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// When the URL is null or empty, null is returned.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocations">One or more image location urls.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value (in case of a string that is null or empty).</exception>
    public SitemapImageNode(string url, params string[] imageLocations)
        : this(url, imageLocations.Select(x => new ImageLocation(x)))
    {
    }

    /// <inheritdoc />
    public string Url { get; }

    /// <inheritdoc />
    public DateTime? LastModified { get; init; }

    /// <summary>
    /// Gets the image locations.
    /// </summary>
    public IReadOnlyCollection<ImageLocation> Images { get; }

    /// <inheritdoc />
    public bool Equals(SitemapImageNode? other)
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
               EqualityHelpers.UnorderedEquals(Images, other.Images) &&
               LastModified == other.LastModified;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as SitemapImageNode);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Url);
            hashCode = (hashCode * 31) + EqualityHelpers.GetUnorderedHashCode(Images);
            hashCode = (hashCode * 31) + LastModified.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="SitemapImageNode"/> objects have the same value.
    /// </summary>
    public static bool operator ==(SitemapImageNode? left, SitemapImageNode? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SitemapImageNode"/> objects have different values.
    /// </summary>
    public static bool operator !=(SitemapImageNode? left, SitemapImageNode? right) => !Equals(left, right);

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocations">One or more image locations.</param>
    /// <returns>A <see cref="SitemapImageNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapImageNode? Create(string? url, IEnumerable<ImageLocation> imageLocations)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, imageLocations);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocation">An image locations.</param>
    /// <returns>A <see cref="SitemapImageNode"/>.</returns>
#if NET6_0_OR_GREATER
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
#endif
    public static SitemapImageNode? Create(string? url, ImageLocation imageLocation)
    {
        if (url == null)
        {
            return null;
        }

        return new(url, imageLocation);
    }
}