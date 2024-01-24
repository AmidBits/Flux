namespace Flux.Units
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Longitude"/>
  public readonly record struct Longitude
    : System.IComparable<Longitude>, IValueQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_degrees; // In degrees.

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double longitude) => m_degrees = WrapExtremum(longitude);

    /// <summary>The <see cref="Units.Angle"/> of the longitude.</summary>
    public Angle Angle { get => new(m_degrees, AngleUnit.Degree); init => m_degrees = WrapExtremum(value.GetUnitValue(AngleUnit.Degree)); }

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_degrees);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => Angle.GetUnitValue(AngleUnit.Radian);

    public string ToSexagesimalDegreeString(AngleDmsFormat format = AngleDmsFormat.DegreesMinutesDecimalSeconds, bool preferUnicode = true, bool useSpaces = false, System.Globalization.CultureInfo? culture = null)
      => Angle.ToDmsString(m_degrees, format, CardinalAxis.EastWest, -1, preferUnicode, useSpaces, culture);

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

    public static explicit operator double(Longitude v) => v.m_degrees;
    public static explicit operator Longitude(double v) => new(v);

    public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    public static Longitude operator -(Longitude v) => new(-v.m_degrees);
    public static Longitude operator +(Longitude a, double b) => new(a.m_degrees + b);
    public static Longitude operator +(Longitude a, Longitude b) => a + b.Value;
    public static Longitude operator /(Longitude a, double b) => new(a.m_degrees / b);
    public static Longitude operator /(Longitude a, Longitude b) => a / b.Value;
    public static Longitude operator *(Longitude a, double b) => new(a.m_degrees * b);
    public static Longitude operator *(Longitude a, Longitude b) => a * b.Value;
    public static Longitude operator %(Longitude a, double b) => new(a.m_degrees % b);
    public static Longitude operator %(Longitude a, Longitude b) => a % b.Value;
    public static Longitude operator -(Longitude a, double b) => new(a.m_degrees - b);
    public static Longitude operator -(Longitude a, Longitude b) => a - b.Value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Longitude other) => m_degrees.CompareTo(other.m_degrees);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    // IQuantifiable<>
    public string ToValueString(string? format = null, bool preferUnicode = true, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
    {
      if (format is not null)
      {
        if (format.StartsWith(AngleDmsFormat.DegreesMinutesDecimalSeconds.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesMinutesDecimalSeconds, preferUnicode, format.EndsWith(' '), culture);
        if (format.StartsWith(AngleDmsFormat.DegreesDecimalMinutes.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesDecimalMinutes, preferUnicode, format.EndsWith(' '), culture);
        if (format.StartsWith(AngleDmsFormat.DecimalDegrees.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DecimalDegrees, preferUnicode, format.EndsWith(' '), culture);

        return Angle.ToUnitValueString(AngleUnit.Degree, format, preferUnicode, useFullName, culture);
      }

      return ToSexagesimalDegreeString();
    }

    public double Value => m_degrees;

    #endregion Implemented interfaces

    public override string ToString() => ToValueString();
  }
}
