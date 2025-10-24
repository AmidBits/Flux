namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Except(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source == target)
          return new(equalityComparer); // A set minus itself is an empty set.

        var except = source.ToHashSet(equalityComparer);

        if (target.Length > 0)
          except.RemoveSpan(target); // If target is empty, source is the result.

        return except;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">Uses the <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Intersect(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        var intersect = new System.Collections.Generic.HashSet<T>(int.Min(source.Length, target.Length));

        if (source.Length > 0 && target.Length > 0)
        {
          if (source.Length < target.Length)
            return Intersect(target, source, equalityComparer); // If source has more items, it's faster to switch it around with target.

          var intersectable = target.ToHashSet(equalityComparer);

          for (var index = source.Length - 1; index >= 0; index--)
          {
            if (source[index] is var item && intersectable.Remove(item))
              intersect.Add(item);

            if (intersectable.Count == 0)
              break;
          }
        }

        return intersect;
      }

      /// <summary>
      /// <para>Creates a new sequence, a set of all subsets (as lists) of the source set, including the empty set and the source itself.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Power_set"/></para>
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.List<T[]> PowerSet()
      {
        var powerSet = new System.Collections.Generic.List<T[]>();

        var powerCount = (int)System.Numerics.BigInteger.Pow(2, source.Length);

        var subsetList = new System.Collections.Generic.List<T>(source.Length);

        for (var o = 0; o < powerCount; o++)
        {
          subsetList.Clear();

          for (var i = 0; i < powerCount; i++)
            if ((o & (1L << i)) > 0)
              subsetList.Add(source[i]);

          powerSet.Add(subsetList.ToArray());
        }

        return powerSet;
      }

      /// <summary>
      /// <para>Returns the number of unfound and also the number of unique elements between <paramref name="source"/> and <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="returnIfUnfound">Optional. Returns immediatly if <paramref name="returnIfUnfound"/> = true and an unfound element is found.</param>
      /// <param name="equalityComparer">Uses the <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
      /// <returns></returns>
      public (int unfoundCount, int uniqueCount) ScanSetCounts(System.ReadOnlySpan<T> target, bool returnIfUnfound, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        var unfoundCount = 0;
        var unique = new System.Collections.Generic.HashSet<T>(equalityComparer);

        foreach (var item in target)
        {
          if (source.IndexOf(item, equalityComparer) > -1) unique.Add(item);
          else if (returnIfUnfound) break;
          else unfoundCount++;
        }

        return (unfoundCount, unique.Count);
      }

      /// <summary>
      /// <para>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">Uses the <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"/>
      public System.Collections.Generic.HashSet<T> SymmetricExcept(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source == target)
          return new(equalityComparer); // A symmetric difference of a set with itself is an empty set.

        if (source.Length == 0)
          return target.ToHashSet(equalityComparer); // If source is empty, target is the result.

        if (target.Length == 0)
          return source.ToHashSet(equalityComparer); // If target is empty, source is the result.

        var symmetricExcept = source.Except(target, equalityComparer);
        symmetricExcept.UnionWith(target.Except(source, equalityComparer));
        return symmetricExcept;
      }

      /// <summary>
      /// <para>Union of distinct elements, i.e. no duplicates. Uses the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="equalityComparer">Uses the <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> if null.</param>
      /// <returns></returns>
      public System.Collections.Generic.HashSet<T> Union(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        var union = source.ToHashSet(equalityComparer, target.Length);
        union.AddSpan(target);
        return union;
      }

      /// <summary>
      /// <para>Union of all elements, i.e. potential duplicates.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<T> UnionAll(System.ReadOnlySpan<T> target)
      {
        var unionAll = source.ToList(target.Length);
        unionAll.AddRange(target);
        return unionAll;
      }
    }
  }
}
