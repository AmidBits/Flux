namespace Flux.IO
{
  // https://www.w3.org/TR/xml/#NT-Char
  // https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlreader
  // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/io/textreader.cs
  public sealed class FilterInvalidXmlReader(System.IO.Stream stream)
    : System.IO.TextReader
  {
    private readonly System.IO.StreamReader m_streamReader = new(stream);

    public System.IO.Stream BaseStream => m_streamReader.BaseStream;

    public override void Close() => m_streamReader.Close();

    protected override void Dispose(bool disposing)
    {
      m_streamReader.Dispose();

      base.Dispose(disposing);
    }

    public override int Peek()
    {
      var peek = m_streamReader.Peek();

      while (IsInvalid(peek, true))
      {
        m_streamReader.Read();

        peek = m_streamReader.Peek();
      }

      return peek;
    }

    public override int Read()
    {
      var read = m_streamReader.Read();

      while (IsInvalid(read, true))
      {
        read = m_streamReader.Read();
      }

      return read;
    }

    public static bool IsInvalid(int c, bool invalidateCompatibilityCharacters)
    {
      if (c == -1)
      {
        return false;
      }

      if (invalidateCompatibilityCharacters && ((c >= 0x7F && c <= 0x84) || (c >= 0x86 && c <= 0x9F) || (c >= 0xFDD0 && c <= 0xFDEF)))
      {
        return true;
      }

      if (c == 0x9 || c == 0xA || c == 0xD || (c >= 0x20 && c <= 0xD7FF) || (c >= 0xE000 && c <= 0xFFFD))
      {
        return false;
      }

      return true;
    }
  }
}
