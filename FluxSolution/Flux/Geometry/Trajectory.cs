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
    #region Trajectory utilities

    /// <summary>
    /// <para>Compute the angle at which the projectile lands.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Range_of_a_projectile#Angle_of_impact"/></para>
    /// </summary>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="initialHeight"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double AngleOfImpact(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Flux.Units.Acceleration.StandardGravity)
    {
      var sin = double.Sin(initialAngle);

      return double.Sqrt(initialVelocity * initialVelocity * sin * sin + 2 * gravitationalAcceleration * initialHeight) / (initialVelocity * double.Cos(initialAngle));
    }

    /// <summary>
    /// <para>What is launch angle that allows the lowest possible launch velocity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)"/></para>
    /// </summary>
    /// <param name="targetX"></param>
    /// <param name="targetY"></param>
    /// <returns></returns>
    public static double AngleOfMinimalVelocity(double targetX, double targetY)
      => double.Atan(targetY / targetX + double.Sqrt(targetY * targetY / targetX * targetX + 1));

    /// <summary>
    /// <para>The "angle of reach" are the (initial) angle(s) at which a projectile must be launched, when fired from [0, 0], in order to go a horizontal distance <paramref name="targetX"/> (<paramref name="targetY"/> = 0), given the <paramref name="initialVelocity"/>.</para>
    /// <para>To hit a target at range (horizontal distance) <paramref name="targetX"/> and altitude (height) <paramref name="targetY"/> when fired from [0, 0] and with <paramref name="initialVelocity"/> the required initial angle(s) of launch if found with this.</para>
    /// <para>There are two solutions: shallow and steep trajectory.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Angle_of_reach"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)"/></para>
    /// </summary>
    /// <param name="targetX"></param>
    /// <param name="targetY"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static (double angleOfShallowTrajectory, double angleOfSteepTrajectory) AngleOfReach(double targetX, double targetY, double initialVelocity, double gravitationalAcceleration = Flux.Units.Acceleration.StandardGravity)
    {
      if (targetY == 0)
      {
        var sin2aor = (gravitationalAcceleration * targetX) / (initialVelocity * initialVelocity); // Sin(2 * angle-of-reach)

        return (
          double.Asin(sin2aor) / 2,
          double.Pi / 4 + double.Acos(sin2aor) / 2
        );
      }
      else
      {
        var v2 = initialVelocity * initialVelocity;

        var gx = gravitationalAcceleration * targetX;

        var root = double.Sqrt(double.Abs(v2 * v2 - gravitationalAcceleration * (gx * targetX + 2 * targetY * v2)));

        return (
          double.Atan((v2 - root) / gx),
          double.Atan((v2 + root) / gx)
        );
      }
    }

    /// <summary>
    /// <para>Yields, at any <paramref name="time"/>, the projectile's horizontal displacement.</para>
    /// </summary>
    /// <param name="time"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <returns></returns>
    public static double DisplacementHorizontal(double time, double initialAngle, double initialVelocity)
      => initialVelocity * time * double.Cos(initialAngle);

    /// <summary>
    /// <para>The time to reach a target for the horizontal velocity.</para>
    /// <para>The horizontal and vertical velocity of a projectile are independent of each other.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Time_of_flight_to_the_target's_position"/></para>
    /// </summary>
    /// <param name="targetX"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <returns></returns>
    public static double DisplacementHorizontalTime(double targetX, double initialAngle, double initialVelocity)
      => targetX / (initialVelocity * double.Cos(initialAngle));

    /// <summary>
    /// <para>Yields, at any <paramref name="time"/>, the projectile's vertical displacement.</para>
    /// </summary>
    /// <param name="time"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double DisplacementVertical(double time, double initialAngle, double initialVelocity, double gravitationalAcceleration = Flux.Units.Acceleration.StandardGravity)
      => initialVelocity * time * double.Sin(initialAngle) - 0.5 * gravitationalAcceleration * time * time;

    /// <summary>
    /// <para>The length of the parabolic arc traced by a projectile, given that the height of launch and landing is the same (air resistance ignored).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Total_Path_Length_of_the_Trajectory"/></para>
    /// </summary>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double LengthOfParabolicArc(double initialAngle, double initialVelocity, double gravitationalAcceleration = Flux.Units.Acceleration.StandardGravity)
    {
      var sin = double.Sin(initialAngle);
      var cos = double.Cos(initialAngle);

      return (initialVelocity * initialVelocity / gravitationalAcceleration) * (sin + cos * cos * double.Atanh(sin));
    }

    #endregion // Trajectory utilities

    #region Planetary trajectories

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Projectile_motion_on_a_planetary_scale"/></para>
    /// </summary>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <param name="earthRadius"></param>
    /// <returns></returns>
    public static double PlanetaryTrajectoryHeight(double initialAngle, double initialVelocity, double gravitationalAcceleration = Units.Acceleration.StandardGravity, double earthRadius = Geometry.Geodesy.EllipsoidReference.EarthMeanRadius)
    {
      var (sin, cos) = double.SinCos(initialAngle);

      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      return (initialVelocity * initialVelocity * sin * sin / gravitationalAcceleration) / (1 - vrpow2 + double.Sqrt(1 - (2 - vrpow2) * vrpow2 * cos * cos));
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Projectile_motion_on_a_planetary_scale"/></para>
    /// </summary>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <param name="earthRadius"></param>
    /// <returns></returns>
    public static double PlanetaryTrajectoryRange(double initialAngle, double initialVelocity, double gravitationalAcceleration = Units.Acceleration.StandardGravity, double earthRadius = Geometry.Geodesy.EllipsoidReference.EarthMeanRadius)
    {
      var sin = double.Sin(initialAngle);

      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      return initialVelocity * initialVelocity * double.Sin(2 * initialAngle) / gravitationalAcceleration / double.Sqrt(1 - (2 - vrpow2) * vrpow2 * sin * sin);
    }

    /// <summary>
    /// <para>The total time for which the projectile remains in the air is called the time of flight.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Projectile_motion_on_a_planetary_scale"/></para>
    /// </summary>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <param name="earthRadius"></param>
    /// <returns></returns>
    public static double PlanetaryTrajectoryTime(double initialAngle, double initialVelocity, double gravitationalAcceleration = Units.Acceleration.StandardGravity, double earthRadius = Geometry.Geodesy.EllipsoidReference.EarthMeanRadius)
    {
      var (sin, cos) = double.SinCos(initialAngle);

      var vr = initialVelocity / double.Sqrt(earthRadius * gravitationalAcceleration);

      var vrpow2 = vr * vr;

      var s2vrpow2 = 2 - vrpow2;

      var sqrts2vrpow2vrsin = double.Sqrt(s2vrpow2) * vr * sin;

      return (2 * initialVelocity * sin / gravitationalAcceleration) * (1 / s2vrpow2) * (1 + 1 / sqrts2vrpow2vrsin * double.Asin(sqrts2vrpow2vrsin / double.Sqrt(1 - s2vrpow2 * vrpow2 * cos * cos)));
    }

    #endregion // Planetary trajectories

    #region Trajectories

    /// <summary>
    /// <para>The greatest height that the object will reach is known as the peak of the object's motion.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Maximum_height_of_projectile"/></para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double TrajectoryHeight(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Units.Acceleration.StandardGravity)
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
    public static double TrajectoryRange(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Units.Acceleration.StandardGravity)
    {
      var v0sin = initialVelocity * double.Sin(initialAngle);

      return (initialVelocity * double.Cos(initialAngle) / gravitationalAcceleration) * (v0sin + double.Sqrt(v0sin * v0sin + (2 * gravitationalAcceleration * initialHeight)));
    }

    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Projectile_motion#Time_of_flight_or_total_time_of_the_whole_journey"/></para>
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialAngle"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravitationalAcceleration"></param>
    /// <returns></returns>
    public static double TrajectoryTime(double initialAngle, double initialVelocity, double initialHeight = 0, double gravitationalAcceleration = Units.Acceleration.StandardGravity)
    {
      var v0sin = initialVelocity * double.Sin(initialAngle);

      return (v0sin + double.Sqrt(v0sin * v0sin + 2 * gravitationalAcceleration * initialHeight)) / gravitationalAcceleration;
    }

    #endregion // Trajectories
  }
}
