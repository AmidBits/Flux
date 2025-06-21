//using Flux.PlanetaryScience;

//namespace Flux
//{
//  public static partial class SexagesimalUnitSubdivisions
//  {
//    public static int ConvertLatitudeOrLongitudeToUnitSign(char latitudeOrLongitude)
//      => latitudeOrLongitude is 'N' or 'E'
//      ? 1
//      : latitudeOrLongitude is 'S' or 'W'
//      ? -1
//      : throw new System.ArgumentOutOfRangeException(nameof(latitudeOrLongitude));

//    public static (CompassCardinalDirection Latitude, CompassCardinalDirection Longitude) ConvertUnitSignToCardinalDirection(int unitSign)
//      => unitSign >= 0
//      ? (CompassCardinalDirection.N, CompassCardinalDirection.E)
//      : (CompassCardinalDirection.S, CompassCardinalDirection.W);

//    /// <summary>
//    /// <para>Convert the traditional sexagsimal unit subdivisions, a.k.a. DMS notation, to decimal degrees.</para>
//    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
//    /// </summary>
//    /// <param name="degrees"></param>
//    /// <param name="minutes"></param>
//    /// <param name="seconds"></param>
//    /// <returns></returns>
//    public static double ConvertSexagesimalUnitSubdivisionsToDecimalDegrees(double degrees, double minutes, double seconds)
//      => degrees + minutes / 60 + seconds / 3600;

//    /// <summary>
//    /// <para>Convert decimal degrees to the traditional sexagsimal unit subdivisions, a.k.a. DMS notation.</para>
//    /// <para></para>
//    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
//    /// </summary>
//    /// <param name="decimalDegrees"></param>
//    /// <returns></returns>
//    public static (int degrees, double decimalMinutes, int minutes, double decimalSeconds) ConvertDecimalDegreesToSexagesimalUnitSubdivisions(double decimalDegrees)
//    {
//      var absDegrees = double.Abs(decimalDegrees);
//      var floorAbsDegrees = double.Floor(absDegrees);
//      var degrees = double.CopySign(floorAbsDegrees, decimalDegrees);
//      var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);
//      var absMinutes = double.Abs(decimalMinutes);
//      var minutes = double.Floor(absMinutes);
//      var decimalSeconds = 60 * (absMinutes - minutes);

//      return (int.CreateChecked(degrees), decimalMinutes, int.CreateChecked(minutes), decimalSeconds);
//    }

//    ///// <summary>
//    ///// <para>Converts <paramref name="decimalDegrees"/>, e.g. +32.221667, to sexagesimal unit subdivisions (degrees, decimalMinutes), e.g. (32, 13.3).</para>
//    ///// </summary>
//    //public static (int Degrees, double DecimalMinutes) ConvertDecimalDegreesToDm(double decimalDegrees)
//    //{
//    //  var absDegrees = double.Abs(decimalDegrees);
//    //  var floorAbsDegrees = System.Convert.ToInt32(double.Floor(absDegrees));

//    //  return (
//    //    double.Sign(decimalDegrees) * floorAbsDegrees,
//    //    60 * (absDegrees - floorAbsDegrees)
//    //  );

//    //  //var degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
//    //  //var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

//    //  //return (degrees, decimalMinutes);
//    //}
//    //public static void ConvertDecimalDegreesToDm(double decimalDegrees, out int degrees, out double decimalMinutes)
//    //{
//    //  var absDegrees = double.Abs(decimalDegrees);
//    //  var floorAbsDegrees = System.Convert.ToInt32(double.Floor(absDegrees));

//    //  degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
//    //  decimalMinutes = 60 * (absDegrees - floorAbsDegrees);
//    //}

//    ///// <summary>
//    ///// <para>Converts <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (degrees, minutes, decimalSeconds), e.g. (32, 13, 18), and returns the <paramref name="decimalMinutes"/>, e.g. 13.3, as an out parameter.</para>
//    ///// </summary>
//    //public static (int Degrees, int Minutes, double DecimalSeconds) ConvertDecimalDegreesToDms(double decimalDegrees, out double decimalMinutes)
//    //{
//    //  (var degrees, decimalMinutes) = ConvertDecimalDegreesToDm(decimalDegrees);

//    //  var (minutes, decimalSeconds) = ConvertDecimalMinutesToMs(decimalMinutes);

//    //  return (degrees, minutes, decimalSeconds);
//    //}

