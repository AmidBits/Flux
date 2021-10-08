namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<T>(this T[,] source, char horizontalSeparator = '\u007c', char verticalSeparator = '\u002d', bool uniformMaxWidth = false, bool centerContent = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var length0 = source.GetLength(0);
      var length1 = source.GetLength(1);

      var maxWidths = new int[length1];

      for (var i0 = length0 - 1; i0 >= 0; i0--)
        for (var i1 = length1 - 1; i1 >= 0; i1--)
          maxWidths[i1] = System.Math.Max(maxWidths[i1], (source[i0, i1]?.ToString() ?? string.Empty).Length);

      var maxWidth = System.Linq.Enumerable.Max(maxWidths, w => w);

      var format = string.Join(horizontalSeparator == '\0' ? null : horizontalSeparator.ToString(), System.Linq.Enumerable.Select(maxWidths, (e, i) => $"{{{i},-{(uniformMaxWidth ? maxWidth : e)}}}"));
      var divide = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator, System.Linq.Enumerable.Select(maxWidths, (e, i) => new string(verticalSeparator, uniformMaxWidth ? maxWidth : e)));

      for (var i0 = 0; i0 < length0; i0++) // Consider row as dimension 0.
      {
        if (!string.IsNullOrEmpty(divide) && i0 > 0)
          yield return divide;

        yield return string.Format(null, format, System.Linq.Enumerable.ToArray(GetValues()));

        System.Collections.Generic.IEnumerable<string> GetValues()
        {
          for (var i1 = 0; i1 < length1; i1++)
            yield return (source[i0, i1]?.ToString() ?? string.Empty) is var v && centerContent ? v.ToStringBuilder().PadEven(maxWidth, ' ', ' ').ToString() : v;
        }
      }
    }
    /// <summary>Returns the two-dimensional array as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleBlock<T>(this T[,] source, char horizontalSeparator = '\u007c', char verticalSeparator = '\u002d', bool uniformWidth = false, bool centerContent = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformWidth, centerContent));
  }
}
