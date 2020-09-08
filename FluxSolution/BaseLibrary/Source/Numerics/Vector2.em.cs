namespace Flux
{
  public static partial class XtendNumerics
  {
    /// <summary>This is basically LERP with the the ability to compute an arbitrary point anywhere on the path from a to b (including before a and after b). The result, when the specified scalar is, <0 is a vector beyond a (backwards), 0 is vector a, 0.5 equals the midpoint vector between a and b, 1 is vector b, and >1 equals a vector beyond b (forward).</summary>>
    public static System.Numerics.Vector2 AlongPathTo(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float scalar = 0.5f)
      => (source + target) * scalar;

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector2 source, System.Numerics.Vector2 target)
      => System.Math.Acos(Flux.Maths.Clamp(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(source), System.Numerics.Vector2.Normalize(target)), -1, 1));

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b, float edgeLength = 1)
      => System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength);

    public static double EuclideanDistanceSquaredTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
    public static double EuclideanDistanceTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this in System.Numerics.Vector2 a, in System.Numerics.Vector2 b, float edgeLength = 1)
      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength;

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCcw(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector2(-source.Y, source.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCw(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector2(source.Y, -source.X);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector2 RotateAroundAxis(this System.Numerics.Vector2 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector2 RotateAroundWorldAxes(this System.Numerics.Vector2 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this System.Numerics.Vector2 source, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    public static System.Numerics.Vector3 ToVector3(this System.Numerics.Vector2 source)
      => new System.Numerics.Vector3(source, 0);

    // /// <summary>Returns a new set of triangular polygons from the specified polygon. The first vertex (in clockwise order) having an angle greater or equal to 0 degrees and less than 180 degrees, to its neighbors are extracted first. The remaining vertices are then evaluated from the beginning again. This process continues until only 3 vertices are left, which are also returned as the last triangle.</summary>
    // public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitByTriangulation(this System.Collections.Generic.IList<System.Numerics.Vector2> source)
    // {
    //   var copy = source.ToList();

    //   while (copy.Count > 3)
    //   {
    //     for (int i = 0; i < copy.Count; i++)
    //     {
    //       var vi = copy[i];
    //       var vm = copy[i == 0 ? copy.Count - 1 : i - 1];
    //       var vp = copy[i == copy.Count - 1 ? 0 : i + 1];

    //       if (System.Math.Acos(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(vm - vi), System.Numerics.Vector2.Normalize(vp - vi))) is double angle && angle > 0 && angle < System.Math.PI)
    //       {
    //         yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { vi, vp, vm };

    //         copy.RemoveAt(i);

    //         break;
    //       }
    //     }
    //   }

    //   yield return copy;
    // }
    // /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    // /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    // public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitInHalf(this System.Collections.Generic.IList<System.Numerics.Vector2> source)
    // {
    //   var halfCount = source.Count / 2;
    //   var halfCountPlus1 = halfCount + 1;

    //   if ((source.Count & 0b1) == 0)
    //   {
    //     yield return source.Take(halfCountPlus1).ToList();
    //     yield return source.Skip(halfCount).Append(source[0]).ToList();
    //   }
    //   else
    //   {
    //     var midway = (source[halfCount] + source[halfCountPlus1]) / 2;

    //     yield return source.Take(halfCountPlus1).Append(midway).ToList();
    //     yield return source.Skip(halfCountPlus1).Append(source[0]).Prepend(midway).ToList();
    //   }
    // }
    // /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    // /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    // //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToMidpoints(this System.Collections.Generic.IList<System.Numerics.Vector2> source)
    // //{
    // //  var c = source.SurfaceCentroid();

    // //  var mps = source.GetMidpoints().ToList();

    // //  for (int i = 0; i < source.Count; i++)
    // //  {
    // //    yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { c, mps[i == 0 ? mps.Count - 1 : i - 1], source[i], mps[i] };
    // //  }
    // //}
    // /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    // /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    // public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToVertices(this System.Collections.Generic.IList<System.Numerics.Vector2> source)
    // {
    //   var c = source.SurfaceCentroid();

    //   for (int i = 0; i < source.Count; i++)
    //   {
    //     yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { c, source[i], source[(i + 1) % source.Count] };
    //   }
    // }
    // /// <summary>Returns a new set of triangles from any polygon point to all other points. Creates a triangle fan from the specified point.</summary>
    // public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitVertexToVertices(this System.Collections.Generic.IList<System.Numerics.Vector2> source, int index)
    // {
    //   if (index < 0 || index >= source.Count - 1)
    //   {
    //     throw new System.ArgumentOutOfRangeException(nameof(index));
    //   }

    //   for (int i = index + 1; i < (index + source.Count) - 1; i++)
    //   {
    //     yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { source[index], source.ElementAt(i % source.Count), source.ElementAt((i + 1) % source.Count) };
    //   }
    // }

    // public static string ToStringEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source) => string.Concat(source.Select(v => string.Format("<{0:N3}, {1:N3}>", v.X, v.Y)));
  }
}