//    //public static void ConvertDecimalDegreesToDms(double decimalDegrees, out int degrees, out double decimalMinutes, out int minutes, out double decimalSeconds)
//    //{
//    //  ConvertDecimalDegreesToDm(decimalDegrees, out degrees, out decimalMinutes);

//    //  ConvertDecimalMinutesToMs(decimalMinutes, out minutes, out decimalSeconds);
//    //}

//    ///// <summary>
//    ///// <para>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal unit subdivisions (minutes, decimalSeconds), e.g. (13, 18).</para>
//    ///// </summary>
//    //public static (int minutes, double decimalSeconds) ConvertDecimalMinutesToMs(double decimalMinutes)
//    //{
//    //  var absMinutes = double.Abs(decimalMinutes);

//    //  var minutes = System.Convert.ToInt32(double.Floor(absMinutes));
//    //  //var decimalSeconds = 60 * (absMinutes - minutes);

//    //  return (
//    //    minutes,
//    //    60 * (absMinutes - minutes)
//    //  );
//    //}

//    //public static void ConvertDecimalMinutesToMs(double decimalMinutes, out int minutes, out double decimalSeconds)
//    //{
//    //  var absMinutes = double.Abs(decimalMinutes);

//    //  minutes = System.Convert.ToInt32(double.Floor(absMinutes));
//    //  decimalSeconds = 60 * (absMinutes - minutes);
//    //}

//    public static double ConvertDmsToDecimalDegrees(string dms)
//      => TryParseLatitudeDms(dms, out var latitudeDegrees)
//      ? latitudeDegrees
//      : TryParseLongitudeDms(dms, out var longitudeDegrees)
//      ? longitudeDegrees
//      : throw new System.InvalidOperationException();

//    /// <summary>
//    /// <para></para>
//    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
//    /// </summary>
//    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    public static string ToStringDms(double decimalDegrees, Units.AngleDmsNotation dmsNotation, PlanetaryScience.CompassCardinalAxis axis, int decimalPoints = -1, Unicode.UnicodeSpacing componentSpacing = Unicode.UnicodeSpacing.None)
//    {
//      var (degrees, decimalMinutes, minutes, decimalSeconds) = ConvertDecimalDegreesToSexagesimalUnitSubdivisions(decimalDegrees);

//      var spacingString = componentSpacing.ToSpacingString();

//      var directional = axis.ToCardinalDirection(degrees < 0).ToString();

//      decimalPoints = decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : dmsNotation switch
//      {
//        Units.AngleDmsNotation.DecimalDegrees => 4,
//        Units.AngleDmsNotation.DegreesDecimalMinutes => 2,
//        Units.AngleDmsNotation.DegreesMinutesDecimalSeconds => 0,
//        _ => throw new NotImplementedException(),
//      };

//      var sb = new SpanMaker<char>();

//      if (dmsNotation == Units.AngleDmsNotation.DecimalDegrees)
//      {
//        sb.Append(new Units.Angle(double.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, $"N{decimalPoints}", null));
//      }
//      else if (dmsNotation == Units.AngleDmsNotation.DegreesDecimalMinutes)
//      {
//        sb.Append(new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null));
//        sb.Append(spacingString);
//        sb.Append(new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, $"N{decimalPoints}", null));
//      }
//      else if (dmsNotation == Units.AngleDmsNotation.DegreesMinutesDecimalSeconds)
//      {
//        sb.Append(new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null));
//        sb.Append(spacingString);
//        sb.Append(new Units.Angle(double.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, "N0", null));
//        sb.Append(spacingString);
//        sb.Append(new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitString(Units.AngleUnit.Arcsecond, $"N{decimalPoints}", null));
//      }

//      sb.Append(spacingString);
//      sb.Append(directional);

//      return sb.ToString();
//    }

//    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d*\.?\d+\s*[\u00B0\u02DA\u030A])\s*(?<Minutes>\d*\.?\d+\s*[\u2032\u0027\u02B9\u00B4])?\s*(?<Seconds>\d*\.?\d+\s*[\u2033\u0022\u02BA\u301E\u201D\u3003])?\s*(?<Direction>[NS])")]
//    private static partial System.Text.RegularExpressions.Regex ParseLatitudeRegex();

//    public static bool TryParseLatitudeDms(string degreesMinutesSeconds, out double decimalDegrees)
//    {
//      decimalDegrees = 0.0;

//      try
//      {
//        if (ParseLatitudeRegex().Match(degreesMinutesSeconds) is var m && m.Success)
//        {
//          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var degrees))
//            decimalDegrees += degrees;

