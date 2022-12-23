using System.Linq;

namespace Flux.Formatting
{
  /// <summary>Enables formatting of any number base (radix) with any characters desired.</summary>
  public sealed class RadixFormatter
    : AFormatter
  {
    public const string FormatIdentifier = @"RADIX";

    public static readonly System.Text.Rune[] RadixNumerals = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Select(c => (System.Text.Rune)c).ToArray();

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) && System.Numerics.BigInteger.TryParse(format[5..], out var radix))
      {
        if (System.Numerics.BigInteger.TryParse(arg?.ToString() ?? string.Empty, out var raw))
        {
          if (TryFormat(raw, RadixNumerals.Take((int)radix).ToArray(), out var newBase))
          {
            return newBase?.ToString() ?? string.Empty;
          }
        }
      }

      return HandleOtherFormats(format, arg);
    }

    public static bool TryFormat(System.Numerics.BigInteger number, System.Text.Rune[] radixNumerals, out Flux.SequenceBuilder<System.Text.Rune>? result)
    {
      try
      {
        var pn = new Text.PositionalNotation(radixNumerals);
        result = pn.NumberToText(number);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
  }
}
