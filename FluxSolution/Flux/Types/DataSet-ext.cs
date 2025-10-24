namespace Flux
{
  public static partial class XtensionDataSet
  {
    extension(System.Data.DataSet source)
    {
      /// <summary>
      /// <para>Writes a jagged array as URGF (tabular) <paramref name="jaggedArray"/> to the <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      public void WriteUrgf(System.IO.TextWriter target)
      {
        for (var tableIndex = 0; tableIndex < source.Tables.Count; tableIndex++)
        {
          if (tableIndex > 0) target.Write((char)UnicodeInformationSeparator.GroupSeparator);

          source.Tables[tableIndex].WriteUrgf(target, true);
        }
      }
    }

    public static void WriteUrgf(this System.Data.DataRowCollection source, System.IO.TextWriter target)
    {
      for (var rowIndex = 0; rowIndex < source.Count; rowIndex++)
      {
        if (rowIndex > 0) target.Write((char)UnicodeInformationSeparator.RecordSeparator);

        target.Write(source[rowIndex].ItemArray.ToUrgfString());
      }
    }

    public static void WriteUrgf(this System.Data.DataColumnCollection source, System.IO.TextWriter target)
    {
      for (var columnIndex = 0; columnIndex < source.Count; columnIndex++)
      {
        if (columnIndex > 0) target.Write((char)UnicodeInformationSeparator.UnitSeparator);

        target.Write(source[columnIndex].ColumnName);
      }
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
#endregion
