namespace Flux
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see cref="https://www.amsat.org/keplerian-elements-tutorial/"/>
  public struct TwoLineElementSet2
    : System.IEquatable<TwoLineElementSet2>
  {
    private readonly double m_radInclination;
    private readonly double m_radRightAscensionOfAscendingNode;
    private readonly double m_eccentricity;
    private readonly double m_radArgumentOfPerigee;
    private readonly double m_radMeanAnomaly;
    private readonly double m_meanMotion;
    private readonly double m_revolutionNumberAtEpoch;

    public TwoLineElementSet2(double radInclination, double radRightAscensionOfAscendingNode, double eccentricity, double radArgumentOfPerigee, double radMeanAnomaly, double meanMotion, double revolutionNumberAtEpoch)
    {
      m_radInclination = radInclination;
      m_radRightAscensionOfAscendingNode = radRightAscensionOfAscendingNode;
      m_eccentricity = eccentricity;
      m_radArgumentOfPerigee = radArgumentOfPerigee;
      m_radMeanAnomaly = radMeanAnomaly;
      m_meanMotion = meanMotion;
      m_revolutionNumberAtEpoch = revolutionNumberAtEpoch;
    }

    /// <summary>The angle between the equator and the orbit plane. The value provided is the TEME mean inclination. Degrees, in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => Angle.ConvertRadianToDegree(m_radInclination); init => m_radInclination = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The angle between vernal equinox and the point where the orbit crosses the equatorial plane (going north). The value provided is the TEME mean right ascension of the ascending node. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double RightAscensionOfAscendingNode { get => Angle.ConvertRadianToDegree(m_radRightAscensionOfAscendingNode); init => m_radRightAscensionOfAscendingNode = Angle.ConvertDegreeToRadian(value); }
    /// <summary>A constant defining the shape of the orbit (0=circular, Less than 1=elliptical). The value provided is the mean eccentricity.</summary>
    [System.Diagnostics.Contracts.Pure] public double Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
    /// <summary>The angle between the ascending node and the orbit's point of closest approach to the earth (perigee). The value provided is the TEME mean argument of perigee. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    [System.Diagnostics.Contracts.Pure] public double ArgumentOfPerigee { get => Angle.ConvertRadianToDegree(m_radArgumentOfPerigee); init => m_radArgumentOfPerigee = Angle.ConvertDegreeToRadian(value); }
    /// <summary>The angle, measured from perigee, of the satellite location in the orbit referenced to a circular orbit with radius equal to the semi-major axis. Degrees.</summary>
    [System.Diagnostics.Contracts.Pure] public double MeanAnomaly { get => Angle.ConvertRadianToDegree(m_radMeanAnomaly); init => m_radMeanAnomaly = Angle.ConvertDegreeToRadian(value); }
    /// <summary> The value is the mean number of orbits per day the object completes. There are 8 digits after the decimal, leaving no trailing space(s) when the following element exceeds 9999. Revolutions per day.</summary>
    [System.Diagnostics.Contracts.Pure] public double MeanMotion { get => m_meanMotion; init => m_meanMotion = value; }
    /// <summary>The orbit number at Epoch Time. This time is chosen very near the time of true ascending node passage as a matter of routine. Revolutions.</summary>
    [System.Diagnostics.Contracts.Pure] public double RevolutionNumberAtEpoch { get => m_revolutionNumberAtEpoch; init => m_revolutionNumberAtEpoch = value; }

    public Flux.Matrix4 ToMatrix4()
    {
      var co = System.Math.Cos(m_radRightAscensionOfAscendingNode);
      var so = System.Math.Sin(m_radRightAscensionOfAscendingNode);
      var ci = System.Math.Cos(m_radInclination);
      var si = System.Math.Sin(m_radInclination);
      var cw = System.Math.Cos(m_radArgumentOfPerigee);
      var sw = System.Math.Sin(m_radArgumentOfPerigee);

      var x1 = co * cw - so * ci * sw;
      var x2 = so * cw + co * ci * sw;
      var x3 = si * sw;

      var y1 = -co * sw - so * ci * cw;
      var y2 = -so * sw + co * ci * cw;
      var y3 = si * cw;

      var z1 = si * so;
      var z2 = -si * co;
      var z3 = ci;

      return new Matrix4(
        x1, x2, x3, 0,
        y1, y2, y3, 0,
        z1, z2, z3, 0,
        0, 0, 0, 1
      );
    }

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static double ComputeProportionalityConstant(double gravitionalConstant, double massOfSun, double massOfPlanet)
      => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

    //[System.Diagnostics.Contracts.Pure]
    //public static EulerAngles ToEulerAngles(CartesianCoordinate3 x, CartesianCoordinate3 y, CartesianCoordinate3 z)
    //{
    //  x = CartesianCoordinate3.Normalize(x);
    //  y = CartesianCoordinate3.Normalize(y);
    //  z = CartesianCoordinate3.Normalize(z);

    //  var alpha = System.Math.Atan2(-x.Y, z.X);
    //  var beta = System.Math.Atan2(z.Z, System.Math.Sqrt(z.X * z.X + z.Y * z.Y));
    //  var gamma = System.Math.Atan2(y.Z, x.Z);

    //  return new(alpha, beta, gamma);
    //}

    //[System.Diagnostics.Contracts.Pure]
    //public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double radInclination, out double radRightAscensionOfAscendingNode, out double radArgumentOfPerigee)
    //{
    //  radRightAscensionOfAscendingNode = System.Math.Atan2(-x2, z1);
    //  radInclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
    //  radArgumentOfPerigee = System.Math.Atan2(y3, x3);
    //}
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(TwoLineElementSet2 a, TwoLineElementSet2 b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(TwoLineElementSet2 a, TwoLineElementSet2 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(TwoLineElementSet2 other)
      => m_radMeanAnomaly == other.m_radMeanAnomaly && m_eccentricity == other.m_eccentricity && m_radInclination == other.m_radInclination && m_radRightAscensionOfAscendingNode == other.m_radRightAscensionOfAscendingNode && m_radArgumentOfPerigee == other.m_radArgumentOfPerigee && m_meanMotion == other.m_meanMotion;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is TwoLineElementSet2 o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => System.HashCode.Combine(m_radMeanAnomaly, m_eccentricity, m_radInclination, m_radRightAscensionOfAscendingNode, m_radArgumentOfPerigee, m_meanMotion);
    [System.Diagnostics.Contracts.Pure]
    public override string ToString()
      => $"{GetType().Name} {{ Inclination = {Inclination}{AngleUnit.Degree.GetUnitString(false, false)}, RightAscensionOfAscendingNode = {RightAscensionOfAscendingNode}{AngleUnit.Degree.GetUnitString(false, false)}, Eccentricity = {Eccentricity}, ArgumentOfPerigee = {ArgumentOfPerigee}{AngleUnit.Degree.GetUnitString(false, false)}, MeanAnomaly = {MeanAnomaly}{AngleUnit.Degree.GetUnitString(false, false)}, MeanMotion = {MeanMotion} }}";
    #endregion Object overrides
  }
}
