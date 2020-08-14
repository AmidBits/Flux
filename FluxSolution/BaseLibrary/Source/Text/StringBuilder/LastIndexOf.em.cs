namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (int index = source.Length - value.Length; index >= 0; index--)
        if (source.Equals(index, value, comparer))
          return index;

      return -1;
    }
    /// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, string value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params char[] values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
          if (comparer.Equals(source[sourceIndex], values[valueIndex]))
            return sourceIndex;

      return -1;
    }
    /// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params char[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
          if (Equals(source, sourceIndex, values[valueIndex], comparer))
            return sourceIndex;

      return -1;
    }
    /// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params string[] values)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Returns all last indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
    public static int[] LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params char[] values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

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
  }
}
