namespace Flux.Units
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Longitude"/>
  public readonly record struct Longitude
    : System.IComparable<Longitude>, IValueQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly Units.Angle m_angle;

    /// <summary>Creates a new Longitude from the specified number of degrees. The value is wrapped within the degree range [-180, +180].</summary>
    public Longitude(double longitude, AngleUnit unit = AngleUnit.Degree) => Angle = new Angle(longitude, unit);

    /// <summary>The <see cref="Units.Angle"/> of the longitude.</summary>
    public Angle Angle { get => m_angle; init => m_angle = new(WrapExtremum(value.GetUnitValue(AngleUnit.Degree)), AngleUnit.Degree); }

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_angle.GetUnitValue(AngleUnit.Degree));

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => Angle.GetUnitValue(AngleUnit.Radian);

    public string ToSexagesimalDegreeString(AngleDmsFormat format = AngleDmsFormat.DegreesMinutesDecimalSeconds, bool preferUnicode = true, bool useSpaces = false, System.Globalization.CultureInfo? culture = null)
      => Angle.ToDmsString(m_angle.GetUnitValue(AngleUnit.Degree), format, CardinalAxis.EastWest, -1, preferUnicode, useSpaces, culture);

    #region Static methods

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

    public static explicit operator double(Longitude v) => v.m_angle.GetUnitValue(AngleUnit.Degree);
    public static explicit operator Longitude(double v) => new(v);

    public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    public static Longitude operator -(Longitude v) => new(-v.m_angle.GetUnitValue(AngleUnit.Degree));
    public static Longitude operator +(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) + b);
    public static Longitude operator +(Longitude a, Longitude b) => a + b.Value;
    public static Longitude operator /(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) / b);
    public static Longitude operator /(Longitude a, Longitude b) => a / b.Value;
    public static Longitude operator *(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) * b);
    public static Longitude operator *(Longitude a, Longitude b) => a * b.Value;
    public static Longitude operator %(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) % b);
    public static Longitude operator %(Longitude a, Longitude b) => a % b.Value;
    public static Longitude operator -(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) - b);
    public static Longitude operator -(Longitude a, Longitude b) => a - b.Value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Longitude other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    // IQuantifiable<>
    public string ToValueString(QuantifiableValueStringOptions options = default)
    {
      if (options.Format is not null)
      {
        if (options.Format.StartsWith(AngleDmsFormat.DegreesMinutesDecimalSeconds.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesMinutesDecimalSeconds, options.PreferUnicode, options.Format.EndsWith(' '), options.CultureInfo);
        if (options.Format.StartsWith(AngleDmsFormat.DegreesDecimalMinutes.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DegreesDecimalMinutes, options.PreferUnicode, options.Format.EndsWith(' '), options.CultureInfo);
        if (options.Format.StartsWith(AngleDmsFormat.DecimalDegrees.GetAcronym()))
          return ToSexagesimalDegreeString(AngleDmsFormat.DecimalDegrees, options.PreferUnicode, options.Format.EndsWith(' '), options.CultureInfo);

        return Angle.ToUnitValueString(AngleUnit.Degree, options);
      }

      return ToSexagesimalDegreeString();
    }

    /// <summary>
    ///  <para>The unit of the <see cref="Longitude.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.GetUnitValue(AngleUnit.Degree);

    #endregion Implemented interfaces

    public override string ToString() => ToValueString();
  }
}
