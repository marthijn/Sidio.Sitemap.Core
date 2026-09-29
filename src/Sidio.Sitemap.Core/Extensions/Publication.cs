namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The publication details of a news entry.
/// </summary>
public sealed class Publication : IEquatable<Publication>
{
    /// <summary>
    /// Creates a new instance of the <see cref="Publication"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="language">The language code (ISO 639).</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public Publication(string name, string language)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(language))
        {
            throw new ArgumentException($"{nameof(language)} cannot be null or empty.", nameof(language));
        }

        Name = name;
        Language = language;
    }

    /// <summary>
    /// Gets the name of the publication.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the language code (ISO 639).
    /// </summary>
    public string Language { get; }

    /// <inheritdoc />
    public bool Equals(Publication? other)
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
               string.Equals(Language, other.Language, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Publication);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Name);
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Language);
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="Publication"/> objects have the same value.
    /// </summary>
    public static bool operator ==(Publication? left, Publication? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="Publication"/> objects have different values.
    /// </summary>
    public static bool operator !=(Publication? left, Publication? right) => !Equals(left, right);
}