//          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var minutes))
//            decimalDegrees += minutes / 60;

//          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var seconds))
//            decimalDegrees += seconds / 3600;

//          if (m.Groups["Direction"] is var g4 && g4.Success && g4.Value[0] == 'S')
//            decimalDegrees = -decimalDegrees;

//          return true;
//        }
//      }
//      catch { }

//      return false;
//    }

//    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d*\.?\d+\s*[\u00B0\u02DA\u030A])\s*(?<Minutes>\d*\.?\d+\s*[\u2032\u0027\u02B9\u00B4])?\s*(?<Seconds>\d*\.?\d+\s*[\u2033\u0022\u02BA\u301E\u201D\u3003])?\s*(?<Direction>[EW])")]
//    private static partial System.Text.RegularExpressions.Regex ParseLongitudeRegex();

//    public static bool TryParseLongitudeDms(string degreesMinutesSeconds, out double decimalDegrees)
//    {
//      decimalDegrees = 0.0;

//      try
//      {
//        if (ParseLongitudeRegex().Match(degreesMinutesSeconds) is var m && m.Success)
//        {
//          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var degrees))
//            decimalDegrees += degrees;

//          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var minutes))
//            decimalDegrees += minutes / 60;

//          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var seconds))
//            decimalDegrees += seconds / 3600;

//          if (m.Groups["Direction"] is var g4 && g4.Success && g4.Value[0] == 'W')
//            decimalDegrees = -decimalDegrees;

//          return true;
//        }
//      }
//      catch { }

//      return false;
//    }

//    //    public static string GetAcronymString(this SexagesimalDegree.Format format)
//    //    => format switch
//    //    {
//    //      SexagesimalDegree.Format.DecimalDegrees => "D",
//    //      SexagesimalDegree.Format.DegreesDecimalMinutes => "DM",
//    //      SexagesimalDegree.Format.DegreesMinutesDecimalSeconds => "DMS",
//    //      _ => throw new System.NotImplementedException(),
//    //    };
//    //  }

//    //  public readonly partial record struct SexagesimalDegree
//    //  {
//    //    public enum Format
//    //    {
//    //      /// <summary>A.k.a. "D".</summary>
//    //      DecimalDegrees,
//    //      /// <summary>A.k.a. "DM".</summary>
//    //      DegreesDecimalMinutes,
//    //      /// <summary>A.k.a. "DMS".</summary>
//    //      DegreesMinutesDecimalSeconds
//    //    }

//    //    private readonly double m_decimalDegrees;

//    //    public SexagesimalDegree(long degrees, long minutes, double decimalSeconds)
//    //      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(degrees, minutes, decimalSeconds);
//    //    public SexagesimalDegree(long degrees, double decimalMinutes)
//    //      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(degrees, decimalMinutes, 0);
//    //    public SexagesimalDegree(double decimalDegrees)
//    //      => m_decimalDegrees = ConvertDegreesMinutesSecondsToDecimalDegree(decimalDegrees, 0, 0);

//    //    public double DecimalDegrees => m_decimalDegrees;
//    //    public double Degrees => ConvertDecimalDegreesToSexagesimalDM(m_decimalDegrees).degrees;
//    //    public double DecimalMinutes => ConvertDecimalDegreesToSexagesimalDM(m_decimalDegrees).decimalMinutes;
//    //    public double Minutes => ConvertDecimalMinutesToSexagesimalMS(DecimalMinutes).minutes;
//    //    public double DecimalSeconds => ConvertDecimalMinutesToSexagesimalMS(DecimalMinutes).decimalSeconds;

//    //    public Angle ToAngle() => new(Angle.ConvertDegreeToRadian(m_decimalDegrees));

//    //    public Latitude ToLatitude() => new(m_decimalDegrees);
//    //    public Longitude ToLongitude() => new(m_decimalDegrees);

//    //    #region Static methods

//    //    /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal (degrees, decimalMinutes), e.g. (32, 13.3).</summary>
//    //    public static (double degrees, double decimalMinutes) ConvertDecimalDegreesToSexagesimalDM(double decimalDegrees)
//    //    {
//    //      var absDegrees = System.Math.Abs(decimalDegrees);
//    //      var floorAbsDegrees = System.Math.Floor(absDegrees);

//    //      var degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
//    //      var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

//    //      return (degrees, decimalMinutes);
//    //    }

