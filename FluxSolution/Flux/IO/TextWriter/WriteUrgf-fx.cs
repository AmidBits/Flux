namespace Flux
{
  public static partial class Streams
  {
    /// <summary>
    /// <para>Writes a jagged array as URGF (tabular) <paramref name="data"/> to the <paramref name="writer"/>.</para>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="data"></param>
    public static void WriteUrgf(this System.IO.TextWriter writer, string[][] data)
    {
      for (var r = 0; r < data.Length; r++)
      {
        if (r > 0) writer.Write((char)UnicodeInformationSeparator.RecordSeparator);

        var record = data[r];

        for (var u = 0; u < record.Length; u++)
        {
          if (u > 0) writer.Write((char)UnicodeInformationSeparator.UnitSeparator);

          writer.Write(record[u]);
        }
      }
    }

    public static void WriteUrgf(this System.Data.DataSet dataSet, System.IO.TextWriter textWriter)
    {
      for (var gi = 0; gi < dataSet.Tables.Count; gi++)
      {
        if (gi > 0) textWriter.Write((char)UnicodeInformationSeparator.GroupSeparator);

        var group = dataSet.Tables[gi];

        for (var ui = 0; ui < group.Columns.Count; ui++)
        {
          if (ui > 0) textWriter.Write((char)UnicodeInformationSeparator.UnitSeparator);

          var unit = group.Columns[ui];

          textWriter.Write(unit.ColumnName);
        }

        for (var ri = 0; ri < group.Rows.Count; ri++)
        {
          textWriter.Write((char)UnicodeInformationSeparator.RecordSeparator);

          var record = group.Rows[ri];

          textWriter.Write(string.Join((char)UnicodeInformationSeparator.UnitSeparator, record.ItemArray));
        }
      }

      //for (var r = 0; r < data.Length; r++)
      //{
      //  if (r > 0) writer.Write((char)Unicode.UnicodeInformationSeparator.RecordSeparator);

      //  var record = data[r];

      //  for (var u = 0; u < record.Length; u++)
      //  {
      //    if (u > 0) writer.Write((char)Unicode.UnicodeInformationSeparator.UnitSeparator);

      //    writer.Write(record[u]);
      //  }
      //}
    }
  }
}

#region Create sample file
/*

using var sw = System.IO.File.CreateText(fileName);

var data = new string[][] { new string[] { "A", "B" }, new string[] { "1", "2" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Write((char)UnicodeInformationSeparator.FileSeparator);

data = new string[][] { new string[] { "C", "D" }, new string[] { "3", "4" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Flush();
sw.Close();

*/
#endregion // Create sample file
