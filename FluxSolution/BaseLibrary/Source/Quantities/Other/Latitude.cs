namespace Flux.Quantities
{
  /// <summary>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90]. Arithmetic results are clamped within the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public readonly record struct Latitude
    : System.IComparable<Latitude>, System.IConvertible, IQuantifiable<double>
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    public static Latitude Zero => new();

    public static Latitude TropicOfCancer => new(23.43648);
    public static Latitude TropicOfCapricorn => new(-23.43648);

    private readonly double m_degLatitude;

    /// <summary>Creates a new Latitude from the specified number of degrees. The value is folded within the degree range [-90, +90]. Folding means oscillating within the range. This means any corresponding Longitude needs to be adjusted by 180 degrees, if synchronization is required.</summary>
    public Latitude(double degLatitude)
      => m_degLatitude = FoldLatitude(degLatitude);
    /// <summary>Creates a new Latitude from the specfied Angle instance. The value is folded within the degree range [-90, +90]. Folding means oscillating within the range. This means any corresponding Longitude needs to be adjusted by 180 degrees, if synchronization is required.</summary>
    public Latitude(Quantities.Angle latitude)
      : this(latitude.ToUnitValue(Quantities.AngleUnit.Degree)) // Call base to ensure value is between min/max.
    { }

    /// <summary>Computes the approximate length in meters per degree of latitudinal height at the specified latitude.</summary>

    public Quantities.Length ApproximateLatitudinalHeight
      => new(GetApproximateLatitudinalHeight(ToRadians()));

    /// <summary>Computes the approximate length in meters per degree of longitudinal width at the specified latitude.</summary>

    public Quantities.Length ApproximateLongitudinalWidth
      => new(GetApproximateLongitudinalWidth(ToRadians()));

    /// <summary>Determines an approximate radius in meters at the specified latitude.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>

    public Quantities.Length ApproximateRadius
      => new(GetApproximateRadius(ToRadians()));

    public string SexagesimalDegreeString => ToSexagesimalDegreeString();

    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas

    public double GetMercatorProjectedY()
      => System.Math.Clamp(System.Math.Log((System.Math.Tan(GenericMath.PiOver4 + ToRadians() / 2))), -System.Math.PI, System.Math.PI);


    public Quantities.Angle ToAngle()
      => new(m_degLatitude, Quantities.AngleUnit.Degree);


    public double ToRadians()
      => Quantities.Angle.ConvertDegreeToRadian(m_degLatitude);


    public string ToSexagesimalDegreeString(Quantities.SexagesimalDegreeFormat format = Quantities.SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, bool useSpaces = false, bool preferUnicode = false)
      => ToAngle().ToSexagesimalDegreeString(format, Quantities.SexagesimalDegreeDirection.NorthSouth, -1, useSpaces, preferUnicode);

    #region Static methods
    /// <summary>A latitude is folded over the range [-90, +90].</summary>

    public static double FoldLatitude(double degLatitude)
      => degLatitude.Fold(MinValue, MaxValue);


    public static Latitude FromRadians(double radLatitude)
      => new(Quantities.Angle.ConvertRadianToDegree(radLatitude) % MaxValue);

    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>

    public static double GetApproximateLatitudinalHeight(double radLatitude)
      => 111132.954 + -559.822 * System.Math.Cos(2 * radLatitude) + 1.175 * System.Math.Cos(4 * radLatitude) + -0.0023 * System.Math.Cos(6 * radLatitude);

    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>

    public static double GetApproximateLongitudinalWidth(double radLatitude)
      => 111412.84 * System.Math.Cos(radLatitude) + -93.5 * System.Math.Cos(3 * radLatitude) + 0.118 * System.Math.Cos(5 * radLatitude);

    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>

    public static double GetApproximateRadius(double radLatitude)
    {
      var cos = System.Math.Cos(radLatitude);
      var sin = System.Math.Sin(radLatitude);

      var numerator = System.Math.Pow(System.Math.Pow(EarthWgs84.EquatorialRadius.Value, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(EarthWgs84.PolarRadius.Value, 2) * sin, 2);
      var denominator = System.Math.Pow(EarthWgs84.EquatorialRadius.Value * cos, 2) + System.Math.Pow(EarthWgs84.PolarRadius.Value * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>

    public static double GetMaximumLatitude(double radLatitude, double radAzimuth)
      => System.Math.Acos(System.Math.Abs(System.Math.Sin(radAzimuth) * System.Math.Cos(radLatitude)));
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v) => v.m_degLatitude;
    public static explicit operator Latitude(double v) => new(v);

    public static bool operator <(Latitude a, Latitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Latitude a, Latitude b) => a.CompareTo(b) >= 0;

    public static Latitude operator -(Latitude v) => new(-v.m_degLatitude);
    public static Latitude operator +(Latitude a, double b) => new(a.m_degLatitude + b);
    public static Latitude operator +(Latitude a, Latitude b) => a + b.Value;
    public static Latitude operator /(Latitude a, double b) => new(a.m_degLatitude / b);
    public static Latitude operator /(Latitude a, Latitude b) => a / b.Value;
    public static Latitude operator *(Latitude a, double b) => new(a.m_degLatitude * b);
    public static Latitude operator *(Latitude a, Latitude b) => a * b.Value;
    public static Latitude operator %(Latitude a, double b) => new(a.m_degLatitude % b);
    public static Latitude operator %(Latitude a, Latitude b) => a % b.Value;
    public static Latitude operator -(Latitude a, double b) => new(a.m_degLatitude - b);
    public static Latitude operator -(Latitude a, Latitude b) => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Latitude other) => m_degLatitude.CompareTo(other.m_degLatitude);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is Latitude o ? CompareTo(o) : -1;

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => m_degLatitude != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_degLatitude);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_degLatitude);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_degLatitude);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_degLatitude);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_degLatitude);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_degLatitude);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_degLatitude);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_degLatitude);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_degLatitude);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_degLatitude);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_degLatitude);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_degLatitude, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_degLatitude);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_degLatitude);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_degLatitude);
    #endregion IConvertible

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => new Angle(m_degLatitude, AngleUnit.Degree).ToUnitString(AngleUnit.Degree, format, preferUnicode, useFullName);

    public double Value { get => m_degLatitude; init => m_degLatitude = value; }
    #endregion Implemented interfaces

    #region Object overrides
    public override string ToString()
      => $"{GetType().Name} {{ {ToQuantityString()}, {ToSexagesimalDegreeString()} }}";
    #endregion Object overrides
  }
}
