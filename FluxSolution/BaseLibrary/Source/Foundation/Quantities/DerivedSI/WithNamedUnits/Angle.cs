namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this AngleUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        AngleUnit.Arcminute => preferUnicode ? "\u2032" : "\u0027",
        AngleUnit.Arcsecond => preferUnicode ? "\u2033" : "\u0022",
        AngleUnit.Degree => preferUnicode ? "\u00B0" : "deg",
        AngleUnit.Gradian => "grad",
        AngleUnit.NatoMil => "mils",
        AngleUnit.Milliradian => "mrad",
        AngleUnit.Radian => preferUnicode ? "\u33ad" : "rad",
        AngleUnit.Turn => "turns",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

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
    : System.IComparable<Angle>, System.IConvertible, System.IEquatable<Angle>, System.IFormattable, ISiDerivedUnitQuantifiable<double, AngleUnit>
  {
    public const AngleUnit DefaultUnit = AngleUnit.Radian;

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
    public Angle(double value, AngleUnit unit = DefaultUnit)
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

    /// <summary>The quantity value in unit radian.</summary>
    public double Value
      => m_value;

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public CartesianCoordinate2 ToCartesian2()
      => (CartesianCoordinate2)ConvertRotationAngleToCartesian2(m_value);
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public CartesianCoordinate2 ToCartesian2Ex()
      => (CartesianCoordinate2)ConvertRotationAngleToCartesian2Ex(m_value);

    public string ToUnitString(AngleUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(useFullName, preferUnicode)}";
    public double ToUnitValue(AngleUnit unit = DefaultUnit)
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
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    public static double ConvertDegreeToGradian(double degree)
      => degree * (10.0 / 9.0);
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
    #endregion Static methods

    #region Trigonometry static methods

    #region Gudermannian
    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static double Gd(double value)
      => System.Math.Atan(System.Math.Sinh(value));

    // Inverse function:

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static double Agd(double value)
      => System.Math.Atanh(System.Math.Sin(value));
    #endregion Gudermannian

    #region Hyperbolic reciprocals/inverse
    // Hyperbolic reciprocals (1 divided by):

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Csch(double v)
      => 1 / System.Math.Sinh(v);
    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Sech(double v)
      => 1 / System.Math.Cosh(v);
    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static double Coth(double v)
      => System.Math.Cosh(v) / System.Math.Sinh(v);

    // Inverse hyperbolic reciprocals:

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acsch(double v)
      => System.Math.Asinh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));
    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Asech(double v)
      => System.Math.Acosh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);
    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public double Acoth(double v)
      => System.Math.Atanh(1 / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;
    #endregion Hyperbolic reciprocals/inverse

    #region Reciprocals/Inverse
    // Reciprocals (1 divided by):

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Csc(double v)
      => 1 / System.Math.Sin(v);
    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Sec(double v)
      => 1 / System.Math.Cos(v);
    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static double Cot(double v)
      => 1 / System.Math.Tan(v);

    // Inverse reciprocals:

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acsc(double v)
      => System.Math.Asin(1 / v);
    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Asec(double v)
      => System.Math.Acos(1 / v);
    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static double Acot(double v)
      => System.Math.Atan(1 / v);
    #endregion Reciprocals/Inverse

    #endregion Trigonometry static methods

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
