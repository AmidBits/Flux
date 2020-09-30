using System.Linq;

namespace Flux.Resources.Ucd
{
  /// <summary>The Unicode block database.</summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://unicode.org/Public/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
  public class Blocks
    : DataFactory
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Ucd\Blocks.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "StartCode", "EndCode", "BlockName" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(int), typeof(int), typeof(string) };

    private static System.Text.RegularExpressions.Regex m_reSplitter = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplitter.Split(line));

    public override object ConvertStringToObject(int index, string value)
    {
      return index switch
      {
        0 => int.Parse(value, System.Globalization.NumberStyles.HexNumber, null),
        1 => int.Parse(value, System.Globalization.NumberStyles.HexNumber, null),
        _ => value
      };
    }
  }
}
