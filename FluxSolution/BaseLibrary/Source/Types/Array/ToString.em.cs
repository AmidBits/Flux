using System.Linq;

namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Xtensions
  {
    // Returns the array elements formatted for the console.
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static string ToConsoleString<T>(this T[,] source, bool useTotalMaxWidth = false, char horizontalSeparator = '\u007C', char verticalSeparator = '\u002D')
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sb = new System.Text.StringBuilder();

      var columnMaxWidths = System.Linq.Enumerable.Range(0, source.GetLength(1)).Select(i => source.GetElements(1, i).Max(o => $"{o.item}".Length)).ToArray();
      if (useTotalMaxWidth) columnMaxWidths = columnMaxWidths.Select(w => columnMaxWidths.Max()).ToArray(); // If used, replace all with total max width.

      var format = string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => $"{{{index},-{width}}}"));

      var verticalSeparatorRow = verticalSeparator != '\0' ? string.Join(horizontalSeparator.ToString(), columnMaxWidths.Select((width, index) => new string(verticalSeparator, width))) : null;

      for (int d0 = 0; d0 < source.GetLength(0); d0++)
      {
        if (d0 > 0 && verticalSeparator != '\0')
        {
          sb.AppendLine(verticalSeparatorRow);
        }

        sb.AppendLine(string.Format(System.Globalization.CultureInfo.CurrentCulture, format, source.GetElements(0, d0).Select(e => (object?)e.item).ToArray()));
      }

      return sb.ToString();
    }
  }
}
