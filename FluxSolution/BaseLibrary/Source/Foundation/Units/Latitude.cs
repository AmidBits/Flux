namespace Flux.Units
{
  /// <summary>Latitude is a geographic coordinate that specifies the north–south position of a point on the Earth's surface. The unit here is defined in the range [-90, +90].</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, IStandardizedScalar
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    private readonly Angle m_angle;

    public Latitude(double degree)
      => m_angle = Angle.FromDegree(Maths.Wrap(degree, MinValue, MaxValue));
    public Latitude(Angle angle)
      : this(angle.Degree)
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
      => m_angle;

    #region Static methods
    public static Latitude Add(Latitude left, Latitude right)
      => new Latitude(left.m_angle + right.m_angle);
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
    public static Latitude Divide(Latitude left, Latitude right)
      => new Latitude(left.m_angle / right.m_angle);
    public static Latitude Multiply(Latitude left, Latitude right)
      => new Latitude(left.m_angle * right.m_angle);
    public static Latitude Negate(Latitude value)
      => new Latitude(-value.m_angle);
    public static Latitude Remainder(Latitude dividend, Latitude divisor)
      => new Latitude(dividend.m_angle % divisor.m_angle);
    public static Latitude Subtract(Latitude left, Latitude right)
      => new Latitude(left.m_angle - right.m_angle);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v)
      => v.m_angle.Degree;
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

    public static Latitude operator +(Latitude a, Latitude b)
      => Add(a, b);
    public static Latitude operator /(Latitude a, Latitude b)
      => Divide(a, b);
    public static Latitude operator *(Latitude a, Latitude b)
      => Multiply(a, b);
    public static Latitude operator -(Latitude v)
      => Negate(v);
    public static Latitude operator %(Latitude a, Latitude b)
      => Remainder(a, b);
    public static Latitude operator -(Latitude a, Latitude b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Latitude other)
      => m_angle.CompareTo(other.m_angle);

    // IEquatable
    public bool Equals(Latitude other)
      => m_angle == other.m_angle;

    // IUnitStandardized
    public double GetScalar()
      => m_angle.Degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Latitude o && Equals(o);
    public override int GetHashCode()
      => m_angle.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_angle.Degree}\u00B0>";
    #endregion Object overrides
  }
}
