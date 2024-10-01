namespace Flux.Quantities
{
  /// <summary>
  /// <para>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Longitude"/></para>
  /// </summary>
  /// <remarks>The value is wrapped within the range [-180, +180].</remarks>
  public readonly record struct Longitude
    : System.IComparable, System.IComparable<Longitude>, System.IFormattable, IValueQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly Angle m_angle;

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/>.</summary>
    public Longitude(Angle angle) => m_angle = new(angle.InDegrees.Wrap(MinValue, MaxValue), AngleUnit.Degree);

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/> and <paramref name="unit"/>.</summary>
    public Longitude(double angle, AngleUnit unit = AngleUnit.Degree) : this(new Angle(angle, unit)) { }

    /// <summary>The <see cref="Quantities.Angle"/> of the <see cref="Longitude"/>.</summary>
    public Angle Angle { get => m_angle; }

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_angle.GetUnitValue(AngleUnit.Degree));

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => Angle.GetUnitValue(AngleUnit.Radian);

    public string ToDecimalString() => m_angle.GetUnitValue(AngleUnit.Degree).ToString(Format.UpTo6Decimals);

    public string ToSexagesimalDegreeString(AngleDmsNotation format = AngleDmsNotation.DegreesMinutesDecimalSeconds, UnicodeSpacing componentSpacing = UnicodeSpacing.None)
      => Angle.ToDmsString(m_angle.GetUnitValue(AngleUnit.Degree), format, CompassCardinalAxis.EastWest, -1, componentSpacing);

    #region Static methods

    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    public static int GetTheoreticalTimezoneOffset(double longitude) => System.Convert.ToInt32(System.Math.Truncate((longitude + System.Math.CopySign(7.5, longitude)) / 15));

    ///// <summary>A longitude is wrapped over within the closed interval (<see cref="MinValue"/> = -180, <see cref="MaxValue"/> = +180).</summary>
    ///// <param name="longitude">The longitude in degrees.</param>
    ///// <remarks>Please note that longitude use a closed interval, so -180 (south pole) and +180 (north pole) are valid values.</remarks>
    //public static double WrapExtremum(double longitude) => longitude.Wrap(MinValue, MaxValue);
    ////=> (longitude < MinValue
    ////? MaxValue - (MinValue - longitude) % (MaxValue - MinValue)
    ////: longitude > MaxValue
    ////? MinValue + (longitude - MinValue) % (MaxValue - MinValue)
    ////: longitude);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    public static Longitude operator -(Longitude v) => new(-v.m_angle.GetUnitValue(AngleUnit.Degree));
    public static Longitude operator +(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) + b);
    public static Longitude operator +(Longitude a, Longitude b) => a + b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Longitude operator /(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) / b);
    public static Longitude operator /(Longitude a, Longitude b) => a / b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Longitude operator *(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) * b);
    public static Longitude operator *(Longitude a, Longitude b) => a * b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Longitude operator %(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) % b);
    public static Longitude operator %(Longitude a, Longitude b) => a % b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Longitude operator -(Longitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) - b);
    public static Longitude operator -(Longitude a, Longitude b) => a - b.m_angle.GetUnitValue(AngleUnit.Degree);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Longitude other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => format is not null
      ? Angle.TryConvertFormatToDmsNotation(format, out var dmsFormat)
        ? ToSexagesimalDegreeString(dmsFormat)
        : m_angle.ToUnitString(AngleUnit.Degree, format, formatProvider)
      : ToSexagesimalDegreeString();

    #region IQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Longitude.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.InDegrees;

    #endregion // IQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