//    //    /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal (degrees, minutes, decimalSeconds), e.g. (32, 13, 18), and returns the <paramref name="decimalMinutes"/>, e.g. 13.3, as an out parameter.</summary>
//    //    public static (double degrees, double minutes, double decimalSeconds) ConvertDecimalDegreesToSexagesimalDMS(double decimalDegrees, out double decimalMinutes)
//    //    {
//    //      (var degrees, decimalMinutes) = ConvertDecimalDegreesToSexagesimalDM(decimalDegrees);

//    //      var (minutes, decimalSeconds) = ConvertDecimalMinutesToSexagesimalMS(decimalMinutes);

//    //      return (degrees, minutes, decimalSeconds);
//    //    }

//    //    /// <summary>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal (minutes, decimalSeconds), e.g. (13, 18).</summary>
//    //    public static (double minutes, double decimalSeconds) ConvertDecimalMinutesToSexagesimalMS(double decimalMinutes)
//    //    {
//    //      var absMinutes = System.Math.Abs(decimalMinutes);

//    //      var minutes = System.Math.Floor(absMinutes);
//    //      var decimalSeconds = 60 * (absMinutes - minutes);

//    //      return (minutes, decimalSeconds);
//    //    }

//    //    public static double ConvertDegreesMinutesSecondsToDecimalDegree(double degrees, double minutes, double seconds)
//    //      => degrees + minutes / 60 + seconds / 3600;

//    //#if NET7_0_OR_GREATER
//    //    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?")]
//    //    private static partial System.Text.RegularExpressions.Regex ParseRegex();
//    //#else
//    //        private static System.Text.RegularExpressions.Regex ParseRegex() => new(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?");
//    //#endif

//    //    public static SexagesimalDegree Parse(string dms)
//    //    {
//    //      var decimalDegrees = 0.0;

//    //      if (ParseRegex().Match(dms) is var m && m.Success)
//    //      {
//    //        if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value, out var degrees))
//    //          decimalDegrees += degrees;

//    //        if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value, out var minutes))
//    //          decimalDegrees += minutes / 60;

//    //        if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value, out var seconds))
//    //          decimalDegrees += seconds / 3600;

//    //        if (m.Groups["Direction"] is var g4 && g4.Success && (g4.Value[0] == 'S' || g4.Value[0] == 'W'))
//    //          decimalDegrees = -decimalDegrees;
//    //      }
//    //      else throw new System.ArgumentOutOfRangeException(nameof(dms));

//    //      return new(decimalDegrees);
//    //    }

//    //    public static bool TryParse(string dms, out SexagesimalDegree result)
//    //    {
//    //      try
//    //      {
//    //        result = Parse(dms);
//    //        return true;
//    //      }
//    //      catch
//    //      {
//    //        result = default;
//    //        return false;
//    //      }
//    //    }

//    //    /// <summary></summary>
//    //    /// <see href="https://en.wikipedia.org/wiki/ISO_6709"/>
//    //    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
//    //    public static string ToString(double decimalDegrees, Format format, CardinalAxis axis, int decimalPoints = -1, bool useSpaces = false, bool preferUnicode = false)
//    //    {
//    //      var (degrees, minutes, decimalSeconds) = ConvertDecimalDegreesToSexagesimalDMS(decimalDegrees, out var decimalMinutes);

//    //      var spacing = useSpaces ? " " : string.Empty;

//    //      var directional = spacing + axis.ToCardinalDirection(degrees < 0).ToString();

//    //      return format switch
//    //      {
//    //        Format.DecimalDegrees
//    //          => new Units.Angle(System.Math.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 4)}", true) + directional, // Show as decimal degrees.
//    //        Format.DegreesDecimalMinutes
//    //          => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", true) + spacing + new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 2)}", preferUnicode) + directional, // Show as degrees and decimal minutes.
//    //        Format.DegreesMinutesDecimalSeconds
//    //          => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", true) + spacing + new Units.Angle(System.Math.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, "N0", preferUnicode).PadLeft(3, '0') + spacing + new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitString(Units.AngleUnit.Arcsecond, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 0)}", preferUnicode) + directional, // Show as degrees, minutes and decimal seconds.
//    //        _
//    //          => throw new System.ArgumentOutOfRangeException(nameof(format)),
//    //      };
//    //    }

//    //    #endregion // Static methods

//    //    #region Implemented interfaces

//    //    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{(m_decimalDegrees < 0 ? '-' : '+')}{System.Math.Abs(m_decimalDegrees)}";

//    //    public double Value => m_decimalDegrees;

//    //    #endregion // Implemented interfaces

//    //    public override string ToString() => ToQuantityString();
//  }
//}
