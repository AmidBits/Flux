namespace Flux.Quantities
{
  /// <summary>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90]. Arithmetic results are clamped within the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public readonly record struct Latitude
    : System.IComparable<Latitude>, IQuantifiable<double>
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    public static Latitude TropicOfCancer => new(23.43648);
    public static Latitude TropicOfCapricorn => new(-23.43648);

    private readonly double m_latitude;

    /// <summary>Creates a new Latitude from the specified number of degrees. The value is folded within the degree range [-90, +90]. Folding means oscillating within the range. This means any corresponding Longitude needs to be adjusted by 180 degrees, if synchronization is required.</summary>
    public Latitude(double latitude) => m_latitude = FoldLatitude(latitude);
    /// <summary>Creates a new Latitude from the specfied Angle instance. The value is folded within the degree range [-90, +90]. Folding means oscillating within the range. This means any corresponding Longitude needs to be adjusted by 180 degrees, if synchronization is required.</summary>
    public Latitude(Quantities.Angle angle) : this(angle.ToUnitValue(Quantities.AngleUnit.Degree)) { } // Call base to ensure value is between min/max.

    public double InRadians => Quantities.Angle.ConvertDegreeToRadian(m_latitude);

    public string SexagesimalDegreeString => ToSexagesimalDegreeString();

    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedY() => System.Math.Clamp(System.Math.Log((System.Math.Tan(GenericMath.PiOver4 + InRadians / 2))), -System.Math.PI, System.Math.PI);

    public Quantities.Angle ToAngle() => new(m_latitude, Quantities.AngleUnit.Degree);

    public string ToSexagesimalDegreeString(Quantities.SexagesimalDegreeFormat format = Quantities.SexagesimalDegreeFormat.DegreesMinutesDecimalSeconds, bool useSpaces = false, bool preferUnicode = false)
      => ToAngle().ToSexagesimalDegreeString(format, Quantities.SexagesimalDegreeDirection.NorthSouth, -1, useSpaces, preferUnicode);

    #region Static methods

    /// <summary>A latitude is folded over the range [-90, +90].</summary>
    /// <param name="latitude">The latitude in degrees.</param>
    public static double FoldLatitude(double latitude) => latitude.Fold(MinValue, MaxValue);

    /// <summary></summary>
    /// <param name="lat">The latitude in radians.</param>
    /// <returns></returns>
    public static Latitude FromRadians(double lat) => new(Quantities.Angle.ConvertRadianToDegree(lat) % MaxValue);

    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLatitudinalHeight(double lat) => 111132.954 + -559.822 * double.Cos(2 * lat) + 1.175 * double.Cos(4 * lat) + -0.0023 * double.Cos(6 * lat);

    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateLongitudinalWidth(double lat) => 111412.84 * double.Cos(lat) + -93.5 * double.Cos(3 * lat) + 0.118 * double.Cos(5 * lat);

    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    /// <param name="lat">The latitude in radians.</param>
    public static double GetApproximateRadius(double lat, EllipsoidReference ellipsoidReference)
    {
      var cos = double.Cos(lat);
      var sin = double.Sin(lat);

      var numerator = double.Pow(double.Pow(ellipsoidReference.EquatorialRadius.Value, 2) * cos, 2) + double.Pow(double.Pow(ellipsoidReference.PolarRadius.Value, 2) * sin, 2);
      var denominator = double.Pow(ellipsoidReference.EquatorialRadius.Value * cos, 2) + double.Pow(ellipsoidReference.PolarRadius.Value * sin, 2);

      return double.Sqrt(numerator / denominator);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    /// <param name="lat">The latitude in radians.</param>
    /// <param name="azm">The azimuth in radians.</param>
    /// <returns></returns>
    public static double GetMaximumLatitude(double lat, double azm) => double.Acos(double.Abs(double.Sin(azm) * double.Cos(lat)));

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator double(Latitude v) => v.m_latitude;
    public static explicit operator Latitude(double v) => new(v);

    public static bool operator <(Latitude a, Latitude b) => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b) => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b) => a.CompareTo(b) > 0;
    public static bool operator >=(Latitude a, Latitude b) => a.CompareTo(b) >= 0;

    public static Latitude operator -(Latitude v) => new(-v.m_latitude);
    public static Latitude operator +(Latitude a, double b) => new(a.m_latitude + b);
    public static Latitude operator +(Latitude a, Latitude b) => a + b.Value;
    public static Latitude operator /(Latitude a, double b) => new(a.m_latitude / b);
    public static Latitude operator /(Latitude a, Latitude b) => a / b.Value;
    public static Latitude operator *(Latitude a, double b) => new(a.m_latitude * b);
    public static Latitude operator *(Latitude a, Latitude b) => a * b.Value;
    public static Latitude operator %(Latitude a, double b) => new(a.m_latitude % b);
    public static Latitude operator %(Latitude a, Latitude b) => a % b.Value;
    public static Latitude operator -(Latitude a, double b) => new(a.m_latitude - b);
    public static Latitude operator -(Latitude a, Latitude b) => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(Latitude other) => m_latitude.CompareTo(other.m_latitude);
    // IComparable
    public int CompareTo(object? other) => other is not null && other is Latitude o ? CompareTo(o) : -1;

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => new Angle(m_latitude, AngleUnit.Degree).ToUnitString(AngleUnit.Degree, format, preferUnicode, useFullName);

    public double Value { get => m_latitude; init => m_latitude = value; }

    #endregion Implemented interfaces

    public override string ToString() => ToSexagesimalDegreeString();
  }
}
