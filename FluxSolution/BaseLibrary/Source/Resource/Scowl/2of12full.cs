using System.Linq;

namespace Flux.Resources.Scowl
{
  /// <summary>The records from 2Of12Full word list.</summary>
  /// <see cref="https://github.com/en-wl/wordlist"/>
  // Download URL: https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt
  /// <seealso cref="http://wordlist.aspell.net/"/>
  /// <seealso cref="https://github.com/en-wl/wordlist/blob/master/"/>
  public class TwoOfTwelveFull
    : ResourceFactory
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\Scowl\2of12full.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"https://raw.githubusercontent.com/en-wl/wordlist/master/alt12dicts/2of12full.txt");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };

    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      => uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Select(s => System.Text.RegularExpressions.Regex.Split(s.Trim(), @"(?<=[\-0-9]+[:#&=]?)\s+")).Prepend((string[])FieldNames);
  }
}

//System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
//foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.SourceUri))
//  System.Console.WriteLine(string.Join('|', strings));
