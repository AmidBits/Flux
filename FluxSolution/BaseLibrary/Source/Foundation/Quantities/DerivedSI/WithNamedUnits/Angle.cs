namespace Flux
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

  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this AngleUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        AngleUnit.Arcminute => preferUnicode ? Unicode.Prime.ToString() : Unicode.Apostrophe.ToString(),
        AngleUnit.Arcsecond => preferUnicode ? Unicode.DoublePrime.ToString() : Unicode.QuotationMark.ToString(),
        AngleUnit.Degree => preferUnicode ? Unicode.DegreeSign.ToString() : "deg",
        AngleUnit.Gradian => "grad",
        AngleUnit.NatoMil => "mils",
        AngleUnit.Milliradian => "mrad",
        AngleUnit.Radian => preferUnicode ? "\u33ad" : "rad",
        AngleUnit.Turn => "turns",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum AngleUnit
  {
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
  public struct Angle
    : System.IComparable, System.IComparable<Angle>, System.IConvertible, System.IEquatable<Angle>, System.IFormattable, IUnitQuantifiable<double, AngleUnit>
  {
    public const AngleUnit DefaultUnit = AngleUnit.Radian;

    /// <summary>This is the symbol for degree.</summary>
    public const char UnicodeDegreeSign = '\u00B0'; // Add 'C' or 'F' to designate "degree Celsius" or "degree Fahrenheit".
    /// <summary>This is the symbol for arc second.</summary>
    public const char UnicodeDoublePrime = '\u2033';
    /// <summary>This is the symbol for arc minute.</summary>
    public const char UnicodePrime = '\u2032';

    public const double OneFullRotationInDegrees = 360;
    public const double OneFullRotationInGradians = 400;
    public const double OneFullRotationInRadians = Maths.PiX2;
    public const double OneFullRotationInTurns = 1;

    private readonly double m_radAngle;

    //private Angle(double value)
    //  => m_value = value;
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

    [System.Diagnostics.Contracts.Pure] public double Degree => ConvertRadianToDegree(m_radAngle);

    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate2 ToCartesian2() => (CartesianCoordinate2)ConvertRotationAngleToCartesian2(m_radAngle);
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate2 ToCartesian2Ex() => (CartesianCoordinate2)ConvertRotationAngleToCartesian2Ex(m_radAngle);

    #region Static methods
    /// <summary>Convert the angle specified in arcminutes to radians.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertArcminuteToRadian(double arcminAngle) => arcminAngle / 3437.746771;
    /// <summary>Convert the angle specified in arcseconds to radians.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertArcsecondToRadian(double arcsecAngle) => arcsecAngle / 206264.806247;
    /// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertDegreeToGradian(double degAngle) => degAngle * (10.0 / 9.0);
    /// <summary>Convert the angle specified in degrees to radians.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertDegreeToRadian(double degAngle) => degAngle * Maths.PiOver180;
    [System.Diagnostics.Contracts.Pure] public static double ConvertDegreeToTurn(double degAngle) => degAngle / 360;
    /// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertGradianToDegree(double gradAngle) => gradAngle * 0.9;
    /// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertGradianToRadian(double gradAngle) => gradAngle * Maths.PiOver200;
    [System.Diagnostics.Contracts.Pure] public static double ConvertGradianToTurn(double gradAngle) => gradAngle / 400;
    [System.Diagnostics.Contracts.Pure] public static double ConvertMilliradianToRadian(double milliradAngle) => milliradAngle * 1000;
    [System.Diagnostics.Contracts.Pure] public static double ConvertNatoMilToRadian(double milAngle) => milAngle * System.Math.PI / 3200;
    /// <summary>Convert the angle specified in radians to arcminutes.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToArcminute(double radAngle) => radAngle * 3437.746771;
    /// <summary>Convert the angle specified in radians to arcseconds.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToArcsecond(double radAngle) => radAngle * 206264.806247;
    /// <summary>Convert the angle specified in radians to degrees.</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToDegree(double radAngle) => radAngle * Maths.PiInto180;
    /// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToGradian(double radAngle) => radAngle * Maths.PiInto200;
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToMilliradian(double radAngle) => radAngle / 1000;
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToNatoMil(double radAngle) => radAngle * 3200 / System.Math.PI;
    [System.Diagnostics.Contracts.Pure] public static double ConvertRadianToTurn(double radAngle) => radAngle / Maths.PiX2;
    /// <summary>Convert the specified counter-clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static (double x, double y) ConvertRotationAngleToCartesian2(double radAngle)
      => (System.Math.Cos(radAngle), System.Math.Sin(radAngle));
    /// <summary>Convert the specified clockwise rotation angle [0, PI*2] (radians) where 'zero' is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static (double x, double y) ConvertRotationAngleToCartesian2Ex(double radAngle)
      => ConvertRotationAngleToCartesian2(Maths.PiX2 - (radAngle % Maths.PiX2 is var rad && rad < 0 ? rad + Maths.PiX2 : rad) + Maths.PiOver2);
    [System.Diagnostics.Contracts.Pure] public static double ConvertTurnToRadian(double revolutions) => revolutions * Maths.PiX2;
    #endregion Static methods

    #region Trigonometry static methods

    #region Gudermannian
    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Gd(double value) => System.Math.Atan(System.Math.Sinh(value));

    // Inverse function:

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    [System.Diagnostics.Contracts.Pure] public static double Agd(double value) => System.Math.Atanh(System.Math.Sin(value));
    #endregion Gudermannian

    #region Hyperbolic Reciprocals/Inverse
    // Hyperbolic reciprocals (1 divided by):

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Csch(double v) => 1 / System.Math.Sinh(v);
    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Sech(double v) => 1 / System.Math.Cosh(v);
    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Coth(double v) => System.Math.Cosh(v) / System.Math.Sinh(v);

    // Inverse hyperbolic reciprocals:

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Acsch(double v) => System.Math.Asinh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));
    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Asech(double v) => System.Math.Acosh(1 / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);
    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Acoth(double v) => System.Math.Atanh(1 / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;
    #endregion Hyperbolic Reciprocals/Inverse

    #region Reciprocals/Inverse
    // Reciprocals (1 divided by):

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Csc(double v) => 1 / System.Math.Sin(v);
    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Sec(double v) => 1 / System.Math.Cos(v);
    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Cot(double v) => 1 / System.Math.Tan(v);

    // Inverse reciprocals:

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Acsc(double v) => System.Math.Asin(1 / v);
    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Asec(double v) => System.Math.Acos(1 / v);
    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Acot(double v) => System.Math.Atan(1 / v);
    #endregion Reciprocals/Inverse

    #region Sinc
    /// <summary>Returns the normalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Sincn(double value) => Sincu(System.Math.PI * value);

    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    [System.Diagnostics.Contracts.Pure] public static double Sincu(double value) => value != 0 ? System.Math.Sin(value) / value : 1;
    #endregion Sinc

    #region Versed/Inverse
    // Versed functions.

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Vsin(double value) => 1 - System.Math.Cos(value);
    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Vcos(double value) => 1 + System.Math.Cos(value);
    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Cvsin(double value) => 1 - System.Math.Sin(value);
    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Cvcos(double value) => 1 + System.Math.Sin(value);

    // Inverse versed functions:

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Avsin(double y) => System.Math.Acos(1 - y);
    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Avcos(double y) => System.Math.Acos(y - 1);
    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Acvsin(double y) => System.Math.Asin(1 - y);
    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Acvcos(double y) => System.Math.Asin(y - 1);
    #endregion Versed/Inverse

    #region Haversed/Inverse
    // Haversed functions (half of the versed versions above):

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Hvsin(double value) => (1 - System.Math.Cos(value)) / 2;
    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Hvcos(double value) => (1 + System.Math.Cos(value)) / 2;
    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Hcvsin(double value) => (1 - System.Math.Sin(value)) / 2;
    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    [System.Diagnostics.Contracts.Pure] public static double Hcvcos(double value) => (1 + System.Math.Sin(value)) / 2;

    // Inversed haversed functions:

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Ahvsin(double y) => System.Math.Acos(1 - 2 * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));
    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Ahvcos(double y) => System.Math.Acos(2 * y - 1); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));
    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Ahcvsin(double y) => System.Math.Asin(1 - 2 * y);
    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    [System.Diagnostics.Contracts.Pure] public static double Ahcvcos(double y) => System.Math.Asin(2 * y - 1);
    #endregion Versine/Haversine

    #endregion Trigonometry static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator Angle(double value) => new(value);
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Angle value) => value.m_radAngle;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Angle a, Angle b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Angle a, Angle b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static Angle operator -(Angle v) => new(-v.m_radAngle);
    [System.Diagnostics.Contracts.Pure] public static Angle operator +(Angle a, double b) => new(a.m_radAngle + b);
    [System.Diagnostics.Contracts.Pure] public static Angle operator +(Angle a, Angle b) => a + b.m_radAngle;
    [System.Diagnostics.Contracts.Pure] public static Angle operator /(Angle a, double b) => new(a.m_radAngle / b);
    [System.Diagnostics.Contracts.Pure] public static Angle operator /(Angle a, Angle b) => a / b.m_radAngle;
    [System.Diagnostics.Contracts.Pure] public static Angle operator *(Angle a, double b) => new(a.m_radAngle * b);
    [System.Diagnostics.Contracts.Pure] public static Angle operator *(Angle a, Angle b) => a * b.m_radAngle;
    [System.Diagnostics.Contracts.Pure] public static Angle operator %(Angle a, double b) => new(a.m_radAngle % b);
    [System.Diagnostics.Contracts.Pure] public static Angle operator %(Angle a, Angle b) => a % b.m_radAngle;
    [System.Diagnostics.Contracts.Pure] public static Angle operator -(Angle a, double b) => new(a.m_radAngle - b);
    [System.Diagnostics.Contracts.Pure] public static Angle operator -(Angle a, Angle b) => a - b.m_radAngle;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Angle other) => m_radAngle.CompareTo(other.m_radAngle);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Angle o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Angle other) => m_radAngle == other.m_radAngle;

    // IFormattable
    [System.Diagnostics.Contracts.Pure]
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider ?? new Formatting.AngleFormatter(), format ?? $"{GetType().Name} {{{{ {{0:D3}} }}}}", this);

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_radAngle;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(AngleUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(useFullName, preferUnicode)}";
    [System.Diagnostics.Contracts.Pure]
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
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Angle o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_radAngle.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} ({ToUnitString(AngleUnit.Degree, @"N2")}) }}";
    #endregion Object overrides
  }
}
