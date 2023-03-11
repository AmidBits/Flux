using System.Linq;

namespace Flux.Resources.W3c
{
  /// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
  public sealed class NamedCharacterReferences
    : ITabularDataAcquirable
  {
    public static string LocalFile
      => @"file://\Resources\W3c\NamedCharacterReferences.json";
    public static System.Uri SourceUri
      => new(@"https://html.spec.whatwg.org/entities.json");
    public System.Uri Uri { get; private set; }

    public NamedCharacterReferences(System.Uri uri)
      => Uri = uri;

    public string[] FieldNames
      => new string[] { "Name", "CodePoints", "Characters", "CharactersAsString" };
    public Type[] FieldTypes
      => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues()
      => GetStrings();

    /// <summary>Returns W3c named character reference data. No field names.</summary>
    public System.Collections.Generic.IEnumerable<object[]> GetStrings()
    {
      var m_reMatchUnicodeNotation = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

      using var jd = System.Text.Json.JsonDocument.Parse(sr.ReadToEnd());

      foreach (var jp in jd.RootElement.EnumerateObject())
      {
        var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
        var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
        var charactersAsString = m_reMatchUnicodeNotation.Replace(characters, match => ((char)int.Parse(match.Value[2..], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

        yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
      }
    }
  }
}
