using System.Diagnostics.CodeAnalysis;

namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The relationship between the video and the restriction.
/// </summary>
public sealed class VideoRestriction : IEquatable<VideoRestriction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoRestriction"/> class.
    /// </summary>
    /// <param name="restriction">A space delimited list of country codes in ISO 3166 format.</param>
    /// <param name="relationship">The relationship.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public VideoRestriction(string restriction, Relationship relationship)
    {
        if (string.IsNullOrWhiteSpace(restriction))
        {
            throw new ArgumentException($"{nameof(restriction)} cannot be null or empty.", nameof(restriction));
        }

        Restriction = restriction;
        Relationship = relationship;
    }

    /// <summary>
    /// Gets the restriction.
    /// </summary>
    public string Restriction { get; }

    /// <summary>
    /// Gets the relationship.
    /// </summary>
    public Relationship Relationship { get; }

    /// <inheritdoc />
    public bool Equals(VideoRestriction? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Restriction, other.Restriction, StringComparison.Ordinal) &&
               Relationship == other.Relationship;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as VideoRestriction);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Restriction);
            hashCode = (hashCode * 31) + (int)Relationship;
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="VideoRestriction"/> objects have the same value.
    /// </summary>
    public static bool operator ==(VideoRestriction? left, VideoRestriction? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="VideoRestriction"/> objects have different values.
    /// </summary>
    public static bool operator !=(VideoRestriction? left, VideoRestriction? right) => !Equals(left, right);
}