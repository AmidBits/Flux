namespace Flux.Quantities
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public readonly record struct Longitude
    : System.IComparable<Longitude>, System.IConvertible, IQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_longitude;

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double longitude) => m_longitude = WrapLongitude(longitude);
    /// <summary>Creates a new Longitude from the specfied Angle instance. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(Quantities.Angle angle) : this(angle.ToUnitValue(Quantities.AngleUnit.Degree)) { } // Call base to ensure value is between min/max.

    public string SexagesimalDegreeString => ToSexagesimalDegreeString();

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset => GetTheoreticalTimezoneOffset(m_longitude);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX() => ToRadians();

    public Quantities.Angle ToAngle() => new(m_longitude, Quantities.AngleUnit.Degree);

    public double ToRadians() => Quantities.Angle.ConvertDegreeToRadian(m_longitude);

    public string ToSexagesimalDegreeString(Quantities.SexagesimalDegreeFormat format = Quantities.SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, bool useSpaces = false, bool preferUnicode = false)
      => ToAngle().ToSexagesimalDegreeString(format, Quantities.SexagesimalDegreeDirection.WestEast, -1, useSpaces, preferUnicode);

    #region Static methods

    /// <summary></summary>
    /// <param name="lon">The longitude in radians.</param>
    public static Longitude FromRadians(double lon) => new(Quantities.Angle.ConvertRadianToDegree(lon));

    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    public static int GetTheoreticalTimezoneOffset(double longitude) => System.Convert.ToInt32(System.Math.Truncate((longitude + System.Math.CopySign(7.5, longitude)) / 15));

    /// <summary>A longitude is wrapped over within the range [-180, +180].</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    public static double WrapLongitude(double longitude) => longitude.Wrap(MinValue, MaxValue);

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator double(Longitude v) => v.m_longitude;
    public static explicit operator Longitude(double v) => new(v);

    public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    public static Longitude operator -(Longitude v) => new(-v.m_longitude);
    public static Longitude operator +(Longitude a, double b) => new(a.m_longitude + b);
    public static Longitude operator +(Longitude a, Longitude b) => a + b.Value;
    public static Longitude operator /(Longitude a, double b) => new(a.m_longitude / b);
    public static Longitude operator /(Longitude a, Longitude b) => a / b.Value;
    public static Longitude operator *(Longitude a, double b) => new(a.m_longitude * b);
    public static Longitude operator *(Longitude a, Longitude b) => a * b.Value;
    public static Longitude operator %(Longitude a, double b) => new(a.m_longitude % b);
    public static Longitude operator %(Longitude a, Longitude b) => a % b.Value;
    public static Longitude operator -(Longitude a, double b) => new(a.m_longitude - b);
    public static Longitude operator -(Longitude a, Longitude b) => a - b.Value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Longitude other) => m_longitude.CompareTo(other.m_longitude);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => m_longitude != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_longitude);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_longitude);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_longitude);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_longitude);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_longitude);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_longitude);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_longitude);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_longitude);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_longitude);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_longitude);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_longitude);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_longitude, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_longitude);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_longitude);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_longitude);
    #endregion IConvertible

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => new Angle(m_longitude, AngleUnit.Degree).ToUnitString(AngleUnit.Degree, format, preferUnicode, useFullName);

    public double Value { get => m_longitude; init => m_longitude = value; }

    #endregion Implemented interfaces

    public override string ToString() => ToSexagesimalDegreeString();
  }
}
