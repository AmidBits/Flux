namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode character database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso href="https://www.unicode.org/Public/UCD/latest/ucd"/>
  /// <seealso href="https://unicode.org/Public/"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
  public sealed class UnicodeData
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\Ucd\UnicodeData.txt");
    public static readonly System.Uri Origin = new(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns the UCD UnicodeData information.</summary>
    public static System.Collections.Generic.IEnumerable<object[]> GetData(System.Uri uri)
    {
      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      foreach (var line in reader.ReadLines(false))
        yield return line.Split(';');
    }

    #region Implemented interfaces

    public string[] FieldNames => new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };
    public System.Type[] FieldTypes => FieldNames.Select((e, i) =>
    {
      return i switch
      {
        0 or 16 or 17 or 18 => typeof(int),
        6 or 7 or 8 or 9 or 10 => typeof(double),
        _ => typeof(string),
      };
    }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}

//namespace Flux.Resources.Ucd
//{
//  /// <summary>The Unicode character database.</summary>
//  /// <see cref="https://www.unicode.org/"/>
//  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
//  /// <seealso cref="https://unicode.org/Public/"/>
//  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
//  public sealed class UnicodeData
//    : ITabularDataAcquirable
//  {
//    public static string LocalFile
//      => @"file://\Resources\Ucd\UnicodeData.txt";
//    public static System.Uri SourceUri
//      => new(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

//    public System.Uri Uri { get; private set; }

//    public UnicodeData(System.Uri uri)
//      => Uri = uri;

//    public string[] FieldNames
//      => new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };
//    public Type[] FieldTypes
//      => FieldNames.Select((e, i) =>
//      {
//        return i switch
//        {
//          0 or 16 or 17 or 18 => typeof(int),
//          6 or 7 or 8 or 9 or 10 => typeof(double),
//          _ => typeof(string),
//        };
//      }).ToArray();

//    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
//      => GetObjects();

//    public System.Collections.Generic.IEnumerable<object[]> GetObjects()
//    {
//      return GetStrings().Select(strings =>
//      {
//        var objects = new object[strings.Length];

//        for (var index = strings.Length - 1; index >= 0; index--)
//        {
//          objects[index] = index switch
//          {
//            0 => int.TryParse(strings[index], System.Globalization.NumberStyles.HexNumber, null, out var value) ? value : System.DBNull.Value,
//            2 => strings[index][0].TryParseUnicodeCategoryMajorMinor(strings[index][1], out var value) ? value.ToUnicodeCategory() : System.DBNull.Value,
//            _ => strings[index],
//          };
//        }

//        return objects;
//      });
//    }

//    /// <summary>Returns Unicode data. No field names.</summary>
//    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
//    {
//      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

//      foreach (var line in sr.ReadLines(false))
//        yield return line.Split(';');
//    }
//  }
//}
