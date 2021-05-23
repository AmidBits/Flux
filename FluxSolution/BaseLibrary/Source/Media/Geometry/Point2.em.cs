namespace Flux
{
  public static partial class GeometryEm
  {
    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Media.Geometry.Point2> GetQuadrantCenterVectors(this Media.Geometry.Point2 source, int subQuadrantSizeOfX, int subQuadrantSizeOfY)
    {
      yield return new Media.Geometry.Point2(source.X + subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
      yield return new Media.Geometry.Point2(source.X - subQuadrantSizeOfX, source.Y + subQuadrantSizeOfY);
      yield return new Media.Geometry.Point2(source.X - subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
      yield return new Media.Geometry.Point2(source.X + subQuadrantSizeOfX, source.Y - subQuadrantSizeOfY);
    }
    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int ToQuadrant(this Media.Geometry.Point2 source, in Media.Geometry.Point2 centerAxis) => ((source.X >= centerAxis.X ? 1 : 0) * 1) + ((source.Y >= centerAxis.Y ? 1 : 0) * 2);

    /// <summary>Convert a point to a 2D vector.</summary>
    public static System.Numerics.Vector2 ToVector2(this in Media.Geometry.Point2 source)
      => new System.Numerics.Vector2(source.X, source.Y);
    /// <summary>Convert a point to a 3D vector.</summary>
    public static System.Numerics.Vector3 ToVector3(this in Media.Geometry.Point2 source)
      => new System.Numerics.Vector3(source.X, source.Y, 0);
  }
}
