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

    public System.Collections.Generic.Dictionary<System.Text.NormalizationForm, string> Forms
      => GetNormalizationForms(Value, false);

    public Token(int index, string value)
    {
      Index = index;
      Value = value;
    }

    public static System.Collections.Generic.Dictionary<System.Text.NormalizationForm, string> GetNormalizationForms(string text, bool include)
    {
      if (text is null) throw new System.ArgumentNullException(nameof(text));
      var forms = new System.Collections.Generic.Dictionary<System.Text.NormalizationForm, string>();
      if (text.Normalize(System.Text.NormalizationForm.FormC) is var c && (!c.Equals(text, System.StringComparison.CurrentCulture) || include) && !forms.ContainsValue(c)) forms.Add(System.Text.NormalizationForm.FormC, c);
      if (text.Normalize(System.Text.NormalizationForm.FormD) is var d && (!d.Equals(text, System.StringComparison.CurrentCulture) || include) && !forms.ContainsValue(d)) forms.Add(System.Text.NormalizationForm.FormD, d);
      if (text.Normalize(System.Text.NormalizationForm.FormKC) is var kc && (!kc.Equals(text, System.StringComparison.CurrentCulture) || include) && !forms.ContainsValue(kc)) forms.Add(System.Text.NormalizationForm.FormKC, kc);
      if (text.Normalize(System.Text.NormalizationForm.FormKD) is var kd && (!kd.Equals(text, System.StringComparison.CurrentCulture) || include) && !forms.ContainsValue(kd)) forms.Add(System.Text.NormalizationForm.FormKD, kd);
      return forms;
    }

    public override string ToString()
      => $"<\"{Value}\" @{Index}{(string.Concat(Forms./*Where(kvp => !kvp.Value.Equals(Text)).*/Select((kvp, i) => $"[{kvp.Key.ToString()}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $" {s}" : s)}>";
  }

  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public class Tokenizer
    : ITokenizer<Token>
  {
    public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
    {
      foreach (var grapheme in expression.GetTextElements((text, index) => (text, index)))
      {
        yield return new Token(grapheme.index, grapheme.text);
      }
    }
  }
}
