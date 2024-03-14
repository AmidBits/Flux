namespace Flux.Units
{
  /// <summary>
  /// <para>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90]. Arithmetic results are clamped within the range.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Latitude"/></para>
  /// </summary>
  public readonly record struct Latitude
    : System.IComparable, System.IComparable<Latitude>, System.IFormattable, IValueQuantifiable<double>
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    public static Latitude TropicOfCancer => new(23.43648);
    public static Latitude TropicOfCapricorn => new(-23.43648);

    private readonly Angle m_angle;

    /// <summary>Creates a new Latitude from the specified number of degrees. The value is folded within the degree range [-90, +90]. Folding means oscillating within the range. This means any corresponding Longitude needs to be adjusted by 180 degrees, if synchronization is required.</summary>
    public Latitude(double latitude, AngleUnit unit = AngleUnit.Degree) => Angle = new Angle(latitude, unit);

    /// <summary>The <see cref="Units.Angle"/> of the latitude.</summary>
    public Angle Angle { get => m_angle; init => m_angle = new(FoldExtremum(value.GetUnitValue(AngleUnit.Degree)), AngleUnit.Degree); }

    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedY()
      => System.Math.Clamp(System.Math.Log(System.Math.Tan(System.Math.PI / 4 + Angle.Value / 2)), -System.Math.PI, System.Math.PI);

    public string ToSexagesimalDegreeString(AngleDmsNotation format = AngleDmsNotation.DegreesMinutesDecimalSeconds, UnicodeSpacing componentSpacing = UnicodeSpacing.None)
      => Angle.ToDmsString(m_angle.GetUnitValue(AngleUnit.Degree), format, CardinalAxis.NorthSouth, -1, componentSpacing);

    #region Static methods

    /// <summary>A latitude is folded over the closed interval (<see cref="MinValue"/> = -90, <see cref="MaxValue"/> = +90).</summary>
    /// <param name="latitude">The latitude in degrees.</param>
    /// <remarks>Please note that latitude use a closed interval, so -90 (south pole) and +90 (north pole) are valid values.</remarks>
    public static double FoldExtremum(double latitude) => latitude.Fold(MinValue, MaxValue);
    //=> (latitude > MaxValue)
    //? IsEvenInteger(TruncMod(latitude - MaxValue, MaxValue - MinValue, out var remHi)) ? MaxValue - remHi : MinValue + remHi
    //: (latitude < MinValue)
    //? IsEvenInteger(TruncMod(MinValue - latitude, MaxValue - MinValue, out var remLo)) ? MinValue + remLo : MaxValue - remLo
    //: latitude;

    //private static double TruncMod(double dividend, double divisor, out double remainder) => (dividend - (remainder = dividend % divisor)) / divisor;
    //private static bool IsEvenInteger(double value) => System.Convert.ToInt64(value) is var integer && ((integer & 1) == 0) && (integer == value);

    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLatitudinalHeight(double lat) => 111132.954 + -559.822 * System.Math.Cos(2 * lat) + 1.175 * System.Math.Cos(4 * lat) + -0.0023 * System.Math.Cos(6 * lat);

    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLongitudinalWidth(double lat) => 111412.84 * System.Math.Cos(lat) + -93.5 * System.Math.Cos(3 * lat) + 0.118 * System.Math.Cos(5 * lat);

    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateRadius(double lat, double equatorialRadius, double polarRadius)
    {
      var (sin, cos) = System.Math.SinCos(lat);

      var numerator = System.Math.Pow(System.Math.Pow(equatorialRadius, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(polarRadius, 2) * sin, 2);
      var denominator = System.Math.Pow(equatorialRadius * cos, 2) + System.Math.Pow(polarRadius * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    /// <param name="lat">The latitude in radians.</param>
    /// <param name="azm">The azimuth in radians.</param>
    /// <returns></returns>
    public static double GetMaximumLatitude(double lat, double azm) => System.Math.Acos(System.Math.Abs(System.Math.Sin(azm) * System.Math.Cos(lat)));

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Latitude a, Latitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Latitude a, Latitude b) => a.CompareTo(b) >= 0;

    public static Latitude operator -(Latitude v) => new(-v.m_angle.GetUnitValue(AngleUnit.Degree));
    public static Latitude operator +(Latitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) + b);
    public static Latitude operator +(Latitude a, Latitude b) => a + b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Latitude operator /(Latitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) / b);
    public static Latitude operator /(Latitude a, Latitude b) => a / b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Latitude operator *(Latitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) * b);
    public static Latitude operator *(Latitude a, Latitude b) => a * b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Latitude operator %(Latitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) % b);
    public static Latitude operator %(Latitude a, Latitude b) => a % b.m_angle.GetUnitValue(AngleUnit.Degree);
    public static Latitude operator -(Latitude a, double b) => new(a.m_angle.GetUnitValue(AngleUnit.Degree) - b);
    public static Latitude operator -(Latitude a, Latitude b) => a - b.m_angle.GetUnitValue(AngleUnit.Degree);
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Latitude other) => m_angle.CompareTo(other.m_angle);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Latitude o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
    {
      if (format is not null)
      {
        if (Angle.TryConvertFormatToDmsNotation(format, out var dmsNotation))
          return ToSexagesimalDegreeString(dmsNotation);

        return Angle.ToUnitValueString(AngleUnit.Degree, format, formatProvider, true);
      }

      return ToSexagesimalDegreeString();
    }

    // IQuantifiable<>
    /// <summary>
    ///  <para>The unit of the <see cref="Latitude.Value"/> property is in <see cref="AngleUnit.Degree"/>.</para>
    /// </summary>
    public double Value => m_angle.GetUnitValue(AngleUnit.Degree);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
