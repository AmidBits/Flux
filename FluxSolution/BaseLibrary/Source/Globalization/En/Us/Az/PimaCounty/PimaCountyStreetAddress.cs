using System.Linq;

namespace Flux.Globalization.EnUs.Az.PimaCounty
{
  /// <summary>The usage below was active and working on and before the year 2023. Much of the guidelines for the naming in Pima county was done by Pima Association of Governments (PAG).</summary>
  /// <see href="https://pagregion.com/"/>
  /// <remarks>It seems the information that used to be available and where the functionality below was created from, now cost money. So much for paying taxes.</remarks>
  public readonly record struct PimaCountyStreetAddress
  {
    /// <summary>Regular expression for Pima county street addresses.</summary>
    /// <see cref="http://webcms.pima.gov/cms/One.aspx?pageId=61696"/>
    public static System.Text.RegularExpressions.Regex Regex
    {
      get
      {
        var directions = string.Join('|', DirectionAliases.SelectMany(kvp => kvp.Value.Any(v => v.StartsWith(kvp.Key, System.StringComparison.InvariantCultureIgnoreCase)) ? kvp.Value.Concat(new string[] { kvp.Key }) : new string[] { kvp.Key }.Concat(kvp.Value)));
        var intersections = @"CL|EPI|PI|SPI";
        var types = string.Join('|', TypeAliases.SelectMany(kvp => kvp.Value.Any(v => v.StartsWith(kvp.Key, System.StringComparison.InvariantCultureIgnoreCase)) ? kvp.Value.Concat(new string[] { kvp.Key }) : new string[] { kvp.Key }.Concat(kvp.Value)));

        return new System.Text.RegularExpressions.Regex(@"(?<Number>\d+)\s+(?<Direction>" + directions + @")(?:\s+(?<Intersection>" + intersections + @"))?\s+(?<Name>.+?)\s+(?<Type>" + types + @")(?:\s+(?<Unit>.*))?");
      }
    }

    public static readonly System.Collections.Generic.Dictionary<string, string[]> DirectionAliases = new()
    {
      { @"E", new string[] { @"East" } },
      { @"N", new string[] { @"North" } },
      { @"S", new string[] { @"South" } },
      { @"W", new string[] { @"West" } }
    };

    public static readonly System.Collections.Generic.Dictionary<string, string[]> TypeAliases = new()
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

    private readonly string m_number;
    private readonly string m_direction;
    private readonly string m_intersection;
    private readonly string m_name;
    private readonly string m_type;
    private readonly string m_unit;

    private PimaCountyStreetAddress(string number, string direction, string intersection, string name, string type, string unit)
    {
      m_number = number;
      m_direction = direction;
      m_intersection = intersection;
      m_name = name;
      m_type = type;
      m_unit = unit;
    }

    public string Number => m_number;
    public string Direction => m_direction;
    public string Intersection => m_intersection;
    public string Name => m_name;
    public string Type => m_type;
    public string Unit => m_unit;

    public bool IsValid => Regex.IsMatch(ToString()!);

    public static PimaCountyStreetAddress Parse(string text)
    {
      var re = Regex;

      if (re.Match(text) is var match && match.Success)
      {
        var number = match.Groups[nameof(Number)].Value;
        var direction = match.Groups[nameof(Direction)].Value;
        var intersection = match.Groups[nameof(Intersection)].Value;
        var name = match.Groups[nameof(Name)].Value;
        var type = match.Groups[nameof(Type)].Value;
        var unit = match.Groups[nameof(Unit)].Value;

        if (!DirectionAliases.ContainsKey(direction) && direction.Any())
          direction = DirectionAliases.First(kvp => kvp.Value.Contains(direction, System.StringComparer.InvariantCultureIgnoreCase)).Key;

        if (!TypeAliases.ContainsKey(type) && type.Any())
          type = TypeAliases.First(kvp => kvp.Value.Contains(type, System.StringComparer.InvariantCultureIgnoreCase)).Key;

        return new PimaCountyStreetAddress(number, direction, intersection, name, type, unit);
      }

      throw new System.InvalidOperationException();
    }
    public static bool TryParse(string text, out PimaCountyStreetAddress result)
    {
      try
      {
        result = Parse(text);
        return true;
      }
      catch { }

      result = new PimaCountyStreetAddress();
      return false;
    }

    public override string? ToString()
    {
      var sb = $"{Number} {Direction} {Intersection} {Name} {Type} {Unit}".ToSpanBuilder();

      sb.NormalizeAll(' ', char.IsWhiteSpace);

      return sb.ToString();
    }
  }
}
