using System.Diagnostics.CodeAnalysis;

namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The relationship between the video and the platform.
/// </summary>
public sealed class VideoPlatform : IEquatable<VideoPlatform>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoPlatform"/> class.
    /// </summary>
    /// <param name="platform">The platform type.</param>
    /// <param name="relationship">The relationship.</param>
    public VideoPlatform(VideoPlatformType platform, Relationship relationship)
    {
        Platform = platform;
        Relationship = relationship;
    }

    /// <summary>
    /// Gets the platform type.
    /// </summary>
    public VideoPlatformType Platform { get; }

    /// <summary>
    /// Gets the relationship.
    /// </summary>
    public Relationship Relationship { get; }

    /// <inheritdoc />
    public bool Equals(VideoPlatform? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Platform == other.Platform && Relationship == other.Relationship;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as VideoPlatform);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + (int)Platform;
            hashCode = (hashCode * 31) + (int)Relationship;
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two <see cref="VideoPlatform"/> instances are equal.
    /// </summary>
    public static bool operator ==(VideoPlatform? left, VideoPlatform? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two <see cref="VideoPlatform"/> instances are not equal.
    /// </summary>
    public static bool operator !=(VideoPlatform? left, VideoPlatform? right) => !Equals(left, right);
}