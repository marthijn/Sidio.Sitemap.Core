using System.Diagnostics.CodeAnalysis;

namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The video uploader.
/// </summary>
public sealed class VideoUploader : IEquatable<VideoUploader>
{
    private const int MaxNameLength = 255;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoUploader"/> class.
    /// </summary>
    /// <param name="name">The video uploader's name. The string value can be a maximum of 255 characters.</param>
    /// <param name="info">An optional value indicating the URL of a web page with additional information about this uploader</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public VideoUploader(string name, string? info = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null or empty.", nameof(name));
        }

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"{nameof(name)} cannot be longer than {MaxNameLength} characters.", nameof(name));
        }

        Name = name;
        Info = info;
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a value indicating the URL of a web page with additional information about this uploader.
    /// </summary>
    public string? Info { get; }

    /// <inheritdoc />
    public bool Equals(VideoUploader? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Name, other.Name, StringComparison.Ordinal) &&
               string.Equals(Info, other.Info, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as VideoUploader);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Name);
            hashCode = (hashCode * 31) + (Info == null ? 0 : StringComparer.Ordinal.GetHashCode(Info));
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="VideoUploader"/> objects have the same value.
    /// </summary>
    public static bool operator ==(VideoUploader? left, VideoUploader? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="VideoUploader"/> objects have different values.
    /// </summary>
    public static bool operator !=(VideoUploader? left, VideoUploader? right) => !Equals(left, right);
}