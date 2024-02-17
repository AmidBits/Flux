using Flux.Units;

namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Returns the angle, in radians, according to the laws of cosine. Zero is to the top.</summary>
    public static double PointToAngularRotation(this System.Windows.Point point, bool inDegrees = false)
    {
      var (radius, azimuth) = Flux.Geometry.PolarCoordinate.FromCartesian2Ex(point.X, point.Y);

      return inDegrees ? Units.Angle.ConvertRadianToDegree(azimuth) : azimuth;
    }

    /// <summary>Returns a unit point of the specified angle, in radians. Zero is to the top.</summary>
    public static System.Windows.Point AngularRotationToPoint(this double angularRotation, double magnitude = 1, bool inDegrees = false)
    {
      var (x, y) = new Flux.Geometry.PolarCoordinate(magnitude, inDegrees ? Units.Angle.ConvertDegreeToRadian(angularRotation) : angularRotation).ToCartesianCoordinate2Ex();

      return new System.Windows.Point(x, y);
    }
  }
}
