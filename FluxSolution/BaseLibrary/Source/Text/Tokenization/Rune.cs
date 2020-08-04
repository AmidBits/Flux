/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text.Tokenization.Rune
{
  /// <summary>An implementation of a demarcated and classified section of a rune.</summary>
  public class Token
    : IToken<System.Text.Rune>
  {
    public int Index { get; }
    public System.Text.Rune Value { get; }

    public System.Globalization.UnicodeCategory UnicodeCategory
      => System.Text.Rune.GetUnicodeCategory(Value);
    public Unicode.CategoryMajor UnicodeCategoryMajor
      => System.Text.Rune.GetUnicodeCategory(Value).ToCategoryMajor();

    public Token(int index, System.Text.Rune value)
    {
      Index = index;
      Value = value;
    }

    public override string ToString() => $"<{UnicodeCategoryMajor} {UnicodeCategory} @{Index} \"{Value}\" +{Value.Utf16SequenceLength}>";
  }

  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public class Tokenizer
    : ITokenizer<Token>
  {
    public bool Normalize { get; } = true;

    public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      if (Normalize) expression = expression.Normalize() ?? throw new System.NullReferenceException(nameof(expression.Normalize));

      var index = 0;

      foreach (var rune in expression.EnumerateRunes())
      {
        yield return new Token(index, rune);

        index += rune.Utf16SequenceLength;
      }
    }
  }
}
