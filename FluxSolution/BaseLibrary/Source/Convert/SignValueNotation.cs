namespace Flux
{
  public static partial class Convert
  {
    /// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
    public static class SignValueNotation
    {
      /// <see cref="https://en.wikipedia.org/wiki/Egyptian_numerals"/>
      public static readonly System.Collections.Generic.Dictionary<string, int> AncientEgyptianNumeralSystemUnicode = new System.Collections.Generic.Dictionary<string, int>()
      {
        { "\U00013060", 1000000 },
        { "\U00013190", 100000 },
        { "\U000130AD", 10000 },
        { "\U000131BC", 1000 },
        { "\U00013362", 100 },
        { "\U00013386", 10 },
        { "\U000133FA", 1 }
      };

      /// <see cref="https://en.wikipedia.org/wiki/Roman_numerals"/>
      public static readonly System.Collections.Generic.Dictionary<string, int> RomanNumeralSystem = new System.Collections.Generic.Dictionary<string, int>()
      {
        { "M", 1000 },
        { "CM", 900 },
        { "D", 500 },
        { "CD", 400 },
        { "C", 100 },
        { "XC", 90 },
        { "L", 50 },
        { "XL", 40 },
        { "X", 10 },
        { "IX", 9 },
        { "V", 5 },
        { "IV", 4 },
        { "I", 1 }
      };

      /// <summary>Convert a number into a sign-value notaion string.</summary>
      /// <remarks>The dictionary has to be ordered descending by value, and like its keys already are, the values must also be unique.</remarks>
      /// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
      public static string NumberToString(System.Numerics.BigInteger number, System.Collections.Generic.Dictionary<string, int> dictionarySignValueSystem)
      {
        var sb = new System.Text.StringBuilder();

        foreach (var item in dictionarySignValueSystem)
        {
          while (number >= item.Value)
          {
            sb.Append(item.Key);

            number -= item.Value;
          }
        }

        return sb.ToString();
      }

      /// <summary>Convert a sign-value notaion string into a number.</summary>
      /// <remarks>The dictionary has to be ordered descending by value, and like its keys already are, the values must also be unique.</remarks>
      /// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
      public static System.Numerics.BigInteger StringToNumber(string number, System.Collections.Generic.Dictionary<string, int> dictionarySignValueSystem)
      {
        var bi = new System.Numerics.BigInteger();

        foreach (var item in dictionarySignValueSystem)
        {
          while (number.StartsWith(item.Key, System.StringComparison.Ordinal))
          {
            bi += item.Value;

            number = number.Substring(item.Key.Length);
          }
        }

        return bi;
      }
    }
  }
}
