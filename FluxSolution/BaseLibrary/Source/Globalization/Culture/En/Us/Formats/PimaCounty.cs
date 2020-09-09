using System.Linq;

namespace Flux.Globalization.EnUs.PimaCounty
{
  public struct StreetAddress
    : System.IEquatable<StreetAddress>
  {
    /// <summary>Regular expression for Pima county street addresses.</summary>
    /// <see cref="http://webcms.pima.gov/cms/One.aspx?pageId=61696"/>
    public const string Regex = @"(?<Number>\d+)?(?:\s*(?<Direction>N|E|S|W)\.?)?(?:\s*(?<Intersection>CL|EPI|PI|SPI)\s+)?(?:\s*(?<Name>.*?))?(?:(?:\s+(?<Type>AV|BL|CI|CT|DR|HY|LN|LP|PL|PW|RD|SQ|ST|SV|TE|TR|WY)\.?)(?:\s+(?<Unit>.*))?)?";

    public static readonly System.Collections.Generic.Dictionary<string, string> Directions = new System.Collections.Generic.Dictionary<string, string>()
    {
      { @"E", @"East" },
      { @"N", @"North" },
      { @"S", @"South" },
      { @"W", @"West" }
    };

    public static readonly System.Collections.Generic.Dictionary<string, string[]> Types = new System.Collections.Generic.Dictionary<string, string[]>()
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

    //public static readonly string ReOrTypes = string.Join('|', Types.SelectMany(kvp => new stringkvp.Value.AsEnumerable().Prepend(kvp.Key)));

    public string Number { get; private set; }
    public string Direction { get; private set; }
    public string Intersection { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Unit { get; private set; }

    public bool IsEmpty
      => string.IsNullOrEmpty(Number) && string.IsNullOrEmpty(Direction) && string.IsNullOrEmpty(Intersection) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Type) && string.IsNullOrEmpty(Unit);
    public bool IsValid
      => System.Text.RegularExpressions.Regex.IsMatch(Regex, ToString());

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
    public static bool TryParse(string text, out StreetAddress result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = new StreetAddress();
      return false;
    }

    // Operators
    public static bool operator ==(StreetAddress a, StreetAddress b)
      => a.Equals(b);
    public static bool operator !=(StreetAddress a, StreetAddress b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(StreetAddress other)
      => Number == other.Number && Direction == other.Direction && Intersection == other.Intersection && Name == other.Name && Type == other.Type && Unit == other.Unit;
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is StreetAddress sa && Equals(sa);
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(Number, Direction, Intersection, Name, Type, Unit);
    public override string? ToString()
      => $"{Number} {Direction} {Intersection} {Name} {Type} {Unit}".ToSpan().NormalizeAll(' ', char.IsWhiteSpace).ToString();
  }
}
