namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The relationship of a value.
/// </summary>
public enum Relationship
{
    /// <summary>
    /// Listed values will be allowed, unlisted values will be denied.
    /// </summary>
    Allow,

    /// <summary>
    /// Listed values will be denied, unlisted values will be allowed.
    /// </summary>
    Deny,
}