using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayJagged
  {
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStringsJagged<T>(this T[][] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var maxColumns = System.Linq.Enumerable.Range(0, source.GetLength(0)).Max(i => source[i].Length);

      var columnMaxWidths = System.Linq.Enumerable.Range(0, maxColumns).Select(i => source.Max(a => i < a.Length ? $"{a[i]}".Length : 0)).ToArray();
      if (uniformMaxWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.

      var format = string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

      for (int index0 = 0, length0 = source.Length; index0 < length0; index0++)
      {
        if (verticalSeparatorRow is not null && index0 > 0)
          yield return verticalSeparatorRow;

        yield return string.Format(System.Globalization.CultureInfo.CurrentCulture, format, source[index0].Select(o => (object?)o).ToArray());
      }
    }
    public static string ToConsoleStringJagged<T>(this T[][] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
      => string.Join(System.Environment.NewLine, ToConsoleStringsJagged(source, horizontalSeparator, verticalSeparator, uniformMaxWidth));

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    // Returns the array elements formatted for the console.
    public static System.Collections.Generic.IEnumerable<string> ToConsoleStrings2d<T>(this T[,] source, System.Func<T, int, string>? formatSelector, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      formatSelector ??= (e, i) => $"{e}";

      var columnMaxWidths = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select(i => source.GetElements(1, i).Select((e, i) => formatSelector(e.item, i)).Max(s => s.Length)).ToArray();
      if (uniformMaxWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.

      var format = string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = verticalSeparator == '\0' ? null : string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => new string(verticalSeparator, width)));

      for (int index0 = 0, length0 = source.GetLength(0); index0 < length0; index0++)
      {
        if (verticalSeparatorRow is not null && index0 > 0)
          yield return verticalSeparatorRow;

        yield return string.Format(System.Globalization.CultureInfo.CurrentCulture, format, source.GetElements(0, index0).Select((e, i) => formatSelector(e.item, i)).ToArray());
      }
    }
    public static string ToConsoleString2d<T>(this T[,] source, System.Func<T, int, string>? formatSelector, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
      => string.Join(System.Environment.NewLine, ToConsoleStrings2d(source, formatSelector, horizontalSeparator, verticalSeparator, uniformMaxWidth));
    public static string ToConsoleString2d<T>(this T[,] source, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D', bool uniformMaxWidth = false)
      => ToConsoleString2d(source, null, horizontalSeparator, verticalSeparator, uniformMaxWidth);
    public static string ToConsoleString2d<T>(T[,] source)
      => ToConsoleString2d(source, '\u007C', '\u002D', false);
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
