namespace Flux
{
  public static partial class TextExtensionMethods
  {
#if NET7_0_OR_GREATER

    public static string ToBinaryString<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Text.PositionalNotation.Base2.NumberToText(value.ToBigInteger(), value.GetBitCount());

    public static string ToDecimalString<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Text.PositionalNotation.Base10.NumberToText(value.ToBigInteger(), Bits.ToMaxDigitCount(value.GetBitCount(), 10, value.ImplementsSignedNumber()));

    public static string ToHexadecimalString<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Text.PositionalNotation.Base16.NumberToText(value.ToBigInteger(), Bits.ToMaxDigitCount(value.GetBitCount(), 16, value.ImplementsSignedNumber()) /*value.GetByteCount() << 1*/);

    public static string ToOctalString<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => Text.PositionalNotation.Base8.NumberToText(value.ToBigInteger(), Bits.ToMaxDigitCount(value.GetBitCount(), 8, value.ImplementsSignedNumber()) /*int.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient*/);

    /// <summary>Creates <paramref name="value"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString<TSelf>(this TSelf value, int radix, int minLength = 1) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new Text.PositionalNotation(radix).NumberToText(value.ToBigInteger(), minLength).ToString();

#else

    public static string ToBinaryString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this int value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());
    public static string ToBinaryString(this long value) => Text.PositionalNotation.Base2.NumberToText(value, value.GetBitCount());

    public static string ToDecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this int value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());
    public static string ToDecimalString(this long value) => Text.PositionalNotation.Base10.NumberToText(value, value.GetBitCount());

    public static string ToHexadecimalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this int value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);
    public static string ToHexadecimalString(this long value) => Text.PositionalNotation.Base16.NumberToText(value, value.GetByteCount() << 1);

    public static string ToOctalString(this System.Numerics.BigInteger value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this int value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);
    public static string ToOctalString(this long value) => Text.PositionalNotation.Base8.NumberToText(value, System.Math.DivRem(value.GetBitCount(), 3) is var dr && dr.Remainder > 0 ? dr.Quotient + 1 : dr.Quotient);

    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this System.Numerics.BigInteger number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this int number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();
    /// <summary>Creates <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
    public static string ToRadixString(this long number, int radix, int minLength = 1) => new Text.PositionalNotation(radix).NumberToText(number, minLength).ToString();

#endif
  }

  namespace Text
  {
#if NET7_0_OR_GREATER

    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public readonly ref struct PositionalNotation
    {
      public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

      public const int MaxRadix = 64;

      public static PositionalNotation Base2 => new(2);
      public static PositionalNotation Base8 => new(8);
      public static PositionalNotation Base10 => new(10);
      public static PositionalNotation Base16 => new(16);

      //public System.Text.Rune NegativeSign { get; init; } = (System.Text.Rune)'\u2212';

      public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

      /// <summary>Convert a number into a positional notation text string.</summary>
      /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
      public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols) => Symbols = symbols;
      public PositionalNotation(int radix) : this(Base64.AsSpan()[..radix].ToListRune().AsSpan()) { }

      /// <summary>Converts <paramref name="number"/> to a positional notation text string with the specified <paramref name="minLength"/>.</summary>
      public string NumberToText(System.Numerics.BigInteger number, int minLength = 1)
      {
        if (System.Numerics.BigInteger.IsNegative(number))
          return $"{Flux.UnicodeCodepoint.MinusSign}{NumberToText(System.Numerics.BigInteger.Abs(number), minLength)}";

        if (number.IsZero)
          return Symbols[0].ToString();

        var radix = Symbols.Length;

        var sb = new SpanBuilder<System.Text.Rune>();

        while (number > System.Numerics.BigInteger.Zero)
        {
          (number, var remainder) = System.Numerics.BigInteger.DivRem(number, radix);

          sb.Insert(0, Symbols[int.CreateChecked(remainder)]);
        }

        if (minLength > sb.Length)
          sb.Insert(0, Symbols[0], minLength - sb.Length);

        return sb.ToString();
      }

      /// <summary>Try converting <paramref name="number"/> to a positional notation <paramref name="text"/> string (as an out parameter) with the specified <paramref name="minLength"/>.</summary>
      public bool TryNumberToText(System.Numerics.BigInteger number, out string text, int minLength = 1)
      {
        try
        {
          text = NumberToText(number, minLength);
          return true;
        }
        catch { }

        text = string.Empty;
        return false;
      }

      /// <summary>Convert a positional notation <paramref name="text"/> string to a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<System.Text.Rune> text)
      {
        var isNegative = (text[0] == (System.Text.Rune)(int)UnicodeCodepoint.MinusSign | text[0] == (System.Text.Rune)(int)UnicodeCodepoint.HyphenMinus);

        var bi = System.Numerics.BigInteger.Zero;

        for (var index = isNegative ? 1 : 0; index < text.Length; index++)
        {
          bi *= Symbols.Length;

          bi += MemoryExtensions.IndexOf(Symbols, text[index]) is var position && position >= 0 ? position : throw new System.InvalidOperationException();
        }

        return isNegative ? -bi : bi;
      }

      /// <summary>Convert a positional notation <paramref name="text"/> string to a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<char> text) => TextToNumber(text.ToListRune().AsSpan());

      /// <summary>Try converting a positional notation <paramref name="text"/> string to a <paramref name="number"/> (as an out parameter).</summary>
      public bool TryTextToNumber(System.ReadOnlySpan<System.Text.Rune> text, out System.Numerics.BigInteger number)
      {
        try
        {
          number = TextToNumber(text);
          return true;
        }
        catch { }

        number = default;
        return false;
      }

      /// <summary>Try converting a positional notation <paramref name="text"/> string to a <paramref name="number"/> (as an out parameter).</summary>
      public bool TryTextToNumber(System.ReadOnlySpan<char> text, out System.Numerics.BigInteger number)
      {
        try
        {
          number = TextToNumber(text);
          return true;
        }
        catch { }

        number = default;
        return false;
      }
    }

