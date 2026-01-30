namespace Flux.PlanetaryScience
{
  /// <summary>
  /// <para>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Latitude"/></para>
  /// </summary>
  /// <remarks>
  /// <para>The value is folded within the range [-90, +90].</para>
  /// </remarks>
  public readonly partial record struct Latitude
    : System.IComparable, System.IComparable<Latitude>, System.IFormattable, Units.IValueQuantifiable<double>
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    public static Latitude AntarcticCircle { get; } = new(-66.5);
    public static Latitude ArcticCircle { get; } = new(66.5);
    public static Latitude Equator { get; }
    public static Latitude TropicOfCancer { get; } = new(23.43648);
    public static Latitude TropicOfCapricorn { get; } = new(-23.43648);

    private readonly Units.Angle m_angle;

    private Latitude(double angleDeg)
      => m_angle = new(INumber.FoldAcross(angleDeg, MinValue, MaxValue), Units.AngleUnit.Degree);

    /// <summary>Creates a new <see cref="Latitude"/> from the specified <paramref name="angle"/>.</summary>
    public Latitude(Units.Angle angle)
      : this(angle.InDegrees)
    { }

    /// <summary>Creates a new <see cref="Latitude"/> from the specified <paramref name="angle"/> and <paramref name="unit"/>.</summary>
    public Latitude(double angle, Units.AngleUnit unit = Units.AngleUnit.Degree)
      : this(new Units.Angle(angle, unit).InDegrees)
    { }

    /// <summary>The <see cref="Units.Angle"/> of the latitude.</summary>
    public Units.Angle Angle { get => m_angle; }

    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedY()
      => double.Clamp(double.Log(double.Tan(double.Pi / 4 + Angle.Value / 2)), -double.Pi, double.Pi);

    public string ToDecimalString() => m_angle.GetUnitValue(Units.AngleUnit.Degree).ToString(IBinaryInteger.GetFormatStringWithCountDecimals(6));

    public string ToDmsNotationString()
    {
      var (wholeDegrees, wholeMinutes, decimalSeconds) = double.DecimalDegreesToSexagesimalUnitSubdivisionsDms(Value);

      return $"{(int)double.Abs(wholeDegrees):D2}\u00B0{(int)wholeMinutes:D2}\u2032{(int)decimalSeconds:D2}\u2033{(double.IsNegative(wholeDegrees) ? 'S' : 'N')}";
    }

    #region Static methods

    ///// <summary>A latitude is folded over the closed interval (<see cref="MinValue"/> = -90, <see cref="MaxValue"/> = +90).</summary>
    ///// <param name="latitude">The latitude in degrees.</param>
    ///// <remarks>Please note that latitude use a closed interval, so -90 (south pole) and +90 (north pole) are valid values.</remarks>
    //public static double FoldExtremum(double latitude) => latitude.Fold(MinValue, MaxValue);
    ////=> (latitude > MaxValue)
    ////? IsEvenInteger(TruncMod(latitude - MaxValue, MaxValue - MinValue, out var remHi)) ? MaxValue - remHi : MinValue + remHi
    ////: (latitude < MinValue)
    ////? IsEvenInteger(TruncMod(MinValue - latitude, MaxValue - MinValue, out var remLo)) ? MinValue + remLo : MaxValue - remLo
    ////: latitude;

    //private static double TruncMod(double dividend, double divisor, out double remainder) => (dividend - (remainder = dividend % divisor)) / divisor;
    //private static bool IsEvenInteger(double value) => System.Convert.ToInt64(value) is var integer && ((integer & 1) == 0) && (integer == value);

    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLatitudinalHeight(double lat) => 111132.954 + -559.822 * double.Cos(2 * lat) + 1.175 * double.Cos(4 * lat) + -0.0023 * double.Cos(6 * lat);

    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLongitudinalWidth(double lat) => 111412.84 * double.Cos(lat) + -93.5 * double.Cos(3 * lat) + 0.118 * double.Cos(5 * lat);

    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateRadius(double lat, double equatorialRadius, double polarRadius)
    {
      var (sin, cos) = double.SinCos(lat);

      var numerator = double.Pow(double.Pow(equatorialRadius, 2) * cos, 2) + double.Pow(double.Pow(polarRadius, 2) * sin, 2);
      var denominator = double.Pow(equatorialRadius * cos, 2) + double.Pow(polarRadius * sin, 2);

      return double.Sqrt(numerator / denominator);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    /// <param name="lat">The latitude in radians.</param>
    /// <param name="azm">The azimuth in radians.</param>
    /// <returns></returns>
    public static double GetMaximumLatitude(double lat, double azm) => double.Acos(double.Abs(double.Sin(azm) * double.Cos(lat)));


    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d+)[\s\u00B0\u02DA\u030A]+(?<Minutes>\d+)[\s\u2032\u0027\u02B9\u00B4]+(?<Seconds>\d+)[\s\u2033\u0022\u02BA\u301E\u201D\u3003]+(?<Direction>[NS])")]
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
    public static Latitude ParseDmsNotation(string text)
    {
      if (ParseDmsNotationRegex().Match(text) is var m && m.Success && m.Groups is var gs)
      {
        var decimalDegrees = 0.0;

        decimalDegrees += int.Parse(gs[1].Value, System.Globalization.NumberStyles.Integer);
        decimalDegrees += int.Parse(gs[2].Value, System.Globalization.NumberStyles.Integer) / 60.0;
        decimalDegrees += int.Parse(gs[3].Value, System.Globalization.NumberStyles.Integer) / 3600.0;

        if (gs[4].Value[0] is 'S') decimalDegrees = -decimalDegrees;

        System.ArgumentOutOfRangeException.ThrowIfLessThan(decimalDegrees, Latitude.MinValue);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(decimalDegrees, Latitude.MaxValue);

        return new(decimalDegrees);
      }

      throw new System.InvalidOperationException();
    }

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Latitude a, Latitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Latitude a, Latitude b) => a.CompareTo(b) >= 0;

    public static Latitude operator -(Latitude v) => new(-v.m_angle.GetUnitValue(Units.AngleUnit.Degree));
    public static Latitude operator +(Latitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) + b);
    public static Latitude operator +(Latitude a, Latitude b) => a + b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Latitude operator /(Latitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) / b);
    public static Latitude operator /(Latitude a, Latitude b) => a / b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Latitude operator *(Latitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) * b);
    public static Latitude operator *(Latitude a, Latitude b) => a * b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Latitude operator %(Latitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) % b);
    public static Latitude operator %(Latitude a, Latitude b) => a % b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    public static Latitude operator -(Latitude a, double b) => new(a.m_angle.GetUnitValue(Units.AngleUnit.Degree) - b);
    public static Latitude operator -(Latitude a, Latitude b) => a - b.m_angle.GetUnitValue(Units.AngleUnit.Degree);
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Latitude other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Latitude o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToDmsNotationString();

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="Latitude.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.InDegrees;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
