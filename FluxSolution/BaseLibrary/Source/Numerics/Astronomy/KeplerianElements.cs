namespace Flux
{
  public static partial class ExtensionMethods
  {

  }

  namespace Numerics
  {
    /// <summary>Kepler elements for computing orbits.
    /// <para><see href="https://en.wikipedia.org/wiki/Orbital_elements"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Kepler%27s_laws_of_planetary_motion"/></para>
    /// <para><see href="https://www.amsat.org/keplerian-elements-tutorial/"/></para>
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct KeplerianElements<TSelf>
     where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      public static readonly TSelf TheObliquityOfTheEclipticInDegrees = TSelf.CreateChecked(23.4);

      private readonly TSelf m_eccentricity;
      private readonly TSelf m_semiMajorAxis;
      private readonly TSelf m_radInclination; // Stored internally as radians.
      private readonly TSelf m_radLongitudeOfAscendingNode; // Stored internally as radians.
      private readonly TSelf m_radArgumentOfPeriapsis; // Stored internally as radians.
      private readonly TSelf m_trueAnomaly;

      public KeplerianElements(TSelf semiMajorAxis, TSelf eccentricity, TSelf inclination, TSelf longitudeOfAscendingNode, TSelf argumentOfPeriapsis, TSelf trueAnomaly)
      {
        m_semiMajorAxis = semiMajorAxis;
        m_eccentricity = eccentricity;
        m_radInclination = inclination;
        m_radLongitudeOfAscendingNode = longitudeOfAscendingNode;
        m_radArgumentOfPeriapsis = argumentOfPeriapsis;
        m_trueAnomaly = trueAnomaly;
      }

      /// <summary>The longest diameter of an ellipse.</summary>
      public TSelf SemiMajorAxis { get => m_semiMajorAxis; init => m_semiMajorAxis = value; }
      /// <summary>The amount by which an orbit around another body deviates from a perfect circle.</summary>
      public TSelf Eccentricity { get => m_eccentricity; init => m_eccentricity = value; }
      /// <summary>The angle between the orbital plane and the reference plane. Inclination is the angle between the orbital plane and the equatorial plane. By convention, inclination is in the range [0, 180] degrees, i.e. [0, PI] radians.</summary>
      public TSelf Inclination { get => TSelf.CreateChecked(Units.Angle.ConvertRadianToDegree(double.CreateChecked(m_radInclination))); init => m_radInclination = TSelf.CreateChecked(Units.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
      /// <summary>The angle between the reference direction and the upward crossing of the orbit on the reference plane (the ascending node) By convention, this is a number in the range [0, 360] degrees, i.e. [0, 2PI] radians.</summary>
      public TSelf LongitudeOfAscendingNode { get => TSelf.CreateChecked(Units.Angle.ConvertRadianToDegree(double.CreateChecked(m_radLongitudeOfAscendingNode))); init => m_radLongitudeOfAscendingNode = TSelf.CreateChecked(Units.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
      /// <summary>The angle between the ascending node and the periapsis. By convention, this is an angle in the range [0, 360] degrees, i.e. [0, 2PI].</summary>
      public TSelf ArgumentOfPeriapsis { get => TSelf.CreateChecked(Units.Angle.ConvertRadianToDegree(double.CreateChecked(m_radArgumentOfPeriapsis))); init => m_radArgumentOfPeriapsis = TSelf.CreateChecked(Units.Angle.ConvertDegreeToRadian(double.CreateChecked(value))); }
      /// <summary>The position of the orbiting body along the trajectory, measured from periapsis. Several alternate values can be used instead of true anomaly, the most common being M the mean anomaly and T, the time since periapsis.</summary>
      public TSelf TrueAnomaly { get => m_trueAnomaly; init => m_trueAnomaly = value; }

      public Numerics.Matrix4<TSelf> ToMatrix4()
      {
        ToRotationMatrix(m_radLongitudeOfAscendingNode, m_radInclination, m_radArgumentOfPeriapsis, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

        return new(
          x1, x2, x3, TSelf.Zero,
          y1, y2, y3, TSelf.Zero,
          z1, z2, z3, TSelf.Zero,
          TSelf.Zero, TSelf.Zero, TSelf.Zero, TSelf.One
        );
      }


      public System.Numerics.Matrix4x4 ToMatrix4x4()
      {
        ToRotationMatrix(m_radLongitudeOfAscendingNode, m_radInclination, m_radArgumentOfPeriapsis, out var x1, out var x2, out var x3, out var y1, out var y2, out var y3, out var z1, out var z2, out var z3);

        return new System.Numerics.Matrix4x4
        (
          float.CreateChecked(x1), float.CreateChecked(x2), float.CreateChecked(x3), 0,
          float.CreateChecked(y1), float.CreateChecked(y2), float.CreateChecked(y3), 0,
          float.CreateChecked(z1), float.CreateChecked(z2), float.CreateChecked(z3), 0,
          0, 0, 0, 1
        );
      }

      #region Static methods

      public static double ComputeProportionalityConstant(double gravitionalConstant, double massOfSun, double massOfPlanet)
        => System.Math.Pow(4 * System.Math.PI, 2) / (gravitionalConstant * (massOfSun + massOfPlanet));

      public static void ToRotationMatrix(TSelf longitudeOfAscendingNode, TSelf inclination, TSelf argumentOfPeriapsis, out TSelf x1, out TSelf x2, out TSelf x3, out TSelf y1, out TSelf y2, out TSelf y3, out TSelf z1, out TSelf z2, out TSelf z3)
      {
        var (so, co) = TSelf.SinCos(longitudeOfAscendingNode);
        var (si, ci) = TSelf.SinCos(inclination);
        var (sw, cw) = TSelf.SinCos(argumentOfPeriapsis);

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

      public static Numerics.EulerAngles<TSelf> ToEulerAngles(Numerics.CartesianCoordinate3<TSelf> x, Numerics.CartesianCoordinate3<TSelf> y, Numerics.CartesianCoordinate3<TSelf> z)
      {
        x = x.Normalized();
        y = y.Normalized();
        z = z.Normalized();

        var alpha = TSelf.Atan2(-x.Y, z.X);
        var beta = TSelf.Atan2(z.Z, TSelf.Sqrt(z.X * z.X + z.Y * z.Y));
        var gamma = TSelf.Atan2(y.Z, x.Z);

        return new(alpha, beta, gamma);
      }

#pragma warning disable IDE0060 // Remove unused parameter
      public static void ToOrbitalElements(TSelf x1, TSelf x2, TSelf x3, TSelf y1, TSelf y2, TSelf y3, TSelf z1, TSelf z2, TSelf z3, out TSelf longitudeOfAscendingNode, out TSelf inclination, out TSelf argumentOfPeriapsis)
#pragma warning restore IDE0060 // Remove unused parameter
      {
        longitudeOfAscendingNode = TSelf.Atan2(-x2, z1);
        inclination = TSelf.Atan2(z3, TSelf.Sqrt(z1 * z1 + z2 * z2));
        argumentOfPeriapsis = TSelf.Atan2(y3, x3);
      }
      #endregion Static methods

      #region Object overrides

      public override string ToString()
        => $"{GetType().Name} {{ SemiMajorAxis = {m_semiMajorAxis}, Eccentricity = {m_eccentricity}, Inclination = {new Units.Angle(double.CreateChecked(m_radInclination)).ToUnitString(Units.AngleUnit.Degree)}, LongitudeOfAscendingNode = {new Units.Angle(double.CreateChecked(m_radLongitudeOfAscendingNode)).ToUnitString(Units.AngleUnit.Degree)}, ArgumentOfPeriapsis = {new Units.Angle(double.CreateChecked(m_radArgumentOfPeriapsis)).ToUnitString(Units.AngleUnit.Degree)}, TrueAnomaly = {m_trueAnomaly} }}";
      #endregion Object overrides
    }
  }
}
