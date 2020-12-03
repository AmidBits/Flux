using System.Linq;

/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux.Text.Tokenization.Grapheme
{
	/// <summary>An implementation of a demarcated and classified section of a grapheme.</summary>
	public class Token
		: IToken<string>
	{
		public int Index { get; }
		public string Value { get; }

		public Token(int index, string value)
		{
			Index = index;
			Value = value;
		}

		public static System.Collections.Generic.Dictionary<System.Text.NormalizationForm, string> GetNormalizationForms(string text, bool include)
		{
			if (text is null) throw new System.ArgumentNullException(nameof(text));

			var forms = new System.Collections.Generic.Dictionary<System.Text.NormalizationForm, string>();
			if (text.Normalize(System.Text.NormalizationForm.FormC) is var c && (include || !c.Equals(text, System.StringComparison.CurrentCulture)) && !forms.ContainsValue(c))
				forms.Add(System.Text.NormalizationForm.FormC, c);
			if (text.Normalize(System.Text.NormalizationForm.FormD) is var d && (include || !d.Equals(text, System.StringComparison.CurrentCulture)) && !forms.ContainsValue(d))
				forms.Add(System.Text.NormalizationForm.FormD, d);
			if (text.Normalize(System.Text.NormalizationForm.FormKC) is var kc && (include || !kc.Equals(text, System.StringComparison.CurrentCulture)) && !forms.ContainsValue(kc))
				forms.Add(System.Text.NormalizationForm.FormKC, kc);
			if (text.Normalize(System.Text.NormalizationForm.FormKD) is var kd && (include || !kd.Equals(text, System.StringComparison.CurrentCulture)) && !forms.ContainsValue(kd))
				forms.Add(System.Text.NormalizationForm.FormKD, kd);
			return forms;
		}

		public override string ToString()
			=> $"<\"{Value}\" @{Index}{(string.Concat(GetNormalizationForms(Value, false).Select((kvp, i) => $"[{kvp.Key}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $" {s}" : string.Empty)}>";
	}

	/// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
	public class Tokenizer
		: ITokenizer<Token>
	{
		public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
		{
			using var sr = new System.IO.StringReader(expression);
			using var trtee = new TextElementEnumerator(sr);

			foreach (var (text, index) in trtee.Select((e, i) => (e, i)))
			{
				yield return new Token(index, text);
			}
		}
	}
}
