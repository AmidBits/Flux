using System.Linq;

namespace Flux.IFormatProvider
{
  /// <summary>A DEA number (DEA Registration Number) is an identifier assigned to a health care provider (such as a physician, optometrist, dentist, or veterinarian) by the United States Drug Enforcement Administration allowing them to write prescriptions for controlled substances.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/DEA_number"/>
  /// <example>
  /// var value = "FN5623740-001AB";
  /// Flux.IFormatProvider.DeaFormatter.TryParse(value, out var result);
  /// System.Console.WriteLine(result);
  /// System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DeaFormatter(), "{0:DEA}", result));
  /// </example>
  public class DeaFormatter
    : FormatProvider
  {
    public const string FormatIdentifier = @"DEA";

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase))
      {
        if (TryParse(arg?.ToString() ?? string.Empty, out var ssn))
        {
          return ssn;
        }
      }

      return HandleOtherFormats(format, arg);
    }

    private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex(@"(?<RegistrantType>[ABCDEFGHJKLMPRSTUX])(?<RegistrantLastNameOr9>[A-Z9])(?<Digits>[0-9]{6})(?<CheckSum>[0-9])(\-(?<AffixedID>.+))?");
    /// <summary>Try to parse the text string, extract a DEA string, and return whether it was successful.</summary>
    public static bool TryParse(string text, out string result)
    {
      try
      {
        if (_regexParse.Match(text) is var m && m.Success)
        {
          var parsed = new System.Text.StringBuilder();

          if (m.Groups["RegistrantType"] is var g1 && g1.Success && !string.IsNullOrEmpty(g1.Value) && g1.Value.Length == 1)
          {
            parsed.Append(g1.Value);
          }

          if (m.Groups["RegistrantLastNameOr9"] is var g2 && g2.Success && !string.IsNullOrEmpty(g2.Value) && g2.Value.Length == 1)
          {
            parsed.Append(g2.Value);
          }

          if (m.Groups["Digits"] is var g3 && g1.Success && !string.IsNullOrEmpty(g3.Value) && g3.Value.Length == 6)
          {
            parsed.Append(g3.Value);

            var (sum135, sum246) = g3.Value.Select(c => c - 48).Aggregate((0, 0), (acc, number, index) => (acc.Item1 + ((index & 1) == 0 ? number : 0), acc.Item2 + ((index & 1) == 1 ? number : 0)), (acc, index) => acc);

            if (m.Groups["CheckSum"] is var g4 && g4.Success && !string.IsNullOrEmpty(g4.Value) && g4.Value.Length == 1)
            {
              parsed.Append(g4.Value);

              var checksumDigit = g4.Value[0];

              if ((sum135 + sum246 * 2).ToString(System.Globalization.CultureInfo.CurrentCulture).Last() != checksumDigit)
              {
                throw new System.Exception("Invalid checksum");
              }
            }
          }

          if (parsed.Length == 9)
          {
            if (m.Groups["AffixedID"] is var g5 && g5.Success && !string.IsNullOrWhiteSpace(g5.Value))
            {
              parsed.Append('-');
              parsed.Append(g5.Value);
            }

            result = parsed.ToString();
            return true;
          }
        }
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = string.Empty;
      return false;
    }
  }
}
