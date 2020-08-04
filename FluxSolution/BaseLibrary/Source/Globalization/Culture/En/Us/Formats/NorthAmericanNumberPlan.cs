namespace Flux.Globalization.EnUs
{
  /// <summary>The North American Numbering Plan (NANP) is a telephone numbering plan that encompasses 25 distinct regions.</summary>
  public sealed class NorthAmericanNumberingPlan
  {
    /// <see cref="https://en.wikipedia.org/wiki/North_American_Numbering_Plan"/>
    public const string Regex = @"(?<!\d)(?<CC>1)?[\s\-\.]*?(?<NPA>[2-9][0-9]{2})?[\s\-\.]*?(?<NXX>[2-9][0-9]{2})[\s\-\.]*?(?<XXXX>[0-9]{4})(?!\d)";

    public string CC { get; private set; } = string.Empty;
    public string NPA { get; private set; } = string.Empty;
    public string NXX { get; private set; } = string.Empty;
    public string XXXX { get; private set; } = string.Empty;

    public static NorthAmericanNumberingPlan Parse(string text)
    {
      var re = new System.Text.RegularExpressions.Regex(Regex);

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

    public override string ToString() => $"{CC}-{NPA}-{NXX}-{XXXX}";

    public static string TranslateAlphabeticMnemonics(string phoneNumberWithAlphabeticMnemonics)
    {
      var sb = new System.Text.StringBuilder();

      foreach (var c in phoneNumberWithAlphabeticMnemonics)
      {
        if (c >= 'A' && c <= 'Z')
        {
          sb.Append(@"22233344455566677778889999"[c - 'A']);
        }
        else if (c >= 'a' && c <= 'z')
        {
          sb.Append(@"22233344455566677778889999"[c - 'a']);
        }
        else
        {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }
  }
}
