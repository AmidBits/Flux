namespace Flux.Mechanics
{
  /// <summary>
  /// <para><see href="https://en.wikipedia.org/wiki/Ballistics"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Trajectory"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Range_of_a_projectile"/></para>
  /// </summary>
  public static partial class Trajectory
  {
    /// <summary>
    /// <para>The "angle of reach" is the angle at which a projectile must be launched in order to go a <paramref name="d"/>, given the <paramref name="v0"/>.</para>
    /// </summary>
    /// <param name="d"></param>
    /// <param name="v0"></param>
    /// <param name="g"></param>
    /// <returns></returns>
    public static (double shallowAngle, double steepAngle) AngleOfReach(double d, double v0, double g = Flux.Quantities.Acceleration.StandardGravity)
    {
      var gd = g * d;
      var v02 = v0 * v0;

      var shallowTrajectory = 0.5 * double.Asin(gd / v02);
      var steepTrajectory = (double.Pi / 4) + 0.5 * double.Acos(gd / v02);

      return (shallowTrajectory, steepTrajectory);
    }

    /// <summary>
    /// <para>To hit a target at range x and altitude y when fired from (0,0) and with initial speed v the required angle(s) of launch if found with this.</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="distance"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static (double shallowAngle, double steepAngle) AngleRequiredToHitPoint(double x, double y, double initialVelocity, double gravitationalAcceleration = Flux.Quantities.Acceleration.StandardGravity)
    {
      var v2 = initialVelocity * initialVelocity;

      var gx = gravitationalAcceleration * x;

      var root = double.Sqrt(double.Abs(v2 * v2 - gravitationalAcceleration * (gx * x + 2 * y * v2)));

      var shallowTrajectory = double.Atan((v2 - root) / gx);
      var steepTrajectory = double.Atan((v2 + root) / gx);

      return (shallowTrajectory, steepTrajectory);
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Maximum_height_of_projectile"/></para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double GetTrajectoryHeight(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      var sin = double.Sin(initialAngle);

      var maxHeight = (initialVelocity * initialVelocity * sin * sin) / (2 * gravitationalAcceleration);

      return double.IsPositive(initialHeight) ? initialHeight + maxHeight : maxHeight;
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Range_of_a_projectile"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Projectile_motion#Maximum_distance_of_projectile"/></para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double GetTrajectoryRange(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      var (sin, cos) = double.SinCos(initialAngle);

      return initialVelocity * cos / gravitationalAcceleration * ((initialVelocity * sin) + double.Sqrt(double.Pow(initialVelocity * sin, 2) + (2 * gravitationalAcceleration * initialHeight)));
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Time_of_flight_or_total_time_of_the_whole_journey"/></para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double GetTrajectoryTime(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      var sin = double.Sin(initialAngle);

      var v0sin = initialVelocity * sin;

      return (v0sin + double.Sqrt(v0sin * v0sin + 2 * gravitationalAcceleration * initialHeight)) / gravitationalAcceleration;
    }

    /// <summary>
    /// <para>Maximum height, unit of <see cref="Quantities.Length"/>.</para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double TrajectoryHeight(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      var maxHeight = initialVelocity * initialVelocity * double.Pow(double.Sin(initialAngle), 2) / (2 * double.Abs(gravitationalAcceleration));

      if (double.IsPositive(initialHeight))
        return initialHeight + maxHeight;

      return maxHeight;
    }

    /// <summary>
    /// <para>Maximum distance, unit of <see cref="Quantities.Length"/>.</para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <param name="trajectoryHeight"></param>
    /// <param name="trajectoryTime"></param>
    /// <returns></returns>
    public static double TrajectoryRange(double initialAngle, double initialVelocity, out double trajectoryHeight, out double trajectoryTime, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      trajectoryTime = TrajectoryTime(initialAngle, initialVelocity, out trajectoryHeight, initialHeight, gravitationalAcceleration);

      return initialVelocity * double.Cos(initialAngle) * trajectoryTime;
    }

    /// <summary>
    /// <para>Maximum time, unit of <see cref="Quantities.Time"/>.</para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <param name="trajectoryHeight"></param>
    /// <returns></returns>
    public static double TrajectoryTime(double initialAngle, double initialVelocity, out double trajectoryHeight, double initialHeight = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity)
    {
      trajectoryHeight = TrajectoryHeight(initialAngle, initialVelocity, initialHeight, gravitationalAcceleration);

      if (double.IsNegative(initialHeight))
        trajectoryHeight -= initialHeight;

      var absg = double.Abs(gravitationalAcceleration);

      return initialVelocity * double.Sin(initialAngle) / absg + double.Sqrt(2 * trajectoryHeight / absg);
    }

    public static double PlanetaryTrajectoryHeight(double initialVelocity, double initialAngle = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity, double earthRadius = EllipsoidReference.EarthMeanRadius)
    {
      var (sin, cos) = double.SinCos(initialAngle);

      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      return (initialVelocity * initialVelocity * sin * sin / gravitationalAcceleration) / (1 - vrpow2 + double.Sqrt(1 - (2 - vrpow2) * vrpow2 * cos * cos));
    }

    public static double PlanetaryTrajectoryRange(double initialVelocity, double initialAngle = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity, double earthRadius = EllipsoidReference.EarthMeanRadius)
    {
      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      return double.Pow(initialVelocity, 2) * double.Sin(2 * initialAngle) / gravitationalAcceleration / double.Sqrt(1 - (2 - vrpow2) * vrpow2 * double.Pow(double.Sin(initialAngle), 2));
    }

    public static double PlanetaryTrajectoryTime(double initialVelocity, double initialAngle = 0, double gravitationalAcceleration = Quantities.Acceleration.StandardGravity, double earthRadius = EllipsoidReference.EarthMeanRadius)
    {
      var (sin, cos) = double.SinCos(initialAngle);

      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      return (2 * initialVelocity * sin / gravitationalAcceleration) * (1 / (2 - vrpow2)) * (1 + 1 / (double.Sqrt(2 - vrpow2) * vr * sin) * double.Asin((double.Sqrt(2 - vrpow2) * vr * sin) / double.Sqrt(1 - (2 - vrpow2) * vrpow2 * cos * cos)));
    }
  }
}

//namespace Flux.Mechanics
//{
//  public interface ITrajectory2D
//  {
//    /// <summary>Gravitational acceleration.</summary>
//    Units.Acceleration GravitationalAcceleration { get; init; }
//    /// <summary>Initial angle.</summary>
//    Units.Angle InitialAngle { get; init; }
//    /// <summary>Initial velocity.</summary>
//    Units.LinearVelocity InitialVelocity { get; init; }

//    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
//    Units.Length MaxHeight { get; }
//    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
//    Units.Length MaxRange { get; }
//    /// <summary>Yields the greatest time the object travels before impact.</summary>
//    Units.Time MaxTime { get; }

//    Units.LinearVelocity GetVelocity(Units.Time time);
//    Units.LinearVelocity GetVelocityX(Units.Time time);
//    Units.LinearVelocity GetVelocityY(Units.Time time);
//    Units.Length GetX(Units.Time time);
//    Units.Length GetY(Units.Time time);
//  }
//}
