namespace Flux.Resources.W3c
{
  /// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
  public sealed class NamedCharacterReferences
    : ITabularDataAcquirable
  {
    public static readonly System.Uri Local = new(@"file://\Resources\W3c\NamedCharacterReferences.json");
    public static readonly System.Uri Origin = new(@"https://html.spec.whatwg.org/entities.json");

    public System.Uri Uri { get; private set; } = Local;

    /// <summary>Returns W3c named character reference data. No field names.</summary>
    public static System.Collections.Generic.IEnumerable<string[]> GetData(System.Uri uri)
    {
      var m_reMatchUnicodeNotation = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

      using var stream = uri.GetStream();
      using var reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

      using var jd = System.Text.Json.JsonDocument.Parse(reader.ReadToEnd());

      foreach (var jp in jd.RootElement.EnumerateObject())
      {
        var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
        var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
        var charactersAsString = m_reMatchUnicodeNotation.Replace(characters, match => ((char)int.Parse(match.Value[2..], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

        yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
      }
    }

    #region Implemented interfaces

    public string[] FieldNames => ["Name", "CodePoints", "Characters", "CharactersAsString"];
    public Type[] FieldTypes => FieldNames.Select(s => typeof(string)).ToArray();

    public System.Collections.Generic.IEnumerable<object[]> GetFieldValues() => GetData(Uri);

    #endregion // Implemented interfaces
  }
}
