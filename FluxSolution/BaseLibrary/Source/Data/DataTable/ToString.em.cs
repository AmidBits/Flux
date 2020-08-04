using System.Linq;

namespace Flux
{
  public static partial class XtensionsData
  {
    public static string ToFormattedString(this System.Data.DataTable source, string horizontalSeparator = @"|", char verticalSeparator = '\0')
    {
      var sb = new System.Text.StringBuilder();

      var columnWidths = new int[source.Columns.Count];
      for (var columnIndex = 0; columnIndex < columnWidths.Length; columnIndex++)
      {
        columnWidths[columnIndex] = source.Rows.Cast<System.Data.DataRow>().Max(dr => dr[columnIndex].ToString()?.Length ?? 0);
      }

      var format = string.Join(horizontalSeparator, columnWidths.Select((width, index) => $"{{{index},-{width}}}"));

      sb.AppendLine(string.Format(format, source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray()));

      for (var row = 0; row < source.Rows.Count; row++)
      {
        if (verticalSeparator != '\0')
        {
          sb.AppendLine(string.Join(horizontalSeparator, columnWidths.Select((width, index) => new string(verticalSeparator, width))));
        }

        sb.AppendLine(string.Format(format, source.Rows[row].ItemArray));
      }

      return sb.ToString();
    }
  }
}
