//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>The maximum range for a trajectory.</summary>
//    /// <param name="initialVelocityMps">Initial velocity in meters per seconds.</param>
//    /// <param name="initialAngleRad">Initial angle in radians.</param>
//    /// <param name="gravitationalPull">Gravitational pull.</param>
//    public static double MaxRange(double initialVelocityMps, double initialAngleRad, double gravitationalPull = EarthNullGravityMeterPerSecond)
//      => System.Math.Sin(2 * initialAngleRad) * initialVelocityMps * initialVelocityMps / gravitationalPull;
//    /// <summary>The maximum height for a trajectory.</summary>
//    /// <param name="initialVelocityMps">Initial velocity in meters per seconds.</param>
//    /// <param name="initialAngleRad">Initial angle in radians.</param>
//    /// <param name="gravitationalPull">Gravitational pull.</param>
//    public static double MaxHeight(double initialVelocityMps, double initialAngleRad, double gravitationalPull = EarthNullGravityMeterPerSecond)
//      => System.Math.Pow(System.Math.Sin(initialAngleRad), 2) * initialVelocityMps * initialVelocityMps / (2 * gravitationalPull);
//  }
//}
