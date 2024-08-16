namespace Flux
{
  namespace Rotations
  {
    /// <summary>Kepler elements for computing orbits.
    /// <para><see href="https://en.wikipedia.org/wiki/Orbital_elements"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Kepler%27s_laws_of_planetary_motion"/></para>
    /// <para><see href="https://www.amsat.org/keplerian-elements-tutorial/"/></para>
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct KeplerianElements
    {
      /// <summary>
      /// <para><see href="https://ssd.jpl.nasa.gov/horizons/app.html#/"/></para>
      /// </summary>
      public static readonly KeplerianElements Mars = new KeplerianElements(new Quantities.OrbitalEccentricity(9.327987858487280E-02), new Quantities.Length(2.279325209790799E+08, Quantities.LengthUnit.Kilometre), new Quantities.Angle(1.847854452956304E+00, Quantities.AngleUnit.Degree), new Quantities.Angle(4.948935675287289E+01, Quantities.AngleUnit.Degree), new Quantities.Angle(2.866702002890120E+02, Quantities.AngleUnit.Degree), new Quantities.Angle(3.270334666858926E+02, Quantities.AngleUnit.Degree));

      public static readonly double TheObliquityOfTheEclipticInDegrees = 23.4;

      private readonly Quantities.OrbitalEccentricity m_eccentricity;
      private readonly Quantities.Length m_semiMajorAxis;
      private readonly Quantities.Angle m_inclination;
      private readonly Quantities.Angle m_longitudeOfAscendingNode;
      private readonly Quantities.Angle m_argumentOfPeriapsis;
      private readonly Quantities.Angle m_trueAnomaly;

      public KeplerianElements(Quantities.OrbitalEccentricity eccentricity, Quantities.Length semiMajorAxis, Quantities.Angle inclination, Quantities.Angle longitudeOfAscendingNode, Quantities.Angle argumentOfPeriapsis, Quantities.Angle trueAnomaly)
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
      public Quantities.OrbitalEccentricity Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
      /// <summary>
      /// <para>The longest diameter of an ellipse.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Semi-major_and_semi-minor_axes"/></para>
      /// </summary>
      public Quantities.Length SemiMajorAxis { get => m_semiMajorAxis; init => m_semiMajorAxis = value; }
      /// <summary>
      /// <para>The angle between the orbital plane and the reference plane. Inclination is the angle between the orbital plane and the equatorial plane. By convention, inclination is in the range [0, 180] degrees, i.e. [0, PI] radians.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Orbital_inclination"/></para>
      /// </summary>
      public Quantities.Angle Inclination { get => m_inclination; init => m_inclination = value; }
      /// <summary>
      /// <para>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node) By convention, this is a number in the range [0, 360] degrees, i.e. [0, 2PI] radians.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Longitude_of_the_ascending_node"/></para>
      /// </summary>
      public Quantities.Angle LongitudeOfAscendingNode { get => m_longitudeOfAscendingNode; init => m_longitudeOfAscendingNode = value; }
      /// <summary>
      /// <para>The angle between the ascending node and the periapsis. By convention, this is an angle in the range [0, 360] degrees, i.e. [0, 2PI].</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Argument_of_periapsis"/></para>
      /// </summary>
      public Quantities.Angle ArgumentOfPeriapsis { get => m_argumentOfPeriapsis; init => m_argumentOfPeriapsis = value; }
      /// <summary>
      /// <para>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/True_anomaly"/></para>
      /// </summary>
      public Quantities.Angle TrueAnomaly { get => m_trueAnomaly; init => m_trueAnomaly = value; }

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
        => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

      public static void ToRotationMatrix(double longitudeOfAscendingNode, double inclination, double argumentOfPeriapsis, out double x1, out double x2, out double x3, out double y1, out double y2, out double y3, out double z1, out double z2, out double z3)
      {
        var (so, co) = System.Math.SinCos(longitudeOfAscendingNode);
        var (si, ci) = System.Math.SinCos(inclination);
        var (sw, cw) = System.Math.SinCos(argumentOfPeriapsis);

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

      //  var alpha = System.Math.Atan2(-x.Y, z.X);
      //  var beta = System.Math.Atan2(z.Z, System.Math.Sqrt(z.X * z.X + z.Y * z.Y));
      //  var gamma = System.Math.Atan2(y.Z, x.Z);

      //  return new(alpha, beta, gamma);
      //}

#pragma warning disable IDE0060 // Remove unused parameter
      public static void ToOrbitalElements(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3, out double longitudeOfAscendingNode, out double inclination, out double argumentOfPeriapsis)
#pragma warning restore IDE0060 // Remove unused parameter
      {
        longitudeOfAscendingNode = System.Math.Atan2(-x2, z1);
        inclination = System.Math.Atan2(z3, System.Math.Sqrt(z1 * z1 + z2 * z2));
        argumentOfPeriapsis = System.Math.Atan2(y3, x3);
      }
      #endregion Static methods

      public override string ToString()
        => $"{GetType().Name} {{ Eccentricity = {m_eccentricity.Value} ({m_eccentricity.GetOrbitalEccentricityClass()}), SemiMajorAxis = {m_semiMajorAxis}, Inclination = {m_inclination.ToUnitValueSymbolString(Quantities.AngleUnit.Degree)}, LongitudeOfAscendingNode = {m_longitudeOfAscendingNode.ToUnitValueSymbolString(Quantities.AngleUnit.Degree)}, ArgumentOfPeriapsis = {m_argumentOfPeriapsis.ToUnitValueSymbolString(Quantities.AngleUnit.Degree)}, TrueAnomaly = {m_trueAnomaly.ToUnitValueSymbolString(Quantities.AngleUnit.Degree)} }}";
    }
  }
}
