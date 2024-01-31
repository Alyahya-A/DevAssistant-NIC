namespace Dev.Assistant.Business.Core.Extensions;

/// <summary>
/// Provides extension methods for working with lists and collections.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Gets the value at the specified position in the list. The index is specified as integer. <br/>
    /// If index is outside the range of valid indexes for the current source, it will returns null (not exception)
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the list.</typeparam>
    /// <param name="source">Target data source.</param>
    /// <param name="index">Represents the position of the source element to get.</param>
    /// <returns>The value at the specified position in the source if the index is valid; otherwise, returns null.</returns>
    public static TSource At<TSource>(this IEnumerable<TSource> source, int index)
    {
        ArgumentNullException.ThrowIfNull(nameof(source));

        if (index >= 0)
        {
            if (source is IList<TSource> list)
            {
                if (index < list.Count) return list[index];
            }
            else
            {
                using IEnumerator<TSource> e = source.GetEnumerator();

                while (true)
                {
                    if (!e.MoveNext()) break;
                    if (index == 0) return e.Current;
                    index--;
                }
            }
        }

        return default;
    }

    /// <summary>
    /// Determines whether the source contains any elements from the target collection.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
    /// <param name="source">Target data source.</param>
    /// <param name="target">Collection to check for elements.</param>
    /// <param name="notFoundValue">The first element in the target collection not found in the source.</param>
    /// <returns>true if at least one element from the target collection is found in the source; otherwise, false.</returns>
    public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> target, out TSource notFoundValue)
    {
        ArgumentNullException.ThrowIfNull(nameof(source));

        notFoundValue = default;

        if (!source.Any())
            return false;

        if (!target.Any())
            return false;

        foreach (var itemTarget in target)
        {
            if (!source.Contains(itemTarget))
            {
                notFoundValue = itemTarget;
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Returns a boolean indicating whether the first element of the sequence that passed the test specified by the predicate.
    /// if the condition passed then the first element of the sequence it will return in <paramref name="value"/> or a default
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the source.</typeparam>
    /// <param name="source">Target data source.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="value">The first element in source that passes the test specified by the predicate.</param>
    /// <returns>true if an element is found successfully; otherwise, false.</returns>
    public static bool TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out TSource value)
    {
        ArgumentNullException.ThrowIfNull(nameof(source));
        ArgumentNullException.ThrowIfNull(nameof(predicate));

        value = source.FirstOrDefault(predicate);

        if (value != null)
            return true;

        value = default;
        return false;
    }

    //public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
    //{
    //    if (source == null) throw Error.ArgumentNull("source");
    //    IList<TSource> list = source as IList<TSource>;
    //    if (list != null) return list[index];
    //    if (index < 0) throw Error.ArgumentOutOfRange("index");
    //    using (IEnumerator<TSource> e = source.GetEnumerator())
    //    {
    //        while (true)
    //        {
    //            if (!e.MoveNext()) throw Error.ArgumentOutOfRange("index");
    //            if (index == 0) return e.Current;
    //            index--;
    //        }
    //    }
    //}

    //public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
    //{
    //    if (source == null) throw Error.ArgumentNull("source");
    //    if (index >= 0)
    //    {
    //        IList<TSource> list = source as IList<TSource>;
    //        if (list != null)
    //        {
    //            if (index < list.Count) return list[index];
    //        }
    //        else
    //        {
    //            using (IEnumerator<TSource> e = source.GetEnumerator())
    //            {
    //                while (true)
    //                {
    //                    if (!e.MoveNext()) break;
    //                    if (index == 0) return e.Current;
    //                    index--;
    //                }
    //            }
    //        }
    //    }
    //    return default(TSource);
    //}
}