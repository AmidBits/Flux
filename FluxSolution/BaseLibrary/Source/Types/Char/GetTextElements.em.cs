namespace Flux
{
  public static partial class ExtensionMethodsChar
  {
    /// <summary>Returns a new sequence of text elements (grapheme strings) from the sequence of characters.</summary>
    /// <remarks>The process uses a small buffer of characters.</remarks>
    public static System.Collections.Generic.IEnumerable<string> GetTextElements(this System.Collections.Generic.IEnumerable<char> chars)
    {
      var buffer = new char[8];
      var length = 0;

      var stringInfo = new System.Globalization.StringInfo();

      foreach (var c in chars ?? throw new System.ArgumentNullException(nameof(chars)))
      {
        buffer[length++] = c;

        if (length == buffer.Length)
          yield return getGrapheme();
      }

      while (length > 0)
        yield return getGrapheme();

      string getGrapheme()
      {
        stringInfo.String = new string(buffer, 0, length);

        var grapheme = stringInfo.SubstringByTextElements(0, 1);

        length -= grapheme.Length;

        System.Array.Copy(buffer, grapheme.Length, buffer, 0, length);

        return grapheme;
      }
    }
  }
}
