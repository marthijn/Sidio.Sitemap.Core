namespace Sidio.Sitemap.Core;

/// <summary>
/// This class represents a sitemap.
/// </summary>
public sealed class Sitemap : IEquatable<Sitemap>
{
    internal const int MaxNodes = 50000;

    private readonly List<ISitemapNode> _nodes = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="Sitemap"/> class.
    /// </summary>
    /// <param name="stylesheet">The text/xsl stylesheet.</param>
    public Sitemap(string? stylesheet = null)
    {
        if (!string.IsNullOrWhiteSpace(stylesheet))
        {
            Stylesheet = stylesheet;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sitemap"/> class.
    /// </summary>
    /// <param name="nodes">The sitemap nodes.</param>
    /// <param name="stylesheet">The text/xsl stylesheet.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public Sitemap(IEnumerable<ISitemapNode> nodes, string? stylesheet = null)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        _ = Add(nodes);

        if (!string.IsNullOrWhiteSpace(stylesheet))
        {
            Stylesheet = stylesheet;
        }
    }

    /// <summary>
    /// Gets the sitemap nodes.
    /// </summary>
    public IReadOnlyList<ISitemapNode> Nodes => _nodes;

    /// <summary>
    /// Gets the stylesheet.
    /// </summary>
    public string? Stylesheet { get; }

    /// <summary>
    /// Adds the specified nodes to the sitemap.
    /// </summary>
    /// <remarks>Nodes that are null will be ignored.</remarks>
    /// <param name="nodes">The nodes.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public int Add(params ISitemapNode?[] nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        return Add(nodes.AsEnumerable());
    }

    /// <summary>
    /// Adds the specified nodes to the sitemap.
    /// </summary>
    /// <remarks>Nodes that are null will be ignored.</remarks>
    /// <param name="nodes">The nodes.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public int Add(IEnumerable<ISitemapNode?> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        var validNodes = nodes.Where(x => x != null).Cast<ISitemapNode>().ToList();
        if (validNodes.Count > 0)
        {
            _nodes.AddRange(validNodes);
        }

        ValidateNumberOfNodes();

        return validNodes.Count;
    }

    private void ValidateNumberOfNodes()
    {
        if (_nodes.Count > MaxNodes)
        {
            throw new InvalidOperationException($"The maximum number of nodes must not exceed {MaxNodes}.");
        }
    }

    /// <inheritdoc />
    public bool Equals(Sitemap? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Stylesheet, other.Stylesheet, StringComparison.Ordinal) &&
               EqualityHelpers.UnorderedEquals(Nodes, other.Nodes);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Sitemap);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            hashCode = (hashCode * 31) + (Stylesheet == null ? 0 : StringComparer.Ordinal.GetHashCode(Stylesheet));
            hashCode = (hashCode * 31) + EqualityHelpers.GetUnorderedHashCode(Nodes);
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether two specified <see cref="Sitemap"/> objects have the same value.
    /// </summary>
    public static bool operator ==(Sitemap? left, Sitemap? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="Sitemap"/> objects have different values.
    /// </summary>
    public static bool operator !=(Sitemap? left, Sitemap? right) => !Equals(left, right);
}