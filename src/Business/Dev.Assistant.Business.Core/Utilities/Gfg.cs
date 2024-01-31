namespace Dev.Assistant.Business.Core.Utilities;

/// <summary>
/// A custom comparer for strings that implements the <see cref="IComparer{T}"/> interface.
/// </summary>
internal class Gfg : IComparer<string>
{
    /// <summary>
    /// Compares two strings and returns an integer that indicates their relative order.
    /// </summary>
    /// <param name="x">The first string to compare.</param>
    /// <param name="y">The second string to compare.</param>
    /// <returns>
    /// A negative value if <paramref name="x"/> is less than <paramref name="y"/>,
    /// 0 if they are equal, and a positive value if <paramref name="x"/> is greater than <paramref name="y"/>.
    /// </returns>
    public int Compare(string x, string y)
    {
        // Handle null cases by considering them equal.
        if (x == null || y == null)
        {
            return 0;
        }

        // Compare strings using their natural order.
        return x.CompareTo(y);
    }
}