namespace Flux
{
  public static partial class UnicodeData
  {
    public const int GroupSeparator = '\u241D';
    public const int RecordSeparator = '\u241E';
    public const int UnitSeparator = '\u241F';

    public static string ReadUnit(this System.IO.TextReader source, out int read)
    {
      var unit = new System.Text.StringBuilder();

      for (read = source.Read(); read > -1; read = source.Read())
      {
        if (read == UnitSeparator || read == RecordSeparator || read == GroupSeparator)
          break;
        else
          unit.Append((char)read);
      }

      return unit.ToString();
    }

    public static System.Collections.Generic.List<string> ReadRecord(this System.IO.TextReader source, out int read)
    {
      var record = new System.Collections.Generic.List<string>();

      do
      {
        var unit = ReadUnit(source, out read);

        record.Add(unit);
      }
      while (read == UnitSeparator);

      return record;
    }

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<string>> ReadRecords(this System.IO.TextReader source)
    {
      int read;

      do
      {
        yield return ReadRecord(source, out read);
      }
      while (read == RecordSeparator);
    }

    public static string[][] ReadGroup(this System.IO.TextReader source, out int read)
    {
      var group = new System.Collections.Generic.List<string[]>();

      do
      {
        var record = ReadRecord(source, out read);

        group.Add(record.ToArray());
      }
      while (read == RecordSeparator);

      return group.ToArray();
    }
  }
}
