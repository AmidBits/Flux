namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns a new string with the specified characters doubled in the string.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, char value)
    {
      for (var index = 0; index < source.Length; index++)
        if (source[index] == value)
          return index;

      return -1;
    }

    /// <summary>Returns a new string with the specified characters doubled in the string.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, string value, Flux.StringComparer comparer)
    {
      for (var index = 0; index < source.Length; index++)
      {
        if (source.Equals(index, value, 0, value.Length, comparer)) return index;
        else if (source.Length - index < value.Length) break;
      }

      return -1;
    }

    /// <summary>Returns the index of the last character in the string. If not found a -1 is returned.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, params char[] values)
    {
      for (var index = 0; index < source.Length; index++)
      {
        var currentChar = source[index];

        foreach (var value in values)
        {
          if (currentChar == value)
          {
            return index;
          }
        }
      }

      return -1;
    }

    /// <summary>Returns the index of the last occurence of value in the string. If not found a -1 is returned.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, Flux.StringComparer comparer, params string[] values)
    {
      for (var index = 0; index < source.Length; index++)
      {
        var currentChar = source[index];

        foreach (var value in values)
        {
          if (comparer.Equals(currentChar, value))
          {
            return index;
          }
        }
      }

      return -1;
    }

    public static int[] IndicesOf(this System.Text.StringBuilder source, params char[] values)
    {
      var indices = new int[values.Length];

      for (var index = indices.Length - 1; index >= 0; index--)
      {
        indices[index] = -1;
      }

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var sourceChar = source[sourceIndex];

        for (var valueIndex = values.Length - 1; valueIndex >= 0; valueIndex--)
        {
          if (sourceChar == values[valueIndex] && indices[valueIndex] == -1)
          {
            indices[valueIndex] = sourceIndex;

            if (!System.Array.Exists(indices, i => i == -1))
            {
              return indices;
            }
          }
        }
      }

      return indices;
    }
  }
}
