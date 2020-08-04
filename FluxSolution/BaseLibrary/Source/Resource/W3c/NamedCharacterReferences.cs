using System.Linq;

namespace Flux.Resources.W3c
{
  /// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
  public static partial class NamedCharacterReferences
  {
    private static readonly System.Text.RegularExpressions.Regex m_reUnicode = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

    public static System.Collections.Generic.IList<string> FieldNames
      => new string[] { "Name", "CodePoints", "Characters", "charactersAsString" };

    public static System.Uri LocalUri
      => new System.Uri(@"file://\Resources\W3c\NamedCharacterReferences.json");
    public static System.Uri SourceUri
      => new System.Uri(@"https://html.spec.whatwg.org/entities.json");

#if NETCOREAPP || NETSTANDARD2_1
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> GetData(System.Uri uri)
    {
      var s = string.Empty;

      if (uri.Equals(LocalUri))
      {
        var localFilePath = LocalUri.AbsoluteUri.Replace(@"file://", ".");

        s = System.IO.File.ReadAllText(localFilePath);
      }
      else
      {
        s = uri.ReadAllText(System.Text.Encoding.UTF8);
      }

      using var jd = System.Text.Json.JsonDocument.Parse(s);

      foreach (var jp in jd.RootElement.EnumerateObject())
      {
        var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
        var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty);
        var charactersAsString = m_reUnicode.Replace(characters, match => ((char)System.Int32.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber)).ToString());

        yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
      }
    }

    public static Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<System.Collections.Generic.IList<string>>(GetData(uri), dr => (System.Collections.Generic.IList<object>)dr) { FieldNames = FieldNames, FieldTypes = FieldNames.Select(fn => typeof(string)).ToArray() };
#endif
  }
}
