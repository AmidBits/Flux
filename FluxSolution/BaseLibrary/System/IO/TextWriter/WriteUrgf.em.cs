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
      const char unitSeparator = '\u001F';
      const char recordSeparator = '\u001E';
      //const char groupSeparator = '\u001D';
      //const char fileSeparator = '\u001C';

      for (var r = 0; r < data.Length; r++)
      {
        if (r > 0) writer.Write(recordSeparator);

        var record = data[r];

        for (var u = 0; u < record.Length; u++)
        {
          if (u > 0) writer.Write(unitSeparator);

          writer.Write(record[u]);
        }
      }
    }
  }
}
