namespace Flux
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see cref="https://www.amsat.org/keplerian-elements-tutorial/"/>
  public struct KeplerianElements
    : System.IEquatable<KeplerianElements>
  {
    public const double TheObliquityOfTheEclipticInDegrees = 23.4;

    private readonly double m_eccentricity;
    private readonly double m_semiMajorAxis;
    private readonly double m_radInclination;
    private readonly double m_radLongitudeOfAscendingNode;
    private readonly double m_radArgumentOfPeriapsis;
    private readonly double m_trueAnomaly;

    public KeplerianElements(double semiMajorAxis, double eccentricity, double inclination, double longitudeOfAscendingNode, double argumentOfPeriapsis, double trueAnomaly)
    {
      m_semiMajorAxis = semiMajorAxis;
      m_eccentricity = eccentricity;
      m_radInclination = inclination;
      m_radLongitudeOfAscendingNode = longitudeOfAscendingNode;
      m_radArgumentOfPeriapsis = argumentOfPeriapsis;
      m_trueAnomaly = trueAnomaly;
    }

    /// <summary>The longest diameter of an ellipse.</summary>
    [System.Diagnostics.Contracts.Pure] public double SemiMajorAxis { get => m_semiMajorAxis; init => m_semiMajorAxis = value; }
    /// <summary>The amount by which an orbit around another body deviates from a perfect circle.</summary>
    [System.Diagnostics.Contracts.Pure] public double Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
    /// <summary>The angle between the orbital plane and the reference plane. Inclination is the angle between the orbital plane and the equatorial plane. By convention, inclination is in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => m_radInclination; init => m_radInclination = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node) By convention, this is a number in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double LongitudeOfAscendingNode { get => m_radLongitudeOfAscendingNode; init => m_radLongitudeOfAscendingNode = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The angle between the ascending node and the periapsis. By convention, this is an angle in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    [System.Diagnostics.Contracts.Pure] public double ArgumentOfPeriapsis { get => m_radArgumentOfPeriapsis; init => m_radArgumentOfPeriapsis = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</summary>
    [System.Diagnostics.Contracts.Pure] public double TrueAnomaly { get => m_trueAnomaly; init => m_trueAnomaly = value; }

    public Flux.Matrix4 ToMatrix4()
    {
      ToRotationMatrix(m_radLongitudeOfAscendingNode, m_radInclination, m_radArgumentOfPeriapsis, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

      return new Matrix4(
        x1, x2, x3, 0,
        y1, y2, y3, 0,
        z1, z2, z3, 0,
        0, 0, 0, 1
      );
    }

    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Matrix4x4 ToMatrix4x4()
    {
      ToRotationMatrix(m_radLongitudeOfAscendingNode, m_radInclination, m_radArgumentOfPeriapsis, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

      return new System.Numerics.Matrix4x4
      (
        (float)x1, (float)x2, (float)x3, 0,
        (float)y1, (float)y2, (float)y3, 0,
        (float)z1, (float)z2, (float)z3, 0,
        0, 0, 0, 1
      );
    }

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static double ComputeProportionalityConstant(double gravitionalConstant, double massOfSun, double massOfPlanet)
      => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

    [System.Diagnostics.Contracts.Pure]
    public static void ToRotationMatrix(double longitudeOfAscendingNode, double inclination, double argumentOfPeriapsis, out double x1, out double x2, out double x3, out double y1, out double y2, out double y3, out double z1, out double z2, out double z3)
    {
      var co = System.Math.Cos(longitudeOfAscendingNode);
      var so = System.Math.Sin(longitudeOfAscendingNode);
      var ci = System.Math.Cos(inclination);
      var si = System.Math.Sin(inclination);
      var cw = System.Math.Cos(argumentOfPeriapsis);
      var sw = System.Math.Sin(argumentOfPeriapsis);

      x1 = co * cw - so * ci * sw;
      x2 = so * cw + co * ci * sw;
      x3 = si * sw;

      y1 = -co * sw - so * ci * cw;
      y2 = -so * sw + co * ci * cw;
      y3 = si * cw;

      z1 = si * so;
      z2 = -si * co;
      z3 = ci;
    }
    [System.Diagnostics.Contracts.Pure]
    public static EulerAngles ToEulerAngles(Vector3 x, Vector3 y, Vector3 z)
    {
      x = x.Normalized();
      y = y.Normalized();
      z = z.Normalized();

      var alpha = System.Math.Atan2(-x.Y, z.X);
      var beta = System.Math.Atan2(z.Z, System.Math.Sqrt(z.X * z.X + z.Y * z.Y));
      var gamma = System.Math.Atan2(y.Z, x.Z);

      return new(alpha, beta, gamma);
    }

    [System.Diagnostics.Contracts.Pure]
    public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double longitudeOfAscendingNode, out double inclination, out double argumentOfPeriapsis)
    {
      longitudeOfAscendingNode = System.Math.Atan2(-x2, z1);
      inclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
      argumentOfPeriapsis = System.Math.Atan2(y3, x3);
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(KeplerianElements a, KeplerianElements b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(KeplerianElements a, KeplerianElements b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(KeplerianElements other)
      => m_semiMajorAxis == other.m_semiMajorAxis && m_eccentricity == other.m_eccentricity && m_radInclination == other.m_radInclination && m_radLongitudeOfAscendingNode == other.m_radLongitudeOfAscendingNode && m_radArgumentOfPeriapsis == other.m_radArgumentOfPeriapsis && m_trueAnomaly == other.m_trueAnomaly;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is KeplerianElements o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_semiMajorAxis, m_eccentricity, m_radInclination, m_radLongitudeOfAscendingNode, m_radArgumentOfPeriapsis, m_trueAnomaly);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ SemiMajorAxis = {m_semiMajorAxis}, Eccentricity = {m_eccentricity}, Inclination = {m_radInclination}, LongitudeOfAscendingNode = {m_radLongitudeOfAscendingNode}, ArgumentOfPeriapsis = {m_radArgumentOfPeriapsis}, TrueAnomaly = {m_trueAnomaly} }}";
    #endregion Object overrides
  }
}
