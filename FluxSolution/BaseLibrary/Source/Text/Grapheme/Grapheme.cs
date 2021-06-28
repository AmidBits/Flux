namespace Flux.Text
{
  public class Grapheme
  {
    public string Chars { get; }
    public System.Collections.Generic.IReadOnlyList<System.Text.Rune> Runes { get; }

    public Grapheme(string textElement)
    {
      if (textElement is null) throw new System.ArgumentNullException(nameof(textElement));
      if (new System.Globalization.StringInfo(textElement).LengthInTextElements != 1) throw new System.ArgumentOutOfRangeException(textElement);

      Chars = textElement;
      Runes = new System.Collections.Generic.List<System.Text.Rune>(textElement.EnumerateRunes());
    }

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();

      sb.Append('<');
      sb.Append(GetType().Name);
      sb.Append('{');
      sb.Append(Chars);
      sb.Append('}');
      sb.Append(',');
      sb.Append(' ');
      sb.Append(Chars.Length);
      sb.Append(@" char");
      if (Chars.Length > 1)
        sb.Append('s');
      sb.Append(' ');
      sb.Append('"');
      for (var ci = 0; ci < Chars.Length; ci++)
      {
        sb.Append(Text.UnicodeStringLiteral.ToString(Chars[ci]));
      }
      sb.Append('"');
      sb.Append(',');
      sb.Append(' ');
      sb.Append(Runes.Count);
      sb.Append(@" rune");
      if (Runes.Count > 1)
        sb.Append('s');
      sb.Append(' ');
      sb.Append('(');
      for (var ri = 0; ri < Runes.Count; ri++)
      {
        if (ri > 0) sb.Append(',');
        sb.Append(Text.UnicodeNotation.ToString(Runes[ri]));
      }
      sb.Append(')');
      sb.Append('>');

      return sb.ToString();
    }

    public static string Pluralize(string word)
    {
      if (System.Text.RegularExpressions.Regex.IsMatch(word, @"(s|z|x|sh|ch|ss|z)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        return $"{word}es";
      else if (System.Text.RegularExpressions.Regex.IsMatch(word, @"[bcdfghjklmnpqrstvwxyz]y$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        return $"{word}ies";
      else if (System.Text.RegularExpressions.Regex.IsMatch(word, @"[bcdfghjklmnpqrstvwxyz]o$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
        return $"{word}es";
      else
      return $"{word}s";
    }
  }
}
