namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns the index of the first character in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char value)
    {
      for (var index = source.Length - 1; index >= 0; index++)
        if (source[index] == value)
          return index;

      return -1;
    }

    /// <summary>Returns the index of the first occurence of value in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, string value, Flux.StringComparer comparer)
    {
      for (int index = source.Length - value.Length; index >= 0; index--)
        if (source.Equals(index, value, 0, value.Length, comparer))
          return index;

      return -1;
    }

    /// <summary>Returns the index of the last character in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params char[] values)
    {
      foreach (var value in values)
        if (source.LastIndexOf(value) is var index && index > -1)
          return index;

      return -1;
    }

    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, Flux.StringComparer comparer, params string[] values)
    {
      foreach (var value in values)
        if (source.LastIndexOf(value, comparer) is var index && index > -1)
          return index;

      return -1;
    }

    public static int[] LastIndicesOfAny(this System.Text.StringBuilder source, params char[] values)
    {
      var lastIndices = new int[values.Length];

      for (var index = lastIndices.Length - 1; index >= 0; index--)
      {
        lastIndices[index] = -1;
      }

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
        {
          if (sourceChar == values[valueIndex] && lastIndices[valueIndex] == -1)
          {
            lastIndices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(lastIndices, i => i == -1))
            {
              return lastIndices;
            }
          }
        }
      }

      return lastIndices;
    }
  }
}
