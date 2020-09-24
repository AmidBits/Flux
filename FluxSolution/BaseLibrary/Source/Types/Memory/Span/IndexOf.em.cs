namespace Flux
{
  public static partial class Xtensions
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
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, T value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns the first index of the specified target within the source, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf(this System.Span<char> source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      for (var index = 0; index < source.Length; index++)
      {
        if (Equals(source, index, target, 0, target.Length, comparer)) return index;
        else if (source.Length - index < target.Length) break;
      }

      return -1;
    }
    /// <summary>Reports the first index of the specified target within the source, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf(this System.Span<char> source, System.ReadOnlySpan<char> value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
      {
        var character = source[index];

        if (System.Array.Exists(values, c => comparer.Equals(c, character)))
          return index;
      }

      return -1;
    }
    /// <summary>Reports the first index of any of the specified characters within the source, or -1 if none were found. Uses the default comparer.</summary>
    public static int IndexOfAny<T>(this System.Span<T> source, params T[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);

    /// <summary>Reports the first index of any of the specified targets within the source, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Span<char> source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (IndexOf(source, values[valueIndex], comparer) is var index && index > -1)
          return index;

      return -1;
    }
    /// <summary>Reports the first index of any of the specified targets within the source, or -1 if none were found. Uses the default comparer.</summary>
    public static int IndexOfAny(this System.Span<char> source, params string[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOf<T>(this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var indices = new int[values.Length];

      System.Array.Fill(indices, -1);

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
          if (indices[valueIndex] == -1 && comparer.Equals(sourceChar, values[valueIndex]))
          {
            indices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(indices, i => i == -1))
              return indices;
          }
        }
      }

      return indices;
    }
    /// <summary>Reports all first indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOf<T>(this System.Span<T> source, params T[] values)
      => IndicesOf(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
