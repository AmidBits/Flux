namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Converts the number to text using the specified symbols. The count of symbols represents the radix of conversion.</summary>
    //public static System.Text.StringBuilder ToRadixString<TSelf>(this TSelf number, System.ReadOnlySpan<System.Text.Rune> symbols)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  => new Text.PositionalNotation(symbols).NumberToText(number);

    public static string ToRadixString<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => Text.PositionalNotation.ForRadix(int.CreateChecked(GenericMath.AssertRadix(radix))).NumberToText(number).ToString();

    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString<TSelf, TRadix>(this TSelf number, TRadix radix, int minimumLength)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var sb = Text.PositionalNotation.ForRadix(int.CreateChecked(GenericMath.AssertRadix(radix))).NumberToText(number);

      var negative = sb[0] == (System.Text.Rune)'-' ? 1 : 0;

      if (minimumLength > (sb.Length - negative))
        sb.Insert(negative, (System.Text.Rune)'0', minimumLength - (sb.Length - negative));

      return sb.ToString();
    }
  }

  namespace Text
  {
    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public readonly ref struct PositionalNotation
    {
      public const int MaxRadix = 62;

      public static PositionalNotation Base2 => new(RuneSequences.Base62[..2]);
      public static PositionalNotation Base8 => new(RuneSequences.Base62[..8]);
      public static PositionalNotation Base10 => new(RuneSequences.Base62[..10]);
      public static PositionalNotation Base16 => new(RuneSequences.Base62[..16]);

      public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

      /// <summary>Convert a number into a positional notation text string.</summary>
      /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
      public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols)
      {
        GenericMath.AssertRadix(symbols.Length, MaxRadix, nameof(symbols));

        Symbols = symbols;
      }

      /// <summary>Converts a number into a positional notation text string.</summary>
      public SpanBuilder<System.Text.Rune> NumberToText<TSelf>(TSelf number)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        var sb = new SpanBuilder<System.Text.Rune>();

        if (TSelf.IsZero(number))
          sb.Append((System.Text.Rune)'0');
        else if (number < TSelf.Zero) // Needs a REAL solution for negative numbers.
          return NumberToText(-number).Insert(0, (System.Text.Rune)'-');

        while (number > TSelf.Zero)
        {
          var (quotient, remainder) = TSelf.DivRem(number, TSelf.CreateChecked(Symbols.Length));

          number = quotient;

          sb.Insert(0, Symbols[int.CreateChecked(remainder)]);
        }

        return sb;
      }

      ///// <summary>Tries to convert a number into a positional notation text string.</summary>
      //public bool TryNumberToText(System.Numerics.BigInteger number, out SpanBuilder<System.Text.Rune>? result)
      //{
      //  try
      //  {
      //    result = NumberToText(number);
      //    return true;
      //  }
      //  catch { }

      //  result = default;
      //  return false;
      //}

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
        => TextToNumber(number.ToListRune().AsReadOnlySpan());

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
        => TryTextToNumber(number.ToListRune().AsReadOnlySpan(), out result);

      /// <summary>Custom instance based on Base62 which results in traditional radix conversions.</summary>
      public static PositionalNotation ForRadix(int radix)
        => GenericMath.AssertRadix(radix, MaxRadix) switch
        {
          2 => Base2,
          8 => Base8,
          10 => Base10,
          16 => Base16,
          _ => PositionalNotation.ForRadix(radix)
        };

      public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
      {
        var dictionary = new System.Collections.Generic.Dictionary<int, string>();
        for (var radix = 2; radix <= MaxRadix; radix++)
          dictionary.Add(radix, ForRadix(radix).NumberToText(number).ToString());
        return dictionary;
      }
    }
  }
}
