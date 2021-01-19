using System.Linq;

namespace Flux.Resources.W3c
{
  public static class NamedCharacterReferences
  {
    public static System.Uri UriLocal
      => new System.Uri(@"file://\Resources\W3c\NamedCharacterReferences.json");
    public static System.Uri UriSource
      => new System.Uri(@"https://html.spec.whatwg.org/entities.json");

    /// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
    {
      yield return new string[] { "Name", "CodePoints", "Characters", "CharactersAsString" };

      if (uri is null) throw new System.ArgumentNullException(nameof(uri));

      var m_reMatchUnicodeNotation = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var jd = System.Text.Json.JsonDocument.Parse(uri.GetStream().ReadAllText(System.Text.Encoding.UTF8));

      foreach (var jp in jd.RootElement.EnumerateObject())
      {
        var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
        var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
        var charactersAsString = m_reMatchUnicodeNotation.Replace(characters, match => ((char)int.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

        yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
      }
    }
  }
}
