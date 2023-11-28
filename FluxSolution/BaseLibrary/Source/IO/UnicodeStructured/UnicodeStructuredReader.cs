namespace Flux.IO
{
  public sealed class UnicodeStructuredReader
  {
    private readonly System.IO.TextReader m_reader;

    private int m_lastRead;

    private UnicodeStructuredReader() { m_reader = default!; m_lastRead = default; }
    public UnicodeStructuredReader(System.IO.TextReader reader) => m_reader = reader;

    public int LastRead => m_lastRead;

    public string ReadUnicodeUnit()
    {
      var unit = new System.Text.StringBuilder();

      for (m_lastRead = m_reader.Read(); m_lastRead > -1; m_lastRead = m_reader.Read())
      {
        if (m_lastRead == (int)UnicodeCodepoint.UnitSeparator || m_lastRead == (int)UnicodeCodepoint.RecordSeparator || m_lastRead == (int)UnicodeCodepoint.GroupSeparator)
          break;
        if (m_lastRead == (int)UnicodeCodepoint.SymbolForUnitSeparator || m_lastRead == (int)UnicodeCodepoint.SymbolForRecordSeparator || m_lastRead == (int)UnicodeCodepoint.SymbolForGroupSeparator)
          break;
        else
          unit.Append((char)m_lastRead);
      }

      return unit.ToString();
    }

    public System.Collections.Generic.List<string> ReadUnicodeRecord()
    {
      var record = new System.Collections.Generic.List<string>();

      do
      {
        var unit = ReadUnicodeUnit();

        record.Add(unit);
      }
      while (m_lastRead == (int)UnicodeCodepoint.UnitSeparator || m_lastRead == (int)UnicodeCodepoint.SymbolForUnitSeparator);

      return record;
    }

    public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<string>> ReadUnicodeRecords()
    {
      do
      {
        yield return ReadUnicodeRecord();
      }
      while (m_lastRead == (int)UnicodeCodepoint.RecordSeparator || m_lastRead == (int)UnicodeCodepoint.SymbolForRecordSeparator);
    }

    public string[][] ReadUnicodeGroup()
    {
      var group = new System.Collections.Generic.List<string[]>();

      do
      {
        var record = ReadUnicodeRecord();

        group.Add(record.ToArray());
      }
      while (m_lastRead == (int)UnicodeCodepoint.RecordSeparator || m_lastRead == (int)UnicodeCodepoint.SymbolForRecordSeparator);

      return group.ToArray();
    }
  }
}
