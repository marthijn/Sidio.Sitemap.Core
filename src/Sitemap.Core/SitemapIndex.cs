namespace Sitemap.Core;

/// <summary>
/// The sitemap index.
/// </summary>
public sealed class SitemapIndex
{
    private readonly List<SitemapIndexNode> _nodes = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndex"/> class.
    /// </summary>
    public SitemapIndex()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndex"/> class.
    /// </summary>
    /// <param name="nodes">The index nodes.</param>
    public SitemapIndex(IEnumerable<SitemapIndexNode> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        _nodes.AddRange(nodes);
    }

    /// <summary>
    /// Gets the sitemap index nodes.
    /// </summary>
    public IReadOnlyList<SitemapIndexNode> Nodes => _nodes;

    /// <summary>
    /// Adds the specified nodes to the sitemap index.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    public void Add(params SitemapIndexNode[] nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        Add(nodes.AsEnumerable());
    }

    /// <summary>
    /// Adds the specified nodes to the sitemap index.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    public void Add(IEnumerable<SitemapIndexNode> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        _nodes.AddRange(nodes);
    }
}