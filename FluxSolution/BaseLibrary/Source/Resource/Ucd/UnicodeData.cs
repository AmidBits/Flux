using System.Linq;

namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode character database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  /// <seealso cref="https://unicode.org/Public/"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
  public class UnicodeData
    : DataFactory
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Ucd\UnicodeData.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Select(s => s.Split(';'));

    public override object ConvertStringToObject(int index, string value)
    {
      return index switch
      {
        0 => int.Parse(value, System.Globalization.NumberStyles.HexNumber, null),
        _ => value
      };
    }
  }
}
