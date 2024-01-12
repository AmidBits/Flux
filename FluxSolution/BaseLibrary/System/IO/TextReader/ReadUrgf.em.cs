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
    public static System.Collections.Generic.IEnumerable<string[][]> ReadUrgf(this System.IO.TextReader reader)
    {
      const char unitSeparator = '\u001F';
      const char recordSeparator = '\u001E';
      const char groupSeparator = '\u001D';
      //const char fileSeparator = '\u001C';

      var group = new System.Collections.Generic.List<string[]>();
      var record = new System.Collections.Generic.List<string>();
      var unit = new System.Text.StringBuilder();

      while (reader.Read() is var read && read != -1 && (char)read is var c)
      {
        if (c == unitSeparator || c == recordSeparator || c == groupSeparator)
        {
          record.Add(unit.ToString());
          unit.Clear();

          if (c == recordSeparator || c == groupSeparator)
          {
            group.Add(record.ToArray());
            record.Clear();

            if (c == groupSeparator)
            {
              yield return group.ToArray();
              group.Clear();
            }
          }
        }
        else
          unit.Append(c);
      }

      record.Add(unit.ToString());
      unit.Clear();

      group.Add(record.ToArray());
      record.Clear();

      yield return group.ToArray();
      group.Clear();
    }
  }
}
