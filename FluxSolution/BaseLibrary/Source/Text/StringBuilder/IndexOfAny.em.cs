namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
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
  }
}
