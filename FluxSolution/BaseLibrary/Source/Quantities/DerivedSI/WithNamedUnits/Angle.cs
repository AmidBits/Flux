﻿namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSpacing(this Quantities.AngleUnit unit, bool preferUnicode, bool useFullName)
      => (unit == Quantities.AngleUnit.Degree && preferUnicode) || unit == Quantities.AngleUnit.Arcminute || unit == Quantities.AngleUnit.Arcsecond ? string.Empty : " ";
    public static string GetUnitString(this Quantities.AngleUnit unit, bool preferUnicode, bool useFullName)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AngleUnit.Arcminute => preferUnicode ? "\u2032" : "\u0027",
        Quantities.AngleUnit.Arcsecond => preferUnicode ? "\u2033" : "\u0022",
        Quantities.AngleUnit.Degree => preferUnicode ? "\u00B0" : "deg",
        Quantities.AngleUnit.Gradian => "grad",
        Quantities.AngleUnit.NatoMil => "mils",
        Quantities.AngleUnit.Milliradian => "mrad",
        Quantities.AngleUnit.Radian => preferUnicode ? "\u33AD" : "rad",
        Quantities.AngleUnit.Turn => "turns",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public static CardinalDirection ToCardinalDirection(this Quantities.SexagesimalDegreeDirection direction, bool isNegative)
      => direction switch
      {
        Quantities.SexagesimalDegreeDirection.WestEast => isNegative ? CardinalDirection.W : CardinalDirection.E,
        Quantities.SexagesimalDegreeDirection.NorthSouth => isNegative ? CardinalDirection.S : CardinalDirection.N,
        _ => throw new System.ArgumentOutOfRangeException(nameof(direction))
      };
  }

  namespace Quantities
  {
    [System.Flags]
    public enum AngleNames
    {
      /// <summary>An angle equal to 0° or not turned is called a zero angle.</summary>
      ZeroAngle = 1,
      /// <summary>An angle smaller than a right angle (less than 90°) is called an acute angle ("acute" meaning "sharp").</summary>
      AcuteAngle = 2,
      /// <summary>An angle equal to 1/4 turn (90° or π/2 radians) is called a right angle. Two lines that form a right angle are said to be normal, orthogonal, or perpendicular.</summary>
      RightAngle = 4,
      /// <summary>An angle larger than a right angle and smaller than a straight angle (between 90° and 180°) is called an obtuse angle ("obtuse" meaning "blunt").</summary>
      ObtuseAngle = 8,
      /// <summary>An angle equal to 1/2 turn (180° or π radians) is called a straight angle.</summary>
      StraightAngle = 16,
      /// <summary>An angle larger than a straight angle but less than 1 turn (between 180° and 360°) is called a reflex angle.</summary>
      ReflexAngle = 32,
      /// <summary>An angle equal to 1 turn (360° or 2π radians) is called a full angle, complete angle, round angle or a perigon.</summary>
      PerigonAngle = 64,
      /// <summary>An angle that is not a multiple of a right angle is called an oblique angle.</summary>
      ObliqueAngle = 128
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

    public enum AngleUnit
    {
      /// <summary>This is the default unit for length.</summary>
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
    }

    /// <summary>Plane angle, unit of radian. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angle"/>
    public readonly struct Angle
      : System.IComparable, System.IComparable<Angle>, System.IConvertible, System.IEquatable<Angle>, IUnitQuantifiable<double, AngleUnit>
    {
      public const AngleUnit DefaultUnit = AngleUnit.Radian;

      public const double OneFullRotationInDegrees = 360;
      public const double OneFullRotationInGradians = 400;
      public const double OneFullRotationInRadians = GenericMath.PiX2;
      public const double OneFullRotationInTurns = 1;

      private readonly double m_radAngle;

      public Angle(double value, AngleUnit unit = DefaultUnit)
        => m_radAngle = unit switch
        {
          AngleUnit.Arcminute => ConvertArcminuteToRadian(value),
          AngleUnit.Arcsecond => ConvertArcsecondToRadian(value),
          AngleUnit.Degree => ConvertDegreeToRadian(value),
          AngleUnit.Gradian => ConvertGradianToRadian(value),
          AngleUnit.NatoMil => ConvertNatoMilToRadian(value),
          AngleUnit.Milliradian => ConvertMilliradianToRadian(value),
          AngleUnit.Radian => value,
          AngleUnit.Turn => ConvertTurnToRadian(value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public Azimuth ToAzimuth()
        => new(ToUnitValue(AngleUnit.Degree));

      /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public Numerics.CartesianCoordinate2<double> ToCartesian2()
        => (Numerics.CartesianCoordinate2<double>)Flux.Convert.RotationAngleToCartesian2(m_radAngle);

      /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public Numerics.CartesianCoordinate2<double> ToCartesian2Ex()
        => (Numerics.CartesianCoordinate2<double>)Flux.Convert.RotationAngleToCartesian2Ex(m_radAngle);

      public double ToDegrees()
        => ConvertRadianToDegree(m_radAngle);

      public Latitude ToLatitude()
        => new(ToUnitValue(AngleUnit.Degree));

      public Longitude ToLongitude()
        => new(ToUnitValue(AngleUnit.Degree));

      public string ToSexagesimalDegreeString(SexagesimalDegreeFormat format, SexagesimalDegreeDirection direction, int decimalPoints = -1, bool useSpaces = false, bool preferUnicode = false)
      {
        var (decimalDegrees, degrees, decimalMinutes, minutes, decimalSeconds) = ConvertDecimalDegreeToSexagesimalDegree(ConvertRadianToDegree(m_radAngle));

        var spacing = useSpaces ? " " : string.Empty;

        var directional = spacing + direction.ToCardinalDirection(degrees < 0).ToString();

        return format switch
        {
          SexagesimalDegreeFormat.DecimalDegrees => new Angle(System.Math.Abs(decimalDegrees), AngleUnit.Degree).ToUnitString(AngleUnit.Degree, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 4)}", true) + directional, // Show as decimal degrees.
          SexagesimalDegreeFormat.DegreesDecimalMinutes => new Angle(System.Math.Abs(degrees), AngleUnit.Degree).ToUnitString(AngleUnit.Degree, "N0", true) + spacing + new Angle(decimalMinutes, AngleUnit.Arcminute).ToUnitString(AngleUnit.Arcminute, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 2)}", preferUnicode) + directional, // Show as degrees and decimal minutes.
          SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds => new Angle(System.Math.Abs(degrees), AngleUnit.Degree).ToUnitString(AngleUnit.Degree, "N0", true) + spacing + new Angle(System.Math.Abs(minutes), AngleUnit.Arcminute).ToUnitString(AngleUnit.Arcminute, "N0", preferUnicode) + spacing + new Angle(decimalSeconds, AngleUnit.Arcsecond).ToUnitString(AngleUnit.Arcsecond, $"N{(decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : 0)}", preferUnicode) + directional, // Show as degrees, minutes and decimal seconds.
          _ => throw new System.ArgumentOutOfRangeException(nameof(format)),
        };
      }

      #region Static methods
      /// <summary>Convert the angle specified in arcminutes to radians.</summary>
      public static double ConvertArcminuteToRadian(double arcminAngle)
            => arcminAngle / 3437.746771;

      /// <summary>Convert the angle specified in arcseconds to radians.</summary>
      public static double ConvertArcsecondToRadian(double arcsecAngle)
        => arcsecAngle / 206264.806247;

      public static (double decimalDegrees, double degrees, double decimalMinutes, double minutes, double decimalSeconds) ConvertDecimalDegreeToSexagesimalDegree(double decimalDegrees)
      {
        var absDegrees = System.Math.Abs(decimalDegrees);
        var floorAbsDegrees = System.Math.Floor(absDegrees);
        var fractionAbsDegrees = absDegrees - floorAbsDegrees;

        var degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
        var decimalMinutes = 60 * fractionAbsDegrees;
        var minutes = System.Math.Floor(decimalMinutes);
        var decimalSeconds = 3600 * fractionAbsDegrees - 60 * minutes;

        return (decimalDegrees, System.Convert.ToInt32(degrees), decimalMinutes, System.Convert.ToInt32(minutes), decimalSeconds);
      }

      ///// <summary>Return the rotation angle using the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static double ConvertCartesian2ToRotationAngle(double x, double y)
      //   => double.Atan2(y, x) is var atan2 && atan2 < 0 ? double.Tau + atan2 : atan2;

      ///// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      //public static double ConvertCartesian2ToRotationAngleEx(double x, double y)
      //  => double.Tau - (double.Atan2(y, -x) is var atan2 && atan2 < 0 ? double.Tau + atan2 : atan2);

      /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
      public static double ConvertDegreeToGradian(double degAngle)
        => degAngle * (10.0 / 9.0);

      /// <summary>Convert the angle specified in degrees to radians.</summary>
      public static double ConvertDegreeToRadian(double degAngle)
        => degAngle * GenericMath.PiOver180;

      public static double ConvertDegreeToTurn(double degAngle)
        => degAngle / 360;

      /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
      public static double ConvertGradianToDegree(double gradAngle)
        => gradAngle * 0.9;

      /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>

      public static double ConvertGradianToRadian(double gradAngle)
        => gradAngle * GenericMath.PiOver200;

      public static double ConvertGradianToTurn(double gradAngle)
        => gradAngle / 400;

      public static double ConvertMilliradianToRadian(double milliradAngle)
        => milliradAngle * 1000;

      public static double ConvertNatoMilToRadian(double milAngle)
        => milAngle * System.Math.PI / 3200;

      /// <summary>Convert the angle specified in radians to arcminutes.</summary>
      public static double ConvertRadianToArcminute(double radAngle)
        => radAngle * 3437.746771;

      /// <summary>Convert the angle specified in radians to arcseconds.</summary>
      public static double ConvertRadianToArcsecond(double radAngle)
        => radAngle * 206264.806247;

      /// <summary>Convert the angle specified in radians to degrees.</summary>
      public static double ConvertRadianToDegree(double radAngle)
        => radAngle * GenericMath.PiInto180;

      /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
      public static double ConvertRadianToGradian(double radAngle)
        => radAngle * GenericMath.PiInto200;

      public static double ConvertRadianToMilliradian(double radAngle)
        => radAngle / 1000;

      public static double ConvertRadianToNatoMil(double radAngle)
        => radAngle * 3200 / System.Math.PI;

      public static double ConvertRadianToTurn(double radAngle)
        => radAngle / GenericMath.PiX2;

      ///// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>

      //public static (double x, double y) ConvertRotationAngleToCartesian2(double radAngle)
      //  => (System.Math.Cos(radAngle), System.Math.Sin(radAngle));

      ///// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      ///// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>

      //public static (double x, double y) ConvertRotationAngleToCartesian2Ex(double radAngle)
      //  => ConvertRotationAngleToCartesian2(GenericMath.PiX2 - (radAngle % GenericMath.PiX2 is var rad && rad < 0 ? rad + GenericMath.PiX2 : rad) + GenericMath.PiOver2);

      public static double ConvertSexagesimalDegreeToDecimalDegree(double degrees, double minutes, double seconds)
        => degrees + minutes / 60 + seconds / 3600;

      public static double ConvertTurnToRadian(double revolutions)
        => revolutions * GenericMath.PiX2;

      public static Angle FromSexagesimalDegrees(double degrees, double minutes, double seconds)
        => new(ConvertDegreeToRadian(ConvertSexagesimalDegreeToDecimalDegree(degrees, minutes, seconds)));

      public static Angle ParseSexagesimalDegrees(string dms)
      {
        var re = new System.Text.RegularExpressions.Regex(@"(?<Degrees>\d+(\.\d+)?)[^0-9\.]*(?<Minutes>\d+(\.\d+)?)?[^0-9\.]*(?<Seconds>\d+(\.\d+)?)?[^ENWS]*(?<Direction>[ENWS])?");

        var decimalDegrees = 0.0;

        if (re.Match(dms) is var m && m.Success)
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

        return new(ConvertDegreeToRadian(decimalDegrees));
      }

      public static bool TryParseSexagesimalDegrees(string dms, out Angle result)
      {
        try
        {
          result = ParseSexagesimalDegrees(dms);
          return true;
        }
        catch
        {
          result = default;
          return false;
        }
      }
      #endregion Static methods

      #region Trigonometry static methods

      #region Gudermannian
      /// <summary>Returns the Gudermannian of the specified value.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
      public static double Gd(double value) => System.Math.Atan(System.Math.Sinh(value));

      // Inverse function:

      /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
      /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
      /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
      public static double Agd(double value) => System.Math.Atanh(System.Math.Sin(value));
      #endregion Gudermannian

      #region Hyperbolic Reciprocals/Inverse
      // Hyperbolic reciprocals (1 divided by):

      /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Csch(double v) => 1 / System.Math.Sinh(v);
      /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Sech(double v) => 1 / System.Math.Cosh(v);
      /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
      public static double Coth(double v) => System.Math.Cosh(v) / System.Math.Sinh(v);

      // Inverse hyperbolic reciprocals:

      /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Acsch(double v) => System.Math.Asinh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));
      /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Asech(double v) => System.Math.Acosh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);
      /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
      public static double Acoth(double v) => System.Math.Atanh(1 / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;
      #endregion Hyperbolic Reciprocals/Inverse

      #region Reciprocals/Inverse
      // Reciprocals (1 divided by):

      /// <summary>Returns the cosecant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Csc(double v) => 1 / System.Math.Sin(v);
      /// <summary>Returns the secant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Sec(double v) => 1 / System.Math.Cos(v);
      /// <summary>Returns the cotangent of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static double Cot(double v) => 1 / System.Math.Tan(v);

      // Inverse reciprocals:

      /// <summary>Returns the inverse cosecant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Acsc(double v) => System.Math.Asin(1 / v);
      /// <summary>Returns the inverse secant of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Asec(double v) => System.Math.Acos(1 / v);
      /// <summary>Returns the inverse cotangent of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
      public static double Acot(double v) => System.Math.Atan(1 / v);
      #endregion Reciprocals/Inverse

      #region Sinc
      /// <summary>Returns the normalized sinc of the specified value.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
      public static double Sincn(double value) => Sincu(System.Math.PI * value);

      /// <summary>Returns the unnormalized sinc of the specified value.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
      public static double Sincu(double value) => value != 0 ? System.Math.Sin(value) / value : 1;
      #endregion Sinc

      #region Versed/Inverse
      // Versed functions.

      /// <summary>Returns the versed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Vsin(double value) => 1 - System.Math.Cos(value);
      /// <summary>Returns the versed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Vcos(double value) => 1 + System.Math.Cos(value);
      /// <summary>Returns the coversed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Cvsin(double value) => 1 - System.Math.Sin(value);
      /// <summary>Returns the coversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Cvcos(double value) => 1 + System.Math.Sin(value);

      // Inverse versed functions:

      /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Avsin(double y) => System.Math.Acos(1 - y);
      /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Avcos(double y) => System.Math.Acos(y - 1);
      /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Acvsin(double y) => System.Math.Asin(1 - y);
      /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Acvcos(double y) => System.Math.Asin(y - 1);
      #endregion Versed/Inverse

      #region Haversed/Inverse
      // Haversed functions (half of the versed versions above):

      /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hvsin(double value) => (1 - System.Math.Cos(value)) / 2;
      /// <summary>Returns the haversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hvcos(double value) => (1 + System.Math.Cos(value)) / 2;
      /// <summary>Returns the hacoversed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hcvsin(double value) => (1 - System.Math.Sin(value)) / 2;
      /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static double Hcvcos(double value) => (1 + System.Math.Sin(value)) / 2;

      // Inversed haversed functions:

      /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahvsin(double y) => System.Math.Acos(1 - 2 * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));
      /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahvcos(double y) => System.Math.Acos(2 * y - 1); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));
      /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahcvsin(double y) => System.Math.Asin(1 - 2 * y);
      /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static double Ahcvcos(double y) => System.Math.Asin(2 * y - 1);
      #endregion Versine/Haversine

      #endregion Trigonometry static methods

      #region Overloaded operators
      public static explicit operator Angle(double value) => new(value);
      public static explicit operator double(Angle value) => value.m_radAngle;

      public static bool operator ==(Angle a, Angle b) => a.Equals(b);
      public static bool operator !=(Angle a, Angle b) => !a.Equals(b);

      public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
      public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) <= 0;
      public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
      public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) >= 0;

      public static Angle operator -(Angle v) => new(-v.m_radAngle);
      public static Angle operator +(Angle a, double b) => new(a.m_radAngle + b);
      public static Angle operator +(Angle a, Angle b) => a + b.m_radAngle;
      public static Angle operator /(Angle a, double b) => new(a.m_radAngle / b);
      public static Angle operator /(Angle a, Angle b) => a / b.m_radAngle;
      public static Angle operator *(Angle a, double b) => new(a.m_radAngle * b);
      public static Angle operator *(Angle a, Angle b) => a * b.m_radAngle;
      public static Angle operator %(Angle a, double b) => new(a.m_radAngle % b);
      public static Angle operator %(Angle a, Angle b) => a % b.m_radAngle;
      public static Angle operator -(Angle a, double b) => new(a.m_radAngle - b);
      public static Angle operator -(Angle a, Angle b) => a - b.m_radAngle;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Angle other) => m_radAngle.CompareTo(other.m_radAngle);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Angle o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
      #endregion IConvertible

      // IEquatable<>
      public bool Equals(Angle other) => m_radAngle == other.m_radAngle;

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{ToUnitString(DefaultUnit)}";

      public double Value { get => m_radAngle; init => m_radAngle = value; }
      // IUnitQuantifiable<>

      public string ToUnitString(AngleUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))}{unit.GetUnitSpacing(preferUnicode, useFullName)}{unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(AngleUnit unit = DefaultUnit)
        => unit switch
        {
          AngleUnit.Arcminute => ConvertRadianToArcminute(m_radAngle),
          AngleUnit.Arcsecond => ConvertRadianToArcsecond(m_radAngle),
          AngleUnit.Degree => ConvertRadianToDegree(m_radAngle),
          AngleUnit.Gradian => ConvertRadianToGradian(m_radAngle),
          AngleUnit.NatoMil => ConvertRadianToNatoMil(m_radAngle),
          AngleUnit.Milliradian => ConvertRadianToMilliradian(m_radAngle),
          AngleUnit.Radian => m_radAngle,
          AngleUnit.Turn => ConvertRadianToTurn(m_radAngle),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj) => obj is Angle o && Equals(o);
      public override int GetHashCode() => m_radAngle.GetHashCode();
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} ({ToUnitString(AngleUnit.Degree, @"N2")}) }}";
      #endregion Object overrides
    }
  }
}