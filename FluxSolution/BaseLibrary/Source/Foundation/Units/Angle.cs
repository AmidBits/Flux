namespace Flux.Units
{
  /// <summary>Represents an angle (stored as a radians and implicitly convertible to/from double and radian).</summary>
  public struct Angle
    : System.IComparable<Angle>, System.IEquatable<Angle>, System.IFormattable
  {
    public const double DegreeFullRotation = 360;
    public const double GradianFullRotation = 400;
    public const double RadianFullRotation = System.Math.PI * 2;
    public const double RevolutionFullRotation = 1;

    private readonly double m_radian;

    public Angle(double radian)
      => m_radian = Maths.Wrap(radian, 0, System.Math.PI * 2);

    public (double x, double y) Cartesian
      => ConvertRotationAngleToCartesian(m_radian, out var _, out var _);
    public (double x, double y) CartesianEx
      => ConvertRotationAngleToCartesianEx(m_radian, out var _, out var _);
    public double Degrees
      => ConvertRadiansToDegrees(m_radian);
    public double Gradians
      => ConvertRadiansToGradians(m_radian);
    public double Radians
      => m_radian;
    public double Revolutions
      => ConvertRadiansToRevolutions(m_radian);

    #region Static methods
    public static Angle Add(Angle left, Angle right)
      => new Angle(left.m_radian + right.m_radian);
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertCartesianToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double ConvertDegreesToGradians(double degree)
      => degree * (10d / 9d);
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double ConvertDegreesToRadians(double degree)
      => degree * (System.Math.PI / 180);
    public static double ConvertDegreesToRevolutions(double degree)
      => degree / 360;
    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double ConvertGradiansToDegrees(double gradian)
      => gradian * (9d / 10d);
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double ConvertGradiansToRadians(double gradian)
      => gradian * (System.Math.PI / 200);
    public static double ConvertGradiansToRevolutions(double gradian)
      => gradian / 400;
    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double ConvertRadiansToDegrees(double radian)
      => radian * (180 / System.Math.PI);
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double ConvertRadiansToGradians(double radian)
      => radian * (200 / System.Math.PI);
    public static double ConvertRadiansToRevolutions(double radian)
      => radian / (System.Math.PI * 2);
    public static double ConvertRevolutionsToRadians(double revolutions)
      => revolutions * (System.Math.PI * 2);
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesian(double radian, out double x, out double y)
      => (x = System.Math.Cos(radian), y = System.Math.Sin(radian));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static (double x, double y) ConvertRotationAngleToCartesianEx(double radian, out double x, out double y)
      => ConvertRotationAngleToCartesian(Maths.PiX2 - (radian >= Maths.PiX2 ? radian % Maths.PiX2 : radian) + Maths.PiOver2, out x, out y);
    public static Angle Divide(Angle left, Angle right)
      => new Angle(left.m_radian / right.m_radian);
    public static Angle FromCartesian(double x, double y)
      => new Angle(ConvertCartesianToRotationAngle(x, y));
    public static Angle FromCartesianEx(double x, double y)
      => new Angle(ConvertCartesianToRotationAngleEx(x, y));
    public static Angle FromDegree(double degree)
      => new Angle(ConvertDegreesToRadians(degree));
    public static Angle FromGradian(double gradian)
      => new Angle(ConvertGradiansToRadians(gradian));
    public static Angle FromRadian(double radian)
      => new Angle(radian);
    public static Angle FromRevolutions(double turns)
      => new Angle(ConvertRevolutionsToRadians(turns));
    public static Angle Multiply(Angle left, Angle right)
      => new Angle(left.m_radian * right.m_radian);
    public static Angle Negate(Angle value)
      => new Angle(-value.m_radian);
    public static Angle Remainder(Angle dividend, Angle divisor)
      => new Angle(dividend.m_radian % divisor.m_radian);
    public static Angle Subtract(Angle left, Angle right)
      => new Angle(left.m_radian - right.m_radian);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator Angle(double value)
      => new Angle(value);
    public static implicit operator double(Angle value)
      => value.m_radian;

    public static bool operator ==(Angle a, Angle b)
      => a.Equals(b);
    public static bool operator !=(Angle a, Angle b)
      => !a.Equals(b);

    public static bool operator <(Angle a, Angle b)
   => a.CompareTo(b) < 0;
    public static bool operator <=(Angle a, Angle b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Angle a, Angle b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Angle a, Angle b)
      => a.CompareTo(b) <= 0;

    public static Angle operator +(Angle a, Angle b)
      => Add(a, b);
    public static Angle operator /(Angle a, Angle b)
      => Divide(a, b);
    public static Angle operator %(Angle a, Angle b)
      => Remainder(a, b);
    public static Angle operator *(Angle a, Angle b)
      => Multiply(a, b);
    public static Angle operator -(Angle a, Angle b)
      => Subtract(a, b);
    public static Angle operator -(Angle v)
      => Negate(v);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Angle other)
      => m_radian.CompareTo(other.m_radian);

    // IEquatable<Angle>
    public bool Equals(Angle other)
      => m_radian == other.m_radian;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new Formatting.AngleFormatter(), format ?? $"<{nameof(Angle)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Angle o && Equals(o);
    public override int GetHashCode()
      => m_radian.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
