using System;

namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Reports the index of the last occurence that satisfies the predicate.</summary>
    public static int LastIndexOf<T>(ref this SpanBuilder<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(ref this SpanBuilder<T> source, T value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = source.Length - 1; index >= 0; index--)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf<T>(ref this SpanBuilder<T> source, T value)
      => LastIndexOf(ref source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(ref this SpanBuilder<T> source, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = source.Length - value.Length; index >= 0; index--)
        if (EqualsAt(ref source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer</summary>
    public static int LastIndexOf<T>(ref this SpanBuilder<T> source, System.ReadOnlySpan<T> value)
      => LastIndexOf(ref source, value, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
