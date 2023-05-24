using Flux.Units;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetAcronymString(this SexagesimalDegree.Format format)
    => format switch
    {
      SexagesimalDegree.Format.DecimalDegrees => "D",
      SexagesimalDegree.Format.DegreesDecimalMinutes => "DM",
      SexagesimalDegree.Format.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }

  public readonly partial record struct SexagesimalDegree
    : IQuantifiable<double>
  {
    public enum Format
    {
      /// <summary>A.k.a. "D".</summary>
      DecimalDegrees,
      /// <summary>A.k.a. "DM".</summary>
      DegreesDecimalMinutes,
      /// <summary>A.k.a. "DMS".</summary>
      DegreesMinutesDecimalSeconds
    }

    private readonly double m_decimalDegrees;

    public SexagesimalDegree(long degrees, long minutes, double decimalSeconds)
      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(degrees, minutes, decimalSeconds);
    public SexagesimalDegree(long degrees, double decimalMinutes)
      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(degrees, decimalMinutes, 0);
    public SexagesimalDegree(double decimalDegrees)
      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(decimalDegrees, 0, 0);

    public double DecimalDegrees => m_decimalDegrees;
    public double Degrees => ConvertDecimalDegreesToSexagesimalDM(m_decimalDegrees).degrees;
    public double DecimalMinutes => ConvertDecimalDegreesToSexagesimalDM(m_decimalDegrees).decimalMinutes;
    public double Minutes => ConvertDecimalMinutesToSexagesimalMS(DecimalMinutes).minutes;
    public double DecimalSeconds => ConvertDecimalMinutesToSexagesimalMS(DecimalMinutes).decimalSeconds;

    public Angle ToAngle() => new(Angle.ConvertDegreeToRadian(m_decimalDegrees));

    public Latitude ToLatitude() => new(m_decimalDegrees);
    public Longitude ToLongitude() => new(m_decimalDegrees);

    #region Static methods

    /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal (degrees, decimalMinutes), e.g. (32, 13.3).</summary>
    public static (double degrees, double decimalMinutes) ConvertDecimalDegreesToSexagesimalDM(double decimalDegrees)
    {
      var absDegrees = System.Math.Abs(decimalDegrees);
      var floorAbsDegrees = System.Math.Floor(absDegrees);

      var degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
      var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

      return (degrees, decimalMinutes);
    }

    /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal (degrees, minutes, decimalSeconds), e.g. (32, 13, 18), and returns the <paramref name="decimalMinutes"/>, e.g. 13.3, as an out parameter.</summary>
    public static (double degrees, double minutes, double decimalSeconds) ConvertDecimalDegreesToSexagesimalDMS(double decimalDegrees, out double decimalMinutes)
    {
      (var degrees, decimalMinutes) = ConvertDecimalDegreesToSexagesimalDM(decimalDegrees);

      var (minutes, decimalSeconds) = ConvertDecimalMinutesToSexagesimalMS(decimalMinutes);

      return (degrees, minutes, decimalSeconds);
    }

    /// <summary>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal (minutes, decimalSeconds), e.g. (13, 18).</summary>
    public static (double minutes, double decimalSeconds) ConvertDecimalMinutesToSexagesimalMS(double decimalMinutes)
    {
      var absMinutes = System.Math.Abs(decimalMinutes);

      var minutes = System.Math.Floor(absMinutes);
      var decimalSeconds = 60 * (absMinutes - minutes);

      return (minutes, decimalSeconds);
    }

    public static double ConvertDegreesMinutesSecondsToDecimalDegree(double degrees, double minutes, double seconds)
      => degrees + minutes / 60 + seconds / 3600;

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?")]
    private static partial System.Text.RegularExpressions.Regex ParseRegex();
#else
        private static System.Text.RegularExpressions.Regex ParseRegex() => new(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?");
#endif

    public static SexagesimalDegree Parse(string dms)
    {
      var decimalDegrees = 0.0;

      if (ParseRegex().Match(dms) is var m && m.Success)
      {
        if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var degrees))
          decimalDegrees += degrees;

        if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var minutes))
          decimalDegrees += minutes / 60;

        if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var seconds))
          decimalDegrees += seconds / 3600;

        if (m.Groups["Direction"] is var g4 && g4.Success && (g4.Value[0] == 'S' || g4.Value[0] == 'W'))
          decimalDegrees = -decimalDegrees;
      }
      else throw new System.ArgumentOutOfRangeException(nameof(dms));

      return new(decimalDegrees);
    }

    public static bool TryParse(string dms, out SexagesimalDegree result)
    {
      try
      {
        result = Parse(dms);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/ISO_6709"/>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string ToString(double decimalDegrees, Format format, CardinalAxis axis, int decimalPoints = -1, bool useSpaces = false, bool preferUnicode = false)
    {
      var (degrees, minutes, decimalSeconds) = ConvertDecimalDegreesToSexagesimalDMS(decimalDegrees, out var decimalMinutes);

      var spacing = useSpaces ? " " : string.Empty;

      var directional = spacing + axis.ToCardinalDirection(degrees < 0).ToString();

      return format switch
      {
        Format.DecimalDegrees
          => new Units.Angle(System.Math.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 4)}", true) + directional, // Show as decimal degrees.
        Format.DegreesDecimalMinutes
          => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", true) + spacing + new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 2)}", preferUnicode) + directional, // Show as degrees and decimal minutes.
        Format.DegreesMinutesDecimalSeconds
          => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", true) + spacing + new Units.Angle(System.Math.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, "N0", preferUnicode).PadLeft(3, '0') + spacing + new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitString(Units.AngleUnit.Arcsecond, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 0)}", preferUnicode) + directional, // Show as degrees, minutes and decimal seconds.
        _
          => throw new System.ArgumentOutOfRangeException(nameof(format)),
      };
    }

    #endregion // Static methods

    #region Implemented interfaces

    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{(m_decimalDegrees < 0 ? '-' : '+')}{System.Math.Abs(m_decimalDegrees)}";

    public double Value => m_decimalDegrees;

    #endregion // Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
