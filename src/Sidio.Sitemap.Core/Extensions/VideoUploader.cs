namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The video uploader.
/// </summary>
public sealed record VideoUploader
{
    private const int MaxNameLength = 255;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoUploader"/> class.
    /// </summary>
    /// <param name="name">The video uploader's name. The string value can be a maximum of 255 characters.</param>
    /// <param name="info">An optional value indicating the URL of a web page with additional information about this uploader</param>
    /// <exception cref="ArgumentException"></exception>
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
}