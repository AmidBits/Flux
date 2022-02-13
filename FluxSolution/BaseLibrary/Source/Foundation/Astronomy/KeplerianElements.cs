namespace Flux
{
  //public static partial class ExtensionMethods
  //{

  //  public System.Numerics.Matrix4x4 ToMatrix(this KeplerianElements source)
  //  {
  //    source.ToRotationMatrix(source.LongitudeOfAscendingNode, source.Inclination, source., out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

  //    return new(
  //      (float)x1, (float)x2, (float)x3, 0f,
  //      (float)y1, (float)y2, (float)y3, 0f,
  //      (float)z1, (float)z2, (float)z3, 0f,
  //      0f, 0f, 0f, 1f
  //    );
  //  }
  //  public void RotationMatrixToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double longitudeOfAscendingNode, out double inclination, out double argumentOfPeriapsis)
  //  {
  //    longitudeOfAscendingNode = System.Math.Atan2(-x2, z1);
  //    inclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
  //    argumentOfPeriapsis = System.Math.Atan2(y3, x3);
  //  }

  //}

  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Orbital_elements"/>
  public struct KeplerianElements
    : System.IEquatable<KeplerianElements>
  {
    /// <summary>The amount by which an orbit around another body deviates from a perfect circle.</summary>
    private readonly double m_eccentricity;
    /// <summary>The longest diameter of an ellipse.</summary>
    private readonly double m_semiMajorAxis;
    /// <summary>The angle between the orbital plane and the reference plane.</summary>
    private readonly double m_inclination;
    /// <summary>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node).</summary>
    private readonly double m_longitudeOfAscendingNode;
    /// <summary>The angle between the ascending node and the periapsis.</summary>
    private readonly double m_argumentOfPeriapsis;
    /// <summary>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</summary>
    private readonly double m_trueAnomaly;

    public KeplerianElements(double semiMajorAxis, double eccentricity, double inclination, double longitudeOfAscendingNode, double argumentOfPeriapsis, double trueAnomaly)
    {
      m_semiMajorAxis = semiMajorAxis;
      m_eccentricity = eccentricity;
      m_inclination = inclination;
      m_longitudeOfAscendingNode = longitudeOfAscendingNode;
      m_argumentOfPeriapsis = argumentOfPeriapsis;
      m_trueAnomaly = trueAnomaly;
    }

    public double SemiMajorAxis
      => m_semiMajorAxis;
    public double Eccentricity
      => m_eccentricity;
    public double Inclination
      => m_inclination;
    public double LongitudeOfAscendingNode
      => m_longitudeOfAscendingNode;
    public double ArgumentOfPeriapsis
      => m_argumentOfPeriapsis;
    public double TrueAnomaly
      => m_trueAnomaly;

    public System.Numerics.Matrix4x4 ToMatrix4x4()
    {
      ToRotationMatrix(m_longitudeOfAscendingNode, m_inclination, m_argumentOfPeriapsis, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

      return new System.Numerics.Matrix4x4(
        (float)x1, (float)x2, (float)x3, 0f,
        (float)y1, (float)y2, (float)y3, 0f,
        (float)z1, (float)z2, (float)z3, 0f,
        0f, 0f, 0f, 1f
      );
    }

    #region Static methods
    public static double ComputeProportionalityConstant(double gravitionalConstant, double massOfSun, double massOfPlanet)
      => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

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
    public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double longitudeOfAscendingNode, out double inclination, out double argumentOfPeriapsis)
    {
      longitudeOfAscendingNode = System.Math.Atan2(-x2, z1);
      inclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
      argumentOfPeriapsis = System.Math.Atan2(y3, x3);
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(KeplerianElements a, KeplerianElements b)
      => a.Equals(b);
    public static bool operator !=(KeplerianElements a, KeplerianElements b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(KeplerianElements other)
      => m_semiMajorAxis == other.m_semiMajorAxis && m_eccentricity == other.m_eccentricity && m_inclination == other.m_inclination && m_longitudeOfAscendingNode == other.m_longitudeOfAscendingNode && m_argumentOfPeriapsis == other.m_argumentOfPeriapsis && m_trueAnomaly == other.m_trueAnomaly;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is KeplerianElements o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_semiMajorAxis, m_eccentricity, m_inclination, m_longitudeOfAscendingNode, m_argumentOfPeriapsis, m_trueAnomaly);
    public override string ToString()
      => $"{GetType().Name} {{ SemiMajorAxis = {m_semiMajorAxis}, Eccentricity = {m_eccentricity}, Inclination = {m_inclination}, LongitudeOfAscendingNode = {m_longitudeOfAscendingNode}, ArgumentOfPeriapsis = {m_argumentOfPeriapsis}, TrueAnomaly = {m_trueAnomaly} }}";
    #endregion Object overrides
  }
}
