namespace Flux
{
  public static partial class ExtensionMethods
  {
    //public static readonly Vector3I[] HexagonDiagonals = new Vector3I[] { new Vector3I(2, -1, -1), new Vector3I(1, -2, 1), new Vector3I(-1, -1, 2), new Vector3I(-2, 1, 1), new Vector3I(-1, 2, -1), new Vector3I(1, 1, -2) };
    //public static readonly Vector3I[] HexagonDirections = new Vector3I[] { new Vector3I(1, 0, -1), new Vector3I(1, -1, 0), new Vector3I(0, -1, 1), new Vector3I(-1, 0, 1), new Vector3I(-1, 1, 0), new Vector3I(0, 1, -1) };

    ///// <summary>Computes a point on the line between the source and the target by amount, where 0 is the source (any number less than 0 would be before the source), and 1 is the target (any number above 1 would be after the target).</summary>
    ///// <param name="source"></param>
    ///// <param name="target"></param>
    ///// <param name="amount"></param>
    ///// <returns></returns>
    //public static System.Numerics.Vector3 HexagonCubeLerp(this Vector3I source, Vector3I target, double amount)
    //  => System.Numerics.Vector3.Lerp(new System.Numerics.Vector3(source.X, source.Y, source.Z), new System.Numerics.Vector3(target.X, target.Y, target.Z), (float)amount);
    ///// <summary>Ensures that computed hex cube coordinates are x+y+z=0 and that midpoint hex points gets rounded to one side if in the middle.</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/#rounding"/>
    //public static System.Numerics.Vector3 AsHexagonCubeRound(this System.Numerics.Vector3 cube)
    //{
    //  var rx = System.Math.Round(cube.X);
    //  var ry = System.Math.Round(cube.Y);
    //  var rz = System.Math.Round(cube.Z);

    //  var x_diff = System.Math.Abs(rx - cube.X);
    //  var y_diff = System.Math.Abs(ry - cube.Y);
    //  var z_diff = System.Math.Abs(rz - cube.Z);

    //  if (x_diff > y_diff && x_diff > z_diff) rx = -ry - rz;
    //  else if (y_diff > z_diff) ry = -rx - rz;
    //  else rz = -rx - ry;

    //  return new System.Numerics.Vector3((float)rx, (float)ry, (float)rz);
    //}
    //public static Vector3I HexagonDiagonalNeighbor(this Vector3I source, int direction)
    //  => source + HexagonDiagonals[direction];
    //public static System.Collections.Generic.IEnumerable<Vector3I> HexagonDiagonalNeighbors(this Vector3I source)
    //  => HexagonDiagonals.Select(diagonal => source + diagonal);
    ///// <summary>Compute the distance between the source and the target.</summary>
    //public static int HexagonDistanceTo(this Vector3I source, Vector3I target)
    //  => (source - target).HexagonLength();
    ///// <summary>Compute the length of the hexagon, relative to 0,0,0.</summary>
    //public static int HexagonLength(this Vector3I source)
    //  => (int)((System.Math.Abs(source.X) + System.Math.Abs(source.Y) + System.Math.Abs(source.Z)) / 2.0);
    //public static Vector3I HexagonNeighbor(this Vector3I source, int direction)
    //  => source + HexagonDirections[direction];
    //public static System.Collections.Generic.IEnumerable<Vector3I> HexagonNeighbors(this Vector3I source)
    //  => HexagonDirections.Select(direction => source + direction);
    //public static Vector3I HexagonRotateLeft(this Vector3I source)
    //  => new Vector3I(-source.Z, -source.X, -source.Y);
    //public static Vector3I HexagonRotateRight(this Vector3I source)
    //  => new Vector3I(-source.Y, -source.Z, -source.X);

    ///// <summary>Converts an arbitrary raw ungridded position into a hexagon cube coordinate.</summary>
    ///// <see cref="http://www-cs-students.stanford.edu/~amitp/Articles/Hexagon2.html"/>
    //public static Vector3I ToHexagonCubeCoordinate(this System.Numerics.Vector3 source)
    //{
    //  var rx = System.Math.Round(source.X);
    //  var ry = System.Math.Round(source.Y);
    //  var rz = System.Math.Round(source.Z);

    //  var ix = (int)rx;
    //  var iy = (int)ry;
    //  var iz = (int)rz;

    //  if (ix + iy + iz is var s)
    //  {
    //    var ax = System.Math.Abs(rx - source.X);
    //    var ay = System.Math.Abs(ry - source.Y);
    //    var az = System.Math.Abs(rz - source.Z);

    //    if (ax >= ay && ax >= az) ix -= s; // ax is max
    //    else if (ay >= ax && ay >= az) iy -= s; // ay is max
    //    else iz -= s; // az is max
    //  }

    //  return new Vector3I(ix, iy, iz);
    //}

    ///// <summary>Converts a hexagon cube coordinate to a hexagon axial coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToAxial(this Vector3I source)
    //  => new Vector2I(source.X, source.Z);
    ///// <summary>Converts a hexagon axial coordinate to a hexagon cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonAxialToCube(this Vector2I source)
    //  => new Vector3I(source.X, -source.X - source.Y, source.Y);

    ///// <summary>Converts a hexagon double height coordinate to a cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonDoubleHeightToCube(this Vector2I source)
    //{
    //  var z = (source.Y - source.X) / 2;

    //  return new Vector3I(source.X, -source.X - z, z);
    //}
    ///// <summary>Converts a hexagon cube coordinate to a double height coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToDoubleHeight(this Vector3I source)
    //  => new Vector2I(source.X, 2 * source.Z + source.X);

    ///// <summary>Converts a hexagon double width coordinate to a cube coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector3I AsHexagonDoubleWidthToCube(this Vector2I source)
    //{
    //  var x = (source.X - source.Y) / 2;

    //  return new Vector3I(x, -x - source.Y, source.Y);
    //}
    ///// <summary>Converts a hexagon cube coordinate to a double width coordinate</summary>
    ///// <see cref="https://www.redblobgames.com/grids/hexagons/"/>
    //public static Vector2I AsHexagonCubeToDoubleWidth(this Vector3I source)
    //  => new Vector2I(2 * source.X + source.Z, source.Z);
  }
}
