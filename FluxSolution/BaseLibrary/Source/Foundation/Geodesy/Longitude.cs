using Flux.Riff.Smf;

namespace Flux
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public record struct Longitude
    : System.IComparable<Longitude>, System.IConvertible, IQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    public static Longitude Zero => new();

    private readonly double m_degLongitude;

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double degLongitude)
      => m_degLongitude = WrapLongitude(degLongitude);
    /// <summary>Creates a new Longitude from the specfied Angle instance. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(Angle longitude)
      : this(longitude.ToUnitValue(AngleUnit.Degree)) // Call base to ensure value is between min/max.
    { }

    public string SexagesimalDegreeString => ToSexagesimalDegreeString();

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    [System.Diagnostics.Contracts.Pure]
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_degLongitude);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    [System.Diagnostics.Contracts.Pure]
    public double GetMercatorProjectedX()
      => ToRadians();

    [System.Diagnostics.Contracts.Pure]
    public Angle ToAngle()
      => new(m_degLongitude, AngleUnit.Degree);

    [System.Diagnostics.Contracts.Pure]
    public double ToRadians()
      => Angle.ConvertDegreeToRadian(m_degLongitude);

    [System.Diagnostics.Contracts.Pure]
    public string ToSexagesimalDegreeString(SexagesimalDegreeFormat format = SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, bool useSpaces = false, bool preferUnicode = false)
      => ToAngle().ToSexagesimalDegreeString(format, SexagesimalDegreeDirection.EastWest, -1, useSpaces, preferUnicode);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static Longitude FromRadians(double radLongitude)
      => new(Angle.ConvertRadianToDegree(radLongitude));

    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="degLongitude"></param>
    [System.Diagnostics.Contracts.Pure]
    public static int GetTheoreticalTimezoneOffset(double degLongitude)
      => System.Convert.ToInt32(System.Math.Truncate((degLongitude + System.Math.CopySign(7.5, degLongitude)) / 15));

    /// <summary>A longitude is wrapped over within the range [-180, +180].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double WrapLongitude(double degLongitude)
      => Maths.Wrap(degLongitude, MinValue, MaxValue);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Longitude v) => v.m_degLongitude;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Longitude(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static Longitude operator -(Longitude v) => new(-v.m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public static Longitude operator +(Longitude a, double b) => new(WrapLongitude(a.m_degLongitude + b));
    [System.Diagnostics.Contracts.Pure] public static Longitude operator +(Longitude a, Longitude b) => a + b.Value;
    [System.Diagnostics.Contracts.Pure] public static Longitude operator /(Longitude a, double b) => new(WrapLongitude(a.m_degLongitude / b));
    [System.Diagnostics.Contracts.Pure] public static Longitude operator /(Longitude a, Longitude b) => a / b.Value;
    [System.Diagnostics.Contracts.Pure] public static Longitude operator *(Longitude a, double b) => new(WrapLongitude(a.m_degLongitude * b));
    [System.Diagnostics.Contracts.Pure] public static Longitude operator *(Longitude a, Longitude b) => a * b.Value;
    [System.Diagnostics.Contracts.Pure] public static Longitude operator %(Longitude a, double b) => new(WrapLongitude(a.m_degLongitude % b));
    [System.Diagnostics.Contracts.Pure] public static Longitude operator %(Longitude a, Longitude b) => a % b.Value;
    [System.Diagnostics.Contracts.Pure] public static Longitude operator -(Longitude a, double b) => new(WrapLongitude(a.m_degLongitude - b));
    [System.Diagnostics.Contracts.Pure] public static Longitude operator -(Longitude a, Longitude b) => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Longitude other) => m_degLongitude.CompareTo(other.m_degLongitude);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_degLongitude != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_degLongitude);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_degLongitude);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_degLongitude, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_degLongitude);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_degLongitude);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_degLongitude);
    #endregion IConvertible

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_degLongitude; init => m_degLongitude = value; }
    #endregion Implemented interfaces

    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {m_degLongitude}\u00B0, {ToSexagesimalDegreeString()} }}";
  }
}
