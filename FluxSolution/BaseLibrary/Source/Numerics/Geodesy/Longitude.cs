namespace Flux.Quantities
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east�west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public readonly record struct Longitude
    : System.IComparable<Longitude>, IQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_longitude;

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double longitude) => m_longitude = WrapLongitude(longitude);
    /// <summary>Creates a new Longitude from the specfied Angle instance. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(Quantities.Angle angle) : this(angle.ToUnitValue(Quantities.AngleUnit.Degree)) { } // Call base to ensure value is between min/max.

    public double InRadians => Quantities.Angle.ConvertDegreeToRadian(m_longitude);

    public string SexagesimalDegreeString => ToSexagesimalDegreeString();

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset => GetTheoreticalTimezoneOffset(m_longitude);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX() => InRadians;

    public Quantities.Angle ToAngle() => new(m_longitude, Quantities.AngleUnit.Degree);

    public string ToSexagesimalDegreeString(SexagesimalDegreeFormat format = SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, bool useSpaces = false, bool preferUnicode = false)
      => format.ToSexagesimalDegreeString(m_longitude, SexagesimalDegreeDirection.WestEast, -1, useSpaces, preferUnicode);

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

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = true, bool useFullName = false)
    {
      if (format is not null)
      {
        if (format.StartsWith(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds.GetAcronymString()))
          return ToSexagesimalDegreeString(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, format.EndsWith(' '), preferUnicode);
        if (format.StartsWith(SexagesimalDegreeFormat.DegreesDecimalMinutes.GetAcronymString()))
          return ToSexagesimalDegreeString(SexagesimalDegreeFormat.DegreesDecimalMinutes, format.EndsWith(' '), preferUnicode);
        if (format.StartsWith(SexagesimalDegreeFormat.DecimalDegrees.GetAcronymString()))
          return ToSexagesimalDegreeString(SexagesimalDegreeFormat.DecimalDegrees, format.EndsWith(' '), preferUnicode);

        return new Angle(m_longitude, AngleUnit.Degree).ToUnitString(AngleUnit.Degree, format, preferUnicode, useFullName);
      }

      return ToSexagesimalDegreeString();
    }

    public double Value { get => m_longitude; init => m_longitude = value; }

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString(SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds.GetAcronymString());
  }
}