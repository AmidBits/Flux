namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
    {
      var angle = Flux.CoordinateSystems.CartesianCoordinate2.ConvertToRotationAngleEx(point.X, point.Y);

      return inDegrees ? Quantity.Angle.ConvertRadianToDegree(angle) : angle;
    }

    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, bool inDegrees = false)
    {
      var (x, y) = Quantity.Angle.ConvertRotationAngleToCartesian2Ex(inDegrees ? Quantity.Angle.ConvertDegreeToRadian(angularRotation) : angularRotation);

      return new System.Windows.Point(x, y);
    }
  }
}