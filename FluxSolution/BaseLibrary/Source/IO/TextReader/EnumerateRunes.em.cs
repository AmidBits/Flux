namespace Flux
{
  public static partial class TextReaderEm
  {
    public static System.Collections.Generic.IEnumerable<(int index, System.Text.Rune value)> EnumerateRunes(this System.IO.TextReader source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new char[8192];
      var offset = 0;
      var length = source.Read(buffer, 0, buffer.Length);

      var indexInTextReader = 0;

      while (length > 0)
      {
        while (System.Text.Rune.DecodeFromUtf16(new System.ReadOnlySpan<char>(buffer, offset, length), out var rune, out var charsConsumed) is var operationStatus && operationStatus == System.Buffers.OperationStatus.Done)
        {
          offset += charsConsumed;
          length -= charsConsumed;

          yield return (indexInTextReader, rune);

          indexInTextReader += charsConsumed;
        }

        if (length > 0)
          System.Array.Copy(buffer, offset, buffer, 0, length);

        offset = 0;
        length += source.Read(buffer, length, buffer.Length - length);
      }
    }
  }
}

/*
      using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
        foreach (var rune in sr.EnumerateRunes())
          System.Console.Write(rune.ToString());
 */
