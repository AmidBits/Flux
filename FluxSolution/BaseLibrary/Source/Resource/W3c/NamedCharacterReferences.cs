using System.Linq;

namespace Flux.Resources.W3c
{
	/// <summary>The character reference names that are supported by HTML, and the code points to which they refer.</summary>
	public class NamedCharacterReferences
		: ResourceFactory
	{
		public static System.Uri LocalUri
			=> new System.Uri(@"file://\Resources\W3c\NamedCharacterReferences.json");
		public static System.Uri SourceUri
			=> new System.Uri(@"https://html.spec.whatwg.org/entities.json");

		public override System.Collections.Generic.IList<string> FieldNames
			=> new string[] { "Name", "CodePoints", "Characters", "CharactersAsString" };
		public override System.Collections.Generic.IList<System.Type> FieldTypes
			=> new System.Type[] { typeof(string), typeof(string), typeof(string), typeof(string) };

		public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
		{
			yield return (string[])FieldNames;

			var m_reUnicodeNotation = new System.Text.RegularExpressions.Regex(@"\\u([0-9a-fA-F]{4})|\\U([0-9a-fA-F]{8})", System.Text.RegularExpressions.RegexOptions.Compiled);

			if (uri is null) throw new System.ArgumentNullException(nameof(uri));

			using var jd = System.Text.Json.JsonDocument.Parse(uri.GetStream().ReadAllText(System.Text.Encoding.UTF8));

			foreach (var jp in jd.RootElement.EnumerateObject())
			{
				var codepoints = string.Join(@",", jp.Value.GetProperty(@"codepoints").EnumerateArray().Select(e => e.GetInt32()));
				var characters = jp.Value.GetProperty(@"characters").GetRawText().Trim().Replace(@"""", string.Empty, System.StringComparison.Ordinal);
				var charactersAsString = m_reUnicodeNotation.Replace(characters, match => ((char)int.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture)).ToString());

				yield return new string[] { jp.Name, codepoints, characters, charactersAsString };
			}
		}
	}
}

//System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
//foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.SourceUri))
//  System.Console.WriteLine(string.Join('|', strings));
