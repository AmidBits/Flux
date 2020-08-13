namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned. Uses the specified equality comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = source.Length - 1; index >= 0; index++)
        if (comparer.Equals(source[index], value))
          return index;

      return -1;
    }
    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned. Uses the default equality comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned. Uses the specified equality comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, string value, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (value is null) throw new System.ArgumentNullException(nameof(value));

      for (int index = source.Length - value.Length; index >= 0; index--)
        if (source.Equals(index, value, 0, value.Length, comparer))
          return index;

      return -1;
    }
    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned. Uses the default equality comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, string value)
      => LastIndexOf(source, value, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns the index of the last occurence of any of the specified characters within the string builder, or -1 if none were found. Uses the specified equality comparer.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params char[] characters)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var character = source[index];

        if (System.Array.Exists(characters, c => comparer.Equals(c, character)))
          return index;
      }

      return -1;
    }
    /// <summary>Returns the index of the last occurence of any of the specified characters within the string builder, or -1 if none were found. Uses the default equality comparer.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params char[] characters)
      => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, characters);

    /// <summary>Returns the index of the last occurence of any of the specified strings within the string builder, or -1 if none were found. Uses the specified equality comparer.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      foreach (var value in values)
        if (LastIndexOf(source, value, comparer) is var index && index > -1)
          return index;

      return -1;
    }
    /// <summary>Returns the index of the last occurence of any of the specified strings within the string builder, or -1 if none were found. Uses the default equality comparer.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params string[] values)
       => LastIndexOfAny(source, System.Collections.Generic.EqualityComparer<char>.Default, values);

    /// <summary>Returns the index of the last occurences of all specified characters within the string builder, or -1 if not found. Uses the specified equality comparer.</summary>
    public static int[] LastIndicesOfAll(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params char[] values)
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
    /// <summary>Returns the index of the last occurences of all specified characters within the string builder, or -1 if not found. Uses the default equality comparer.</summary>
    public static int[] LastIndicesOfAll(this System.Text.StringBuilder source, params char[] values)
      => LastIndicesOfAll(source, System.Collections.Generic.EqualityComparer<char>.Default, values);
  }
}
