using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</summary>
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings<T>(this T[,] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var columnMaxWidths = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select(i => source.GetElements(1, i).Max(o => $"{o.item}".Length)).ToList();
      if (uniformMaxWidth)
        columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToList(); // If used, replace all with total max width.

      var format = string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

      for (var row = 0; row < source.GetLength(0); row++) // Consider row as dimension 0.
      {
        if (!(verticalSeparatorRow is null) && row > 0)
          yield return string.Join(horizontalSeparator, columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

        yield return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, source.GetElements(0, row).Select(e => (object?)e.item).ToArray());
      }
    }
    /// <summary>Returns the two-dimensional array as a ready-to-print grid-like formatted string, that can be printed in the console.</summary>
    public static string ToConsoleString<T>(this T[,] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u2015', bool uniformMaxWidth = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings(source, horizontalSeparator, verticalSeparator, uniformMaxWidth));
  }
}
