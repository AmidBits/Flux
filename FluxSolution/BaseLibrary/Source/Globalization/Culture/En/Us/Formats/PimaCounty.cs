namespace Flux.Globalization.EnUs.PimaCounty
{
  public sealed class StreetAddress
  {
    /// <summary>Regular expression for Pima county street addresses.</summary>
    /// <see cref="http://webcms.pima.gov/cms/One.aspx?pageId=61696"/>
    public const string Regex = @"(?<Number>\d+)?(?:\s*(?<Direction>N|E|S|W)\.?)?(?:\s*(?<Intersection>CL|EPI|PI|SPI)\s+)?(?:\s*(?<Name>.*?))?(?:(?:\s+(?<Type>AV|BL|CI|CT|DR|HY|LN|LP|PL|PW|RD|SQ|ST|SV|TE|TR|WY)\.?)(?:\s+(?<Unit>.*))?)?";

    public static System.Collections.Generic.Dictionary<string, string> Directions = new System.Collections.Generic.Dictionary<string, string>()
    {
      { @"E", @"East" },
      { @"N", @"North" },
      { @"S", @"South" },
      { @"W", @"West" }
    };

    public static System.Collections.Generic.Dictionary<string, string[]> Types = new System.Collections.Generic.Dictionary<string, string[]>()
    {
      { @"AV", new string[] { @"Avenue" } },
      { @"BL", new string[] { @"Boulevard", @"Blvd" } },
      { @"CI", new string[] { @"Circle" } },
      { @"CT", new string[] { @"Court", @"Crt" } },
      { @"DR", new string[] { @"Drive" } },
      { @"HY", new string[] { @"Highway", @"Hwy" } },
      { @"LN", new string[] { @"Lane" } },
      { @"LP", new string[] { @"Loop" } },
      { @"PL", new string[] { @"Place" } },
      { @"PW", new string[] { @"Parkway", @"Pkwy" } },
      { @"RD", new string[] { @"Road" } },
      { @"SQ", new string[] { @"Square" } },
      { @"ST", new string[] { @"Street" } },
      { @"SV", new string[] { @"Stravenue" } },
      { @"TE", new string[] { @"Terrace" } },
      { @"TR", new string[] { @"Trail", @"Trl" } },
      { @"WY", new string[] { @"Way" } },
    };

    public string Number { get; private set; } = string.Empty;
    public string Direction { get; private set; } = string.Empty;
    public string Intersection { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Unit { get; private set; } = string.Empty;

    public static StreetAddress Parse(string text)
    {
      var re = new System.Text.RegularExpressions.Regex(Regex);

      if (re.Match(text) is var match && match.Success)
      {
        var sa = new StreetAddress
        {
          Number = match.Groups[nameof(Number)].Value,
          Direction = match.Groups[nameof(Direction)].Value,
          Intersection = match.Groups[nameof(Intersection)].Value,
          Name = match.Groups[nameof(Name)].Value,
          Type = match.Groups[nameof(Type)].Value,
          Unit = match.Groups[nameof(Unit)].Value
        };

        return sa;
      }

      throw new System.InvalidOperationException();
    }

    public override string ToString() => $"{Number} {Direction} {Intersection} {Name} {Type} {Unit}".NormalizeAll(' ', ' ');
  }
}
