namespace Flux
{
  /// <summary>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90].</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, Quantity.IValuedUnit
  {
    public static Latitude TropicOfCancer
      => new Latitude(23.43648);
    public static Latitude TropicOfCapricorn
      => new Latitude(-23.43648);

    public const double MaxValue = +90;
    public const double MinValue = -90;

    private readonly double m_value;

    public Latitude(double degree)
      => m_value = Maths.Fold(degree, MinValue, MaxValue);
    public Latitude(Quantity.Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    /// <summary>Computes the approximate length in meters per degree of latitudinal height at the specified latitude.</summary>
    public Quantity.Length GetApproximateLatitudinalHeight()
      => new Quantity.Length(ComputeApproximateLatitudinalHeight(Radian));
    /// <summary>Computes the approximate length in meters per degree of longitudinal width at the specified latitude.</summary>
    public Quantity.Length GetApproximateLongitudinalWidth()
      => new Quantity.Length(ComputeApproximateLongitudinalWidth(Radian));
    /// <summary>Determines an approximate radius in meters at the specified latitude.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public Quantity.Length GetApproximateRadius()
      => new Quantity.Length(ComputeApproximateRadius(Radian));
    /// <summary>Projects the latitude to a mercator Y value in the range [-PI, PI]. The Y value is logarithmic.</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectY()
      => System.Math.Clamp(System.Math.Log((System.Math.Tan(System.Math.PI / 4 + Radian / 2))), -System.Math.PI, System.Math.PI);

    public double Radian
      => Quantity.Angle.ConvertDegreeToRadian(m_value);

    public double Value
      => m_value;

    #region Static methods
    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    public static double ComputeApproximateLatitudinalHeight(double radLatitude)
      => 111132.954 + -559.822 * System.Math.Cos(2 * radLatitude) + 1.175 * System.Math.Cos(4 * radLatitude) + -0.0023 * System.Math.Cos(6 * radLatitude);
    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    public static double ComputeApproximateLongitudinalWidth(double radLatitude)
      => 111412.84 * System.Math.Cos(radLatitude) + -93.5 * System.Math.Cos(3 * radLatitude) + 0.118 * System.Math.Cos(5 * radLatitude);
    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public static double ComputeApproximateRadius(double radLatitude)
    {
      var cos = System.Math.Cos(radLatitude);
      var sin = System.Math.Sin(radLatitude);

      var numerator = System.Math.Pow(System.Math.Pow(Earth.EquatorialRadius.Value, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(Earth.PolarRadius.Value, 2) * sin, 2);
      var denominator = System.Math.Pow(Earth.EquatorialRadius.Value * cos, 2) + System.Math.Pow(Earth.PolarRadius.Value * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v)
      => v.m_value;
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
      => new Latitude(-v.m_value);
    public static Latitude operator +(Latitude a, double b)
      => new Latitude(a.m_value + b);
    public static Latitude operator +(Latitude a, Latitude b)
      => a + b.Value;
    public static Latitude operator /(Latitude a, double b)
      => new Latitude(a.m_value / b);
    public static Latitude operator /(Latitude a, Latitude b)
      => a / b.Value;
    public static Latitude operator *(Latitude a, double b)
      => new Latitude(a.m_value * b);
    public static Latitude operator *(Latitude a, Latitude b)
      => a * b.Value;
    public static Latitude operator %(Latitude a, double b)
      => new Latitude(a.m_value % b);
    public static Latitude operator %(Latitude a, Latitude b)
      => a % b.Value;
    public static Latitude operator -(Latitude a, double b)
      => new Latitude(a.m_value - b);
    public static Latitude operator -(Latitude a, Latitude b)
      => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Latitude other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Latitude other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Latitude o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
