namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.AngleUnit unit)
    {
      switch (unit)
      {
        case Quantity.AngleUnit.Arcminute:
          return Quantity.Angle.PrimeSymbol.ToString();
        case Quantity.AngleUnit.Arcsecond:
          return Quantity.Angle.DoublePrimeSymbol.ToString();
        case Quantity.AngleUnit.Degree:
          return @" deg";
        case Quantity.AngleUnit.Gradian:
          return @" grad";
        case Quantity.AngleUnit.Radian:
          return @" rad";
        case Quantity.AngleUnit.Turn:
          return @" turns";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
  }

  namespace Quantity
  {
    public enum AngleUnit
    {
      Arcminute,
      Arcsecond,
      Degree,
      Gradian,
      Radian,
      Turn,
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
      public const double OneFullRotationInRadians = Maths.PiX2;
      public const double OneFullRotationInTurns = 1;

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
          case AngleUnit.Turn:
            m_value = ConvertTurnToRadian(value);
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }
      }

      public double Degree
        => ConvertRadianToDegree(m_value);
      public double Radian
        => m_value;

      /// <summary>The quantity value in unit radian.</summary>
      public double Value
        => m_value;

      /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public CoordinateSystems.CartesianCoordinate2 ToCartesian2()
        => (CoordinateSystems.CartesianCoordinate2)ConvertRotationAngleToCartesian2(m_value);
      /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public CoordinateSystems.CartesianCoordinate2 ToCartesian2Ex()
        => (CoordinateSystems.CartesianCoordinate2)ConvertRotationAngleToCartesian2Ex(m_value);

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
          case AngleUnit.Turn:
            return ConvertRadianToTurn(m_value);
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
        => $"<{GetType().Name}: {m_value} rad ({Degree:N2}{DegreeSymbol})>";
      #endregion Object overrides
    }
  }
}
