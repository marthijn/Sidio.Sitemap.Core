using System.Diagnostics.CodeAnalysis;

namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents the location of an image in a <see cref="SitemapImageNode"/>.
/// </summary>
public sealed class ImageLocation : IEquatable<ImageLocation>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageLocation"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    public ImageLocation(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        Url = url;
    }

    /// <summary>
    /// Gets the image URL.
    /// </summary>
    public string Url { get; }

    /// <inheritdoc />
    public bool Equals(ImageLocation? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Url, other.Url, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as ImageLocation);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Url);

    /// <summary>
    /// Determines whether two specified <see cref="ImageLocation"/> objects have the same value.
    /// </summary>
    public static bool operator ==(ImageLocation? left, ImageLocation? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="ImageLocation"/> objects have different values.
    /// </summary>
    public static bool operator !=(ImageLocation? left, ImageLocation? right) => !Equals(left, right);
}