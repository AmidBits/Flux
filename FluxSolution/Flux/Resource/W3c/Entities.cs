namespace Flux
{
  public static partial class Resource
  {
    #region W3c

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex RegexW3cEntities();

    /// <summary>
    /// <para>The character reference names that are supported by HTML, and the code points to which they refer.</para>
    /// <para>Local: <see href="file://\Resources\W3c\NamedCharacterReferences.json"/></para>
    /// <para>Remote: <see href="https://html.spec.whatwg.org/entities.json"/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<string[]> GetW3cEntities(string file = @"file://\Resources\W3c\NamedCharacterReferences.json")
    {
      yield return new string[] { "Name", "CodePoints", "Characters", "CharactersAsString" };

      using var stream = new System.Uri(file).GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      using var jd = System.Text.Json.JsonDocument.Parse(reader.ReadToEnd());

      foreach (var jp in jd.RootElement.EnumerateObject())
      {
        var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
        var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
        var charactersAsString = RegexW3cEntities().Replace(characters, match => ((char)int.Parse(match.Value[2..], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

        yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
      }
    }

    #endregion // W3c
  }
}
