namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Determines the index of the first occurence that satisfy the predicate.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Returns the first index of the specified character within the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Length; index++)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the first index of the specified character within the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns the first index of the specified string within the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf(this System.ReadOnlySpan<char> source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      for (var index = 0; index < source.Length; index++)
      {
        if (Equals(source, index, value, 0, value.Length, comparer)) return index;
        else if (source.Length - index < value.Length) break;
      }

      return -1;
    }
    /// <summary>Returns the first index of the specified string within the string builder, or -1 if not found. Uses the default comparer.</summary>
    public static int IndexOf(this System.ReadOnlySpan<char> source, string value)
      => IndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
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
    /// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the default comparer.</summary>
    public static int IndexOfAny<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);

    /// <summary>Returns the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        if (IndexOf(source, values[valueIndex], comparer) is var index && index > -1)
          return index;

      return -1;
    }
    /// <summary>Returns the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.ReadOnlySpan<char> source, params string[] values)
      => IndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Returns all first indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOf<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
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
    /// <summary>Returns all first indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
    public static int[] IndicesOf<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => IndicesOf(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
