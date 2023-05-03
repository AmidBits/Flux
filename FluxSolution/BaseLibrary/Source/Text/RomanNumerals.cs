namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Roman_numerals"/>
  public sealed class RomanNumerals
  {
    ///// <see cref="https://en.wikipedia.org/wiki/Roman_numerals"/>
    //public static readonly SignValueNotation RomanNumeralSystem = new(new System.Collections.Generic.Dictionary<string, int>() {
    //  { "M", 1000 },
    //  { "CM", 900 },
    //  { "D", 500 },
    //  { "CD", 400 },
    //  { "C", 100 },
    //  { "XC", 90 },
    //  { "L", 50 },
    //  { "XL", 40 },
    //  { "X", 10 },
    //  { "IX", 9 },
    //  { "V", 5 },
    //  { "IV", 4 },
    //  { "I", 1 }
    //});

    //public static readonly System.Collections.Generic.Dictionary<System.Text.Rune, int> AncientRomanNumerals = new(new System.Collections.Generic.Dictionary<System.Text.Rune, int>() {
    //  { (System.Text.Rune)'\u216F', 1000 },
    //  { (System.Text.Rune)'\u216E', 500 },
    //  { (System.Text.Rune)'\u216D', 100 },
    //  { (System.Text.Rune)'\u216C', 50 },
    //  { (System.Text.Rune)'\u2169', 10 },
    //  { (System.Text.Rune)'\u2164', 5 },
    //  { (System.Text.Rune)'\u2160', 1 },
    //});

    public static readonly System.Collections.Generic.Dictionary<System.Text.Rune, int> AncientRomanSmallNumerals = new(new System.Collections.Generic.Dictionary<System.Text.Rune, int>() {
      { (System.Text.Rune)'\u217F', 1000 },
      { (System.Text.Rune)'\u217E', 500 },
      { (System.Text.Rune)'\u217D', 100 },
      { (System.Text.Rune)'\u217C', 50 },
      { (System.Text.Rune)'\u2179', 10 },
      { (System.Text.Rune)'\u2174', 5 },
      { (System.Text.Rune)'\u2170', 1 },
    });

    ///// <summary>Convert a number into a sign-value notaion string.</summary>
    ///// <remarks>The dictionary has to be ordered descending by value, and like its keys already are, the values must also be unique.</remarks>
    ///// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
    ///// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>

    //public System.Text.StringBuilder NumberToText(System.Numerics.BigInteger number)
    //{
    //  var sb = new System.Text.StringBuilder();

    //  foreach (var item in SignValueSystem)
    //  {
    //    while (number >= item.Value)
    //    {
    //      sb.Append(item.Key);

    //      number -= item.Value;
    //    }
    //  }

    //  return sb;
    //}

    /// <summary>Convert a sign-value notaion string into a number.</summary>
    /// <remarks>The dictionary has to be ordered descending by value, and like its keys already are, the values must also be unique.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Sign-value_notation"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Numeral_system"/>
    /// <seealso href="https://www.geeksforgeeks.org/python-program-to-convert-integer-to-roman/"/>
    public static System.ReadOnlySpan<char> NumberToText(int number)
    {
      var div = (int)number.ToBigInteger().PowOf(10);

      var sb = new SpanBuilder<string>();

      while (number > 0)
      {
        var lastCount = number / div;

        if (lastCount <= 3)
        {
          sb.Append(ConvertMaxValueToBasicLatin(div), lastCount);
        }
        else if (lastCount == 4)
        {
          sb.Append(ConvertMaxValueToBasicLatin(div));
          sb.Append(ConvertMaxValueToBasicLatin(div * 5));
        }
        else if (5 <= lastCount && lastCount <= 8)
        {
          sb.Append(ConvertMaxValueToBasicLatin(div * 5));
          sb.Append(ConvertMaxValueToBasicLatin(div), lastCount - 5);
        }
        else if (lastCount == 9)
        {
          sb.Append(ConvertMaxValueToBasicLatin(div));
          sb.Append(ConvertMaxValueToBasicLatin(div * 10));
        }

        number %= div;
        div /= 10;
      }

      return sb.AsReadOnlySpan().ToString();
    }

    public static int TextToNumber(System.ReadOnlySpan<char> number)
    {
      var sum = 0;

      for (var i = 0; i < number.Length; i++)
      {
        var currentValue = ConvertBasicLatinToMaxValue(ConvertNumberFormsToBasicLatin(number[i]));

        if (i + 1 < number.Length && ConvertBasicLatinToMaxValue(ConvertNumberFormsToBasicLatin(number[i + 1])) is var nextValue && nextValue > currentValue)
          sum -= currentValue;
        else
          sum += currentValue;
      }

      return sum;
    }

    /// <summary>Converts a basic latin code point into a number value.</summary>
    public static int ConvertBasicLatinToMaxValue(char number)
      => number switch
      {
        'M' => 1000,
        'D' => 500,
        'C' => 100,
        'L' => 50,
        'X' => 10,
        'V' => 5,
        'I' => 1,
        _ => throw new System.ArgumentOutOfRangeException(nameof(number))
      };

    /// <summary>Converts a roman numeral basic latin code point into a roman numeral number forms code point.</summary>
    public static (char normal, char small) ConvertBasicLatinToNumberForms(char charBasicLatin)
      => charBasicLatin switch
      {
        'M' => ('\u216F', '\u217F'),
        'D' => ('\u216E', '\u217E'),
        'C' => ('\u216D', '\u217D'),
        'L' => ('\u216C', '\u217C'),
        'X' => ('\u2169', '\u2179'),
        'V' => ('\u2164', '\u2174'),
        'I' => ('\u2160', '\u2170'),
        _ => (charBasicLatin, charBasicLatin)
      };

    /// <summary>Converts a number value into a basic latin code point.</summary>
    public static string ConvertMaxValueToBasicLatin(int number)
      => number switch
      {
        >= 1000 => "M",
        >= 900 => "CM",
        >= 500 => "D",
        >= 400 => "CD",
        >= 100 => "C",
        >= 90 => "XC",
        >= 50 => "L",
        >= 40 => "XL",
        >= 10 => "X",
        >= 9 => "IX",
        >= 5 => "V",
        >= 4 => "IV",
        >= 1 => "I",
        _ => throw new System.ArgumentOutOfRangeException(nameof(number))
      };

    /// <summary>Converts a roman numeral number forms code point into a roman numeral basic latin code point.</summary>
    public static char ConvertNumberFormsToBasicLatin(char charNumberForms)
      => charNumberForms switch
      {
        '\u216F' or '\u217F' => 'M',
        '\u216E' or '\u217E' => 'D',
        '\u216D' or '\u217D' => 'C',
        '\u216C' or '\u217C' => 'L',
        '\u2169' or '\u2179' => 'X',
        '\u2164' or '\u2174' => 'V',
        '\u2160' or '\u2170' => 'I',
        _ => charNumberForms
      };
  }
}
