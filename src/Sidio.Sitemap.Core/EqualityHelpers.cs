namespace Sidio.Sitemap.Core;

internal static class EqualityHelpers
{
    /// <summary>
    /// Determines whether two sequences are equal, ignoring the order of elements.
    /// </summary>
    /// <param name="left">The first sequence to compare.</param>
    /// <param name="right">The second sequence to compare.</param>
    /// <typeparam name="T">The type of elements in the sequences.</typeparam>
    /// <returns><c>true</c> if the sequences are equal; otherwise, <c>false</c>.</returns>
    public static bool UnorderedEquals<T>(IEnumerable<T>? left, IEnumerable<T>? right)
        where T : notnull
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left == null || right == null)
        {
            return false;
        }

        var comparer = EqualityComparer<T>.Default;
        var elementCounts = new Dictionary<T, int>(comparer);

        foreach (var item in left)
        {
            if (elementCounts.TryGetValue(item, out var count))
            {
                elementCounts[item] = count + 1;
            }
            else
            {
                elementCounts[item] = 1;
            }
        }

        foreach (var item in right)
        {
            if (!elementCounts.TryGetValue(item, out var count))
            {
                return false;
            }

            if (count == 1)
            {
                _ = elementCounts.Remove(item);
            }
            else
            {
                elementCounts[item] = count - 1;
            }
        }

        return elementCounts.Count == 0;
    }

    /// <summary>
    /// Returns a hash code for a sequence, ignoring the order of elements.
    /// </summary>
    /// <param name="values">The sequence of values.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>A hash code for the sequence.</returns>
    public static int GetUnorderedHashCode<T>(IEnumerable<T>? values)
        where T : notnull
    {
        if (values == null)
        {
            return 0;
        }

        var comparer = EqualityComparer<T>.Default;
        var count = 0;
        var sum = 0;
        var xor = 0;

        unchecked
        {
            foreach (var item in values)
            {
                var itemHashCode = item == null ? 0 : comparer.GetHashCode(item);
                count++;
                sum += itemHashCode;
                xor ^= itemHashCode;
            }

            var hashCode = 17;
            hashCode = (hashCode * 31) + count;
            hashCode = (hashCode * 31) + sum;
            hashCode = (hashCode * 31) + xor;
            return hashCode;
        }
    }
}
