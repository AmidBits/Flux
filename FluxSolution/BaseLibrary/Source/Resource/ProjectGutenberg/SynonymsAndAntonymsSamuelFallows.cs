using System.Linq;

namespace Flux.Resources.ProjectGutenberg
{
  public sealed class SynonymsAndAntonymsSamuelFallows
    : ATabularDataAcquirer
  {
    public static string LocalFile
      => @"file://\Resources\ProjectGutenberg\51155-0.txt";
    public static System.Uri UriSource
      => new(@"http://www.gutenberg.org/files/51155/51155-0.txt");

    public System.Uri Uri { get; private set; }

    public SynonymsAndAntonymsSamuelFallows(System.Uri uri)
      => Uri = uri;

    /// <summary>A Complete Dictionary of Synonyms and Antonyms by Samuel Fallows (Acdsasf).</summary>
    /// <remarks>Returns keywords, synonyms and antonyms.</summary>
    /// <see cref="http://www.gutenberg.org/ebooks/51155"/>
    public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
    {
      yield return new string[] { "Keywords", "Synonyms", "Antonyms" };

      var reSection = new System.Text.RegularExpressions.Regex(@"(?<=(KEY:|SYN:|ANT:))\s", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

      foreach (var item in EnumerateArrays().Select(a => new string[] { string.Join(@",", a[0]), string.Join(@",", a[1]), string.Join(@",", a[2]) }))
        yield return item;

      System.Collections.Generic.IEnumerable<string[][]> EnumerateArrays()
      {
        var lines = new System.Text.StringBuilder();

        using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

        foreach (var line in sr.ReadLines(false))
        {
          if (line == @"=" || line.Length == 0)
          {
            if (lines.StartsWith(@"KEY:"))
            {
              var list = reSection.Replace(lines.ToString(), @",").ToLower(System.Globalization.CultureInfo.CurrentCulture).Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim(' ', '.')).Where(s => s.Length > 0).ToList();

              var iKey = list.IndexOf(@"key:");
              var iSyn = list.IndexOf(@"syn:");
              var iAnt = list.IndexOf(@"ant:");

              var aKey = iKey > -1 ? list.GetRange(iKey + 1, (iSyn > -1 ? iSyn : iAnt > -1 ? iAnt : list.Count) - 1).ToArray() : System.Array.Empty<string>();
              var aSyn = iSyn > -1 ? list.GetRange(iSyn + 1, (iAnt > -1 ? iAnt - iSyn : list.Count - iSyn) - 1).ToArray() : System.Array.Empty<string>();
              var aAnt = iAnt > -1 ? list.GetRange(iAnt + 1, (list.Count - iAnt) - 1).ToArray() : System.Array.Empty<string>();

              if (aKey.Length > 0)
                yield return new string[][] { aKey, aSyn, aAnt };
            }

            lines.Clear();
          }
          else
          {
            if (lines.Length > 0)
              lines.Append(',');

            lines.Append(line);
          }
        }
      }
    }
  }
}
