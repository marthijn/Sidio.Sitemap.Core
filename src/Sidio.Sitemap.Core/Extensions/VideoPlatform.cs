namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The relationship between the video and the platform.
/// </summary>
public sealed class VideoPlatform
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
}