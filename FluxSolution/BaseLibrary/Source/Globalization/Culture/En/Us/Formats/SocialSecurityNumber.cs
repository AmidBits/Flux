namespace Flux.Globalization.EnUs
{
  public sealed class SocialSecurityNumber
  {
    /// <summary>A regular expression that complies with SSN regulations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Social_Security_number#Structure"/>
    public const string Regex = @"(?<!\d)(?<AAA>(?!(000|666|9\d\d))\d{3}).?(?<GG>(?!00)\d{2}).?(?<SSSS>(?!0000)\d{4})(?!\d)";

    public string AAA { get; private set; } = string.Empty;
    public string GG { get; private set; } = string.Empty;
    public string SSSS { get; private set; } = string.Empty;

    public static SocialSecurityNumber Parse(string text)
    {
      var re = new System.Text.RegularExpressions.Regex(Regex);

      if (re.Match(text) is var match && match.Success)
      {
        var ssn = new SocialSecurityNumber
        {
          AAA = match.Groups[nameof(AAA)].Value,
          GG = match.Groups[nameof(GG)].Value,
          SSSS = match.Groups[nameof(SSSS)].Value
        };

        return ssn;
      }

      throw new System.InvalidOperationException();
    }

    public override string ToString() => $"{AAA}-{GG}-{SSSS}";
  }
}
