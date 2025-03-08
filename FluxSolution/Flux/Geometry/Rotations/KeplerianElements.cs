namespace Flux.Geometry.Rotations
{
  /// <summary>Kepler elements for computing orbits.
  /// <para><see href="https://en.wikipedia.org/wiki/Orbital_elements"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Kepler%27s_laws_of_planetary_motion"/></para>
  /// <para><see href="https://www.amsat.org/keplerian-elements-tutorial/"/></para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct KeplerianElements
    : System.IFormattable
  {
    /// <summary>
    /// <para><see href="https://ssd.jpl.nasa.gov/horizons/app.html#/"/></para>
    /// </summary>
    public static KeplerianElements Mars { get; } = new(new Units.OrbitalEccentricity(9.327987858487280E-02), new Units.Length(MetricPrefix.Kilo, 2.279325209790799E+08), new Units.Angle(1.847854452956304E+00, Units.AngleUnit.Degree), new Units.Angle(4.948935675287289E+01, Units.AngleUnit.Degree), new Units.Angle(2.866702002890120E+02, Units.AngleUnit.Degree), new Units.Angle(3.270334666858926E+02, Units.AngleUnit.Degree));

    public static readonly double TheObliquityOfTheEclipticInDegrees = 23.4;

    private readonly Units.OrbitalEccentricity m_eccentricity;
    private readonly Units.Length m_semiMajorAxis;
    private readonly Units.Angle m_inclination;
    private readonly Units.Angle m_longitudeOfAscendingNode;
    private readonly Units.Angle m_argumentOfPeriapsis;
    private readonly Units.Angle m_trueAnomaly;

    public KeplerianElements(Units.OrbitalEccentricity eccentricity, Units.Length semiMajorAxis, Units.Angle inclination, Units.Angle longitudeOfAscendingNode, Units.Angle argumentOfPeriapsis, Units.Angle trueAnomaly)
    {
      m_eccentricity = eccentricity;
      m_semiMajorAxis = semiMajorAxis;
      m_inclination = inclination;
      m_longitudeOfAscendingNode = longitudeOfAscendingNode;
      m_argumentOfPeriapsis = argumentOfPeriapsis;
      m_trueAnomaly = trueAnomaly;
    }

    /// <summary>
    /// <para>The amount by which an orbit around another body deviates from a perfect circle.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Orbital_eccentricity"/></para>
    /// </summary>
    public Units.OrbitalEccentricity Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }

    /// <summary>
    /// <para>The longest diameter of an ellipse.</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Semi-major_and_semi-minor_axes"/></para>
    /// </summary>
    public Units.Length SemiMajorAxis { get => m_semiMajorAxis; init => m_semiMajorAxis = value; }

    /// <summary>
    /// <para>The angle between the orbital plane and the reference plane. Inclination is the angle between the orbital plane and the equatorial plane. By convention, inclination is in the range [0, 180] degrees, i.e. [0, PI] radians.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Orbital_inclination"/></para>
    /// </summary>
    public Units.Angle Inclination { get => m_inclination; init => m_inclination = value; }

    /// <summary>
    /// <para>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node) By convention, this is a number in the range [0, 360] degrees, i.e. [0, 2PI] radians.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Longitude_of_the_ascending_node"/></para>
    /// </summary>
    public Units.Angle LongitudeOfAscendingNode { get => m_longitudeOfAscendingNode; init => m_longitudeOfAscendingNode = value; }

    /// <summary>
    /// <para>The angle between the ascending node and the periapsis. By convention, this is an angle in the range [0, 360] degrees, i.e. [0, 2PI].</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Argument_of_periapsis"/></para>
    /// </summary>
    public Units.Angle ArgumentOfPeriapsis { get => m_argumentOfPeriapsis; init => m_argumentOfPeriapsis = value; }

    /// <summary>
    /// <para>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/True_anomaly"/></para>
    /// </summary>
    public Units.Angle TrueAnomaly { get => m_trueAnomaly; init => m_trueAnomaly = value; }

    public System.Numerics.Matrix4x4 ToMatrix4()
    {
      ToRotationMatrix(m_longitudeOfAscendingNode.Value, m_inclination.Value, m_argumentOfPeriapsis.Value, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

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

    public static void ToRotationMatrix(double longitudeOfAscendingNode, double inclination, double argumentOfPeriapsis, out double x1, out double x2, out double x3, out double y1, out double y2, out double y3, out double z1, out double z2, out double z3)
    {
      var (so, co) = double.SinCos(longitudeOfAscendingNode);
      var (si, ci) = double.SinCos(inclination);
      var (sw, cw) = double.SinCos(argumentOfPeriapsis);

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

    //public static Numerics.EulerAngles<double> ToEulerAngles(Numerics.CartesianCoordinate3<double> x, Numerics.CartesianCoordinate3<double> y, Numerics.CartesianCoordinate3<double> z)
    //{
    //  x = x.Normalized();
    //  y = y.Normalized();
    //  z = z.Normalized();

    //  var alpha = double.Atan2(-x.Y, z.X);
    //  var beta = double.Atan2(z.Z, double.Sqrt(z.X * z.X + z.Y * z.Y));
    //  var gamma = double.Atan2(y.Z, x.Z);

    //  return new(alpha, beta, gamma);
    //}

#pragma warning disable IDE0060 // Remove unused parameter
    public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double longitudeOfAscendingNode, out double inclination, out double argumentOfPeriapsis)
#pragma warning restore IDE0060 // Remove unused parameter
    {
      longitudeOfAscendingNode = double.Atan2(-x2, z1);
      inclination = double.Atan2(z3, double.Sqrt(z1 * z1 + z2 * z2));
      argumentOfPeriapsis = double.Atan2(y3, x3);
    }
    #endregion // Static methods

    #region Interface implementations

    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"{GetType().Name} {{ Eccentricity = {m_eccentricity.Value.ToString(format, formatProvider)} ({m_eccentricity.GetOrbitalEccentricityClass()}), SemiMajorAxis = {m_semiMajorAxis.ToString(format, formatProvider)}, Inclination = {m_inclination.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, LongitudeOfAscendingNode = {m_longitudeOfAscendingNode.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, ArgumentOfPeriapsis = {m_argumentOfPeriapsis.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}, TrueAnomaly = {m_trueAnomaly.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)} }}";

    #endregion // Interface implementations

    public override string ToString() => ToString(null, null);
  }
}
