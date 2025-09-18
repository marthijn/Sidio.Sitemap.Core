namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The relationship between the video and the restriction.
/// </summary>
public sealed class VideoRestriction
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
}