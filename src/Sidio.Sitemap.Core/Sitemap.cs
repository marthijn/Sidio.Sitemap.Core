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
        ArgumentNullException.ThrowIfNull(nodes);
        _nodes.AddRange(nodes);
        ValidateNumberOfNodes();
    }

    /// <summary>
    /// Gets the sitemap nodes.
    /// </summary>
    public IReadOnlyList<ISitemapNode> Nodes => _nodes;

    /// <summary>
    /// Adds the specified nodes to the sitemap.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public void Add(params ISitemapNode[] nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        Add(nodes.AsEnumerable());
    }

    /// <summary>
    /// Adds the specified nodes to the sitemap.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <exception cref="InvalidOperationException">Thrown when the number of nodes exceeds the maximum number of nodes.</exception>
    public void Add(IEnumerable<ISitemapNode> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        _nodes.AddRange(nodes);
        ValidateNumberOfNodes();
    }

    private void ValidateNumberOfNodes()
    {
        if (_nodes.Count > MaxNodes)
        {
            throw new InvalidOperationException($"The maximum number of nodes must not exceed {MaxNodes}.");
        }
    }
}