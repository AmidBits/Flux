using System.Linq;

namespace Flux.Formatting
{
  /// <summary>Enables formatting of any number base (radix) with any characters desired.</summary>
  public class RadixFormatter
    : AFormatter
  {
    public const string FormatIdentifier = @"RADIX";

    public static readonly string[] RadixNumerals = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Select(c => c.ToString()).ToArray();

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

    public static bool TryFormat(System.Numerics.BigInteger number, string[] radixNumerals, out string result)
    {
      try
      {
        var pn = new Text.PositionalNotation(radixNumerals);
        result = pn.NumberToText(number);
        return true;
      }
      catch { }

      result = string.Empty;
      return false;
    }
  }
}
