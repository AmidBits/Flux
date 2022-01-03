namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the index of the last occurence that satisfies the predicate.</summary>
    public static int LastIndexOf<T>(this System.Span<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(this System.Span<T> source, T value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf<T>(this System.Span<T> source, T value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Span<char> source, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (int index = source.Length - value.Length; index >= 0; index--)
        if (source.Equals(index, value, comparer))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer</summary>
    public static int LastIndexOf(this System.Span<char> source, System.ReadOnlySpan<char> value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
