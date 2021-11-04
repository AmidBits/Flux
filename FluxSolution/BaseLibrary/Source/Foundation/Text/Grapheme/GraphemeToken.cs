using System.Linq;

/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a demarcated and classified section of a grapheme.</summary>
  public sealed class GraphemeToken
    : IToken<GraphemeCluster>
  {
    public int Index { get; }
    public GraphemeCluster Value { get; }

    public GraphemeToken(int index, GraphemeCluster value)
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
      => $"<\"{Value}\" @{Index}{(string.Concat(GetNormalizationForms(Value.Chars, false).Select((kvp, i) => $"[{kvp.Key}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $" {s}" : string.Empty)}>";
  }
}
