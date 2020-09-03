using System.Linq;

namespace Flux.Resources.Scowl
{
  /// <summary>The records from 2Of12Full word list.</summary>
  /// <see cref="https://github.com/en-wl/wordlist"/>
  // Download URL: https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt
  /// <seealso cref="http://wordlist.aspell.net/"/>
  /// <seealso cref="https://github.com/en-wl/wordlist/blob/master/"/>
  public static partial class TwoOfTwelveFull
  {
    public static System.Collections.Generic.IList<string> FieldNames
      => new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };

    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Scowl\2of12full.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://raw.githubusercontent.com/en-wl/wordlist/master/alt12dicts/2of12full.txt");

    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> GetData(System.Uri uri)
      => uri.ReadLines(System.Text.Encoding.UTF8).Select(s => System.Text.RegularExpressions.Regex.Split(s.Trim(), @"(?<=[\-0-9]+[:#&=]?)\s+"));

    public static Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>>(GetData(uri), dr => (System.Collections.Generic.IList<object>)dr, FieldNames);
  }
}
