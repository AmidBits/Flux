namespace Flux
{
  public static partial class Fx
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
