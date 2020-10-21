using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static object[,] ToArray(this System.Data.DataTable source, bool includeColumnName)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var offset = includeColumnName ? 1 : 0;

      var array = new object[source.Rows.Count + offset, source.Columns.Count];

      if (includeColumnName)
        for (var column = 0; column < source.Columns.Count; column++)
          array[0, column] = source.Columns[column].ColumnName;

      for (var row = 0; row < source.Rows.Count; row++)
        for (var column = 0; column < source.Columns.Count; column++)
          array[row + offset, column] = source.Rows[row][column];

      return array;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

    public static string ToFormattedString(this System.Data.DataTable source, string horizontalSeparator = @"|", char verticalSeparator = '\0')
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sb = new System.Text.StringBuilder();

      var columnWidths = new int[source.Columns.Count];
      for (var columnIndex = 0; columnIndex < columnWidths.Length; columnIndex++)
        columnWidths[columnIndex] = source.Rows.Cast<System.Data.DataRow>().Max(dr => dr[columnIndex].ToString()?.Length ?? 0);

      var format = string.Join(horizontalSeparator, columnWidths.Select((width, index) => $"{{{index},-{width}}}"));

      sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, format, source.Columns.Cast<System.Data.DataColumn>().Select(dc => dc.ColumnName).ToArray());
      sb.AppendLine();

      for (var row = 0; row < source.Rows.Count; row++)
      {
        if (verticalSeparator != '\0')
          sb.AppendLine(string.Join(horizontalSeparator, columnWidths.Select((width, index) => new string(verticalSeparator, width))));

        sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, format, source.Rows[row].ItemArray);
        sb.AppendLine();
      }

      return sb.ToString();
    }

    /// <summary>Creates a new XDocument with the data from the DataTable.</summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.Linq.XDocument();
      using (var xw = xd.CreateWriter())
        source.WriteXml(xw);
      return xd;
    }

    /// <summary>Creates a new XmlDocument with the data from the DataTable.</summary>
    public static System.Xml.XmlDocument ToXmlDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.XmlDocument();
      using (var writer = xd.CreateNavigator().AppendChild())
        source.WriteXml(writer);
      return xd;
    }
  }
}
