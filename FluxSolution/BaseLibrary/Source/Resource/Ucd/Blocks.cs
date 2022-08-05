namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode block database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://unicode.org/Public/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
  public sealed class Blocks
    : ATabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\Ucd\Blocks.txt";
    public static System.Uri UriSource
      => new(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public System.Uri Uri { get; private set; }

    public Blocks(System.Uri uri)
      => Uri = uri;

    public override string[] FieldNames
      => new string[] { "StartCode", "EndCode", "BlockName" };
    public override System.Type[] FieldTypes
      => new System.Type[] { typeof(int), typeof(int), typeof(string) };

    public override System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
    {
      using var e = GetStrings().GetEnumerator();

      while (e.MoveNext())
      {
        var objects = new object[e.Current.Length];

        for (var index = objects.Length - 1; index >= 0; index--)
        {
          switch (index)
          {
            case 0:
            case 1:
              objects[index] = int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var intValue) ? intValue : System.DBNull.Value;
              break;
            default:
              objects[index] = e.Current[index];
              break;
          }
        }

        yield return objects;
      }
    }

    /// <summary>Returns Unicode blocks data. No field names.</summary>
    public System.Collections.Generic.IEnumerable<string[]> GetStrings()
    {
      var m_reSplit = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      foreach (var array in sr.ReadLines(false).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplit.Split(line)))
        yield return array;
    }
  }
}
