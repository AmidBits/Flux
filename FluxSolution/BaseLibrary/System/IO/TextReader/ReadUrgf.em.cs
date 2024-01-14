namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a sequence of fields by parsing URGF data.</para>
    /// </summary>
    /// <param name="reader">The <see cref="System.IO.TextReader"/> to read from.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static System.Collections.Generic.IEnumerable<string[]> ReadUrgf(this System.IO.TextReader reader)
    {
      const char unitSeparator = '\u001F';
      const char recordSeparator = '\u001E';
      //const char groupSeparator = '\u001D';
      //const char fileSeparator = '\u001C';

      var record = new System.Collections.Generic.List<string>();
      var unit = new System.Text.StringBuilder();

      while (reader.Read() is var read && read != -1 && (char)read is var c)
      {
        if (c == unitSeparator || c == recordSeparator)
        {
          record.Add(unit.ToString());
          unit.Clear();

          if (c == recordSeparator)
          {
            yield return record.ToArray();
            record.Clear();
          }
        }
        else
          unit.Append(c);
      }

      if (unit.Length > 0 || record.Count > 0)
      {
        record.Add(unit.ToString());
        unit.Clear();

        yield return record.ToArray();
      }
    }
  }
}
