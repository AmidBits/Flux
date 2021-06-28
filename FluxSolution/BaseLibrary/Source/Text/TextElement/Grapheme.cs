using System.Linq;

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
      => $"<{GetType().Name}: {{{Chars}}}, {Chars.Length} chars \"{string.Concat(Chars.Select(c => Text.UnicodeStringLiteral.ToString(c)))}\", {Runes.Count} runes [{string.Join(',', Runes.Select(r => Text.UnicodeNotation.ToString(r)))}]>";
  }
}
