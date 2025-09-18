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
    /// <param name="stylesheet">The text/xsl stylesheet.</param>
    public SitemapIndex(string? stylesheet = null)
    {
        if (!string.IsNullOrWhiteSpace(stylesheet))
        {
            Stylesheet = stylesheet;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SitemapIndex"/> class.
    /// </summary>
    /// <param name="nodes">The index nodes.</param>
    /// <param name="stylesheet">The text/xsl stylesheet.</param>
    public SitemapIndex(IEnumerable<SitemapIndexNode?> nodes, string? stylesheet = null)
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
    /// Gets the sitemap index nodes.
    /// </summary>
    public IReadOnlyList<SitemapIndexNode> Nodes => _nodes;

    /// <summary>
    /// Gets the stylesheet.
    /// </summary>
    public string? Stylesheet { get; }

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

        var validNodes = nodes.Where(x => x != null).Cast<SitemapIndexNode>().ToList();

        if (validNodes.Count > 0)
        {
            _nodes.AddRange(validNodes);
        }

        return validNodes.Count;
    }
}