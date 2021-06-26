namespace Flux.Units
{
  /// <summary>Latitude.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Latitude"/>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, IStandardizedScalar
  {
    public const double MaxValue = +90;
    public const double MinValue = -90;

    private readonly double m_degree;

    public Latitude(double degree)
      => m_degree = Maths.Wrap(degree, MinValue, MaxValue);

    public double Degree
      => m_degree;
    public double Radian
      => Angle.ConvertDegreeToRadian(m_degree);

    public Angle ToAngle()
      => Angle.FromDegree(m_degree);

    public Length ToApproximateHeight
      => ComputeApproximateHeightAt(Degree);
    public Length ToApproximateWidth
      => ComputeApproximateWidthAt(Degree);
    public Length ToApproximateRadius
      => ComputeApproximateRadiusAt(Degree);

    #region Static methods
    public static Latitude Add(Latitude left, Latitude right)
      => new Latitude(left.m_degree + right.m_degree);
    /// <summary>Computes the approximate length in meters of a degree of latitude at the specified latitude.</summary>
    public static double ComputeApproximateHeightAt(double latitude)
    {
      const double heightAtEquatorInMeters = 110567;
      const double heightAtPolesInMeters = 111699;

      var radian = Angle.ConvertDegreeToRadian(latitude);

      return System.Math.Sin(radian) * (heightAtPolesInMeters - heightAtEquatorInMeters) + heightAtEquatorInMeters;
    }
    /// <summary>Determines an approximate radius in meters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Earth_radius#Radius_at_a_given_geodetic_latitude"/>
    /// <seealso cref="https://gis.stackexchange.com/questions/20200/how-do-you-compute-the-earths-radius-at-a-given-geodetic-latitude"/>
    public static double ComputeApproximateRadiusAt(double latitude)
    {
      var radian = Angle.ConvertDegreeToRadian(latitude);

      var cos = System.Math.Cos(radian);
      var sin = System.Math.Sin(radian);

      var numerator = System.Math.Pow(System.Math.Pow(EarthRadii.EquatorialInMeters, 2) * cos, 2) + System.Math.Pow(System.Math.Pow(EarthRadii.PolarInMeters, 2) * sin, 2);
      var denominator = System.Math.Pow(EarthRadii.EquatorialInMeters * cos, 2) + System.Math.Pow(EarthRadii.PolarInMeters * sin, 2);

      return System.Math.Sqrt(numerator / denominator);
    }
    /// <summary>Computes the approximate length in meters of a degree of longitude at the specified latitude.</summary>
    /// <returns>The approximate length in meters of a degree of longitude.</returns>
    public static double ComputeApproximateWidthAt(double latitude)
    {
      const double widthAtEquatorInMeters = 111321;

      var radian = Angle.ConvertDegreeToRadian(latitude);

      return System.Math.Cos(radian) * widthAtEquatorInMeters;
    }
    public static Latitude Divide(Latitude left, Latitude right)
      => new Latitude(left.m_degree / right.m_degree);
    public static Latitude Multiply(Latitude left, Latitude right)
      => new Latitude(left.m_degree * right.m_degree);
    public static Latitude Negate(Latitude value)
      => new Latitude(-value.m_degree);
    public static Latitude Remainder(Latitude dividend, Latitude divisor)
      => new Latitude(dividend.m_degree % divisor.m_degree);
    public static Latitude Subtract(Latitude left, Latitude right)
      => new Latitude(left.m_degree - right.m_degree);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Latitude v)
      => v.m_degree;
    public static implicit operator Latitude(double v)
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
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Latitude other)
      => m_degree == other.m_degree;

    // IUnitStandardized
    public double GetScalar()
      => m_degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Latitude o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_degree}\u00B0>";
    #endregion Object overrides
  }
}
