namespace Flux
{
  public static partial class IEnumerableComparing
  {
    #region CommonPrefixLength

    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static int CommonPrefixLength<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int maxLength = int.MaxValue)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Zip(target).TakeWhile((e, i) => equalityComparer.Equals(e.First, e.Second) && i < maxLength).Count();
    }

    #endregion

    #region CompareCount

    /// <summary>
    /// <para>Compares the number of elements in <paramref name="source"/> that satisfies the <paramref name="predicate"/> (all elements if null) against the specified <paramref name="count"/>.</para>
    /// </summary>
    /// <returns>Depending on <paramref name="source"/> count: -1 when less than, 0 when equal to, or 1 when greater than, the specified <paramref name="count"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static int CompareCount<T>(this System.Collections.Generic.IEnumerable<T> source, int count, System.Func<T, int, bool>? predicate = null)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(count);

      predicate ??= (e, i) => true; // Default predicate to all elements;

      var index = 0;
      var counter = 0;

      foreach (var item in source)
        if (predicate(item, index++))
          if (++counter > count)
            break;

      return counter > count ? 1 : counter < count ? -1 : 0;
    }

    #endregion

    #region ContainsAll

    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    /// <remarks>This extension method leverages (and re-use) the type <see cref="System.Collections.Generic.ISet{T}"/> for speed.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if ((target is System.Collections.Generic.ICollection<T> tct && tct.Count == 0) || (target is System.Collections.ICollection tc && tc.Count == 0) || target is null) // If target is empty (or null), all is included, the result is true.
        return true;

      var shs = source is System.Collections.Generic.ISet<T> hsTemporary // For speed...
        ? hsTemporary // Re-use the ISet<T> if available.
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default); // Otherwise, create a HashSet<T>.

      if (shs.Count == 0) // If source is empty, it cannot contain anything, the result is false.
        return false;

      return target.All(shs.Contains);
    }

    #endregion

    #region ContainsAny

    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    /// <remarks>This extension method leverages (and re-use) the type <see cref="System.Collections.Generic.ISet{T}"/> for speed.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      var shs = source is System.Collections.Generic.ISet<T> hsTemporary // For speed...
        ? hsTemporary // Re-use the ISet<T> if available.
        : new System.Collections.Generic.HashSet<T>(source, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default); // Otherwise, create a HashSet<T>.

      if (
        shs.Count == 0 // If source is empty, it cannot contain any, so the result is false.
        || (target is System.Collections.Generic.ICollection<T> tct && tct.Count == 0) || (target is System.Collections.ICollection tc && tc.Count == 0) || target is null // If target is empty (or null), there is nothing to contain, so the result is false.
      )
        return false;

      return target.Any(shs.Contains);
    }

    #endregion

    #region IsCommonPrefix

    /// <summary>
    /// <para>Determines whether the source sequence begins with the target sequence. Uses the specified equality comparer.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool IsCommonPrefix<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, int length, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      => CommonPrefixLength(source, target, equalityComparer, length) == length;

    #endregion

    #region RunLengthEncode

    /// <summary>
    /// <para>Run-length encodes <paramref name="source"/> by converting consecutive instances of the same element into a <c>KeyValuePair{T,int}</c> representing the item and its occurrence count. Uses the specified <paramref name="equalityComparer"/> (default if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<(T Item, int Count)> RunLengthEncode<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source.ThrowOnNull().GetEnumerator();

      if (e.MoveNext())
      {
        var previous = e.Current;

        var count = 1;

        while (e.MoveNext())
        {
          if (equalityComparer.Equals(previous, e.Current))
            count++;
          else
          {
            yield return (previous, count);

            previous = e.Current;
            count = 1;
          }
        }

        yield return (previous, count);
      }
    }

    #endregion

    #region ThrowOnNull

    /// <summary>Throws an exception if <paramref name="source"/> is null. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNull<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      foreach (var item in source ?? throw new System.ArgumentNullException(paramName ?? nameof(source), "The sequence cannot be null."))
        yield return item;  // Must yield for deferred execution.
    }

    #endregion

    #region ThrowOnNullOrEmpty

    /// <summary>Throws an exception if <paramref name="source"/> is null or the sequence empty. Deferred execution.</summary>
    /// <exception cref="System.ArgumentNullException">The sequence cannot be null.</exception>
    /// <exception cref="System.ArgumentException">The sequence cannot be empty.</exception>
    public static System.Collections.Generic.IEnumerable<TSource> ThrowOnNullOrEmpty<TSource>(this System.Collections.Generic.IEnumerable<TSource>? source, string? paramName = null)
    {
      using var e = source.ThrowOnNull(paramName).GetEnumerator();

      if (e.MoveNext())
      {
        do
          yield return e.Current;
        while (e.MoveNext());

        yield break;
      }

      throw new System.ArgumentException("The sequence cannot be empty.", paramName ?? nameof(source));
    }

    #endregion
  }
}
