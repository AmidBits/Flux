namespace Flux
{
  public static partial class Maths
  {
    public static double MaxRange(double initialVelocityMps, double initialAngleRad, double gravitationalPull = EarthNullGravityMeterPerSecond)
      => System.Math.Sin(2 * initialAngleRad) * initialVelocityMps * initialVelocityMps / gravitationalPull;
    public static double MaxHeight(double initialVelocityMps, double initialAngleRad, double gravitationalPull = EarthNullGravityMeterPerSecond)
      => System.Math.Pow(System.Math.Sin(initialAngleRad), 2) * initialVelocityMps * initialVelocityMps / (2 * gravitationalPull);
  }
}
