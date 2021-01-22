namespace Flux
{
  public struct Angle
    : System.IEquatable<Angle>
  {
    public const double MultiplierDegreeToRadian = System.Math.PI / 180;
    public const double MultiplierDegreeToGradian = 10d / 9d;
    public const double MultiplierGradianToDegree = 9d / 10d;
    public const double MultiplierGradianToRadian = System.Math.PI / 200;
    public const double MultiplierRadianToDegree = 180 / System.Math.PI;
    public const double MultiplierRadianToGradian = 200 / System.Math.PI;

    private readonly double m_radian;

    public double Degrees
      => ConvertRadianToDegree(m_radian);
    public double Gradians
      => ConvertRadianToGradian(m_radian);
    public double Radians
      => m_radian;

    public Angle(double radian)
      => m_radian = radian;

    #region // Statics
    public static Angle Add(Angle left, Angle right)
      => new Angle(left.m_radian + right.m_radian);
    public static Angle Divide(Angle left, Angle right)
      => new Angle(left.m_radian / right.m_radian);
    public static Angle FromDegree(double degree)
      => new Angle(degree * MultiplierDegreeToRadian);
    public static Angle FromGradian(double gradian)
      => new Angle(gradian * MultiplierGradianToRadian);
    public static Angle FromRadian(double radian)
      => new Angle(radian);
    public static Angle Multiply(Angle left, Angle right)
      => new Angle(left.m_radian * right.m_radian);
    public static Angle Negate(Angle value)
      => new Angle(-value.m_radian);
    public static Angle Remainder(Angle dividend, Angle divisor)
      => new Angle(dividend.m_radian % divisor.m_radian);
    public static Angle Subtract(Angle left, Angle right)
      => new Angle(left.m_radian - right.m_radian);

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngle(System.Numerics.Vector2 vector)
      => ConvertCartesianToRotationAngle(vector.X, vector.Y);
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertCartesianToRotationAngle(y, -x);
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertCartesianToRotationAngleEx(System.Numerics.Vector2 vector)
      => ConvertCartesianToRotationAngleEx(vector.X, vector.Y);
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double ConvertDegreeToGradian(double degrees)
      => degrees * MultiplierDegreeToGradian;
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    public static double ConvertDegreeToRadian(double degrees)
      => degrees * MultiplierDegreeToRadian;
    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    public static double ConvertGradianToDegree(double gradians)
      => gradians * MultiplierGradianToDegree;
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    public static double ConvertGradianToRadian(double gradians)
      => gradians * MultiplierGradianToRadian;
    /// <summary>Convert the angle specified in radians to degrees.</summary>
    public static double ConvertRadianToDegree(double radians)
      => radians * MultiplierRadianToDegree;
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    public static double ConvertRadianToGradian(double radians)
      => radians * MultiplierRadianToGradian;
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    public static System.Numerics.Vector2 ConvertRotationAngleToCartesian(double radians, out double x, out double y)
      => new System.Numerics.Vector2((float)(x = System.Math.Cos(radians)), (float)(y = System.Math.Sin(radians)));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    public static System.Numerics.Vector2 ConvertRotationAngleToCartesianEx(double radians, out double x, out double y)
      => ConvertRotationAngleToCartesian(Maths.PiX2 - (radians >= Maths.PiX2 ? radians % Maths.PiX2 : radians) + Maths.PiOver2, out x, out y);
    #endregion // Statics

    // Operators
    public static bool operator ==(Angle a, Angle b)
      => a.Equals(b);
    public static bool operator !=(Angle a, Angle b)
      => !a.Equals(b);
    public static Angle operator +(Angle a, Angle b)
      => Add(a, b);
    public static Angle operator /(Angle a, Angle b)
      => Divide(a, b);
    public static Angle operator *(Angle a, Angle b)
      => Multiply(a, b);
    public static Angle operator -(Angle v)
      => Negate(v);
    public static Angle operator %(Angle a, Angle b)
      => Remainder(a, b);
    public static Angle operator -(Angle a, Angle b)
      => Subtract(a, b);

    // IEquatable<Angle>
    public bool Equals(Angle other)
      => m_radian == other.m_radian;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new IFormatProvider.AngleFormatProvider(), format ?? $"<{nameof(Angle)}: {{0:D3}}>", this);
    // Overrides
    public override bool Equals(object? obj)
      => obj is Angle o && Equals(o);
    public override int GetHashCode()
      => m_radian.GetHashCode();
    public override string ToString()
      => ToString(null, null);
  }
}
