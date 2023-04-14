namespace Flux.Numerics.MapProjections
{
  // https://en.wikipedia.org/wiki/Natural_Earth_projection
  public readonly record struct NaturalEarthProjection
    : IMapForwardProjectable<double>
  {
    public static readonly NaturalEarthProjection Default;

    //#pragma warning disable CA1822 // Mark members as static
    public Numerics.CartesianCoordinate3<double> ProjectForward(Numerics.IGeographicCoordinate<double> project)
    {
      var lat = Units.Angle.ConvertDegreeToRadian(project.Latitude);
      var lon = Units.Angle.ConvertDegreeToRadian(project.Longitude);

      var latP2 = System.Math.Pow(lat, 2);
      var latP4 = latP2 * latP2;
      var latP6 = System.Math.Pow(lat, 6);
      var latP8 = latP4 * latP4;
      var latP10 = System.Math.Pow(lat, 10);
      var latP12 = latP6 * latP6;

      var x = lon * (0.870700 - 0.131979 * latP2 - 0.013791 * latP4 + 0.003971 * latP10 - 0.001529 * latP12);
      var y = lat * (1.007226 + 0.015085 * latP2 - 0.044475 * latP6 + 0.028874 * latP8 - 0.005916 * latP10);

      return new(x, y, project.Altitude);
    }
    //#pragma warning restore CA1822 // Mark members as static
  }

}
