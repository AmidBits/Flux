namespace Flux
{
  /// <summary>
  /// <para>A.k.a. "DMS" for short.</para>
  /// </summary>
  public static partial class SexagesimalUnitSubdivisions
  {
    //#region Conversion methods

    //public static double ConvertAngleToPercentSlope(double radAngle)
    //  => double.Tan(radAngle);

    /// <summary>
    /// <para>Convert decimal degrees to the traditional sexagsimal unit subdivisions, a.k.a. DMS notation.</para>
    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="decimalDegrees"></param>
    /// <returns></returns>
    public static (int degrees, double decimalMinutes, int minutes, double decimalSeconds) FromDecimalDegrees(double decimalDegrees)
    {
      var absDegrees = double.Abs(decimalDegrees);
      var floorAbsDegrees = double.Floor(absDegrees);
      var degrees = double.CopySign(floorAbsDegrees, decimalDegrees);
      var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);
      var absMinutes = double.Abs(decimalMinutes);
      var minutes = double.Floor(absMinutes);
      var decimalSeconds = 60 * (absMinutes - minutes);

      return (int.CreateChecked(degrees), decimalMinutes, int.CreateChecked(minutes), decimalSeconds);
    }

    /// <summary>
    /// <para>Convert the traditional sexagsimal unit subdivisions, a.k.a. DMS notation, to decimal degrees.</para>
    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="degrees"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static double ToDecimalDegrees(double degrees, double minutes, double seconds)
      => degrees + minutes / 60d + seconds / 3600d;

    /////// <summary>Convert the angle specified in arcminutes to radians.</summary>
    ////public static double ConvertArcminuteToRadian(double arcminAngle) => arcminAngle / 3437.7467707849396;

    /////// <summary>Convert the angle specified in arcseconds to radians.</summary>
    ////public static double ConvertArcsecondToRadian(double arcsecAngle) => arcsecAngle / 206264.80624709636;

    ///// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (wholeDegrees, decimalMinutes), e.g. (32, 13.3).</summary>
    //public static (int wholeDegrees, double decimalMinutes) ConvertDecimalDegreesToDm(double decimalDegrees)
    //{
    //  var absDegrees = double.Abs(decimalDegrees);
    //  var floorAbsDegrees = System.Convert.ToInt32(double.Floor(absDegrees));

    //  var degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
    //  var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

    //  return (degrees, decimalMinutes);
    //}

    ///// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (wholeDegrees, decimalMinutes, wholeMinutes, decimalSeconds), e.g. (32, 13.3, 13, 18).</summary>
    //public static (int wholeDegrees, double decimalMinutes, int wholeMinutes, double decimalSeconds) ConvertDecimalDegreesToDms(double decimalDegrees)
    //{
    //  var (degrees, decimalMinutes) = ConvertDecimalDegreesToDm(decimalDegrees);

    //  var (minutes, decimalSeconds) = ConvertDecimalMinutesToMs(decimalMinutes);

    //  return (degrees, decimalMinutes, minutes, decimalSeconds);
    //}

    ///// <summary>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal unit subdivisions (wholeMinutes, decimalSeconds), e.g. (13, 18).</summary>
    //private static (int wholeMinutes, double decimalSeconds) ConvertDecimalMinutesToMs(double decimalMinutes)
    //{
    //  var absMinutes = double.Abs(decimalMinutes);

    //  var minutes = System.Convert.ToInt32(double.Floor(absMinutes));
    //  var decimalSeconds = 60 * (absMinutes - minutes);

    //  return (minutes, decimalSeconds);
    //}

    ////public static (double decimalDegrees, double degrees, double decimalMinutes, double minutes, double decimalSeconds) ConvertDecimalDegreeToSexagesimalDegree(double decimalDegrees)
    ////{
    ////  var absDegrees = double.Abs(decimalDegrees);
    ////  var floorAbsDegrees = double.Floor(absDegrees);
    ////  var fractionAbsDegrees = absDegrees - floorAbsDegrees;

    ////  var degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
    ////  var decimalMinutes = 60 * fractionAbsDegrees;
    ////  var minutes = double.Floor(decimalMinutes);
    ////  var decimalSeconds = 3600 * fractionAbsDegrees - 60 * minutes;

    ////  return (decimalDegrees, System.Convert.ToInt32(degrees), decimalMinutes, System.Convert.ToInt32(minutes), decimalSeconds);
    ////}

    /////// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    ////public static double ConvertDegreeToGradian(double degAngle) => degAngle * (10.0 / 9.0);

    /////// <summary>Convert the angle specified in degrees to radians.</summary>
    ////public static double ConvertDegreeToRadian(double degAngle) => degAngle * (double.Pi / 180);

    ////public static double ConvertDegreeToTurn(double degAngle) => degAngle / 360;

    /////// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    ////public static double ConvertGradianToDegree(double gradAngle) => gradAngle * 0.9;

    /////// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    ////public static double ConvertGradianToRadian(double gradAngle) => gradAngle * (double.Pi / 200);

    ////public static double ConvertGradianToTurn(double gradAngle) => gradAngle / 400;

    ////public static double ConvertMilliradianToRadian(double milliradAngle) => milliradAngle * 1000;

    ////public static double ConvertNatoMilToRadian(double milAngle) => milAngle * double.Pi / 3200;

    /////// <summary>Convert the angle specified in radians to arcminutes.</summary>
    ////public static double ConvertRadianToArcminute(double radAngle) => radAngle * 3437.7467707849396;

    /////// <summary>Convert the angle specified in radians to arcseconds.</summary>
    ////public static double ConvertRadianToArcsecond(double radAngle) => radAngle * 206264.80624709636;

    /////// <summary>Convert the angle specified in radians to degrees.</summary>
    ////public static double ConvertRadianToDegree(double radAngle) => radAngle * (180 / double.Pi);

    /////// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    ////public static double ConvertRadianToGradian(double radAngle) => radAngle * (200 / double.Pi);

    ////public static double ConvertRadianToMilliradian(double radAngle) => radAngle / 1000;

    ////public static double ConvertRadianToNatoMil(double radAngle) => radAngle * 3200 / double.Pi;

    ////public static double ConvertRadianToTurn(double radAngle) => radAngle / double.Tau;

    ////public static double ConvertRadianToWarsawPactMil(double radAngle) => radAngle * 3000 / double.Pi;

    ////public static double ConvertSexagesimalDegreeToDecimalDegree(double degrees, double minutes, double seconds)
    ////  => degrees + minutes / 60 + seconds / 3600;

    ////public static double ConvertTurnToRadian(double revolutions) => revolutions * double.Tau;

    ////public static double ConvertWarsawPactMilToRadian(double milAngle) => milAngle * double.Pi / 3000;

    //public static bool TryConvertFormatToDmsNotation(string? format, out AngleDmsNotation result)
    //{
    //  if (!string.IsNullOrWhiteSpace(format))
    //  {
    //    if (format.StartsWith(AngleDmsNotation.DegreesMinutesDecimalSeconds.GetAcronym()))
    //    {
    //      result = AngleDmsNotation.DegreesMinutesDecimalSeconds;
    //      return true;
    //    }

    //    if (format.StartsWith(AngleDmsNotation.DegreesDecimalMinutes.GetAcronym()))
    //    {
    //      result = AngleDmsNotation.DegreesDecimalMinutes;
    //      return true;
    //    }

    //    if (format.StartsWith(AngleDmsNotation.DecimalDegrees.GetAcronym()))
    //    {
    //      result = AngleDmsNotation.DecimalDegrees;
    //      return true;
    //    }
    //  }

    //  result = default;
    //  return false;
    //}

    //#endregion // Conversion methods

    //[System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d*\.?\d+\s*[\u00B0\u02DA\u030A]?)\s*(?<Minutes>\d*\.?\d+\s*[\u2032\u0027\u02B9\u00B4])?\s*(?<Seconds>\d*\.?\d+\s*[\u2033\u0022\u02BA\u301E\u201D\u3003])?\s*(?<Direction>[ENWS])?")]
    //private static partial System.Text.RegularExpressions.Regex ParseDmsNotationRegex();

    ///// <summary>
    ///// <para></para>
    ///// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    ///// </summary>
    ///// <param name="dmsNotation"></param>
    ///// <returns></returns>
    ///// <exception cref="System.InvalidOperationException"></exception>
    //public static System.Collections.Generic.List<IValueQuantifiable<double>> ParseDmsNotations(string dmsNotation)
    //{
    //  var list = new System.Collections.Generic.List<IValueQuantifiable<double>>();

    //  var evm = ParseDmsNotationRegex().EnumerateMatches(dmsNotation);

    //  foreach (var vm in evm)
    //  {
    //    System.Console.WriteLine($"({vm.Index}, {vm.Length}) = '{dmsNotation.AsSpan().Slice(vm.Index, vm.Length).ToSpanMaker().RemoveAll(char.IsWhiteSpace).AsReadOnlySpan()}'");

    //    if (ParseDmsNotationRegex().Match(dmsNotation, vm.Index, vm.Length) is var m && m.Success)
    //    {
    //      var decimalDegrees = 0.0;

    //      if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var degrees))
    //        decimalDegrees += degrees;

    //      if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var minutes))
    //        decimalDegrees += minutes / 60;

    //      if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var seconds))
    //        decimalDegrees += seconds / 3600;

    //      if (m.Groups["Direction"] is var g4 && g4.Success && g4.Length == 1 && g4.Value[0] is 'S' or 'W')
    //        decimalDegrees = -decimalDegrees;

    //      if (g4.Success && (g4.Value[0] is 'N' or 'S'))
    //        list.Add(new PlanetaryScience.Latitude(decimalDegrees));
    //      else if (g4.Success && (g4.Value[0] is 'E' or 'W'))
    //        list.Add(new PlanetaryScience.Longitude(decimalDegrees));
    //      else
    //        throw new System.InvalidOperationException();
    //    }
    //  }

    //  return list;
    //}

    ///// <summary>
    ///// <para></para>
    ///// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    ///// </summary>
    ///// <param name="decimalDegrees"></param>
    ///// <param name="dmsNotation"></param>
    ///// <param name="axis"></param>
    ///// <param name="decimalPoints"></param>
    ///// <param name="componentSpacing"></param>
    ///// <returns></returns>
    ///// <exception cref="NotImplementedException"></exception>
    ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
    //public static string ToStringDmsNotation(double decimalDegrees, AngleDmsNotation dmsNotation, PlanetaryScience.CompassCardinalAxis axis, int decimalPoints = -1, UnicodeSpacing componentSpacing = UnicodeSpacing.None)
    //{
    //  var (degrees, decimalMinutes, minutes, decimalSeconds) = ConvertDecimalDegreesToSexagesimalUnitSubdivisions(decimalDegrees);

    //  var spacingString = componentSpacing.ToSpacingString();

    //  var directional = axis.ToCardinalDirection(degrees < 0).ToString();

    //  decimalPoints = decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : dmsNotation switch
    //  {
    //    AngleDmsNotation.DecimalDegrees => 4,
    //    AngleDmsNotation.DegreesDecimalMinutes => 2,
    //    AngleDmsNotation.DegreesMinutesDecimalSeconds => 0,
    //    _ => throw new NotImplementedException(),
    //  };

    //  return dmsNotation switch
    //  {
    //    AngleDmsNotation.DecimalDegrees
    //      => new Units.Angle(double.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, $"N{decimalPoints}", null) + spacingString + directional, // Show as decimal degrees.
    //    AngleDmsNotation.DegreesDecimalMinutes
    //      => new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null) + spacingString + new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, $"N{decimalPoints}", null) + spacingString + directional, // Show as degrees and decimal minutes.
    //    AngleDmsNotation.DegreesMinutesDecimalSeconds
    //      => new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null) + spacingString + new Units.Angle(double.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, "N0", null) + spacingString + new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitString(Units.AngleUnit.Arcsecond, $"N{decimalPoints}", null) + spacingString + directional, // Show as degrees, minutes and decimal seconds.
    //    _
    //      => throw new System.ArgumentOutOfRangeException(nameof(dmsNotation)),
    //  };
    //}

    ///// <summary>
    ///// <para></para>
    ///// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    ///// </summary>
    ///// <param name="dmsNotation"></param>
    ///// <param name="result"></param>
    ///// <returns></returns>
    //public static bool TryParseDmsNotations(string dmsNotation, out System.Collections.Generic.List<IValueQuantifiable<double>> result)
    //{
    //  try
    //  {
    //    result = ParseDmsNotations(dmsNotation);
    //    return true;
    //  }
    //  catch
    //  {
    //    result = default!;
    //    return false;
    //  }
    //}
  }
}
