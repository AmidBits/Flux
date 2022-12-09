namespace Flux.Globalization.EnUs
{
  public struct DeaRegistrationNumber
    : System.IEquatable<DeaRegistrationNumber>
  {
    public static readonly DeaRegistrationNumber Empty;

    /// <summary>A DEA number (DEA Registration Number) is an identifier assigned to a health care provider (such as a physician, optometrist, dentist, or veterinarian) by the United States Drug Enforcement Administration allowing them to write prescriptions for controlled substances.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/DEA_number"/>
    public const string Regex = @"(?<RegistrantType>[ABCDEFGHJKLMPRSTUX])(?<RegistrantLastNameOr9>[A-Z9])(?<Digits>[0-9]{6})(?<Checksum>[0-9])(\-(?<AffixedID>.+))?";

    public string RegistrantType { get; private set; }
    public string RegistrantLastNameOr9 { get; private set; }
    public string Digits { get; private set; }
    public string Checksum { get; private set; }
    public string AffixedID { get; private set; }

    public bool IsValid
      => System.Text.RegularExpressions.Regex.IsMatch(Regex, ToString()!);

    public static DeaRegistrationNumber Parse(string text)
    {
      var re = new System.Text.RegularExpressions.Regex(Regex);

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

      result = new DeaRegistrationNumber();
      return false;
    }

    // Operators
    public static bool operator ==(DeaRegistrationNumber a, DeaRegistrationNumber b)
      => a.Equals(b);
    public static bool operator !=(DeaRegistrationNumber a, DeaRegistrationNumber b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(DeaRegistrationNumber other)
      => RegistrantType == other.RegistrantType && RegistrantLastNameOr9 == other.RegistrantLastNameOr9 && Digits == other.Digits && Checksum == other.Checksum && AffixedID == other.AffixedID;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is DeaRegistrationNumber o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(RegistrantType, RegistrantLastNameOr9, Digits, Checksum, AffixedID);
    public override string? ToString()
      => $"{GetType().Name} {{ {ToUnitString()} }}";

    public string ToUnitString()
      => $"{RegistrantType}{RegistrantLastNameOr9}{Digits}{Checksum}-{AffixedID}";
  }
}
