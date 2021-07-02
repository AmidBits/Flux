namespace Flux.Cultural.EnUs
{
  public struct SocialSecurityNumber
    : System.IEquatable<SocialSecurityNumber>
  {
    public static readonly SocialSecurityNumber Empty;
    public bool IsEmpty => Equals(Empty);

    /// <summary>A regular expression that complies with SSN regulations.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Social_Security_number#Structure"/>
    public const string Regex = @"(?<!\d)(?<AAA>(?!(000|666|9\d\d))\d{3}).?(?<GG>(?!00)\d{2}).?(?<SSSS>(?!0000)\d{4})(?!\d)";

    public string AAA { get; private set; }
    public string GG { get; private set; }
    public string SSSS { get; private set; }

    public bool IsValid
      => System.Text.RegularExpressions.Regex.IsMatch(Regex, ToString()!);

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
    public static bool TryParse(string text, out SocialSecurityNumber result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
      catch { }

      result = new SocialSecurityNumber();
      return false;
    }

    // Operators
    public static bool operator ==(SocialSecurityNumber a, SocialSecurityNumber b)
      => a.Equals(b);
    public static bool operator !=(SocialSecurityNumber a, SocialSecurityNumber b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(SocialSecurityNumber other)
      => AAA == other.AAA && GG == other.GG && SSSS == other.SSSS;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is SocialSecurityNumber o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(AAA, GG, SSSS);
    public override string? ToString()
      => $"{AAA}-{GG}-{SSSS}";
  }
}
