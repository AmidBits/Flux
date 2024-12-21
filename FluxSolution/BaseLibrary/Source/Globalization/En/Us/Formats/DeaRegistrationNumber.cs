
namespace Flux.Globalization.En.Us
{
  /// <summary>A DEA number (DEA Registration Number) is an identifier assigned to a health care provider (such as a physician, optometrist, dentist, or veterinarian) by the United States Drug Enforcement Administration allowing them to write prescriptions for controlled substances.</summary>
  /// <see href="https://en.wikipedia.org/wiki/DEA_number"/>
  public partial record struct DeaRegistrationNumber
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<RegistrantType>[ABCDEFGHJKLMPRSTUX])(?<RegistrantLastNameOr9>[A-Z9])(?<Digits>[0-9]{6})(?<Checksum>[0-9])(\-(?<AffixedID>.+))?", System.Text.RegularExpressions.RegexOptions.Compiled)]
    private static partial System.Text.RegularExpressions.Regex MatchingRegex();

    ///// <summary>A DEA number (DEA Registration Number) is an identifier assigned to a health care provider (such as a physician, optometrist, dentist, or veterinarian) by the United States Drug Enforcement Administration allowing them to write prescriptions for controlled substances.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/DEA_number"/>
    //public const string Regex = @"(?<RegistrantType>[ABCDEFGHJKLMPRSTUX])(?<RegistrantLastNameOr9>[A-Z9])(?<Digits>[0-9]{6})(?<Checksum>[0-9])(\-(?<AffixedID>.+))?";

    public string RegistrantType { get; private set; }
    public string RegistrantLastNameOr9 { get; private set; }
    public string Digits { get; private set; }
    public string Checksum { get; private set; }
    public string AffixedID { get; private set; }

    public readonly bool IsValid
      => MatchingRegex().IsMatch(ToString()!);

    public static DeaRegistrationNumber Parse(string text)
    {
      var re = MatchingRegex();

      if (re.Match(text) is var match && match.Success)
      {
        var drn = new DeaRegistrationNumber
        {
          RegistrantType = match.Groups[nameof(RegistrantType)].Value,
          RegistrantLastNameOr9 = match.Groups[nameof(RegistrantLastNameOr9)].Value,
          Digits = match.Groups[nameof(Digits)].Value,
          Checksum = match.Groups[nameof(Checksum)].Value,
          AffixedID = match.Groups[nameof(AffixedID)].Value
        };

        return drn;
      }

      throw new System.InvalidOperationException();
    }

    public static bool TryParse(string text, out DeaRegistrationNumber result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    public readonly override string? ToString()
      => $"{RegistrantType}{RegistrantLastNameOr9}{Digits}{Checksum}-{AffixedID}";
  }
}
