namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Reports the index of the last occurence that satisfies the predicate.</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          return index;

      return -1;
    }

    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, T value, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf<T>(this System.ReadOnlySpan<T> source, T value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.ReadOnlySpan<char> source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (int index = source.Length - value.Length; index >= 0; index--)
        if (source.Equals(index, value, comparer))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the occurence of the target within the source. Or -1 if not found. Uses the default comparer</summary>
    public static int LastIndexOf(this System.ReadOnlySpan<char> source, string value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (comparer.Equals(source[sourceIndex], values[valueIndex]))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the specified targets in the source. Or -1 if none were found. Uses the default comparer</summary>
    public static int LastIndexOfAny<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);

    /// <summary>Reports the last index of any of the targets within the source. or -1 if none is found. Uses the specified comparer.</summary>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
          if (Equals(source, sourceIndex, values[valueIndex], comparer))
            return sourceIndex;

      return -1;
    }
    /// <summary>Reports the last index of any of the targets within the source. or -1 if none is found. Uses the default comparer</summary>
    public static int LastIndexOfAny(this System.ReadOnlySpan<char> source, params string[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the specified comparer.</summary>
    public static int[] LastIndicesOfAny<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] values)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var lastIndices = new int[values.Length];

      System.Array.Fill(lastIndices, -1);

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
        {
          if (lastIndices[valueIndex] == -1 && comparer.Equals(sourceChar, values[valueIndex]))
          {
            lastIndices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(lastIndices, i => i == -1))
              return lastIndices;
          }
        }
      }

      return lastIndices;
    }
    /// <summary>Reports all last indices of the specified targets within the source (-1 if not found). Uses the default comparer.</summary>
    public static int[] LastIndicesOfAny<T>(this System.ReadOnlySpan<T> source, params T[] values)
      => LastIndicesOfAny(source, System.Collections.Generic.EqualityComparer<T>.Default, values);
  }
}
