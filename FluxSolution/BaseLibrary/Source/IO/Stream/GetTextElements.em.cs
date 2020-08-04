namespace Flux.IO
{
  public static partial class ExtensionMethodsStream
  {
    public static System.Collections.Generic.IEnumerable<string> GetTextElements(this System.IO.TextReader source)
    {
      var buffer = new char[4];
      var length = 0;

      var stringInfo = new System.Globalization.StringInfo();

      while (source.Read() is var read && read != -1)
      {
        buffer[length++] = (char)read;

        if (length == buffer.Length)
        {
          yield return getGrapheme();
        }
      }

      while (length > 0)
      {
        yield return getGrapheme();
      }

      string getGrapheme()
      {
        stringInfo.String = new string(buffer, 0, length);

        var grapheme = stringInfo.SubstringByTextElements(0, 1);

        length -= grapheme.Length;

        System.Array.Copy(buffer, grapheme.Length, buffer, 0, length);

        //for(int sourceIndex = grapheme.Length, targetIndex = 0, count = 0; count < length; count++)
        //{
        //  buffer[targetIndex] = buffer[sourceIndex];
        //}

        return grapheme;
      }
    }

    public static System.Collections.Generic.IEnumerable<string> GetTextElements(this System.IO.Stream stream, System.Text.Encoding encoding)
    {
      using var sr = new System.IO.StreamReader(stream, encoding ?? System.Text.Encoding.UTF8);
      
      foreach (var textElement in sr.GetTextElements())
      {
        yield return textElement;
      }
    }
  }
}
