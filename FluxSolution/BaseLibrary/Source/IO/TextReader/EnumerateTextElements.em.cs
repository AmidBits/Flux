namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<(int index, string value)> EnumerateTextElements(this System.IO.TextReader source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new char[8192];
      var offset = 0;
      var length = source.Read(buffer, 0, buffer.Length);

      var indexInTextReader = 0;

      while (length > 0)
      {
        var stringInfo = new System.Globalization.StringInfo(new string(buffer, offset, length));

        for (int index = 0, count = stringInfo.LengthInTextElements; index < count; index++)
        {
          var textElement = stringInfo.SubstringByTextElements(index, 1);
          offset += textElement.Length;
          length -= textElement.Length;

          yield return (indexInTextReader, textElement);

          indexInTextReader += textElement.Length;
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
        foreach (var textElement in sr.EnumerateTextElements())
          System.Console.Write(textElement.ToString());
 */
