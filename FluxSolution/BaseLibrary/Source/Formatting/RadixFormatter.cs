using System.Linq;

namespace Flux.Formatting
{
  /// <summary>Enables formatting of any number base (radix) with any characters desired.</summary>
  public sealed class RadixFormatter
    : AFormatter
  {
    public const string FormatIdentifier = @"RADIX";

    public static readonly char[] RadixNumerals = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Select(c => (char)c).ToArray();

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) && System.Numerics.BigInteger.TryParse(format[5..], out var radix))
      {
        if (System.Numerics.BigInteger.TryParse(arg?.ToString() ?? string.Empty, out var raw))
        {
          if (TryFormat(raw, RadixNumerals.Take((int)radix).ToArray(), out var newBase))
          {
            return newBase;
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }

    public static bool TryFormat(System.Numerics.BigInteger number, char[] radixNumerals, out string result)
    {
      try
      {
        result = PositionalNotation.NumberToText(number, radixNumerals, (char)UnicodeCodepoint.HyphenMinus).ToString();
        return true;
      }
      catch { }

      result = string.Empty;
      return false;
    }
  }
}
