namespace Flux.IFormatProvider
{
  /// <summary>The North American Numbering Plan (NANP) is a telephone numbering plan that encompasses 25 distinct regions.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/North_American_Numbering_Plan"/>
  public class NanpFormatter
    : FormatProvider
  {
    public const string FormatIdentifier = @"NANP";

    public override string Format(string? format, object? arg, System.IFormatProvider? formatProvider)
    {
      if (!string.IsNullOrEmpty(format) && format.StartsWith(FormatIdentifier, System.StringComparison.OrdinalIgnoreCase) && format.Substring(4) is string)
      {
        if (TryParse(arg?.ToString() ?? string.Empty, out var nanp))
        {
          return nanp;
        }
      }

      return HandleOtherFormats(format, arg);
    }

    /// <summary>Extracts country code, area code and subscriber number.</summary>
    /// <returns>A dictionary with cc (country code), NPA (area code), NXX (central office code of subscriber number) and xxxx (station code of subscriber number).</returns>
    public static System.Collections.Generic.Dictionary<string, string> ToParts(string telephoneNumber, bool exludeEmpty)
    {
      var dictionary = new System.Collections.Generic.Dictionary<string, string>();

      var match = _regexParse.Match(telephoneNumber);

      foreach (var groupName in _regexParse.GetGroupNames())
      {
        if (!int.TryParse(groupName, out _)) // extract named groups and exclude implicit numerical ones //
        {
          if (!exludeEmpty || string.IsNullOrEmpty(match.Groups[groupName].Value))
          {
            dictionary.Add(groupName, match.Groups[groupName].Value);
          }
        }
      }

      return dictionary;
    }

    public static string TranslateAlphabeticMnemonics(string phoneNumberWithAlphabeticMnemonics)
    {
      var sb = new System.Text.StringBuilder();

      foreach (var c in phoneNumberWithAlphabeticMnemonics)
      {
        if (c >= 'A' && c <= 'Z')
        {
          sb.Append(@"22233344455566677778889999"[c - 'A']);
        }
        else
        {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }

    private static readonly System.Text.RegularExpressions.Regex _regexParse = new System.Text.RegularExpressions.Regex(string.Format(@"(?<!\d)(?<cc>{1})?{0}(?<NPA>{2})?{0}(?<NXX>{3}){0}(?<xxxx>{4})(?!\d)", @"[\s\-\.]*?", @"1", @"[2-9][0-9]{2}", @"[2-9][0-9]{2}", @"[0-9]{4}"));
    /// <summary>Try to parse the text, extract a NANP string, and return whether it was succesful.</summary>
    public static bool TryParse(string text, out string nanp)
    {
      try
      {
        if (_regexParse.Match(text) is System.Text.RegularExpressions.Match m && m.Success)
        {
          var parsed = new System.Text.StringBuilder();

          if (m.Groups["cc"] is var g1 && g1.Success && !string.IsNullOrEmpty(g1.Value))
          {
            parsed.Append(g1.Value);
            parsed.Append('-');
          }

          if (m.Groups["NPA"] is var g2 && g2.Success && !string.IsNullOrEmpty(g2.Value))
          {
            parsed.Append(g2.Value);
            parsed.Append('-');
          }

          if (m.Groups["NXX"] is var g3 && g3.Success && !string.IsNullOrEmpty(g3.Value))
          {
            parsed.Append(g3.Value);
            parsed.Append('-');
          }

          if (m.Groups["xxxx"] is var g4 && g4.Success && !string.IsNullOrEmpty(g4.Value))
          {
            parsed.Append(g4.Value);
          }

          if (parsed.Length is int pl && ((pl == 7 + 1) || (pl == 10 + 2) || (pl == 11 + 3)))
          {
            nanp = parsed.ToString();
            return true;
          }
        }
      }
      catch { }

      nanp = string.Empty;
      return false;
    }
  }
}
