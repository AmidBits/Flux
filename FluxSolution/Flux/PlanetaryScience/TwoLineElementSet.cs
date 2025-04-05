namespace Flux.PlanetaryScience
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see href="https://www.amsat.org/keplerian-elements-tutorial/"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct TwoLineElementSet2
  {
    private readonly double m_inclination;
    private readonly double m_rightAscensionOfAscendingNode;
    private readonly double m_eccentricity;
    private readonly double m_argumentOfPerigee;
    private readonly double m_meanAnomaly;
    private readonly double m_meanMotion;
    private readonly double m_revolutionNumberAtEpoch;

    public TwoLineElementSet2(double inclinationRadian, double rightAscensionOfAscendingNodeRadian, double eccentricity, double argumentOfPerigeeRadian, double meanAnomalyRadian, double meanMotion, double revolutionNumberAtEpoch)
    {
      m_inclination = inclinationRadian;
      m_rightAscensionOfAscendingNode = rightAscensionOfAscendingNodeRadian;
      m_eccentricity = eccentricity;
      m_argumentOfPerigee = argumentOfPerigeeRadian;
      m_meanAnomaly = meanAnomalyRadian;
      m_meanMotion = meanMotion;
      m_revolutionNumberAtEpoch = revolutionNumberAtEpoch;
    }

    public TwoLineElementSet2(Units.Angle inclination, Units.Angle rightAscensionOfAscendingNode, double eccentricity, Units.Angle argumentOfPerigee, Units.Angle meanAnomaly, double meanMotion, double revolutionNumberAtEpoch)
      : this(inclination.Value, rightAscensionOfAscendingNode.Value, eccentricity, argumentOfPerigee.Value, meanAnomaly.Value, meanMotion, revolutionNumberAtEpoch)
    { }

    public TwoLineElementSet2(double inclinationValue, Units.AngleUnit inclinationUnit, double rightAscensionOfAscendingNodeValue, Units.AngleUnit rightAscensionOfAscendingNodeUnit, double eccentricity, double argumentOfPerigeeValue, Units.AngleUnit argumentOfPerigeeUnit, double meanAnomalyValue, Units.AngleUnit meanAnomalyUnit, double meanMotion, double revolutionNumberAtEpoch)
      : this(Units.Angle.ConvertFromUnit(inclinationUnit, inclinationValue), Units.Angle.ConvertFromUnit(rightAscensionOfAscendingNodeUnit, rightAscensionOfAscendingNodeValue), eccentricity, Units.Angle.ConvertFromUnit(argumentOfPerigeeUnit, argumentOfPerigeeValue), Units.Angle.ConvertFromUnit(meanAnomalyUnit, meanAnomalyValue), meanMotion, revolutionNumberAtEpoch)
    { }

    /// <summary>The angle between the equator and the orbit plane. The value provided is the TEME mean inclination. Degrees, in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
    public Units.Angle Inclination { get => new(m_inclination); init => m_inclination = value.Value; }

    /// <summary>The angle between vernal equinox and the point where the orbit crosses the equatorial plane (going north). The value provided is the TEME mean right ascension of the ascending node. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    public Units.Angle RightAscensionOfAscendingNode { get => new(m_rightAscensionOfAscendingNode); init => m_rightAscensionOfAscendingNode = value.Value; }

    /// <summary>A constant defining the shape of the orbit (0=circular, Less than 1=elliptical). The value provided is the mean eccentricity.</summary>
    public PlanetaryScience.OrbitalEccentricity Eccentricity { get => new(m_eccentricity); init => m_eccentricity = value.Value; }

    /// <summary>The angle between the ascending node and the orbit's point of closest approach to the earth (perigee). The value provided is the TEME mean argument of perigee. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    public Units.Angle ArgumentOfPerigee { get => new(m_argumentOfPerigee); init => m_argumentOfPerigee = value.Value; }

    /// <summary>The angle, measured from perigee, of the satellite location in the orbit referenced to a circular orbit with radius equal to the semi-major axis. Degrees.</summary>
    public Units.Angle MeanAnomaly { get => new(m_meanAnomaly); init => m_meanAnomaly = value.Value; }

    /// <summary> The value is the mean number of orbits per day the object completes. There are 8 digits after the decimal, leaving no trailing space(s) when the following element exceeds 9999. Revolutions per day.</summary>
    public double MeanMotion => m_meanMotion;

    /// <summary>The orbit number at Epoch Time. This time is chosen very near the time of true ascending node passage as a matter of routine. Revolutions.</summary>
    public double RevolutionNumberAtEpoch => m_revolutionNumberAtEpoch;

    public System.Numerics.Matrix4x4 ToMatrix4()
    {
      KeplerianElements.ToRotationMatrix(m_rightAscensionOfAscendingNode, m_inclination, m_argumentOfPerigee, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

      //var (so, co) = double.SinCos(m_rightAscensionOfAscendingNode.Value);
      //var (si, ci) = double.SinCos(m_inclination.Value);
      //var (sw, cw) = double.SinCos(m_argumentOfPerigee.Value);

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
      => double.Pow(4 * double.Pi, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

    //
    //public static EulerAngles ToEulerAngles(CartesianCoordinate3 x, CartesianCoordinate3 y, CartesianCoordinate3 z)
    //{
    //  x = CartesianCoordinate3.Normalize(x);
    //  y = CartesianCoordinate3.Normalize(y);
    //  z = CartesianCoordinate3.Normalize(z);

    //  var alpha = double.Atan2(-x.Y, z.X);
    //  var beta = double.Atan2(z.Z, double.Sqrt(z.X * z.X + z.Y * z.Y));
    //  var gamma = double.Atan2(y.Z, x.Z);

    //  return new(alpha, beta, gamma);
    //}

    //
    //public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double radInclination, out double radRightAscensionOfAscendingNode, out double radArgumentOfPerigee)
    //{
    //  radRightAscensionOfAscendingNode = double.Atan2(-x2, z1);
    //  radInclination = double.Atan2(z3, double.Sqrt(z1 * z1 + z2 * z2));
    //  radArgumentOfPerigee = double.Atan2(y3, x3);
    //}
    #endregion Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"{GetType().Name} {{ Inclination = {Inclination.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, RightAscensionOfAscendingNode = {RightAscensionOfAscendingNode.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, Eccentricity = {Eccentricity.ToString(format, formatProvider)}, ArgumentOfPerigee = {ArgumentOfPerigee.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, MeanAnomaly = {MeanAnomaly.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, MeanMotion = {m_meanMotion}, RevolutionAtEpoch = {m_revolutionNumberAtEpoch} }}";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
