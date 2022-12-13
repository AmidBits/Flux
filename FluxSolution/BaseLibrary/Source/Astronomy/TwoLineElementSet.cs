namespace Flux
{
  /// <summary>Kepler elements for computing orbits.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Orbital_elements"/>
  /// <see cref="https://www.amsat.org/keplerian-elements-tutorial/"/>
  public readonly record struct TwoLineElementSet2<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    private readonly TSelf m_radInclination;
    private readonly TSelf m_radRightAscensionOfAscendingNode;
    private readonly TSelf m_eccentricity;
    private readonly TSelf m_radArgumentOfPerigee;
    private readonly TSelf m_radMeanAnomaly;
    private readonly TSelf m_meanMotion;
    private readonly TSelf m_revolutionNumberAtEpoch;

    public TwoLineElementSet2(TSelf radInclination, TSelf radRightAscensionOfAscendingNode, TSelf eccentricity, TSelf radArgumentOfPerigee, TSelf radMeanAnomaly, TSelf meanMotion, TSelf revolutionNumberAtEpoch)
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
    public TSelf Inclination { get => TSelf.CreateChecked(Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(m_radInclination))); init => m_radInclination = TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
    /// <summary>The angle between vernal equinox and the point where the orbit crosses the equatorial plane (going north). The value provided is the TEME mean right ascension of the ascending node. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
    public TSelf RightAscensionOfAscendingNode { get => TSelf.CreateChecked(Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(m_radRightAscensionOfAscendingNode))); init => m_radRightAscensionOfAscendingNode = TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
    /// <summary>A constant defining the shape of the orbit (0=circular, Less than 1=elliptical). The value provided is the mean eccentricity.</summary>
    public TSelf Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
    /// <summary>The angle between the ascending node and the orbit's point of closest approach to the earth (perigee). The value provided is the TEME mean argument of perigee. Degrees, in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
    public TSelf ArgumentOfPerigee { get => TSelf.CreateChecked(Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(m_radArgumentOfPerigee))); init => m_radArgumentOfPerigee = TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
    /// <summary>The angle, measured from perigee, of the satellite location in the orbit referenced to a circular orbit with radius equal to the semi-major axis. Degrees.</summary>
    public TSelf MeanAnomaly { get => TSelf.CreateChecked(Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(m_radMeanAnomaly))); init => m_radMeanAnomaly = TSelf.CreateChecked(Quantities.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
    /// <summary> The value is the mean number of orbits per day the object completes. There are 8 digits after the decimal, leaving no trailing space(s) when the following element exceeds 9999. Revolutions per day.</summary>
    public TSelf MeanMotion { get => m_meanMotion; init => m_meanMotion = value; }
    /// <summary>The orbit number at Epoch Time. This time is chosen very near the time of true ascending node passage as a matter of routine. Revolutions.</summary>
    public TSelf RevolutionNumberAtEpoch { get => m_revolutionNumberAtEpoch; init => m_revolutionNumberAtEpoch = value; }

    public Matrix4<TSelf> ToMatrix4()
    {
      var co = TSelf.Cos(m_radRightAscensionOfAscendingNode);
      var so = TSelf.Sin(m_radRightAscensionOfAscendingNode);
      var ci = TSelf.Cos(m_radInclination);
      var si = TSelf.Sin(m_radInclination);
      var cw = TSelf.Cos(m_radArgumentOfPerigee);
      var sw = TSelf.Sin(m_radArgumentOfPerigee);

      var x1 = co * cw - so * ci * sw;
      var x2 = so * cw + co * ci * sw;
      var x3 = si * sw;

      var y1 = -co * sw - so * ci * cw;
      var y2 = -so * sw + co * ci * cw;
      var y3 = si * cw;

      var z1 = si * so;
      var z2 = -si * co;
      var z3 = ci;

      return new(
        x1, x2, x3, TSelf.Zero,
        y1, y2, y3, TSelf.Zero,
        z1, z2, z3, TSelf.Zero,
        TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
      );
    }

    #region Static methods

    public static TSelf ComputeProportionalityConstant(TSelf gravitionalConstant, TSelf massOfSun, TSelf massOfPlanet)
      => TSelf.Pow(TSelf.CreateChecked(4) * TSelf.Pi, TSelf.CreateChecked(2)) / (gravitionalConstant * (massOfSun + massOfPlanet));

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

    #region Object overrides

    public override string ToString()
      => $"{GetType().Name} {{ Inclination = {new Quantities.Angle(double.CreateChecked(m_radInclination)).ToUnitString(Quantities.AngleUnit.Degree)}, RightAscensionOfAscendingNode = {new Quantities.Angle(double.CreateChecked(m_radRightAscensionOfAscendingNode)).ToUnitString(Quantities.AngleUnit.Degree)}, Eccentricity = {m_eccentricity}, ArgumentOfPerigee = {new Quantities.Angle(double.CreateChecked(m_radArgumentOfPerigee)).ToUnitString(Quantities.AngleUnit.Degree)}, MeanAnomaly = {new Quantities.Angle(double.CreateChecked(m_radMeanAnomaly)).ToUnitString(Quantities.AngleUnit.Degree)}, MeanMotion = {m_meanMotion} }}";
    #endregion Object overrides
  }
}
