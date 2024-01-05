namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
    {
      var angle = Flux.Units.Angle.Cartesian2ToRotationAngleEx(point.X, point.Y);

      return inDegrees ? Units.Angle.RadianToDegree(angle) : angle;
    }

    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, bool inDegrees = false)
    {
      var (x, y) = Flux.Units.Angle.RotationAngleToCartesian2Ex(inDegrees ? Units.Angle.DegreeToRadian(angularRotation) : angularRotation);

      return new System.Windows.Point(x, y);
    }
  }
}
