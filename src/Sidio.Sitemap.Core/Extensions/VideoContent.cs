namespace Sidio.Sitemap.Core.Extensions;

/// <summary>
/// The video content.
/// </summary>
public sealed class VideoContent : IEquatable<VideoContent>
{
    private const int DescriptionMaxLength = 2048;

    private const int DurationMinValue = 1;

    private const int DurationMaxValue = 28800;

    private const decimal RatingMinValue = 0.0m;

    private const decimal RatingMaxValue = 5.0m;

    private const int MaxTags = 32;

    private readonly int? _duration;

    private readonly decimal? _rating;

    private readonly IReadOnlyCollection<string> _tags = new List<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoContent"/> class.
    /// </summary>
    /// <param name="thumbnailUrl">A URL pointing to the video thumbnail image file.</param>
    /// <param name="title">The title of the video.</param>
    /// <param name="description">A description of the video. Maximum 2048 characters.</param>
    /// <param name="contentUrl">A URL pointing to the actual video media file.</param>
    /// <param name="playerUrl">A URL pointing to a player for a specific video. Usually this is the information in the src attribute of an embed-tag.</param>
    /// <exception cref="ArgumentException">Thrown when an argument has an invalid value.</exception>
    public VideoContent(string thumbnailUrl, string title, string description, string? contentUrl, string? playerUrl)
    {
        if (string.IsNullOrWhiteSpace(thumbnailUrl))
        {
            throw new ArgumentException($"{nameof(thumbnailUrl)} cannot be null or empty.", nameof(thumbnailUrl));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException($"{nameof(title)} cannot be null or empty.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException($"{nameof(description)} cannot be null or empty.", nameof(description));
        }

        if (description.Length > DescriptionMaxLength)
        {
            throw new ArgumentException($"{nameof(description)} cannot be longer than {DescriptionMaxLength} characters.", nameof(description));
        }

        if (string.IsNullOrWhiteSpace(contentUrl) && string.IsNullOrWhiteSpace(playerUrl))
        {
            throw new ArgumentException($"Either a {nameof(contentUrl)} or a {nameof(playerUrl)} is required.");
        }

        ThumbnailUrl = thumbnailUrl;
        Title = title;
        Description = description;
        ContentUrl = contentUrl;
        PlayerUrl = playerUrl;
    }

    /// <summary>
    /// Gets a URL pointing to the video thumbnail image file.
    /// </summary>
    public string ThumbnailUrl { get; }

    /// <summary>
    /// Gets the title of the video.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets a description of the video.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets a URL pointing to the actual video media file.
    /// </summary>
    public string? ContentUrl { get; }

    /// <summary>
    /// Gets a URL pointing to a player for a specific video.
    /// </summary>
    public string? PlayerUrl { get; }

    /// <summary>
    /// Gets the duration of the video, in seconds. Value must be from 1 to 28800.
    /// </summary>
    public int? Duration
    {
        get => _duration;
        init
        {
            if (value is < DurationMinValue or > DurationMaxValue)
            {
                throw new ArgumentException($"{nameof(Duration)} must be between {DurationMinValue} and {DurationMaxValue} seconds.");
            }

            _duration = value;
        }
    }

    /// <summary>
    /// Gets the date after which the video is no longer be available.
    /// </summary>
    public DateTimeOffset? ExpirationDate { get; init; }

    /// <summary>
    /// Gets the rating of the video. Supported values are float numbers in the range 0.0 (low) to 5.0 (high).
    /// </summary>
    public decimal? Rating
    {
        get => _rating;
        init
        {
            if (value is < RatingMinValue or > RatingMaxValue)
            {
                throw new ArgumentException($"{nameof(Rating)} must be between {RatingMinValue} and {RatingMaxValue}.");
            }

            _rating = value;
        }
    }

    /// <summary>
    /// Gets the number of times the video has been viewed.
    /// </summary>
    public int? ViewCount { get; init; }

    /// <summary>
    /// Gets the date the video was first published.
    /// </summary>
    public DateTimeOffset? PublicationDate { get; init; }

    /// <summary>
    /// Gets a value indicating whether the video is available with SafeSearch.
    /// </summary>
    public bool? FamilyFriendly { get; init; }

    /// <summary>
    /// Gets the video restriction.
    /// </summary>
    public VideoRestriction? Restriction { get; init; }

    /// <summary>
    /// Gets the video platform.
    /// </summary>
    public VideoPlatform? Platform { get; init; }

    /// <summary>
    /// Gets a value indicating whether a subscription is required to view the video.
    /// </summary>
    public bool? RequiresSubscription { get; init; }

    /// <summary>
    /// Gets the uploader of the video.
    /// </summary>
    public VideoUploader? Uploader { get; init; }

    /// <summary>
    /// Gets a value indicating whether the video is a live stream.
    /// </summary>
    public bool? Live { get; init; }

    /// <summary>
    /// Gets arbitrary string tags describing the video.
    /// </summary>
    public IReadOnlyCollection<string> Tags
    {
        get => _tags;
        init
        {
            if (value.Count > MaxTags)
            {
                throw new ArgumentException($"{nameof(Tags)} cannot contain more than {MaxTags} tags.");
            }

            _tags = value;
        }
    }

    /// <inheritdoc />
    public bool Equals(VideoContent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(ThumbnailUrl, other.ThumbnailUrl, StringComparison.Ordinal) &&
               string.Equals(Title, other.Title, StringComparison.Ordinal) &&
               string.Equals(Description, other.Description, StringComparison.Ordinal) &&
               string.Equals(ContentUrl, other.ContentUrl, StringComparison.Ordinal) &&
               string.Equals(PlayerUrl, other.PlayerUrl, StringComparison.Ordinal) &&
               Duration == other.Duration &&
               ExpirationDate == other.ExpirationDate &&
               Rating == other.Rating &&
               ViewCount == other.ViewCount &&
               PublicationDate == other.PublicationDate &&
               FamilyFriendly == other.FamilyFriendly &&
               Equals(Restriction, other.Restriction) &&
               Equals(Platform, other.Platform) &&
               RequiresSubscription == other.RequiresSubscription &&
               Equals(Uploader, other.Uploader) &&
               Live == other.Live &&
               EqualityHelpers.UnorderedEquals(Tags, other.Tags);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as VideoContent);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(ThumbnailUrl);
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Title);
            hashCode = (hashCode * 31) + StringComparer.Ordinal.GetHashCode(Description);
            hashCode = (hashCode * 31) + (ContentUrl == null ? 0 : StringComparer.Ordinal.GetHashCode(ContentUrl));
            hashCode = (hashCode * 31) + (PlayerUrl == null ? 0 : StringComparer.Ordinal.GetHashCode(PlayerUrl));
            hashCode = (hashCode * 31) + Duration.GetHashCode();
            hashCode = (hashCode * 31) + ExpirationDate.GetHashCode();
            hashCode = (hashCode * 31) + Rating.GetHashCode();
            hashCode = (hashCode * 31) + ViewCount.GetHashCode();
            hashCode = (hashCode * 31) + PublicationDate.GetHashCode();
            hashCode = (hashCode * 31) + FamilyFriendly.GetHashCode();
            hashCode = (hashCode * 31) + (Restriction?.GetHashCode() ?? 0);
            hashCode = (hashCode * 31) + (Platform?.GetHashCode() ?? 0);
            hashCode = (hashCode * 31) + RequiresSubscription.GetHashCode();
            hashCode = (hashCode * 31) + (Uploader?.GetHashCode() ?? 0);
            hashCode = (hashCode * 31) + Live.GetHashCode();
            hashCode = (hashCode * 31) + EqualityHelpers.GetUnorderedHashCode(Tags);
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="VideoContent"/> objects have the same value.
    /// </summary>
    public static bool operator ==(VideoContent? left, VideoContent? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="VideoContent"/> objects have different values.
    /// </summary>
    public static bool operator !=(VideoContent? left, VideoContent? right) => !Equals(left, right);
}