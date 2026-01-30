namespace Flux.PlanetaryScience
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

    private Longitude(double angleDeg)
      => m_angle = new(INumber.WrapAround(angleDeg, MinValue, MaxValue), Units.AngleUnit.Degree);

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/>.</summary>
    public Longitude(Units.Angle angle)
      : this(angle.InDegrees)
    { }

    /// <summary>Creates a new <see cref="Longitude"/> from the specified <paramref name="angle"/> and <paramref name="unit"/>.</summary>
    public Longitude(double angle, Units.AngleUnit unit = Units.AngleUnit.Degree)
      : this(new Units.Angle(angle, unit).InDegrees)
    { }

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

    public string ToDecimalString() => m_angle.GetUnitValue(Units.AngleUnit.Degree).ToString(IBinaryInteger.GetFormatStringWithCountDecimals(6));

    public string ToDmsNotationString()
    {
      var (wholeDegrees, wholeMinutes, decimalSeconds) = double.DecimalDegreesToSexagesimalUnitSubdivisionsDms(Value);

      return $"{(int)double.Abs(wholeDegrees):D2}\u00B0{(int)wholeMinutes:D2}\u2032{(int)decimalSeconds:D2}\u2033{(double.IsNegative(wholeDegrees) ? 'W' : 'E')}";
    }

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

    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d+)[\s\u00B0\u02DA\u030A]+(?<Minutes>\d+)[\s\u2032\u0027\u02B9\u00B4]+(?<Seconds>\d+)[\s\u2033\u0022\u02BA\u301E\u201D\u3003]+(?<Direction>[EW])")]
    private static partial System.Text.RegularExpressions.Regex ParseDmsNotationRegex();

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="dmsNotation"></param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static Longitude ParseDmsNotation(string text)
    {
      if (ParseDmsNotationRegex().Match(text) is var m && m.Success && m.Groups is var gs)
      {
        var decimalDegrees = 0.0;

        decimalDegrees += int.Parse(gs[1].Value, System.Globalization.NumberStyles.Integer);
        decimalDegrees += int.Parse(gs[2].Value, System.Globalization.NumberStyles.Integer) / 60.0;
        decimalDegrees += int.Parse(gs[3].Value, System.Globalization.NumberStyles.Integer) / 3600.0;

        if (gs[4].Value[0] is 'W') decimalDegrees = -decimalDegrees;

        System.ArgumentOutOfRangeException.ThrowIfLessThan(decimalDegrees, Longitude.MinValue);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(decimalDegrees, Longitude.MaxValue);

        return new(decimalDegrees);
      }

      throw new System.InvalidOperationException();
    }

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
      => ToDmsNotationString();

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
