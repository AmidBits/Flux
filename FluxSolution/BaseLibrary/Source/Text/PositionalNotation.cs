namespace Flux.Text
{
  /// <summary>Convert a number into a positional notation text string.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
  /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
  public readonly ref struct PositionalNotation
  {
    public static PositionalNotation Base2 => new(RuneSequences.Base62[..2]);
    public static PositionalNotation Base8 => new(RuneSequences.Base62[..8]);
    public static PositionalNotation Base10 => new(RuneSequences.Base62[..10]);
    public static PositionalNotation Base16 => new(RuneSequences.Base62[..16]);

    public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
    public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols)
      => Symbols = symbols;

    /// <summary>Converts a number into a positional notation text string.</summary>
    public Flux.SequenceBuilder<System.Text.Rune> NumberToText(System.Numerics.BigInteger number)
    {
      var sb = new Flux.SequenceBuilder<System.Text.Rune>();

      if (number.IsZero)
        sb.Append((System.Text.Rune)'0');
      else if (number < 0) // Needs a REAL solution for negative numbers.
        return NumberToText(-number).Insert(0, (System.Text.Rune)'-');

      while (number > 0)
      {
        number = System.Numerics.BigInteger.DivRem(number, Symbols.Length, out var remainder);

        sb.Insert(0, Symbols[(int)System.Numerics.BigInteger.Abs(remainder)]);
      }

      return sb;
    }

    /// <summary>Tries to convert a number into a positional notation text string.</summary>
    public bool TryNumberToText(System.Numerics.BigInteger number, out Flux.SequenceBuilder<System.Text.Rune>? result)
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
    public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<System.Text.Rune> number)
    {
      var bi = System.Numerics.BigInteger.Zero;

      for (var index = 0; index < number.Length; index++)
      {
        bi *= Symbols.Length;

        var position = MemoryExtensions.IndexOf(Symbols, number[index]);

        bi += position > -1 ? position : throw new System.InvalidOperationException();
      }

      return bi;
    }
    /// <summary>Convert a positional notation text string into a number.</summary>
    public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<char> number)
      => TextToNumber(number.ToRunes());

    /// <summary>Convert a positional notation text string into a number.</summary>
    public bool TryTextToNumber(System.ReadOnlySpan<System.Text.Rune> number, out System.Numerics.BigInteger result)
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
    /// <summary>Convert a positional notation text string into a number.</summary>
    public bool TryTextToNumber(System.ReadOnlySpan<char> number, out System.Numerics.BigInteger result)
      => TryTextToNumber(number.ToRunes(), out result);

    /// <summary>Custom instance based on Base62 which results in traditional radix conversions.</summary>
    public static PositionalNotation ForRadix(int radix)
      => radix switch
      {
        2 => Base2,
        8 => Base8,
        10 => Base10,
        16 => Base16,
        var r when r >= 2 && r <= 62 => new PositionalNotation(RuneSequences.Base62[..r]),
        _ => throw new System.ArgumentOutOfRangeException(nameof(radix))
      };

    public static System.Collections.Generic.Dictionary<int, Flux.SequenceBuilder<System.Text.Rune>> ToStringRadices(System.Numerics.BigInteger number)
    {
      var dictionary = new System.Collections.Generic.Dictionary<int, Flux.SequenceBuilder<System.Text.Rune>>();
      for (var radix = 2; radix <= 62; radix++)
        dictionary.Add(radix, ForRadix(radix).NumberToText(number));
      return dictionary;
    }
  }
}
