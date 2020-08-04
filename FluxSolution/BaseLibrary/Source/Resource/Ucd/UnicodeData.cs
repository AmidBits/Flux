using System.Linq;

namespace Flux.Resources.Ucd
{
  /// <summary></summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  public static partial class UnicodeData
  {
    public static System.Collections.Generic.IList<string> FieldNames
      => new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };

    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Ucd\UnicodeData.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> GetData(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Select(s => s.Split(';'));

    /// <summary>The Unicode character database.</summary>
    /// <see cref="https://unicode.org/Public/"/>
    // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
    public static Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>>(GetData(uri), dr => (System.Collections.Generic.IList<object>)dr, FieldNames) { FieldTypes = FieldNames.Select(fn => typeof(string)).ToList() };
  }
}
