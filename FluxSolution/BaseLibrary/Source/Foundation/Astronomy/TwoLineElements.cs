namespace Flux
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see cref="https://www.amsat.org/keplerian-elements-tutorial/"/>
  public struct TwoLineElements
    : System.IEquatable<TwoLineElements>
  {
    private readonly double m_radInclination;
    private readonly double m_radRightAscensionOfAscendingNode;
    private readonly double m_eccentricity;
    private readonly double m_radArgumentOfPerigee;
    private readonly double m_radMeanAnomaly;
    private readonly double m_meanMotion;

    public TwoLineElements(double radInclination, double radRightAscensionOfAscendingNode, double eccentricity, double radArgumentOfPerigee, double radMeanAnomaly, double meanMotion)
    {
      m_radInclination = radInclination;
      m_radRightAscensionOfAscendingNode = radRightAscensionOfAscendingNode;
      m_eccentricity = eccentricity;
      m_radArgumentOfPerigee = radArgumentOfPerigee;
      m_radMeanAnomaly = radMeanAnomaly;
      m_meanMotion = meanMotion;
    }

    /// <summary>The angle between the orbital plane and the reference plane. Inclination is the angle between the orbital plane and the equatorial plane. By convention, inclination is in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => m_radInclination; init => m_radInclination = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node) By convention, this is a number in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double RightAscensionOfAscendingNode { get => m_radRightAscensionOfAscendingNode; init => m_radRightAscensionOfAscendingNode = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The amount by which an orbit around another body deviates from a perfect circle.</summary>
    [System.Diagnostics.Contracts.Pure] public double Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
    /// <summary>The angle between the ascending node and the periapsis. By convention, this is an angle in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    [System.Diagnostics.Contracts.Pure] public double ArgumentOfPerigee { get => m_radArgumentOfPerigee; init => m_radArgumentOfPerigee = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The longest diameter of an ellipse.</summary>
    [System.Diagnostics.Contracts.Pure] public double MeanAnomaly { get => m_radMeanAnomaly; init => m_radMeanAnomaly = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</summary>
    [System.Diagnostics.Contracts.Pure] public double MeanMotion { get => m_meanMotion; init => m_meanMotion = value; }

    public Flux.Matrix4 ToMatrix4()
    {
      ToRotationMatrix(m_radInclination, m_radRightAscensionOfAscendingNode, m_radArgumentOfPerigee, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

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
      ToRotationMatrix(m_radInclination, m_radRightAscensionOfAscendingNode, m_radArgumentOfPerigee, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

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
    public static void ToRotationMatrix(double radInclination, double radRightAscensionOfAscendingNode, double radArgumentOfPerigee, out double x1, out double x2, out double x3, out double y1, out double y2, out double y3, out double z1, out double z2, out double z3)
    {
      var co = System.Math.Cos(radRightAscensionOfAscendingNode);
      var so = System.Math.Sin(radRightAscensionOfAscendingNode);
      var ci = System.Math.Cos(radInclination);
      var si = System.Math.Sin(radInclination);
      var cw = System.Math.Cos(radArgumentOfPerigee);
      var sw = System.Math.Sin(radArgumentOfPerigee);

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
    public static EulerAngles ToEulerAngles(CartesianCoordinate3 x, CartesianCoordinate3 y, CartesianCoordinate3 z)
    {
      x = CartesianCoordinate3.Normalize(x);
      y = CartesianCoordinate3.Normalize(y);
      z = CartesianCoordinate3.Normalize(z);

      var alpha = System.Math.Atan2(-x.Y, z.X);
      var beta = System.Math.Atan2(z.Z, System.Math.Sqrt(z.X * z.X + z.Y * z.Y));
      var gamma = System.Math.Atan2(y.Z, x.Z);

      return new(alpha, beta, gamma);
    }

    [System.Diagnostics.Contracts.Pure]
    public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double radInclination, out double radRightAscensionOfAscendingNode, out double radArgumentOfPerigee)
    {
      radRightAscensionOfAscendingNode = System.Math.Atan2(-x2, z1);
      radInclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
      radArgumentOfPerigee = System.Math.Atan2(y3, x3);
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(TwoLineElements a, TwoLineElements b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(TwoLineElements a, TwoLineElements b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(TwoLineElements other)
      => m_radMeanAnomaly == other.m_radMeanAnomaly && m_eccentricity == other.m_eccentricity && m_radInclination == other.m_radInclination && m_radRightAscensionOfAscendingNode == other.m_radRightAscensionOfAscendingNode && m_radArgumentOfPerigee == other.m_radArgumentOfPerigee && m_meanMotion == other.m_meanMotion;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is TwoLineElements o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_radMeanAnomaly, m_eccentricity, m_radInclination, m_radRightAscensionOfAscendingNode, m_radArgumentOfPerigee, m_meanMotion);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ SemiMajorAxis = {m_radMeanAnomaly}, Eccentricity = {m_eccentricity}, Inclination = {m_radInclination}, LongitudeOfAscendingNode = {m_radRightAscensionOfAscendingNode}, ArgumentOfPeriapsis = {m_radArgumentOfPerigee}, TrueAnomaly = {m_meanMotion} }}";
    #endregion Object overrides
  }
}
