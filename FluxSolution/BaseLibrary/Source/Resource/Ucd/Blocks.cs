using System.Linq;

namespace Flux.Resources.Ucd
{
  /// <summary></summary>
  /// <see cref="https://www.unicode.org/"/>
  /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
  public static partial class Blocks
  {
    private static System.Text.RegularExpressions.Regex m_reSplitter = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

    public static System.Collections.Generic.IList<string> FieldNames
      => new string[] { "StartCode", "EndCode", "BlockName" };

    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Ucd\Blocks.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> GetData(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplitter.Split(line));

    /// <summary>The Unicode character database.</summary>
    /// <see cref="https://unicode.org/Public/"/>
    // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt
    public static Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>>(GetData(uri), dr => (System.Collections.Generic.IList<object>)dr, FieldNames) { FieldTypes = FieldNames.Select(fn => typeof(string)).ToList() };
  }
}
