namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Searches a text (source) for the index of a substring (target). Returns -1 if not found. Uses the specified equality comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    /// </summary>
    public static int BoyerMooreHorspoolIndex<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var skippable = BoyerMooreHorspoolTable(source, target, equalityComparer);

      var skip = 0;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      while (sourceLength - skip >= targetLength)
      {
        if (IsSame(source[skip..], target, targetLength, equalityComparer))
          return skip;

        skip += skippable[source[skip + targetLength - 1]];
      }

      return -1;

      static bool IsSame(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int length, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      {
        for (var i = length - 1; i >= 0; i--)
          if (!equalityComparer.Equals(source[i], target[i]))
            return false;

        return true;
      }
    }

    /// <summary>
    /// <para>Creates a map of the amount of safely skippable elements.</para>
    /// <see href="https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore%E2%80%93Horspool_algorithm"/>
    /// </summary>
    public static System.Collections.Generic.Dictionary<T, int> BoyerMooreHorspoolTable<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      where T : notnull
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var table = new System.Collections.Generic.Dictionary<T, int>(equalityComparer); // The alphabet.

      var sourceLength = source.Length;
      var targetLength = target.Length;

      for (var index = System.Math.Max(targetLength, sourceLength) - 1; index >= 0; index--)
      {
        if (index < targetLength && target[index] is var wc && !table.ContainsKey(wc)) // Add to alphabet from source (word/needle), if it is not already in the table.
          table.Add(wc, targetLength);
        if (index < sourceLength && source[index] is var tc && !table.ContainsKey(tc)) // Add to alphabet from target (text/haystack), if it is not already in the table.
          table.Add(tc, targetLength);
      }

      for (var i = 0; i < targetLength; i++)
        table[target[i]] = targetLength - 1 - i;

      return table;
    }
  }
}
