namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
      => inDegrees ? Flux.Maths.RadianToDegree(Flux.Maths.CartesianToRotationAngleEx(point.X, point.Y)) : Flux.Maths.CartesianToRotationAngleEx(point.X, point.Y);
    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, bool inDegrees = false)
    {
      var (x, y) = inDegrees ? Flux.Maths.RotationAngleToCartesianEx(Flux.Maths.DegreeToRadian(angularRotation)) : Flux.Maths.RotationAngleToCartesianEx(angularRotation);

      return new System.Windows.Point(x, y);
    }
  }
}
