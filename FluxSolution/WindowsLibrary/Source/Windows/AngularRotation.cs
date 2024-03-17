using Flux.Units;

namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
    {
      var (_, azimuth) = Flux.Geometry.Coordinates.PolarCoordinate.ConvertCartesian2ToPolarEx(point.X, point.Y);

      return inDegrees ? Units.Angle.ConvertRadianToDegree(azimuth) : azimuth;
    }

    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, double magnitude = 1, bool inDegrees = false)
    {
      var (x, y) = Flux.Geometry.Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(magnitude, inDegrees ? Units.Angle.ConvertDegreeToRadian(angularRotation) : angularRotation);

      return new System.Windows.Point(x, y);
    }
  }
}
