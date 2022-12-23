namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode character database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  /// <seealso cref="https://unicode.org/Public/"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
  public sealed class UnicodeData
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Ucd\UnicodeData.txt";
    public static System.Uri SourceUri
      => new(@"https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt");

    public System.Uri Uri { get; private set; }

    public UnicodeData(System.Uri uri)
      => Uri = uri;

    public string[] FieldNames
      => new string[] { "CodePoint", "Name", "GeneralCategory", "CanonicalCombiningClass", "BidiClass", "DecompositionTypeMapping", "NumericType6", "NumericType7", "NumericType8", "BidiMirrored", "Unicode1Name", "IsoComment", "SimpleUppercaseMapping", "SimpleLowercaseMapping", "SimpleTitlecaseMapping" };
    public Type[] FieldTypes
      => FieldNames.Select((e, i) =>
      {
        return i switch
        {
          0 or 16 or 17 or 18 => typeof(int),
          6 or 7 or 8 or 9 or 10 => typeof(double),
          _ => typeof(string),
        };
      }).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().GetEnumerator();

      if (e.MoveNext())
      {
        yield return e.Current;

        while (e.MoveNext())
        {
          var objects = new object[e.Current.Length];

          for (var index = objects.Length - 1; index >= 0; index--)
          {
            objects[index] = index switch
            {
              0 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var value) ? value : System.DBNull.Value,
              2 => e.Current[index][0].TryParseUnicodeCategoryMajorMinor(e.Current[index][1], out var value) ? value.ToUnicodeCategory() : System.DBNull.Value,
              _ => e.Current[index],
            };
          }

          yield return objects;
        }
      }
    }

    /// <summary>Returns Unicode data. No field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
    {
      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      foreach (var line in sr.ReadLines(false))
        yield return line.Split(';');
    }
  }
}
