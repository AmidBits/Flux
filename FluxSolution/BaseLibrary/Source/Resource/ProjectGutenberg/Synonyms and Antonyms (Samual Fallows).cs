using System.Linq;

namespace Flux.Resources.ProjectGutenberg
{
  /// <summary>A Complete Dictionary of Synonyms and Antonyms by Samuel Fallows (Acdsasf).</summary>
  /// <remarks>Returns keywords, synonyms and antonyms.</summary>
  /// <see cref="http://www.gutenberg.org/ebooks/51155"/>
  public class SynonymsAndAntonymsSamuelFallows
    : DataShaper
  {
    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\ProjectGutenberg\51155-0.txt");
    public static System.Uri SourceUri
      => new System.Uri(@"http://www.gutenberg.org/files/51155/51155-0.txt");

    public override System.Collections.Generic.IList<string> FieldNames
      => new string[] { "Keywords", "Synonyms", "Antonyms" };
    public override System.Collections.Generic.IList<System.Type> FieldTypes
      => new System.Type[] { typeof(string), typeof(string), typeof(string) };

    private static readonly System.Text.RegularExpressions.Regex m_reSections = new System.Text.RegularExpressions.Regex(@"(?<=(KEY:|SYN:|ANT:))\s", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    public static System.Collections.Generic.IEnumerable<string[][]> GetArrays(System.Uri uri)
    {
      var lines = new System.Text.StringBuilder();

      foreach (var line in uri.ReadLines(System.Text.Encoding.UTF8))
      {
        if (line == @"=" || line.Length == 0)
        {
          if (lines.StartsWith(@"KEY:"))
          {
            var list = m_reSections.Replace(lines.ToString(), @",").ToLower(System.Globalization.CultureInfo.CurrentCulture).Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim(' ', '.')).Where(s => s.Length > 0).ToList();

            var iKey = list.IndexOf(@"key:");
            var iSyn = list.IndexOf(@"syn:");
            var iAnt = list.IndexOf(@"ant:");

            var aKey = iKey > -1 ? list.GetRange(iKey + 1, (iSyn > -1 ? iSyn : iAnt > -1 ? iAnt : list.Count) - 1).ToArray() : System.Array.Empty<string>();
            var aSyn = iSyn > -1 ? list.GetRange(iSyn + 1, (iAnt > -1 ? iAnt - iSyn : list.Count - iSyn) - 1).ToArray() : System.Array.Empty<string>();
            var aAnt = iAnt > -1 ? list.GetRange(iAnt + 1, (list.Count - iAnt) - 1).ToArray() : System.Array.Empty<string>();

            if (aKey.Length > 0) yield return new string[][] { aKey, aSyn, aAnt };
          }

          lines.Clear();
        }
        else
        {
          if (lines.Length > 0) lines.Append(',');

          lines.Append(line);
        }
      }
    }

    /// <summary>Returns an IEnumerable<string[]> with keywords, synonyms and antonyms.</summary>
    public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      => GetArrays(uri).Select(a => new string[] { string.Join(@",", a[0]), string.Join(@",", a[1]), string.Join(@",", a[2]) });
  }
}
