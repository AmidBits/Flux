namespace Flux.Quantity
{
  public enum AngleUnit
  {
    Arcminute,
    Arcsecond,
    Degree,
    Gradian,
    Radian,
    Revolution,
  }

  /// <summary>Plane angle, unit of radian. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angle"/>
  public struct Angle
    : System.IComparable<Angle>, System.IEquatable<Angle>, System.IFormattable, IValuedUnit
  {
    public const char DegreeSymbol = '\u00B0'; // Add 'C' or 'F' to designate "degree Celsius" or "degree Fahrenheit".
    public const char DoublePrimeSymbol = '\u2033'; // Designates arc second.
    public const char PrimeSymbol = '\u2032'; // Designates arc minute.

    public const double OneFullRotationInDegrees = 360;
    public const double OneFullRotationInGradians = 400;
    public const double OneFullRotationInRadians = System.Math.PI * 2;
    public const double OneFullRotationInRevolutions = 1;

    private readonly double m_value;

    public Angle(double value, AngleUnit unit = AngleUnit.Radian)
    {
      switch (unit)
      {
        case AngleUnit.Arcminute:
          m_value = ConvertArcminuteToRadian(value);
          break;
        case AngleUnit.Arcsecond:
          m_value = ConvertArcsecondToRadian(value);
          break;
        case AngleUnit.Degree:
          m_value = ConvertDegreeToRadian(value);
          break;
        case AngleUnit.Gradian:
          m_value = ConvertGradianToRadian(value);
          break;
        case AngleUnit.Radian:
          m_value = value;
          break;
        case AngleUnit.Revolution:
          m_value = ConvertRevolutionToRadian(value);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Degree
      => ConvertRadianToDegree(m_value);
    public double Radian
      => m_value;

    public double Value
      => m_value;

    public (double x, double y) ToCartesian()
      => ConvertRotationAngleToCartesian(m_value, out var _, out var _);
    public (double x, double y) ToCartesianEx()
      => ConvertRotationAngleToCartesianEx(m_value, out var _, out var _);
    public double ToUnitValue(AngleUnit unit = AngleUnit.Radian)
    {
      switch (unit)
      {
        case AngleUnit.Arcminute:
          return ConvertRadianToArcminute(m_value);
        case AngleUnit.Arcsecond:
          return ConvertRadianToArcsecond(m_value);
        case AngleUnit.Degree:
          return ConvertRadianToDegree(m_value);
        case AngleUnit.Gradian:
          return ConvertRadianToGradian(m_value);
        case AngleUnit.Radian:
          return m_value;
        case AngleUnit.Revolution:
          return ConvertRadianToRevolution(m_value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Convert the angle specified in arcminutes to radians.</summary>
    public static double ConvertArcminuteToRadian(double arcminute)
      => arcminute / 3437.746771;
    /// <summary>Convert the angle specified in arcseconds to radians.</summary>
    public static double ConvertArcsecondToRadian(double arcsecond)
      => arcsecond / 206264.806247;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertCartesianToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double ConvertDegreeToGradian(double degree)
      => degree * (10d / 9d);
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double ConvertDegreeToRadian(double degree)
      => degree * (System.Math.PI / 180);
    public static double ConvertDegreeToRevolution(double degree)
      => degree / 360;
    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double ConvertGradianToDegree(double gradian)
      => gradian * (9d / 10d);
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double ConvertGradianToRadian(double gradian)
      => gradian * (System.Math.PI / 200);
    public static double ConvertGradianToRevolution(double gradian)
      => gradian / 400;
    /// <summary>Convert the angle specified in radians to arcminutes.</summary>
    public static double ConvertRadianToArcminute(double radian)
      => radian * 3437.746771;
    /// <summary>Convert the angle specified in radians to arcseconds.</summary>
    public static double ConvertRadianToArcsecond(double radian)
      => radian * 206264.806247;
    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double ConvertRadianToDegree(double radian)
      => radian * (180 / System.Math.PI);
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double ConvertRadianToGradian(double radian)
      => radian * (200 / System.Math.PI);
    public static double ConvertRadianToRevolution(double radian)
      => radian / (System.Math.PI * 2);
    public static double ConvertRevolutionToRadian(double revolutions)
      => revolutions * (System.Math.PI * 2);
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesian(double radian, out double x, out double y)
      => (x = System.Math.Cos(radian), y = System.Math.Sin(radian));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesianEx(double radian, out double x, out double y)
      => ConvertRotationAngleToCartesian(Maths.PiX2 - (radian >= Maths.PiX2 ? radian % Maths.PiX2 : radian) + Maths.PiOver2, out x, out y);

    public static Angle FromCartesian(double x, double y)
      => new Angle(ConvertCartesianToRotationAngle(x, y));
    public static Angle FromCartesianEx(double x, double y)
      => new Angle(ConvertCartesianToRotationAngleEx(x, y));

    public static string GetUnitSymbol(AngleUnit unit)
    {
      switch (unit)
      {
        case AngleUnit.Arcminute:
          return PrimeSymbol.ToString();
        case AngleUnit.Arcsecond:
          return DoublePrimeSymbol.ToString();
        case AngleUnit.Degree:
          return @" deg";
        case AngleUnit.Gradian:
          return @" grad";
        case AngleUnit.Radian:
          return @" rad";
        case AngleUnit.Revolution:
          return @" turns";
        default:
          return string.Empty;
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator Angle(double value)
      => new Angle(value);
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
      => new Angle(-v.m_value);
    public static Angle operator +(Angle a, double b)
      => new Angle(a.m_value + b);
    public static Angle operator +(Angle a, Angle b)
      => a + b.m_value;
    public static Angle operator /(Angle a, double b)
      => new Angle(a.m_value / b);
    public static Angle operator /(Angle a, Angle b)
      => a / b.m_value;
    public static Angle operator *(Angle a, double b)
      => new Angle(a.m_value * b);
    public static Angle operator *(Angle a, Angle b)
      => a * b.m_value;
    public static Angle operator %(Angle a, double b)
      => new Angle(a.m_value % b);
    public static Angle operator %(Angle a, Angle b)
      => a % b.m_value;
    public static Angle operator -(Angle a, double b)
      => new Angle(a.m_value - b);
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
      => string.Format(formatProvider ?? new Formatting.AngleFormatter(), format ?? $"<{nameof(Angle)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Angle o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} rad>";
    #endregion Object overrides
  }
}
