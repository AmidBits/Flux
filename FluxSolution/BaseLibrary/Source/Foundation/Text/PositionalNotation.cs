using System;

namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
  public ref struct PositionalNotation
  {
    public static PositionalNotation Base2
      => new(Sequences.Base62[..2]);
    public static PositionalNotation Base8
      => new(Sequences.Base62[..8]);
    public static PositionalNotation Base10
      => new(Sequences.Base62[..10]);
    public static PositionalNotation Base16
      => new(Sequences.Base62[..16]);

    public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

    public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols)
      => Symbols = symbols;

    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    /// System.Collections.Generic.IList<string>
    [System.Diagnostics.Contracts.Pure]
    public System.Text.StringBuilder NumberToText(System.Numerics.BigInteger number)
    {
      var sb = new System.Text.StringBuilder(128);

      if (number.IsZero)
      {
        sb.Append('0');
      }
      else while (number != 0)
        {
          number = System.Numerics.BigInteger.DivRem(number, Symbols.Length, out var remainder);

          sb.Insert(0, Symbols[(int)System.Numerics.BigInteger.Abs(remainder)]);
        }

      return sb;
    }
    [System.Diagnostics.Contracts.Pure]
    public bool TryNumberToText(System.Numerics.BigInteger number, out System.Text.StringBuilder? result)
    {
      try
      {
        result = NumberToText(number);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Convert a positional notation text string into a number.</summary>
    /// <param name="number">Must consist of only TextElements (i.e. graphemes).</param>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<char> number)
    {
      var bi = System.Numerics.BigInteger.Zero;

      foreach (var rune in number.EnumerateRunes())
      {
        bi *= Symbols.Length;

        var position = Symbols.IndexOf(rune);

        bi += position > -1 ? position : throw new System.InvalidOperationException();
      }

      return bi;
    }
    /// <summary>Convert a positional notation text string into a number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public bool TryTextToNumber(System.ReadOnlySpan<char> number, out System.Numerics.BigInteger result)
    {
      try
      {
        result = TextToNumber(number);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    /// <summary>Custom instance based on Base62 which results in traditional radix conversions.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static PositionalNotation ForRadix(int radix)
      => radix switch
      {
        2 => Base2,
        8 => Base8,
        10 => Base10,
        16 => Base16,
        var r when r >= 2 && r <= 62 => new PositionalNotation(Sequences.Base62[..r]),
        _ => throw new System.ArgumentOutOfRangeException(nameof(radix))
      };

    [System.Diagnostics.Contracts.Pure]
    public static System.Collections.Generic.Dictionary<int, System.Text.StringBuilder> ToStringRadices(System.Numerics.BigInteger number)
    {
      var dictionary = new System.Collections.Generic.Dictionary<int, System.Text.StringBuilder>();
      for (var radix = 2; radix <= 62; radix++)
        dictionary.Add(radix, ForRadix(radix).NumberToText(number));
      return dictionary;
    }
  }
}
