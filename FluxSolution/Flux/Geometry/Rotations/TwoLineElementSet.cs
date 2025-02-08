namespace Flux.Geometry.Rotations
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see href="https://www.amsat.org/keplerian-elements-tutorial/"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct TwoLineElementSet2
  {
    private readonly Units.Angle m_inclination;
    private readonly Units.Angle m_rightAscensionOfAscendingNode;
    private readonly double m_eccentricity;
    private readonly Units.Angle m_argumentOfPerigee;
    private readonly Units.Angle m_meanAnomaly;
    private readonly double m_meanMotion;
    private readonly double m_revolutionNumberAtEpoch;

    public TwoLineElementSet2(Units.Angle inclination, Units.Angle rightAscensionOfAscendingNode, double eccentricity, Units.Angle argumentOfPerigee, Units.Angle meanAnomaly, double meanMotion, double revolutionNumberAtEpoch)
    {
      m_inclination = inclination;
      m_rightAscensionOfAscendingNode = rightAscensionOfAscendingNode;
      m_eccentricity = eccentricity;
      m_argumentOfPerigee = argumentOfPerigee;
      m_meanAnomaly = meanAnomaly;
      m_meanMotion = meanMotion;
      m_revolutionNumberAtEpoch = revolutionNumberAtEpoch;
    }

    public TwoLineElementSet2(double inclinationValue, Units.AngleUnit inclinationUnit, double rightAscensionOfAscendingNodeValue, Units.AngleUnit rightAscensionOfAscendingNodeUnit, double eccentricity, double argumentOfPerigeeValue, Units.AngleUnit argumentOfPerigeeUnit, double meanAnomalyValue, Units.AngleUnit meanAnomalyUnit, double meanMotion, double revolutionNumberAtEpoch)
      : this(
          new(inclinationValue, inclinationUnit),
          new(rightAscensionOfAscendingNodeValue, rightAscensionOfAscendingNodeUnit),
          eccentricity,
          new(argumentOfPerigeeValue, argumentOfPerigeeUnit),
          new(meanAnomalyValue, meanAnomalyUnit),
          meanMotion,
          revolutionNumberAtEpoch
        )
    { }

    /// <summary>The angle between the equator and the orbit plane. The value provided is the TEME mean inclination. Degrees, in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
    public Units.Angle Inclination => m_inclination;

    /// <summary>The angle between vernal equinox and the point where the orbit crosses the equatorial plane (going north). The value provided is the TEME mean right ascension of the ascending node. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    public Units.Angle RightAscensionOfAscendingNode => m_rightAscensionOfAscendingNode;

    /// <summary>A constant defining the shape of the orbit (0=circular, Less than 1=elliptical). The value provided is the mean eccentricity.</summary>
    public double Eccentricity => m_eccentricity;

    /// <summary>The angle between the ascending node and the orbit's point of closest approach to the earth (perigee). The value provided is the TEME mean argument of perigee. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    public Units.Angle ArgumentOfPerigee => m_argumentOfPerigee;

    /// <summary>The angle, measured from perigee, of the satellite location in the orbit referenced to a circular orbit with radius equal to the semi-major axis. Degrees.</summary>
    public Units.Angle MeanAnomaly => m_meanAnomaly;

    /// <summary> The value is the mean number of orbits per day the object completes. There are 8 digits after the decimal, leaving no trailing space(s) when the following element exceeds 9999. Revolutions per day.</summary>
    public double MeanMotion => m_meanMotion;

    /// <summary>The orbit number at Epoch Time. This time is chosen very near the time of true ascending node passage as a matter of routine. Revolutions.</summary>
    public double RevolutionNumberAtEpoch => m_revolutionNumberAtEpoch;

    public System.Numerics.Matrix4x4 ToMatrix4()
    {
      KeplerianElements.ToRotationMatrix(m_rightAscensionOfAscendingNode.Value, m_inclination.Value, m_argumentOfPerigee.Value, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

      //var (so, co) = System.Math.SinCos(m_rightAscensionOfAscendingNode.Value);
      //var (si, ci) = System.Math.SinCos(m_inclination.Value);
      //var (sw, cw) = System.Math.SinCos(m_argumentOfPerigee.Value);

      //var x1 = co * cw - so * ci * sw;
      //var x2 = so * cw + co * ci * sw;
      //var x3 = si * sw;

      //var y1 = -co * sw - so * ci * cw;
      //var y2 = -so * sw + co * ci * cw;
      //var y3 = si * cw;

      //var z1 = si * so;
      //var z2 = -si * co;
      //var z3 = ci;

      return new(
        (float)x1, (float)x2, (float)x3, 0,
        (float)y1, (float)y2, (float)y3, 0,
        (float)z1, (float)z2, (float)z3, 0,
        0, 0, 0, 1
      );
    }

    #region Static methods

    public static double ComputeProportionalityConstant(double gravitionalConstant, double massOfSun, double massOfPlanet)
      => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

    //
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

    //
    //public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double radInclination, out double radRightAscensionOfAscendingNode, out double radArgumentOfPerigee)
    //{
    //  radRightAscensionOfAscendingNode = System.Math.Atan2(-x2, z1);
    //  radInclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
    //  radArgumentOfPerigee = System.Math.Atan2(y3, x3);
    //}
    #endregion Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"{GetType().Name} {{ Inclination = {m_inclination.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, RightAscensionOfAscendingNode = {m_rightAscensionOfAscendingNode.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, Eccentricity = {m_eccentricity}, ArgumentOfPerigee = {m_argumentOfPerigee.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, MeanAnomaly = {m_meanAnomaly.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, MeanMotion = {m_meanMotion} }}";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
