using System.Diagnostics;
using Flux.Text;

namespace Flux.Unicode
{
  public static partial class Utility
  {
    public const char FileSeparator = '\u001C';
    public const char GroupSeparator = '\u001D';
    public const char RecordSeparator = '\u001E';
    public const char UnitSeparator = '\u001F';

    public const char SymbolForFileSeparator = '\u241C';
    public const char SymbolForGroupSeparator = '\u241D';
    public const char SymbolForRecordSeparator = '\u241E';
    public const char SymbolForUnitSeparator = '\u241F';

    public static string ReadUnit(this System.IO.TextReader source, out int lastCharacterRead)
    {
      var unit = new System.Text.StringBuilder();

      for (lastCharacterRead = source.Read(); lastCharacterRead > -1; lastCharacterRead = source.Read())
      {
        if (lastCharacterRead == UnitSeparator || lastCharacterRead == RecordSeparator || lastCharacterRead == GroupSeparator)
          break;
        if (lastCharacterRead == SymbolForUnitSeparator || lastCharacterRead == SymbolForRecordSeparator || lastCharacterRead == SymbolForGroupSeparator)
          break;
        else
          unit.Append((char)lastCharacterRead);
      }

      return unit.ToString();
    }

    public static System.Collections.Generic.List<string> ReadRecord(this System.IO.TextReader source, out int lastCharacterRead)
    {
      var record = new System.Collections.Generic.List<string>();

      do
      {
        var unit = ReadUnit(source, out lastCharacterRead);

        record.Add(unit);
      }
      while (lastCharacterRead == UnitSeparator || lastCharacterRead == SymbolForUnitSeparator);

      return record;
    }

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<string>> ReadRecords(this System.IO.TextReader source)
    {
      int lastCharacterRead;

      do
      {
        yield return ReadRecord(source, out lastCharacterRead);
      }
      while (lastCharacterRead == RecordSeparator || lastCharacterRead == SymbolForRecordSeparator);
    }

    public static string[][] ReadGroup(this System.IO.TextReader source, out int lastCharacterRead)
    {
      var group = new System.Collections.Generic.List<string[]>();

      do
      {
        var record = ReadRecord(source, out lastCharacterRead);

        group.Add(record.ToArray());
      }
      while (lastCharacterRead == RecordSeparator || lastCharacterRead == SymbolForRecordSeparator);

      return group.ToArray();
    }

    public static string WriteGroup(string[][] data)
    {
      var sb = new System.Text.StringBuilder();

      var rLen = data.Length;

      for (var r = 0; r < data.Length; r++)
      {
        var cLen = data[r].Length;

        for (var c = 0; c < cLen; c++)
        {
          sb.Append(data[r][c]);

          if (c < cLen - 1)
            sb.Append(UnitSeparator);
        }

        if (r < rLen - 1)
          sb.Append(RecordSeparator);
      }

      sb.Append(GroupSeparator);

      return sb.ToString();
    }

    //public static string WriteGroupFromArrayRank2(string[,] data)
    //{
    //  var sb = new System.Text.StringBuilder();

    //  var rLen = data.GetLength(0);
    //  var cLen = data.GetLength(1);

    //  for (var r = 0; r < rLen; r++)
    //  {
    //    for (var c = 0; c < cLen; c++)
    //    {
    //      sb.Append(data[r, c]);

    //      if (c < cLen - 1)
    //        sb.Append(UnitSeparator);
    //    }

    //    if (r < rLen - 1)
    //      sb.Append(RecordSeparator);
    //  }

    //  sb.Append(GroupSeparator);

    //  return sb.ToString();
    //}
  }
}
