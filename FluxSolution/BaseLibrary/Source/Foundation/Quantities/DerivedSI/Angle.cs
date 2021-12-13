namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this AngleUnit unit)
      => unit switch
      {
        AngleUnit.Arcminute => Angle.PrimeSymbol.ToString(),
        AngleUnit.Arcsecond => Angle.DoublePrimeSymbol.ToString(),
        AngleUnit.Degree => Angle.DegreeSymbol.ToString(),
        AngleUnit.Gradian => @" grad",
        AngleUnit.NatoMil => @" mils",
        AngleUnit.Milliradian => @" mrad",
        AngleUnit.Radian => @" rad",
        AngleUnit.Turn => @" turns",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AngleUnit
  {
    Arcminute,
    Arcsecond,
    Degree,
    Gradian,
    /// <summary>This is the NATO angle of mils.</summary>
    NatoMil,
    /// <summary>This is sometimes also refered to as a 'mil'.</summary>
    Milliradian,
    Radian,
    Turn,
  }

  /// <summary>Plane angle, unit of radian. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angle"/>
  public struct Angle
    : System.IComparable<Angle>, System.IEquatable<Angle>, System.IFormattable, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const char DegreeSymbol = '\u00B0'; // Add 'C' or 'F' to designate "degree Celsius" or "degree Fahrenheit".
    public const char DoublePrimeSymbol = '\u2033'; // Designates arc second.
    public const char PrimeSymbol = '\u2032'; // Designates arc minute.

    public const double OneFullRotationInDegrees = 360;
    public const double OneFullRotationInGradians = 400;
    public const double OneFullRotationInRadians = Maths.PiX2;
    public const double OneFullRotationInTurns = 1;

    private readonly double m_value;

    //private Angle(double value)
    //  => m_value = value;
    public Angle(double value, AngleUnit unit = AngleUnit.Radian)
      => m_value = unit switch
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

    public double DerivedUnitValue
      => m_value;

    /// <summary>The quantity value in unit radian.</summary>
    public double GeneralUnitValue
      => m_value;

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public CartesianCoordinate2 ToCartesian2()
      => (CartesianCoordinate2)ConvertRotationAngleToCartesian2(m_value);
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public CartesianCoordinate2 ToCartesian2Ex()
      => (CartesianCoordinate2)ConvertRotationAngleToCartesian2Ex(m_value);

    public string ToUnitString(AngleUnit unit = AngleUnit.Radian, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(AngleUnit unit = AngleUnit.Radian)
      => unit switch
      {
        AngleUnit.Arcminute => ConvertRadianToArcminute(m_value),
        AngleUnit.Arcsecond => ConvertRadianToArcsecond(m_value),
        AngleUnit.Degree => ConvertRadianToDegree(m_value),
        AngleUnit.Gradian => ConvertRadianToGradian(m_value),
        AngleUnit.NatoMil => ConvertRadianToNatoMil(m_value),
        AngleUnit.Milliradian => ConvertRadianToMilliradian(m_value),
        AngleUnit.Radian => m_value,
        AngleUnit.Turn => ConvertRadianToTurn(m_value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Convert the angle specified in arcminutes to radians.</summary>
    public static double ConvertArcminuteToRadian(double arcminute)
      => arcminute / 3437.746771;
    /// <summary>Convert the angle specified in arcseconds to radians.</summary>
    public static double ConvertArcsecondToRadian(double arcsecond)
      => arcsecond / 206264.806247;
    private const double DegreeToGradianMultiplier = 10.0 / 9.0;
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double ConvertDegreeToGradian(double degree)
      => degree * DegreeToGradianMultiplier;
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double ConvertDegreeToRadian(double degree)
      => degree * Maths.PiOver180;
    public static double ConvertDegreeToTurn(double degree)
      => degree / 360;
    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double ConvertGradianToDegree(double gradian)
      => gradian * 0.9;
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double ConvertGradianToRadian(double gradian)
      => gradian * Maths.PiOver200;
    public static double ConvertGradianToTurn(double gradian)
      => gradian / 400;
    public static double ConvertMilliradianToRadian(double milliradian)
      => milliradian * 1000;
    public static double ConvertNatoMilToRadian(double mil)
      => mil * System.Math.PI / 3200;
    /// <summary>Convert the angle specified in radians to arcminutes.</summary>
    public static double ConvertRadianToArcminute(double radian)
      => radian * 3437.746771;
    /// <summary>Convert the angle specified in radians to arcseconds.</summary>
    public static double ConvertRadianToArcsecond(double radian)
      => radian * 206264.806247;
    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double ConvertRadianToDegree(double radian)
      => radian * Maths.PiInto180;
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double ConvertRadianToGradian(double radian)
      => radian * Maths.PiInto200;
    public static double ConvertRadianToMilliradian(double radian)
      => radian / 1000;
    public static double ConvertRadianToNatoMil(double radian)
      => radian * 3200 / System.Math.PI;
    public static double ConvertRadianToTurn(double radian)
      => radian / Maths.PiX2;
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesian2(double radian)
      => (System.Math.Cos(radian), System.Math.Sin(radian));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesian2Ex(double radian)
      => ConvertRotationAngleToCartesian2(Maths.PiX2 - (radian % Maths.PiX2 is var rad && rad < 0 ? rad + Maths.PiX2 : rad) + Maths.PiOver2);
    public static double ConvertTurnToRadian(double revolutions)
      => revolutions * Maths.PiX2;

    //public static Angle FromCartesian2(double x, double y)
    //  => new Angle(CartesianCoord2.ConvertToRotationAngle(x, y));
    //public static Angle FromCartesian2Ex(double x, double y)
    //  => new Angle(CartesianCoord2.ConvertToRotationAngleEx(x, y));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator Angle(double value)
      => new(value);
    public static explicit operator double(Angle value)
      => value.m_value;

    public static bool operator ==(Angle a, Angle b)
      => a.Equals(b);
    public static bool operator !=(Angle a, Angle b)
      => !a.Equals(b);

    public static bool operator <(Angle a, Angle b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Angle a, Angle b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Angle a, Angle b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Angle a, Angle b)
      => a.CompareTo(b) >= 0;

    public static Angle operator -(Angle v)
      => new(-v.m_value);
    public static Angle operator +(Angle a, double b)
      => new(a.m_value + b);
    public static Angle operator +(Angle a, Angle b)
      => a + b.m_value;
    public static Angle operator /(Angle a, double b)
      => new(a.m_value / b);
    public static Angle operator /(Angle a, Angle b)
      => a / b.m_value;
    public static Angle operator *(Angle a, double b)
      => new(a.m_value * b);
    public static Angle operator *(Angle a, Angle b)
      => a * b.m_value;
    public static Angle operator %(Angle a, double b)
      => new(a.m_value % b);
    public static Angle operator %(Angle a, Angle b)
      => a % b.m_value;
    public static Angle operator -(Angle a, double b)
      => new(a.m_value - b);
    public static Angle operator -(Angle a, Angle b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Angle other)
      => m_value.CompareTo(other.m_value);

    // IEquatable<Angle>
    public bool Equals(Angle other)
      => m_value == other.m_value;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new Formatting.AngleFormatter(), format ?? $"{GetType().Name} {{{{ {{0:D3}} }}}}", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Angle o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} ({ToUnitString(AngleUnit.Degree, @"N2")}) }}";
    #endregion Object overrides
  }
}
