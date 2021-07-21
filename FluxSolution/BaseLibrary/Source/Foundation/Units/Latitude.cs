namespace Flux.Units
{
  /// <summary>Latitude, unit of degree, is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90].</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, IValuedUnit
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    private readonly Angle m_value;

    public Latitude(double degree)
      => m_value = Angle.FromUnitValue(AngleUnit.Degree, System.Math.Clamp(degree, MinValue, MaxValue));
    public Latitude(Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    /// <summary>Computes the approximate length in meters per degree of latitudinal height at the specified latitude.</summary>
    public Length ApproximateLatitudinalHeight
      => new Length(ComputeApproximateLatitudinalHeight(Angle.Degree));
    /// <summary>Computes the approximate length in meters per degree of longitudinal width at the specified latitude.</summary>
    public Length ApproximateLongitudinalWidth
      => new Length(ComputeApproximateLongitudinalWidth(Angle.Degree));
    /// <summary>Determines an approximate radius in meters at the specified latitude.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public Length ApproximateRadius
      => new Length(ComputeApproximateRadius(Angle.Degree));

    public Angle Angle
      => m_value;

    public double Value
      => m_value.Degree;

    #region Static methods
    /// <summary>Computes the approximate length in meters per degree of latitudinal at the specified latitude.</summary>
    public static double ComputeApproximateLatitudinalHeight(double latitude)
    {
      //const double heightAtEquatorInMeters = 110567;
      //const double heightAtPolesInMeters = 111699;

      //var radian = Angle.ConvertDegreeToRadian(latitude);

      //return System.Math.Sin(radian) * (heightAtPolesInMeters - heightAtEquatorInMeters) + heightAtEquatorInMeters;
      latitude = Units.Angle.ConvertDegreeToRadian(latitude);

      return 111132.954 + -559.822 * System.Math.Cos(2 * latitude) + 1.175 * System.Math.Cos(4 * latitude) + -0.0023 * System.Math.Cos(6 * latitude);
    }
    /// <summary>Computes the approximate length in meters per degree of longitudinal at the specified latitude.</summary>
    public static double ComputeApproximateLongitudinalWidth(double latitude)
    {
      //const double widthAtEquatorInMeters = 111321;

      //var radian = Angle.ConvertDegreeToRadian(latitude);

      //return System.Math.Cos(radian) * widthAtEquatorInMeters;
      latitude = Units.Angle.ConvertDegreeToRadian(latitude);

      return 111412.84 * System.Math.Cos(latitude) + -93.5 * System.Math.Cos(3 * latitude) + 0.118 * System.Math.Cos(5 * latitude);
    }
    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public static double ComputeApproximateRadius(double latitude)
    {
      latitude = Angle.ConvertDegreeToRadian(latitude);

      var cos = System.Math.Cos(latitude);
      var sin = System.Math.Sin(latitude);

      var numerator = System.Math.Pow(System.Math.Pow(EarthRadii.EquatorialInMeters, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(EarthRadii.PolarInMeters, 2) * sin, 2);
      var denominator = System.Math.Pow(EarthRadii.EquatorialInMeters * cos, 2) + System.Math.Pow(EarthRadii.PolarInMeters * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v)
      => v.m_value.Degree;
    public static explicit operator Latitude(double v)
      => new Latitude(v);

    public static bool operator <(Latitude a, Latitude b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Latitude a, Latitude b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Latitude a, Latitude b)
      => a.Equals(b);
    public static bool operator !=(Latitude a, Latitude b)
      => !a.Equals(b);

    public static Latitude operator -(Latitude v)
      => new Latitude(-v.m_value);
    public static Latitude operator +(Latitude a, Latitude b)
      => new Latitude(a.m_value + b.m_value);
    public static Latitude operator /(Latitude a, Latitude b)
      => new Latitude(a.m_value / b.m_value);
    public static Latitude operator *(Latitude a, Latitude b)
      => new Latitude(a.m_value * b.m_value);
    public static Latitude operator %(Latitude a, Latitude b)
      => new Latitude(a.m_value % b.m_value);
    public static Latitude operator -(Latitude a, Latitude b)
      => new Latitude(a.m_value - b.m_value);
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
      => $"<{GetType().Name}: {m_value.Degree}\u00B0>";
    #endregion Object overrides
  }
}
