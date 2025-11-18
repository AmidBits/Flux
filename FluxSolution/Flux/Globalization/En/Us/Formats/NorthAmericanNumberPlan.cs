namespace Flux.Globalization.En.Us
{
  /// <summary>The North American Numbering Plan (NANP) is a telephone numbering plan that encompasses 25 distinct regions.</summary>
  /// <see href="https://en.wikipedia.org/wiki/North_American_Numbering_Plan"/>
  public partial record struct NorthAmericanNumberingPlan
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<!\d)(?<CC>1)?[\s\-\.]*?(?<NPA>[2-9][0-9]{2})?[\s\-\.]*?(?<NXX>[2-9][0-9]{2})[\s\-\.]*?(?<XXXX>[0-9]{4})(?!\d)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex MatchingRegex();

    public string CC { get; private set; }
    public string NPA { get; private set; }
    public string NXX { get; private set; }
    public string XXXX { get; private set; }

    public readonly bool IsValid
      => MatchingRegex().IsMatch(ToString()!);

    public static NorthAmericanNumberingPlan Parse(string text)
    {
      var re = MatchingRegex();

      if (re.Match(text) is var match && match.Success)
      {
        var nanp = new NorthAmericanNumberingPlan
        {
          CC = match.Groups[nameof(CC)].Value,
          NPA = match.Groups[nameof(NPA)].Value,
          NXX = match.Groups[nameof(NXX)].Value,
          XXXX = match.Groups[nameof(XXXX)].Value
        };

        return nanp;
      }

      throw new System.InvalidOperationException();
    }
    public static bool TryParse(string text, out NorthAmericanNumberingPlan result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
      catch { }

      result = new NorthAmericanNumberingPlan();
      return false;
    }

    public static string TranslateAlphabeticMnemonics(string phoneNumberWithAlphabeticMnemonics)
    {
      System.ArgumentNullException.ThrowIfNull(phoneNumberWithAlphabeticMnemonics);

      var sm = new System.Text.StringBuilder();

      foreach (var c in phoneNumberWithAlphabeticMnemonics)
      {
        if (c >= 'A' && c <= 'Z')
          sm.Append(@"22233344455566677778889999"[c - 'A']);
        else if (c >= 'a' && c <= 'z')
          sm.Append(@"22233344455566677778889999"[c - 'a']);
        else
          sm.Append(c);
      }

      return sm.ToString();
    }

    public readonly override string? ToString()
      => $"{CC}-{NPA}-{NXX}-{XXXX}";
  }
}
