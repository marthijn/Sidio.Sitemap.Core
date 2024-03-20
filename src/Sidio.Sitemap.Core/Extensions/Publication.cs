namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The publication details of a news entry.
/// </summary>
public sealed record Publication
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
}