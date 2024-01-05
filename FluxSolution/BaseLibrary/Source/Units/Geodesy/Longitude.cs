namespace Flux.Units
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public readonly record struct Longitude
    : System.IComparable<Longitude>, IQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_longitude;

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double longitude) => m_longitude = WrapExtremum(longitude);
    /// <summary>Creates a new Longitude from the specfied Angle instance. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(Angle angle) : this(angle.ToUnitValue(AngleUnit.Degree)) { } // Call base to ensure value is between min/max.

    public double Radians => Angle.DegreeToRadian(m_longitude);

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset => GetTheoreticalTimezoneOffset(m_longitude);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX() => Radians;

    public Angle ToAngle() => new(m_longitude, AngleUnit.Degree);

    public string ToSexagesimalDegreeString(AngleDmsFormat format = AngleDmsFormat.DegreesMinutesDecimalSeconds, bool preferUnicode = true, bool useSpaces = false)
      => Angle.ToDmsString(m_longitude, format, CardinalAxis.EastWest, -1, preferUnicode, useSpaces);

    #region Static methods

    /// <summary></summary>
    /// <param name="lon">The longitude in radians.</param>
    public static Longitude FromRadians(double lon) => new(Angle.RadianToDegree(lon));

    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    public static int GetTheoreticalTimezoneOffset(double longitude) => System.Convert.ToInt32(System.Math.Truncate((longitude + System.Math.CopySign(7.5, longitude)) / 15));

    /// <summary>A longitude is wrapped over within the closed interval (<see cref="MinValue"/> = -180, <see cref="MaxValue"/> = +180).</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    /// <remarks>Please note that longitude use a closed interval, so -180 (south pole) and +180 (north pole) are valid values.</remarks>
    public static double WrapExtremum(double longitude) => longitude.Wrap(MinValue, MaxValue);
    //=> (longitude < MinValue
    //? MaxValue - (MinValue - longitude) % (MaxValue - MinValue)
    //: longitude > MaxValue
    //? MinValue + (longitude - MinValue) % (MaxValue - MinValue)
    //: longitude);

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
        if (format.StartsWith(AngleDmsFormat.DegreesMinutesDecimalSeconds.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesMinutesDecimalSeconds, preferUnicode, format.EndsWith(' '));
        if (format.StartsWith(AngleDmsFormat.DegreesDecimalMinutes.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesDecimalMinutes, preferUnicode, format.EndsWith(' '));
        if (format.StartsWith(AngleDmsFormat.DecimalDegrees.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DecimalDegrees, preferUnicode, format.EndsWith(' '));

        return ToAngle().ToUnitString(AngleUnit.Degree, format, preferUnicode, useFullName);
      }

      return ToSexagesimalDegreeString();
    }

    public double Value { get => m_longitude; init => m_longitude = value; }

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
