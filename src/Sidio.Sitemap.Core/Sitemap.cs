namespace Sidio.Sitemap.Core;

/// <summary>
/// This class represents a sitemap.
/// </summary>
public sealed class Sitemap
{
    internal const int MaxNodes = 50000;

    private readonly List<ISitemapNode> _nodes = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="Sitemap"/> class.
    /// </summary>
    public Sitemap()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sitemap"/> class.
    /// </summary>
    /// <param name="nodes">The sitemap nodes.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public Sitemap(IEnumerable<ISitemapNode> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        _ = Add(nodes);
    }

    /// <summary>
    /// Gets the sitemap nodes.
    /// </summary>
    public IReadOnlyList<ISitemapNode> Nodes => _nodes;

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

        var nonNullableNodes = nodes.Where(x => x != null).Cast<ISitemapNode>().ToArray();

        if (nonNullableNodes.Length > 0)
        {
            _nodes.AddRange(nonNullableNodes);
        }

        ValidateNumberOfNodes();

        return nonNullableNodes.Length;
    }

    private void ValidateNumberOfNodes()
    {
        if (_nodes.Count > MaxNodes)
        {
            throw new InvalidOperationException($"The maximum number of nodes must not exceed {MaxNodes}.");
        }
    }
}