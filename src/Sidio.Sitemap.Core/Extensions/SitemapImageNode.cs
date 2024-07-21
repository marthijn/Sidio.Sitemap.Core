namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// Represents a node in a sitemap with images.
/// </summary>
public sealed record SitemapImageNode : ISitemapNode
{
    private const int MaxImages = 1000;

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocations">One or more image locations.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public SitemapImageNode(string url, IEnumerable<ImageLocation> imageLocations)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"{nameof(url)} cannot be null or empty.", nameof(url));
        }

        if (imageLocations == null)
        {
            throw new ArgumentNullException(nameof(imageLocations));
        }

        Url = url;
        Images = imageLocations.ToList();

        switch (Images.Count)
        {
            case 0:
                throw new ArgumentException($"A {nameof(SitemapImageNode)} must contain at least one image location.", nameof(imageLocations));
            case > MaxImages:
                throw new ArgumentException($"A {nameof(SitemapImageNode)} must contain at most {MaxImages} image locations.", nameof(imageLocations));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocation">An image locations.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public SitemapImageNode(string url, ImageLocation imageLocation)
        : this(url, new[] { imageLocation })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapImageNode"/> class.
    /// </summary>
    /// <param name="url">The URL of the page. This URL must begin with the protocol (such as http) and end with a trailing slash, if your web server requires it. This value must be less than 2,048 characters.</param>
    /// <param name="imageLocations">One or more image location urls.</param>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public SitemapImageNode(string url, params string[] imageLocations)
        : this(url, imageLocations.Select(x => new ImageLocation(x)))
    {
    }

    /// <inheritdoc />
    public string Url { get; }

    /// <summary>
    /// Gets the image locations.
    /// </summary>
    public IReadOnlyCollection<ImageLocation> Images { get; }
}