//namespace Flux.Mechanics
//{
//  public interface ITrajectory2D
//  {
//    /// <summary>Gravitational acceleration in meters per second square (M/S²).</summary>
//    double GravitationalAcceleration { get; }
//    /// <summary>Initial angle in radians (RAD).</summary>
//    double InitialAngle { get; }
//    /// <summary>Initial velocity in meters per second (M/S).</summary>
//    double InitialVelocity { get; }

//    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
//    double MaxHeight { get; }
//    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
//    double MaxRange { get; }
//    /// <summary>Yields the greatest time the object travels before impact.</summary>
//    double MaxTime { get; }

//    double GetVelocity(double time);
//    double GetVelocityX(double time);
//    double GetVelocityY(double time);
//    double GetX(double time);
//    double GetY(double time);
//  }

//  //https://en.wikipedia.org/wiki/Trajectory
//  //https://en.wikipedia.org/wiki/Ballistics
//  //https://en.wikipedia.org/wiki/Projectile_motion

//  //public class Trajectory
//  //{
//  //  /// <summary>
//  //  /// <para>The "angle of reach" is the angle at which a projectile must be launched in order to go a <paramref name="d"/>, given the <paramref name="v0"/>.</para>
//  //  /// </summary>
//  //  /// <param name="d"></param>
//  //  /// <param name="v0"></param>
//  //  /// <param name="g"></param>
//  //  /// <returns></returns>
//  //  public static (double shallowAngle, double steepAngle) AngleOfReach(double d, double v0, double g = Flux.Quantities.Acceleration.StandardGravity)
//  //  {
//  //    var gd = g * d;
//  //    var v02 = v0 * v0;

//  //    var shallowTrajectory = 0.5 * double.Asin(gd / v02);
//  //    var steepTrajectory = (double.Pi / 4) + 0.5 * double.Acos(gd / v02);

//  //    return (shallowTrajectory, steepTrajectory);
//  //  }

//  //  /// <summary>
//  //  /// <para>To hit a target at range x and altitude y when fired from (0,0) and with initial speed v the required angle(s) of launch if found with this.</para>
//  //  /// </summary>
//  //  /// <param name="x"></param>
//  //  /// <param name="y"></param>
//  //  /// <param name="distance"></param>
//  //  /// <param name="initialVelocity"></param>
//  //  /// <param name="gravitationalAcceleration"></param>
//  //  /// <returns></returns>
//  //  public static (double shallowAngle, double steepAngle) AngleRequiredToHitPoint(double x, double y, double initialVelocity, double gravitationalAcceleration = Flux.Quantities.Acceleration.StandardGravity)
//  //  {
//  //    var v2 = initialVelocity * initialVelocity;

//  //    var gx = gravitationalAcceleration * x;

//  //    var root = double.Sqrt(double.Abs(v2 * v2 - gravitationalAcceleration * (gx * x + 2 * y * v2)));

//  //    var shallowTrajectory = double.Atan((v2 - root) / gx);
//  //    var steepTrajectory = double.Atan((v2 + root) / gx);

//  //    return (shallowTrajectory, steepTrajectory);
//  //  }

//  //  //public static double RangeOfFlight(double a0, double v0, double h0 = 0, double g = Flux.Quantities.Acceleration.StandardGravity)
//  //  //  => v0 * double.Cos(a0) / g * ((v0 * double.Sin(a0)) + double.Sqrt((v0 * v0 * double.Sin(a0) * double.Sin(a0)) + (2 * g * h0)));

