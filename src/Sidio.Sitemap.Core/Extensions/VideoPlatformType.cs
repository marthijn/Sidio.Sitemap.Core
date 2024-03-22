namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The video platform type.
/// </summary>
[Flags]
public enum VideoPlatformType
{
    /// <summary>
    /// Computer browsers on desktops and laptops.
    /// </summary>
    Web = 1,

    /// <summary>
    /// Mobile browsers, such as those on cellular phones or tablets.
    /// </summary>
    Mobile = 1 << 1,

    /// <summary>
    /// TV browsers, such as those available through GoogleTV devices and game consoles.
    /// </summary>
    Tv = 1 << 2,
}