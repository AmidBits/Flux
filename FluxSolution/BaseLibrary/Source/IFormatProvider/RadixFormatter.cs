using System.Linq;

namespace Flux.IFormatProvider
{
  /// <summary>Enables formatting of any number base (radix) with any characters desired.</summary>
  public class RadixFormatter : FormatProvider
  {
    public const string FormatIdentifier = @"RADIX";

    public static readonly string[] DefaultNumerals = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Select(c => c.ToString()).ToArray();

    public string[] RadixNumerals = DefaultNumerals;

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) && System.Numerics.BigInteger.TryParse(format.Substring(5), out var radix))
      {
        if (System.Numerics.BigInteger.TryParse(arg?.ToString() ?? string.Empty, out var raw))
        {
          if (TryConvertTo(raw, RadixNumerals.Take((int)radix).ToArray(), out var newBase))
          {
            return newBase;
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }

    public static bool TryConvertFrom(string number, string[] radixNumerals, out System.Numerics.BigInteger result)
    {
      try
      {
        result = Flux.Text.PositionalNotation.TextToNumber(number, radixNumerals.Select(c => c.ToString()).ToArray());
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }
    public static bool TryConvertTo(System.Numerics.BigInteger number, string[] radixNumerals, out string result)
    {
      try
      {
        result = Flux.Text.PositionalNotation.NumberToText(number, radixNumerals.Select(c => c.ToString()).ToArray());
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = string.Empty;
      return false;
    }

    public static bool TryConvert(string sourceNumber, string[] sourceNumerals, out string targetNumber, string[] targetNumerals)
    {
      targetNumber = string.Empty;

      if (TryConvertFrom(sourceNumber, sourceNumerals, out var base10))
      {
        if (TryConvertTo(base10, targetNumerals, out targetNumber))
        {
          return true;
        }
      }

      return false;
    }

    public static bool TryParse(string number, string[] numerals, out System.Numerics.BigInteger result) => TryConvertFrom(number, numerals, out result);
  }
}
