using System.Linq;

namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
  public sealed class SignValueNotation
  {
    /// <see cref="https://en.wikipedia.org/wiki/Egyptian_numerals"/>
    public static readonly SignValueNotation AncientEgyptianNumeralSystemUnicode = new(new System.Collections.Generic.Dictionary<string, int>()
    {
      { "\U00013060", 1000000 },
      { "\U00013190", 100000 },
      { "\U000130AD", 10000 },
      { "\U000131BC", 1000 },
      { "\U00013362", 100 },
      { "\U00013386", 10 },
      { "\U000133FA", 1 }
    });

    /// <see cref="https://en.wikipedia.org/wiki/Roman_numerals"/>
    public static readonly SignValueNotation RomanNumeralSystem = new(new System.Collections.Generic.Dictionary<string, int>() { { "M", 1000 }, { "CM", 900 }, { "D", 500 }, { "CD", 400 }, { "C", 100 }, { "XC", 90 }, { "L", 50 }, { "XL", 40 }, { "X", 10 }, { "IX", 9 }, { "V", 5 }, { "IV", 4 }, { "I", 1 } });

    public System.Collections.Generic.Dictionary<string, int> SignValueSystem { get; }

    public SignValueNotation(System.Collections.Generic.Dictionary<string, int> signValueSystem)
     => SignValueSystem = signValueSystem;
    public SignValueNotation(params System.Collections.Generic.KeyValuePair<string, int>[] signValueSystem)
      => SignValueSystem = signValueSystem.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    /// <summary>Convert a number into a sign-value notaion string.</summary>
    /// <remarks>The dictionary has to be ordered descending by value, and like its keys already are, the values must also be unique.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Sign-value_notation"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Numeral_system"/>
    public string NumberToString(System.Numerics.BigInteger number)
    {
      var sb = new System.Text.StringBuilder();

      foreach (var item in SignValueSystem)
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
    public System.Numerics.BigInteger StringToNumber(string number)
    {
      if (number is null) throw new System.ArgumentNullException(nameof(number));

      var bi = new System.Numerics.BigInteger();

      foreach (var item in SignValueSystem)
      {
        while (number.StartsWith(item.Key, System.StringComparison.Ordinal))
        {
          bi += item.Value;

          number = number[item.Key.Length..];
        }
      }

      return bi;
    }
  }
}
