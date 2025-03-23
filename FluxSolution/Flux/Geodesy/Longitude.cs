namespace Flux.Geodesy
{
  /// <summary>
  /// <para>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Longitude"/></para>
  /// </summary>
  /// <remarks>The value is wrapped within the range [-180, +180].</remarks>
  public readonly partial record struct Longitude
    : System.IComparable, System.IComparable<Longitude>, System.IFormattable, Units.IValueQuantifiable<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly Units.Angle m_angle;

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/>.</summary>
    public Longitude(Units.Angle angle) => m_angle = new(angle.InDegrees.WrapAround(MinValue, MaxValue), Units.AngleUnit.Degree);

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/> and <paramref name="unit"/>.</summary>
    public Longitude(double angle, Units.AngleUnit unit = Units.AngleUnit.Degree) : this(new Units.Angle(angle, unit)) { }

    /// <summary>The <see cref="Units.Angle"/> of the <see cref="Longitude"/>.</summary>
    public Units.Angle Angle { get => m_angle; }

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_angle.GetUnitValue(Units.AngleUnit.Degree));

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => Angle.GetUnitValue(Units.AngleUnit.Radian);

    public string ToDecimalString() => m_angle.GetUnitValue(Units.AngleUnit.Degree).ToString(6.FormatUpToFractionalDigits());

    //public string ToSexagesimalDegreeString(Units.AngleDmsNotation format = Units.AngleDmsNotation.DegreesMinutesDecimalSeconds, Unicode.UnicodeSpacing componentSpacing = Unicode.UnicodeSpacing.None)
    //  => Units.Angle.ToStringDmsNotation(m_angle.GetUnitValue(Units.AngleUnit.Degree), format, CompassCardinalAxis.EastWest, -1, componentSpacing);

    public string ToDmsNotationString(Units.AngleDmsNotation dmsNotation = Units.AngleDmsNotation.DegreesMinutesDecimalSeconds, Unicode.UnicodeSpacing componentSpacing = Unicode.UnicodeSpacing.None)
      => Units.Angle.ToStringDmsNotation(m_angle.GetUnitValue(Units.AngleUnit.Degree), dmsNotation, CompassCardinalAxis.EastWest, -1, componentSpacing);

    #region Static methods

    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="longitude">The longitude in degrees.</param>
    public static int GetTheoreticalTimezoneOffset(double longitude) => System.Convert.ToInt32(double.Truncate((longitude + double.CopySign(7.5, longitude)) / 15));

    ///// <summary>A longitude is wrapped over within the closed interval (<see cref="MinValue"/> = -180, <see cref="MaxValue"/> = +180).</summary>
    ///// <param name="longitude">The longitude in degrees.</param>
    ///// <remarks>Please note that longitude use a closed interval, so -180 (south pole) and +180 (north pole) are valid values.</remarks>
    //public static double WrapExtremum(double longitude) => longitude.Wrap(MinValue, MaxValue);
    ////=> (longitude < MinValue
    ////? MaxValue - (MinValue - longitude) % (MaxValue - MinValue)
    ////: longitude > MaxValue
    ////? MinValue + (longitude - MinValue) % (MaxValue - MinValue)
    ////: longitude);

    public static Longitude ParseDmsNotation(string dmsNotation)
      => Units.Angle.TryParseDmsNotations(dmsNotation, out var dmsNotations) ? (Longitude)dmsNotations.First(e => e is Longitude) : throw new System.InvalidOperationException();

    //public static string ToStringDmsNotation(double decimalDegrees, Units.AngleDmsNotation dmsNotation = Units.AngleDmsNotation.DegreesMinutesDecimalSeconds, int decimalPoints = -1, Unicode.UnicodeSpacing componentSpacing = Unicode.UnicodeSpacing.None)
    //  => Units.Angle.ToStringDmsNotation(decimalDegrees, dmsNotation, CompassCardinalAxis.EastWest, decimalPoints, componentSpacing);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Longitude a, Longitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b) => a.CompareTo(b) >= 0;

    public static Longitude operator -(Longitude v) => new(-v.m_angle.GetUnitValue(Units.AngleUnit.Degree));
    public static Longitude operator +(Longitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) + b);
    public static Longitude operator +(Longitude a, Longitude b) => a + b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Longitude operator /(Longitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) / b);
    public static Longitude operator /(Longitude a, Longitude b) => a / b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Longitude operator *(Longitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) * b);
    public static Longitude operator *(Longitude a, Longitude b) => a * b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Longitude operator %(Longitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) % b);
    public static Longitude operator %(Longitude a, Longitude b) => a % b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Longitude operator -(Longitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) - b);
    public static Longitude operator -(Longitude a, Longitude b) => a - b.m_angle.GetUnitValue(Units.AngleUnit.Degree);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Longitude other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Longitude o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => format is not null
      ? Units.Angle.TryConvertFormatToDmsNotation(format, out var dmsFormat)
        ? ToDmsNotationString(dmsFormat)
        : m_angle.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)
      : ToDmsNotationString();

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Longitude.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.InDegrees;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
