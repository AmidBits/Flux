using System.Linq;

namespace Flux.Globalization.EnUs.PimaCounty
{
  public struct StreetAddress
    : System.IEquatable<StreetAddress>
  {
    public static readonly StreetAddress Empty;
    public bool IsEmpty => Equals(Empty);

    /// <summary>Regular expression for Pima county street addresses.</summary>
    /// <see cref="http://webcms.pima.gov/cms/One.aspx?pageId=61696"/>
    public static System.Text.RegularExpressions.Regex Regex
    {
      get
      {
        var directions = string.Join('|', DirectionAliases.SelectMany(kvp => kvp.Value.Any(v => v.StartsWith(kvp.Key, System.StringComparison.InvariantCultureIgnoreCase)) ? kvp.Value.Append(kvp.Key) : kvp.Value.Prepend(kvp.Key)));
        var intersections = @"CL|EPI|PI|SPI";
        var types = string.Join('|', TypeAliases.SelectMany(kvp => kvp.Value.Any(v => v.StartsWith(kvp.Key, System.StringComparison.InvariantCultureIgnoreCase)) ? kvp.Value.Append(kvp.Key) : kvp.Value.Prepend(kvp.Key)));

        return new System.Text.RegularExpressions.Regex(@"(?<Number>\d+)\s+(?<Direction>" + directions + @")(?:\s+(?<Intersection>" + intersections + @"))?\s+(?<Name>.+?)\s+(?<Type>" + types + @")(?:\s+(?<Unit>.*))?");
      }
    }

    public static readonly System.Collections.Generic.Dictionary<string, string[]> DirectionAliases = new System.Collections.Generic.Dictionary<string, string[]>()
    {
      { @"E", new string[] { @"East" } },
      { @"N", new string[] { @"North" } },
      { @"S", new string[] { @"South" } },
      { @"W", new string[] { @"West" } }
    };

    public static readonly System.Collections.Generic.Dictionary<string, string[]> TypeAliases = new System.Collections.Generic.Dictionary<string, string[]>()
    {
      { @"AV", new string[] { @"Avenue", @"Ave" } },
      { @"BL", new string[] { @"Boulevard", @"Blvd" } },
      { @"CI", new string[] { @"Circle", @"Cir" } },
      { @"CT", new string[] { @"Court", @"Crt" } },
      { @"DR", new string[] { @"Drive" } },
      { @"HY", new string[] { @"Highway", @"Hwy" } },
      { @"LN", new string[] { @"Lane" } },
      { @"LP", new string[] { @"Loop" } },
      { @"PL", new string[] { @"Place" } },
      { @"PW", new string[] { @"Parkway", @"Pkwy" } },
      { @"RD", new string[] { @"Road" } },
      { @"SQ", new string[] { @"Square" } },
      { @"ST", new string[] { @"Street", "Str" } },
      { @"SV", new string[] { @"Stravenue" } },
      { @"TE", new string[] { @"Terrace" } },
      { @"TR", new string[] { @"Trail", @"Trl" } },
      { @"WY", new string[] { @"Way" } },
    };

    public string Number { get; private set; }
    public string Direction { get; private set; }
    public string Intersection { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Unit { get; private set; }

    public bool IsValid
      => Regex.IsMatch(ToString());

    public static StreetAddress Parse(string text)
    {
      var re = Regex;

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

        if (!DirectionAliases.ContainsKey(sa.Direction) && sa.Direction.Any())
          sa.Direction = DirectionAliases.First(kvp => kvp.Value.Contains(sa.Direction, System.StringComparer.InvariantCultureIgnoreCase)).Key;

        if (!TypeAliases.ContainsKey(sa.Type) && sa.Type.Any())
          sa.Type = TypeAliases.First(kvp => kvp.Value.Contains(sa.Type, System.StringComparer.InvariantCultureIgnoreCase)).Key;

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
      => obj is StreetAddress o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Number, Direction, Intersection, Name, Type, Unit);
    public override string? ToString()
      => $"{Number} {Direction} {Intersection} {Name} {Type} {Unit}".ToSpan().NormalizeAll(' ', char.IsWhiteSpace).ToString();
  }
}
