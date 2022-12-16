namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode block database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://unicode.org/Public/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
  public sealed partial class Blocks
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Ucd\Blocks.txt";
    public static System.Uri SourceUri
      => new(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public System.Uri Uri { get; private set; }

    public Blocks(System.Uri uri)
      => Uri = uri;

    public string[] FieldNames
      => new string[] { "StartCode", "EndCode", "BlockName" };
    public System.Type[] FieldTypes
      => new System.Type[] { typeof(int), typeof(int), typeof(string) };

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().GetEnumerator();

      while (e.MoveNext())
      {
        var objects = new object[e.Current.Length];

        for (var index = objects.Length - 1; index >= 0; index--)
        {
          objects[index] = index switch
          {
            0 or 1 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var intValue) ? intValue : System.DBNull.Value,
            _ => e.Current[index],
          };
        }

        yield return objects;
      }
    }


    [System.Text.RegularExpressions.GeneratedRegex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture)]
    private static partial System.Text.RegularExpressions.Regex SplitRegex();

    /// <summary>Returns Unicode blocks data. No field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
    {
      var reSplit = SplitRegex();

      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      foreach (var line in sr.ReadLines(false))
        if (line.Length > 0 && !line.StartsWith('#'))
          yield return reSplit.Split(line);
    }
  }
}
