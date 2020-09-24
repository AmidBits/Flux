using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    public static string ToFormattedString(this System.Data.DataTable source, string horizontalSeparator = @"|", char verticalSeparator = '\0')
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sb = new System.Text.StringBuilder();

      var columnWidths = new int[source.Columns.Count];
      for (var columnIndex = 0; columnIndex < columnWidths.Length; columnIndex++)
      {
        columnWidths[columnIndex] = source.Rows.Cast<System.Data.DataRow>().Max(dr => dr[columnIndex].ToString()?.Length ?? 0);
      }

      var format = string.Join(horizontalSeparator, columnWidths.Select((width, index) => $"{{{index},-{width}}}"));

      sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, format, source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray());
      sb.AppendLine();

      for (var row = 0; row < source.Rows.Count; row++)
      {
        if (verticalSeparator != '\0')
        {
          sb.AppendLine(string.Join(horizontalSeparator, columnWidths.Select((width, index) => new string(verticalSeparator, width))));
        }

        sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, format, source.Rows[row].ItemArray);
        sb.AppendLine();
      }

      return sb.ToString();
    }
  }
}
