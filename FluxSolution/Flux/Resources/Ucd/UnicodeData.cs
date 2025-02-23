namespace Flux
{
  public static partial class Resources
  {
    #region UCD UnicodeData

    /// <summary>
    /// <para>The Unicode character database.</para>
    /// <see href="https://www.unicode.org/"/>
    /// <seealso href="https://www.unicode.org/Public/UCD/latest/ucd"/>
    /// <seealso href="https://unicode.org/Public/"/>
    /// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
    /// <para>Local: <see href="file://\Resources\Ucd\UnicodeData.txt"/></para>
    /// <para>Remote: <see href="https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt"/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetUcdUnicodeData(string file = @"file://\Resources\Ucd\UnicodeData.txt")
    {
      yield return new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };

      using var stream = new System.Uri(file).GetStream();
      using var reader = new StreamReader(stream);

      foreach (var fields in reader.ReadCsv())
        yield return fields;
    }

    #endregion // UCD UnicodeData
  }
}
