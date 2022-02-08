namespace Flux
{
  /// <summary>Axis-angle 3D rotation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Axis-angle_representation"/>
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
