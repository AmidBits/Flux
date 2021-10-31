namespace Flux
{
  /// <summary>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90]. The value is clamped within the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, Quantity.IValuedUnit
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    public static Latitude TropicOfCancer
      => new Latitude(23.43648);
    public static Latitude TropicOfCapricorn
      => new Latitude(-23.43648);

    private readonly double m_degree;

    public Latitude(double degLatitude)
      => m_degree = IsLatitude(degLatitude) ? degLatitude : throw new System.ArgumentOutOfRangeException(nameof(degLatitude));
    public Latitude(Quantity.Angle angle)
      : this(angle.ToUnitValue(Quantity.AngleUnit.Degree)) // Call base to ensure value is between min/max.
    { }

    /// <summary>Computes the approximate length in meters per degree of latitudinal height at the specified latitude.</summary>
    public Quantity.Length ApproximateLatitudinalHeight
      => new Quantity.Length(GetApproximateLatitudinalHeight(ToAngle().Value));
    /// <summary>Computes the approximate length in meters per degree of longitudinal width at the specified latitude.</summary>
    public Quantity.Length ApproximateLongitudinalWidth
      => new Quantity.Length(GetApproximateLongitudinalWidth(ToAngle().Value));
    /// <summary>Determines an approximate radius in meters at the specified latitude.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public Quantity.Length ApproximateRadius
      => new Quantity.Length(GetApproximateRadius(ToAngle().Value));

    public double Value
      => m_degree;

    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedY()
      => System.Math.Clamp(System.Math.Log((System.Math.Tan(Maths.PiOver4 + ToAngle().Value / 2))), -System.Math.PI, System.Math.PI);

    public Quantity.Angle ToAngle()
      => new Quantity.Angle(m_degree, Quantity.AngleUnit.Degree);

    #region Static methods
    public static double Clamp(double degLatitude)
      => System.Math.Clamp(degLatitude, MinValue, MaxValue);

    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    public static double GetApproximateLatitudinalHeight(double radLatitude)
      => 111132.954 + -559.822 * System.Math.Cos(2 * radLatitude) + 1.175 * System.Math.Cos(4 * radLatitude) + -0.0023 * System.Math.Cos(6 * radLatitude);
    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    public static double GetApproximateLongitudinalWidth(double radLatitude)
      => 111412.84 * System.Math.Cos(radLatitude) + -93.5 * System.Math.Cos(3 * radLatitude) + 0.118 * System.Math.Cos(5 * radLatitude);
    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public static double GetApproximateRadius(double radLatitude)
    {
      var cos = System.Math.Cos(radLatitude);
      var sin = System.Math.Sin(radLatitude);

      var numerator = System.Math.Pow(System.Math.Pow(Earth.EquatorialRadius.Value, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(Earth.PolarRadius.Value, 2) * sin, 2);
      var denominator = System.Math.Pow(Earth.EquatorialRadius.Value * cos, 2) + System.Math.Pow(Earth.PolarRadius.Value * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }

    /// <summary>Clairaut’s formula will give you the maximum latitude of a great circle path, given a bearing and latitude on the great circle.</summary>
    public static double GetMaximumLatitude(double radLatitude, double radAzimuth)
      => System.Math.Acos(System.Math.Abs(System.Math.Sin(radAzimuth) * System.Math.Cos(radLatitude)));

    /// <summary>Returns whether the specified latitude (in degrees) is a valid latitude, i.e. [-90, +90].</summary>
    public static bool IsLatitude(double degLatitude)
      => degLatitude >= MinValue && degLatitude <= MaxValue;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v)
      => v.m_degree;
    public static explicit operator Latitude(double v)
      => new Latitude(v);

    public static bool operator <(Latitude a, Latitude b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Latitude a, Latitude b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Latitude a, Latitude b)
      => a.Equals(b);
    public static bool operator !=(Latitude a, Latitude b)
      => !a.Equals(b);

    public static Latitude operator -(Latitude v)
      => new Latitude(-v.m_degree);
    public static Latitude operator +(Latitude a, double b)
      => new Latitude(Clamp(a.m_degree + b));
    public static Latitude operator +(Latitude a, Latitude b)
      => a + b.Value;
    public static Latitude operator /(Latitude a, double b)
      => new Latitude(Clamp(a.m_degree / b));
    public static Latitude operator /(Latitude a, Latitude b)
      => a / b.Value;
    public static Latitude operator *(Latitude a, double b)
      => new Latitude(Clamp(a.m_degree * b));
    public static Latitude operator *(Latitude a, Latitude b)
      => a * b.Value;
    public static Latitude operator %(Latitude a, double b)
      => new Latitude(Clamp(a.m_degree % b));
    public static Latitude operator %(Latitude a, Latitude b)
      => a % b.Value;
    public static Latitude operator -(Latitude a, double b)
      => new Latitude(Clamp(a.m_degree - b));
    public static Latitude operator -(Latitude a, Latitude b)
      => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Latitude other)
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Latitude other)
      => m_degree == other.m_degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Latitude o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_degree}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
