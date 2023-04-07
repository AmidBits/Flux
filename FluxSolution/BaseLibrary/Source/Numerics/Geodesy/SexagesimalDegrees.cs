namespace Flux
{
  public static partial class SexagesimalDegreesExtensionMethods
  {
    public static string GetAcronymString(this SexagesimalDegreeFormat format)
      => format switch
      {
        SexagesimalDegreeFormat.DecimalDegrees => "D",
        SexagesimalDegreeFormat.DegreesDecimalMinutes => "DM",
        SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds => "DMS",
        _ => throw new System.NotImplementedException(),
      };

    public static CardinalDirection ToCardinalDirection(this SexagesimalDegreeDirection direction, bool isNegative)
      => direction switch
      {
        SexagesimalDegreeDirection.WestEast => isNegative ? CardinalDirection.W : CardinalDirection.E,
        SexagesimalDegreeDirection.NorthSouth => isNegative ? CardinalDirection.S : CardinalDirection.N,
        _ => throw new System.ArgumentOutOfRangeException(nameof(direction))
      };

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/ISO_6709"/>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string ToSexagesimalDegreeString(this SexagesimalDegreeFormat format, double degAngle, SexagesimalDegreeDirection direction, int decimalPoints = -1, bool useSpaces = false, bool preferUnicode = false)
    {
      var (decimalDegrees, degrees, decimalMinutes, minutes, decimalSeconds) = Quantities.Angle.ConvertDecimalDegreeToSexagesimalDegree(degAngle);

      var spacing = useSpaces ? " " : string.Empty;

      var directional = spacing + direction.ToCardinalDirection(degrees < 0).ToString();

      return format switch
      {
        SexagesimalDegreeFormat.DecimalDegrees
          => new Quantities.Angle(double.Abs(decimalDegrees), Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 4)}", true) + directional, // Show as decimal degrees.
        SexagesimalDegreeFormat.DegreesDecimalMinutes
          => new Quantities.Angle(double.Abs(degrees), Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, "N0", true) + spacing + new Quantities.Angle(decimalMinutes, Quantities.AngleUnit.Arcminute).ToUnitString(Quantities.AngleUnit.Arcminute, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 2)}", preferUnicode) + directional, // Show as degrees and decimal minutes.
        SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds
          => new Quantities.Angle(double.Abs(degrees), Quantities.AngleUnit.Degree).ToUnitString(Quantities.AngleUnit.Degree, "N0", true) + spacing + new Quantities.Angle(double.Abs(minutes), Quantities.AngleUnit.Arcminute).ToUnitString(Quantities.AngleUnit.Arcminute, "N0", preferUnicode).PadLeft(3, '0') + spacing + new Quantities.Angle(decimalSeconds, Quantities.AngleUnit.Arcsecond).ToUnitString(Quantities.AngleUnit.Arcsecond, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 0)}", preferUnicode) + directional, // Show as degrees, minutes and decimal seconds.
        _
          => throw new System.ArgumentOutOfRangeException(nameof(format)),
      };
    }
  }

  public enum SexagesimalDegreeFormat
  {
    DecimalDegrees,
    DegreesDecimalMinutes,
    DegreesMinutesDecimalSeconds
  }

  public enum SexagesimalDegreeDirection
  {
    None,
    /// <summary>From negative (west) to positive (east).</summary>
    WestEast,
    /// <summary>From negative (south) to positive) (north).</summary>
    NorthSouth
  }
}
