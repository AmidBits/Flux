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