//  //  /// <summary>
//  //  /// <para>Maximum distance, unit of <see cref="Quantities.Length"/>.</para>
//  //  /// </summary>
//  //  /// <param name="initialHeight"></param>
//  //  /// <param name="initialAngle"></param>
//  //  /// <param name="initialVelocity"></param>
//  //  /// <param name="gravitationalAcceleration"></param>
//  //  /// <param name="maximumHeight"></param>
//  //  /// <param name="maximumTime"></param>
//  //  /// <returns></returns>
//  //  public static Quantities.Length MaximumDistance(Quantities.Length initialHeight, Quantities.Angle initialAngle, Quantities.Speed initialVelocity, Quantities.Acceleration gravitationalAcceleration, out Quantities.Length maximumHeight, out Quantities.Time maximumTime)
//  //  {
//  //    maximumTime = MaximumTime(initialHeight, initialAngle, initialVelocity, gravitationalAcceleration, out maximumHeight);

//  //    return new(initialVelocity.Value * double.Cos(initialAngle.Value) * maximumTime.Value);
//  //  }

//  //  /// <summary>
//  //  /// <para>Maximum height, unit of <see cref="Quantities.Length"/>.</para>
//  //  /// </summary>
//  //  /// <param name="initialHeight"></param>
//  //  /// <param name="initialAngle"></param>
//  //  /// <param name="initialVelocity"></param>
//  //  /// <param name="gravitationalAcceleration"></param>
//  //  /// <returns></returns>
//  //  public static Quantities.Length MaximumHeight(Quantities.Length initialHeight, Quantities.Angle initialAngle, Quantities.Speed initialVelocity, Quantities.Acceleration gravitationalAcceleration)
//  //  {
//  //    var maxHeight = double.Pow(initialVelocity.Value, 2) * double.Pow(double.Sin(initialAngle.Value), 2) / (2 * double.Abs(gravitationalAcceleration.Value));

//  //    if (double.IsPositive(initialHeight.Value))
//  //      return initialHeight + maxHeight;

//  //    return new(maxHeight);
//  //  }

//  //  /// <summary>
//  //  /// <para>Maximum time, unit of <see cref="Quantities.Time"/>.</para>
//  //  /// </summary>
//  //  /// <param name="initialHeight"></param>
//  //  /// <param name="initialAngle"></param>
//  //  /// <param name="initialVelocity"></param>
//  //  /// <param name="gravitationalAcceleration"></param>
//  //  /// <param name="maximumHeight"></param>
//  //  /// <returns></returns>
//  //  public static Quantities.Time MaximumTime(Quantities.Length initialHeight, Quantities.Angle initialAngle, Quantities.Speed initialVelocity, Quantities.Acceleration gravitationalAcceleration, out Quantities.Length maximumHeight)
//  //  {
//  //    maximumHeight = MaximumHeight(initialHeight, initialAngle, initialVelocity, gravitationalAcceleration);

//  //    if (double.IsNegative(initialHeight.Value))
//  //      maximumHeight -= initialHeight;

//  //    var absg = double.Abs(gravitationalAcceleration.Value);

//  //    return new(initialVelocity.Value * double.Sin(initialAngle.Value) / absg + double.Sqrt(2 * maximumHeight.Value / absg));
//  //  }
//  //}
//}

////namespace Flux.Mechanics
////{
////  public interface ITrajectory2D
////  {
////    /// <summary>Gravitational acceleration.</summary>
////    Units.Acceleration GravitationalAcceleration { get; init; }
////    /// <summary>Initial angle.</summary>
////    Units.Angle InitialAngle { get; init; }
////    /// <summary>Initial velocity.</summary>
////    Units.LinearVelocity InitialVelocity { get; init; }

////    /// <summary>Yields the greatest parabolic height an object reaches within the trajectory</summary>
////    Units.Length MaxHeight { get; }
////    /// <summary>Yields the greatest distance the object travels along the x-axis.</summary>
////    Units.Length MaxRange { get; }
////    /// <summary>Yields the greatest time the object travels before impact.</summary>
////    Units.Time MaxTime { get; }

////    Units.LinearVelocity GetVelocity(Units.Time time);
////    Units.LinearVelocity GetVelocityX(Units.Time time);
////    Units.LinearVelocity GetVelocityY(Units.Time time);
////    Units.Length GetX(Units.Time time);
////    Units.Length GetY(Units.Time time);
////  }
////}
