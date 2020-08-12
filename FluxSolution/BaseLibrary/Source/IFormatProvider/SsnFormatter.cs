namespace Flux.IFormatProvider
{
  /// <summary>Returns whether the specified SSN complies with SSN regulations.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Social_Security_number#Structure"/>
  public class SsnFormatter : FormatProvider
  {
    public const string FormatIdentifier = @"SSN";

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase))
      {
        if (TryParse(arg?.ToString() ?? string.Empty, out var ssn))
        {
          return ssn ?? string.Empty;
        }
      }

      return HandleOtherFormats(format, arg);
    }

    public override object GetFormat(System.Type? formatType) => formatType == typeof(System.ICustomFormatter) ? this : null!;

    private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex(string.Format(@"(?<!\d)(?<AAA>{1}){0}(?<GG>{2}){0}(?<SSSS>{3})(?!\d)", @".?", @"(?!(000|666|9\d\d))\d{3}", @"(?!00)\d{2}", @"(?!0000)\d{4}"));
    /// <summary>Try to parse the text string, extract a SSN string, and return whether it was successful.</summary>
    public static bool TryParse(string text, out string? result)
    {
      try
      {
        if (_regexParse.Match(text) is System.Text.RegularExpressions.Match m && m.Success)
        {
          var parsed = new System.Text.StringBuilder();

          if (m.Groups["AAA"] is var g1 && g1.Success && !string.IsNullOrEmpty(g1.Value) && g1.Value.Length == 3)
          {
            parsed.Append(g1.Value);
          }

          parsed.Append('-');

          if (m.Groups["GG"] is var g2 && g2.Success && !string.IsNullOrEmpty(g2.Value) && g2.Value.Length == 2)
          {
            parsed.Append(g2.Value);
          }

          parsed.Append('-');

          if (m.Groups["SSSS"] is var g3 && g3.Success && !string.IsNullOrEmpty(g3.Value) && g3.Value.Length == 4)
          {
            parsed.Append(g3.Value);
          }

          if (parsed.Length == (3 + 1 + 2 + 1 + 4))
          {
            result = parsed.ToString();
            return true;
          }
        }
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default;
      return false;
    }
  }
}
