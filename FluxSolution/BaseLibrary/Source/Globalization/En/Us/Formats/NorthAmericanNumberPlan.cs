namespace Flux.Globalization.EnUs
{
  /// <summary>The North American Numbering Plan (NANP) is a telephone numbering plan that encompasses 25 distinct regions.</summary>
  /// <see href="https://en.wikipedia.org/wiki/North_American_Numbering_Plan"/>
  public partial struct NorthAmericanNumberingPlan
    : System.IEquatable<NorthAmericanNumberingPlan>
  {
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<!\d)(?<CC>1)?[\s\-\.]*?(?<NPA>[2-9][0-9]{2})?[\s\-\.]*?(?<NXX>[2-9][0-9]{2})[\s\-\.]*?(?<XXXX>[0-9]{4})(?!\d)", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex MatchingRegex();
#else
    private static System.Text.RegularExpressions.Regex MatchingRegex() => new(@"(?<!\d)(?<CC>1)?[\s\-\.]*?(?<NPA>[2-9][0-9]{2})?[\s\-\.]*?(?<NXX>[2-9][0-9]{2})[\s\-\.]*?(?<XXXX>[0-9]{4})(?!\d)");
#endif

    public string CC { get; private set; }
    public string NPA { get; private set; }
    public string NXX { get; private set; }
    public string XXXX { get; private set; }

    public bool IsValid
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
      if (phoneNumberWithAlphabeticMnemonics is null) throw new System.ArgumentNullException(nameof(phoneNumberWithAlphabeticMnemonics));

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

    // Operators
    public static bool operator ==(NorthAmericanNumberingPlan a, NorthAmericanNumberingPlan b)
      => a.Equals(b);
    public static bool operator !=(NorthAmericanNumberingPlan a, NorthAmericanNumberingPlan b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(NorthAmericanNumberingPlan other)
      => CC == other.CC && NPA == other.NPA && NXX == other.NXX && XXXX == other.XXXX;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is NorthAmericanNumberingPlan o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CC, NPA, NXX, XXXX);
    public override string? ToString()
      => $"{GetType().Name} {{ {ToUnitString()} }}";

    public string ToUnitString()
      => $"{CC}-{NPA}-{NXX}-{XXXX}";
  }
}
