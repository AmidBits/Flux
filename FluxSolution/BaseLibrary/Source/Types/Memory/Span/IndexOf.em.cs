namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the index of the first occurence that satisfy the predicate.</summary>
    public static int IndexOf<T>(this System.Span<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, T value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = 0; index < source.Length; index++)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, T value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = 0; index < source.Length; index++)
      {
        if (Equals(source, index, target, 0, target.Length, comparer)) return index;
        else if (source.Length - index < target.Length) break;
      }

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, System.Span<T> value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
