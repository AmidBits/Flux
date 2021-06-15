namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
      => inDegrees ? Flux.Media.Units.Angle.ConvertRadiansToDegrees(Flux.Media.Units.Angle.ConvertCartesianToRotationAngleEx(point.X, point.Y)) : Flux.Media.Units.Angle.ConvertCartesianToRotationAngleEx(point.X, point.Y);
    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, bool inDegrees = false)
    {
      Flux.Media.Units.Angle.ConvertRotationAngleToCartesianEx(inDegrees ? Flux.Media.Units.Angle.ConvertDegreesToRadians(angularRotation) : angularRotation, out var x, out var y);

      return new System.Windows.Point(x, y);
    }
  }
}
