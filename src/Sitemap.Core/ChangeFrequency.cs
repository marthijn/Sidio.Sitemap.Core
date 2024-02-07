namespace Sitemap.Core;

/// <summary>
/// Frequency the page is likely to change.
/// </summary>
public enum ChangeFrequency
{
    Hourly,
    Daily,
    Weekly,
    Monthly,
    Yearly,

    /// <summary>
    /// The value "always" should be used to describe documents that change each time they are accessed.
    /// </summary>
    Always,

    /// <summary>
    /// The value "never" should be used to describe archived URLs.
    /// </summary>
    Never,
}