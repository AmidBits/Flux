namespace Flux.IO
{
  public sealed class UnicodeStructuredWriter
  {
    private System.IO.TextWriter m_writer;

    private bool m_useSymbolsInsteadOfControl;

    private UnicodeStructuredWriter() { m_writer = default!; m_useSymbolsInsteadOfControl = default; }
    public UnicodeStructuredWriter(System.IO.TextWriter writer) => m_writer = writer;

    public bool UseSymbolsInsteadOfControl { get => m_useSymbolsInsteadOfControl; init => m_useSymbolsInsteadOfControl = value; }

    public void WriteUnicodeGroup(string[][] jaggedArray)
    {
      var rowLength = jaggedArray.Length;

      for (var ri = 0; ri < rowLength; ri++)
      {
        if (ri > 0) m_writer.Write((char)(m_useSymbolsInsteadOfControl ? UnicodeCodepoint.SymbolForRecordSeparator : UnicodeCodepoint.RecordSeparator));

        var columnLength = jaggedArray[ri].Length;

        for (var ci = 0; ci < columnLength; ci++)
        {
          if (ci > 0) m_writer.Write((char)(m_useSymbolsInsteadOfControl ? UnicodeCodepoint.SymbolForUnitSeparator : UnicodeCodepoint.UnitSeparator));

          m_writer.Write(jaggedArray[ri][ci]);
        }
      }
    }
  }
}
