namespace Sidio.Sitemap.Core;

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
    public SitemapIndex(IEnumerable<SitemapIndexNode?> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        _ = Add(nodes);
    }

    /// <summary>
    /// Gets the sitemap index nodes.
    /// </summary>
    public IReadOnlyList<SitemapIndexNode> Nodes => _nodes;

    /// <summary>
    /// Adds the specified nodes to the sitemap index.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <remarks>Nodes that are null will be ignored.</remarks>
    /// <returns>The actual number of nodes added.</returns>
    public int Add(params SitemapIndexNode?[] nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        return Add(nodes.AsEnumerable());
    }

    /// <summary>
    /// Adds the specified nodes to the sitemap index.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <remarks>Nodes that are null will be ignored.</remarks>
    /// <returns>The actual number of nodes added.</returns>
    public int Add(IEnumerable<SitemapIndexNode?> nodes)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        var nonNullableNodes = nodes.Where(x => x != null).Cast<SitemapIndexNode>().ToArray();

        if (nonNullableNodes.Length > 0)
        {
            _nodes.AddRange(nonNullableNodes);
        }

        return nonNullableNodes.Length;
    }
}