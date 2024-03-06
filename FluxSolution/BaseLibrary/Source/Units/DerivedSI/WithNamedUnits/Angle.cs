namespace Flux
{
  public static partial class Em
  {
    //public static UnicodeSpacing GetUnitSpacing(this Units.AngleUnit unit, bool preferUnicode)
    //  => unit.HasUnitSpacing(preferUnicode) ? UnicodeSpacing.NoBreakSpace : UnicodeSpacing.None;

    //public static string GetUnitSpacingString(this Units.AngleUnit unit, bool preferUnicode)
    //  => unit.HasUnitSpacing(preferUnicode) ? unit.GetUnitSpacing(preferUnicode).ToSpacingString() : string.Empty;

    public static string GetUnitString(this Units.AngleUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.AngleUnit.Arcminute => preferUnicode ? "\u2032" : "\u0027",
        Units.AngleUnit.Arcsecond => preferUnicode ? "\u2033" : "\u0022",
        Units.AngleUnit.Degree => preferUnicode ? "\u00B0" : "deg",
        Units.AngleUnit.Gradian => preferUnicode ? "\u1D4D" : "gon",
        Units.AngleUnit.NatoMil => "mils (NATO)",
        Units.AngleUnit.Milliradian => "mrad",
        Units.AngleUnit.Radian => preferUnicode ? "\u33AD" : "rad",
        Units.AngleUnit.Turn => "turns",
        Units.AngleUnit.WarsawPactMil => "mils (Warsaw Pact)",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public static bool HasUnitSpacing(this Units.AngleUnit unit, bool preferUnicode)
      => !((unit == Units.AngleUnit.Degree && preferUnicode)
      || (unit == Units.AngleUnit.Gradian && preferUnicode)
      || (unit == Units.AngleUnit.Radian && preferUnicode)
      || unit == Units.AngleUnit.Arcminute
      || unit == Units.AngleUnit.Arcsecond);
  }

  namespace Units
  {
    public enum AngleUnit
    {
      /// <summary>This is the default unit for <see cref="Angle"/>.</summary>
      Radian,
      Arcminute,
      Arcsecond,
      Degree,
      Gradian,
      /// <summary>This is the NATO angle of mils.</summary>
      NatoMil,
      /// <summary>This is sometimes also refered to as a 'mil'.</summary>
      Milliradian,
      Turn,
      /// <summary>This is the Warsaw pact angle of mils.</summary>
      WarsawPactMil,
    }

    /// <summary>Plane angle, unit of radian. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angle"/>
    public readonly partial record struct Angle
      : System.IComparable, System.IComparable<Angle>, System.IFormattable, IUnitValueQuantifiable<double, AngleUnit>
    {
      public static readonly Angle FullTurn = new(System.Math.Tau);
      public static readonly Angle HalfTurn = new(System.Math.PI);
      public static readonly Angle QuarterTurn = new(System.Math.PI / 2);

      public static readonly Angle Zero;

      /// <summary>Angle in radians.</summary>
      private readonly double m_angle;

      public Angle(double value, AngleUnit unit = AngleUnit.Radian)
        => m_angle = unit switch
        {
          AngleUnit.Arcminute => ConvertArcminuteToRadian(value),
          AngleUnit.Arcsecond => ConvertArcsecondToRadian(value),
          AngleUnit.Degree => ConvertDegreeToRadian(value),
          AngleUnit.Gradian => ConvertGradianToRadian(value),
          AngleUnit.NatoMil => ConvertNatoMilToRadian(value),
          AngleUnit.Milliradian => ConvertMilliradianToRadian(value),
          AngleUnit.Radian => value,
          AngleUnit.Turn => ConvertTurnToRadian(value),
          AngleUnit.WarsawPactMil => ConvertWarsawPactMilToRadian(value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public double Degrees => ConvertRadianToDegree(m_angle);

      public AngleNames GetAngleNames()
      {
        var angleNames = AngleNames.Unknown;

        if (AngleNames.ZeroAngle.Is(this)) angleNames |= AngleNames.ZeroAngle;
        if (AngleNames.AcuteAngle.Is(this)) angleNames |= AngleNames.AcuteAngle;
        if (AngleNames.RightAngle.Is(this)) angleNames |= AngleNames.RightAngle;
        if (AngleNames.ObtuseAngle.Is(this)) angleNames |= AngleNames.ObtuseAngle;
        if (AngleNames.StraightAngle.Is(this)) angleNames |= AngleNames.StraightAngle;
        if (AngleNames.ReflexAngle.Is(this)) angleNames |= AngleNames.ReflexAngle;
        if (AngleNames.PerigonAngle.Is(this)) angleNames |= AngleNames.PerigonAngle;
        if (AngleNames.ObliqueAngle.Is(this)) angleNames |= AngleNames.ObliqueAngle;

        return angleNames;
      }

      #region Static methods

      #region Conversion methods

      /// <summary>Convert the angle specified in arcminutes to radians.</summary>
      public static double ConvertArcminuteToRadian(double arcminAngle) => arcminAngle / 3437.746771;

      /// <summary>Convert the angle specified in arcseconds to radians.</summary>
      public static double ConvertArcsecondToRadian(double arcsecAngle) => arcsecAngle / 206264.806247;

      ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static double ConvertCartesian2ToRotationAngle(double x, double y) => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2;

      ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static double ConvertCartesian2ToRotationAngleEx(double x, double y) => System.Math.Tau - ConvertCartesian2ToRotationAngle(y, -x);

      /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (degrees, decimalMinutes), e.g. (32, 13.3).</summary>
      public static (int degrees, double minutes) ConvertDecimalDegreesToDm(double decimalDegrees)
      {
        var absDegrees = System.Math.Abs(decimalDegrees);
        var floorAbsDegrees = System.Convert.ToInt32(System.Math.Floor(absDegrees));

        var degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
        var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

        return (degrees, decimalMinutes);
      }

      /// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (degrees, minutes, decimalSeconds), e.g. (32, 13, 18), and returns the <paramref name="decimalMinutes"/>, e.g. 13.3, as an out parameter.</summary>
      public static (int degrees, int minutes, double seconds) ConvertDecimalDegreesToDms(double decimalDegrees, out double decimalMinutes)
      {
        (var degrees, decimalMinutes) = ConvertDecimalDegreesToDm(decimalDegrees);

        var (minutes, decimalSeconds) = ConvertDecimalMinutesToMs(decimalMinutes);

        return (degrees, minutes, decimalSeconds);
      }

      /// <summary>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal unit subdivisions (minutes, decimalSeconds), e.g. (13, 18).</summary>
      private static (int minutes, double seconds) ConvertDecimalMinutesToMs(double decimalMinutes)
      {
        var absMinutes = System.Math.Abs(decimalMinutes);

        var minutes = System.Convert.ToInt32(System.Math.Floor(absMinutes));
        var decimalSeconds = 60 * (absMinutes - minutes);

        return (minutes, decimalSeconds);
      }

      //public static (double decimalDegrees, double degrees, double decimalMinutes, double minutes, double decimalSeconds) ConvertDecimalDegreeToSexagesimalDegree(double decimalDegrees)
      //{
      //  var absDegrees = System.Math.Abs(decimalDegrees);
      //  var floorAbsDegrees = System.Math.Floor(absDegrees);
      //  var fractionAbsDegrees = absDegrees - floorAbsDegrees;

      //  var degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
      //  var decimalMinutes = 60 * fractionAbsDegrees;
      //  var minutes = System.Math.Floor(decimalMinutes);
      //  var decimalSeconds = 3600 * fractionAbsDegrees - 60 * minutes;

      //  return (decimalDegrees, System.Convert.ToInt32(degrees), decimalMinutes, System.Convert.ToInt32(minutes), decimalSeconds);
      //}

      /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
      public static double ConvertDegreeToGradian(double degAngle) => degAngle * (10.0 / 9.0);

      /// <summary>Convert the angle specified in degrees to radians.</summary>
      public static double ConvertDegreeToRadian(double degAngle) => degAngle * (System.Math.PI / 180);

      public static double ConvertDegreeToTurn(double degAngle) => degAngle / 360;

      /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
      public static double ConvertGradianToDegree(double gradAngle) => gradAngle * 0.9;

      /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
      public static double ConvertGradianToRadian(double gradAngle) => gradAngle * (System.Math.PI / 200);

      public static double ConvertGradianToTurn(double gradAngle) => gradAngle / 400;

      public static double ConvertMilliradianToRadian(double milliradAngle) => milliradAngle * 1000;

      public static double ConvertNatoMilToRadian(double milAngle) => milAngle * System.Math.PI / 3200;

      public static double ConvertWarsawPactMilToRadian(double milAngle) => milAngle * System.Math.PI / 3000;

      /// <summary>Convert the angle specified in radians to arcminutes.</summary>
      public static double ConvertRadianToArcminute(double radAngle) => radAngle * 3437.746771;

      /// <summary>Convert the angle specified in radians to arcseconds.</summary>
      public static double ConvertRadianToArcsecond(double radAngle) => radAngle * 206264.806247;

      /// <summary>Convert the angle specified in radians to degrees.</summary>
      public static double ConvertRadianToDegree(double radAngle) => radAngle * (180 / System.Math.PI);

      /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
      public static double ConvertRadianToGradian(double radAngle) => radAngle * (200 / System.Math.PI);

      public static double ConvertRadianToMilliradian(double radAngle) => radAngle / 1000;

      public static double ConvertRadianToNatoMil(double radAngle) => radAngle * 3200 / System.Math.PI;

      public static double ConvertRadianToTurn(double radAngle) => radAngle / System.Math.Tau;

      public static double ConvertRadianToWarsawPactMil(double radAngle) => radAngle * 3000 / System.Math.PI;

      ///// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (i.e. radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static (double x, double y) ConvertRotationAngleToCartesian2(double radAngle, double? radius = null)
      //  => System.Math.SinCos(radAngle) is var (sin, cos) && radius.HasValue ? (cos * radius.Value, sin * radius.Value) : (cos, sin);

      ///// <summary>Convert the specified clockwise rotation angle [0, PI*2] (i.e. radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static (double x, double y) ConvertRotationAngleToCartesian2Ex(double radAngle, double? radius = null)
      //  => ConvertRotationAngleToCartesian2(System.Math.Tau - (radAngle % System.Math.Tau is var rad && rad < 0 ? rad + System.Math.Tau : rad) + System.Math.PI / 2, radius);
      ////=> (-System.Math.Sin(radAngle), System.Math.Cos(radAngle));

      public static double ConvertSexagesimalDegreeToDecimalDegree(double degrees, double minutes, double seconds)
        => degrees + minutes / 60 + seconds / 3600;

      public static double ConvertTurnToRadian(double revolutions) => revolutions * System.Math.Tau;

      #endregion // Conversion methods

      #region Trigonometry static methods

      #region Gudermannian
      /// <summary>Returns the Gudermannian of the specified value.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
      public static double Gd(double value) => System.Math.Atan(System.Math.Sinh(value));

      // Inverse function:

      /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
      /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
      /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
      public static double Agd(double value) => System.Math.Atanh(System.Math.Sin(value));
      #endregion Gudermannian

      #region Hyperbolic Reciprocals/Inverse
      // Hyperbolic reciprocals (1 divided by):

      /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Csch(double v) => 1 / System.Math.Sinh(v);
      /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Sech(double v) => 1 / System.Math.Cosh(v);
      /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Coth(double v) => System.Math.Cosh(v) / System.Math.Sinh(v);

      // Inverse hyperbolic reciprocals:

      /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Acsch(double v) => System.Math.Asinh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));
      /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Asech(double v) => System.Math.Acosh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);
      /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Acoth(double v) => System.Math.Atanh(1 / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;
      #endregion Hyperbolic Reciprocals/Inverse

      #region Reciprocals/Inverse
      // Reciprocals (1 divided by):

      /// <summary>Returns the cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Csc(double v) => 1 / System.Math.Sin(v);
      /// <summary>Returns the secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Sec(double v) => 1 / System.Math.Cos(v);
      /// <summary>Returns the cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Cot(double v) => 1 / System.Math.Tan(v);

      // Inverse reciprocals:

      /// <summary>Returns the inverse cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Acsc(double v) => System.Math.Asin(1 / v);
      /// <summary>Returns the inverse secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Asec(double v) => System.Math.Acos(1 / v);
      /// <summary>Returns the inverse cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Acot(double v) => System.Math.Atan(1 / v);
      #endregion Reciprocals/Inverse

      #region Sinc
      /// <summary>Returns the normalized sinc of the specified value.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
      public static double Sincn(double value) => Sincu(System.Math.PI * value);

      /// <summary>Returns the unnormalized sinc of the specified value.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
      public static double Sincu(double value) => value != 0 ? System.Math.Sin(value) / value : 1;
      #endregion Sinc

      #region Versed/Inverse
      // Versed functions.

      /// <summary>Returns the versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Vsin(double value) => 1 - System.Math.Cos(value);
      /// <summary>Returns the versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Vcos(double value) => 1 + System.Math.Cos(value);
      /// <summary>Returns the coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Cvsin(double value) => 1 - System.Math.Sin(value);
      /// <summary>Returns the coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Cvcos(double value) => 1 + System.Math.Sin(value);

      // Inverse versed functions:

      /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Avsin(double y) => System.Math.Acos(1 - y);
      /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Avcos(double y) => System.Math.Acos(y - 1);
      /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Acvsin(double y) => System.Math.Asin(1 - y);
      /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Acvcos(double y) => System.Math.Asin(y - 1);
      #endregion Versed/Inverse

      #region Haversed/Inverse
      // Haversed functions (half of the versed versions above):

      /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hvsin(double value) => (1 - System.Math.Cos(value)) / 2;
      /// <summary>Returns the haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hvcos(double value) => (1 + System.Math.Cos(value)) / 2;
      /// <summary>Returns the hacoversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hcvsin(double value) => (1 - System.Math.Sin(value)) / 2;
      /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hcvcos(double value) => (1 + System.Math.Sin(value)) / 2;

      // Inversed haversed functions:

      /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahvsin(double y) => System.Math.Acos(1 - 2 * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));
      /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahvcos(double y) => System.Math.Acos(2 * y - 1); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));
      /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahcvsin(double y) => System.Math.Asin(1 - 2 * y);
      /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahcvcos(double y) => System.Math.Asin(2 * y - 1);
      #endregion Versine/Haversine

      #endregion Trigonometry static methods

      public static bool TryConvertFormatToDmsNotation(string? format, out AngleDmsNotation result)
      {
        if (!string.IsNullOrWhiteSpace(format))
        {
          if (format.StartsWith(AngleDmsNotation.DegreesMinutesDecimalSeconds.GetAcronym()))
          {
            result = AngleDmsNotation.DegreesMinutesDecimalSeconds;
            return true;
          }

          if (format.StartsWith(AngleDmsNotation.DegreesDecimalMinutes.GetAcronym()))
          {
            result = AngleDmsNotation.DegreesDecimalMinutes;
            return true;
          }

          if (format.StartsWith(AngleDmsNotation.DecimalDegrees.GetAcronym()))
          {
            result = AngleDmsNotation.DecimalDegrees;
            return true;
          }
        }

        result = default;
        return false;
      }

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_6709"/>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static string ToDmsString(double decimalDegrees, AngleDmsNotation dmsNotation, CardinalAxis axis, int decimalPoints = -1, UnicodeSpacing componentSpacing = UnicodeSpacing.None)
      {
        var (degrees, minutes, decimalSeconds) = ConvertDecimalDegreesToDms(decimalDegrees, out var decimalMinutes);

        var spacingString = componentSpacing.ToSpacingString();

        var directional = axis.ToCardinalDirection(degrees < 0).ToString();

        decimalPoints = decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : dmsNotation switch
        {
          AngleDmsNotation.DecimalDegrees => 4,
          AngleDmsNotation.DegreesDecimalMinutes => 2,
          AngleDmsNotation.DegreesMinutesDecimalSeconds => 0,
          _ => throw new NotImplementedException(),
        };

        return dmsNotation switch
        {
          AngleDmsNotation.DecimalDegrees
            => new Units.Angle(System.Math.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitValueString(Units.AngleUnit.Degree, $"N{decimalPoints}") + spacingString + directional, // Show as decimal degrees.
          AngleDmsNotation.DegreesDecimalMinutes
            => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitValueString(Units.AngleUnit.Degree, "N0") + spacingString + new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitValueString(Units.AngleUnit.Arcminute, $"N{decimalPoints}") + spacingString + directional, // Show as degrees and decimal minutes.
          AngleDmsNotation.DegreesMinutesDecimalSeconds
            => new Units.Angle(System.Math.Abs(degrees), Units.AngleUnit.Degree).ToUnitValueString(Units.AngleUnit.Degree, "N0") + spacingString + new Units.Angle(System.Math.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitValueString(Units.AngleUnit.Arcminute, "N0").PadLeft(3, '0') + spacingString + new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitValueString(Units.AngleUnit.Arcsecond, $"N{decimalPoints}") + spacingString + directional, // Show as degrees, minutes and decimal seconds.
          _
            => throw new System.ArgumentOutOfRangeException(nameof(dmsNotation)),
        };
      }

#if NET7_0_OR_GREATER
      [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?")]
      private static partial System.Text.RegularExpressions.Regex ParseDmsRegex();
#else
              private static System.Text.RegularExpressions.Regex ParseDmsRegex() => new(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?");
#endif

      public static Angle ParseDms(string degreesMinutesSeconds)
      {
        var decimalDegrees = 0.0;

        if (ParseDmsRegex().Match(degreesMinutesSeconds) is var m && m.Success)
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
        else throw new System.ArgumentOutOfRangeException(nameof(degreesMinutesSeconds));

        return new(ConvertDegreeToRadian(decimalDegrees));
      }

      public static bool TryParseDms(string dms, out Angle result)
      {
        try
        {
          result = ParseDms(dms);
          return true;
        }
        catch
        {
          result = default;
          return false;
        }
      }

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
      public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) <= 0;
      public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
      public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) >= 0;

      public static Angle operator -(Angle v) => new(-v.m_angle);
      public static Angle operator +(Angle a, double b) => new(a.m_angle + b);
      public static Angle operator +(Angle a, Angle b) => a + b.m_angle;
      public static Angle operator /(Angle a, double b) => new(a.m_angle / b);
      public static Angle operator /(Angle a, Angle b) => a / b.m_angle;
      public static Angle operator *(Angle a, double b) => new(a.m_angle * b);
      public static Angle operator *(Angle a, Angle b) => a * b.m_angle;
      public static Angle operator %(Angle a, double b) => new(a.m_angle % b);
      public static Angle operator %(Angle a, Angle b) => a % b.m_angle;
      public static Angle operator -(Angle a, double b) => new(a.m_angle - b);
      public static Angle operator -(Angle a, Angle b) => a - b.m_angle;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Angle o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Angle other) => m_angle.CompareTo(other.m_angle);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AngleUnit.Radian, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Angle.Value"/> property is in <see cref="AngleUnit.Radian"/>.</para>
      /// </summary>
      public double Value => m_angle;

      // IUnitQuantifiable<>
      public double GetUnitValue(AngleUnit unit)
        => unit switch
        {
          AngleUnit.Arcminute => ConvertRadianToArcminute(m_angle),
          AngleUnit.Arcsecond => ConvertRadianToArcsecond(m_angle),
          AngleUnit.Degree => ConvertRadianToDegree(m_angle),
          AngleUnit.Gradian => ConvertRadianToGradian(m_angle),
          AngleUnit.NatoMil => ConvertRadianToNatoMil(m_angle),
          AngleUnit.Milliradian => ConvertRadianToMilliradian(m_angle),
          AngleUnit.Radian => m_angle,
          AngleUnit.Turn => ConvertRadianToTurn(m_angle),
          AngleUnit.WarsawPactMil => ConvertRadianToWarsawPactMil(m_angle),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngleUnit unit = AngleUnit.Radian, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = true, UnicodeSpacing unitSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unit.HasUnitSpacing(preferUnicode) ? unitSpacing.ToSpacingString() : string.Empty);
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