#else


    /// <summary>Convert a number into a positional notation text string.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Positional_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/List_of_numeral_systems#Standard_positional_numeral_systems"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public readonly ref struct PositionalNotation
    {
      public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

      public const int MaxRadix = 64;

      public static PositionalNotation Base2 => new(2);
      public static PositionalNotation Base8 => new(8);
      public static PositionalNotation Base10 => new(10);
      public static PositionalNotation Base16 => new(16);

      public System.Text.Rune NegativeSign { get; init; } = (System.Text.Rune)'-';

      public System.ReadOnlySpan<System.Text.Rune> Symbols { get; }

      /// <summary>Convert a number into a positional notation text string.</summary>
      /// <param name="symbols">Symbols must be represented as TextElements (i.e. graphemes).</param>
      public PositionalNotation(System.ReadOnlySpan<System.Text.Rune> symbols) => Symbols = symbols;
      public PositionalNotation(int radix) : this(Base64.AsSpan().Slice(0, radix).ToListRune().AsSpan()) { }

      /// <summary>Converts a number into a positional notation text string.</summary>
      public string NumberToText(System.Numerics.BigInteger number, int minLength = 1)
      {
        if (number < 0)
          return NegativeSign.ToString() + NumberToText(-number, minLength);

        if (number.IsZero)
          return Symbols[0].ToString();

        var radix = Symbols.Length;

        var sb = new SpanBuilder<System.Text.Rune>();

        while (number > 0)
        {
          number = System.Numerics.BigInteger.DivRem(number, radix, out var remainder);

          sb.Insert(0, Symbols[(int)remainder]);
        }

        if (minLength > sb.Length)
          sb.Insert(0, Symbols[0], minLength - sb.Length);

        return sb.ToString();
      }

      /// <summary>Tries to convert a number into a positional notation text string.</summary>
      public bool TryNumberToText(System.Numerics.BigInteger number, out string result, int minLength = 1)
      {
        try
        {
          result = NumberToText(number, minLength);
          return true;
        }
        catch { }

        result = string.Empty;
        return false;
      }

      /// <summary>Convert a positional notation text string into a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<System.Text.Rune> number)
      {
        var isNegative = number[0] == NegativeSign;

        var bi = System.Numerics.BigInteger.Zero;

        for (var index = isNegative ? 1 : 0; index < number.Length; index++)
        {
          bi *= Symbols.Length;

          bi += MemoryExtensions.IndexOf(Symbols, number[index]) is var position && position >= 0 ? position : throw new System.InvalidOperationException();
        }

        return isNegative ? -bi : bi;
      }
      /// <summary>Convert a positional notation text string into a number.</summary>
      public System.Numerics.BigInteger TextToNumber(System.ReadOnlySpan<char> number) => TextToNumber(number.ToListRune().AsSpan().AsReadOnlySpan());

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
      public bool TryTextToNumber(System.ReadOnlySpan<char> number, out System.Numerics.BigInteger result) => TryTextToNumber(number.ToListRune().AsSpan().AsReadOnlySpan(), out result);

      public static System.Collections.Generic.Dictionary<int, string> ToStringRadices(System.Numerics.BigInteger number)
      {
        var dictionary = new System.Collections.Generic.Dictionary<int, string>();
        for (var radix = 2; radix <= MaxRadix; radix++)
          dictionary.Add(radix, new PositionalNotation(radix).NumberToText(number).ToString());
        return dictionary;
      }
    }

#endif
  }
}
