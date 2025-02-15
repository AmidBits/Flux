namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Geometry.CoordinateSystems.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
      => Geometry.CoordinateSystems.PolarCoordinate.FromCartesianCoordinates(source.X, source.Y);
  }
}
