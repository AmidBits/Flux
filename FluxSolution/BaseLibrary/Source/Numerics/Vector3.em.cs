using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector3 source, System.Numerics.Vector3 before, System.Numerics.Vector3 after)
      => (before - source).AngleTo(after - source);

    /// <summary>Calculate the angle between the source vector and the specified target vector. (2D/3D)
    /// when dot eq 0 then the vectors are perpendicular
    /// when dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction)
    /// when dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction)
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
      => System.Numerics.Vector3.Cross(source, target) is var cross ? System.Math.Atan2(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(cross), cross), System.Numerics.Vector3.Dot(source, target)) : throw new System.Exception();

    public static double AngleToAxisX(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.Y * source.Y + source.Z * source.Z), source.X);
    public static double AngleToAxisY(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.Z * source.Z + source.X * source.X), source.Y);
    public static double AngleToAxisZ(this System.Numerics.Vector3 source)
      => System.Math.Atan2(System.Math.Sqrt(source.X * source.X + source.Y * source.Y), source.Z);

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b, float edgeLength = 1)
      => System.Math.Max(System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength), (b.Z - a.Z) / edgeLength);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise. (2D/3D)</summary>
    public static float ComputeAreaSigned(this System.Collections.Generic.IList<System.Numerics.Vector3> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (leading.X * trailing.Y - trailing.X * leading.Y) / 2).Sum();
    /// <summary>Compute the surface area of the polygon. (2D/3D)</summary>
    public static float ComputeArea(this System.Collections.Generic.IList<System.Numerics.Vector3> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static System.Numerics.Vector3 ComputeCentroid(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.Aggregate(System.Numerics.Vector3.Zero, (acc, vector, index) => acc + vector, (acc, count) => acc / count);

    /// <summary>Compute the surface normal of the polygon, which is simply the cross product of three vertices (as in a subtriangle of the polygon). (2D/3D)</summary>
    //  Modified from http://www.fullonsoftware.co.uk/snippets/content/Math_-_Calculating_Face_Normals.pdf
    public static System.Numerics.Vector3 ComputeNormal(this System.Collections.Generic.IList<System.Numerics.Vector3> source)
      => source is null ? throw new System.ArgumentNullException(nameof(source)) : (source.Count >= 3 ? System.Numerics.Vector3.Cross(source[1] - source[0], source[2] - source[0]) : throw new System.ArgumentOutOfRangeException(nameof(source)));

    /// <summary>Compute the perimeter length of the polygon. (2D/3D)</summary>
    public static float ComputePerimeter(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (leading, trailing)).Sum(pair => (pair.trailing - pair.leading).Length());

    public static double EuclideanDistanceSquaredTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z);
    public static double EuclideanDistanceTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple(source, 2, (leading, midling, trailing, index) => AngleBetween(midling, leading, trailing));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple(source, 2, (leading, midling, trailing, index) => (leading, midling, trailing, index, AngleBetween(midling, leading, trailing)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> GetMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple(source, true, (leading, trailing, index) => (trailing + leading) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 leading, System.Numerics.Vector3 midpoint, System.Numerics.Vector3 trailing, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple(source, true, (leading, trailing, index) => (leading, (trailing + leading) / 2, trailing, index));

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      bool negative = false, positive = false;

      foreach (var angle in GetAngles(source))
      {
        if (angle < 0)
          negative = true;
        else
          positive = true;

        if (negative && positive)
          return false;
      }

      return negative ^ positive;
    }

    /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple(2, (leading, midling, trailing, index) => AngleBetween(midling, leading, trailing)).AllEqual(out _);

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple(true, (leading, trailing, index) => (trailing - leading).Length()).AllEqual(out _);

    public static System.Numerics.Vector3 LerpTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float percent = 0.5f)
      => System.Numerics.Vector3.Lerp(source, target, percent);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b, float edgeLength = 1)
      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength + System.Math.Abs(b.Z - a.Z) / edgeLength;

    public static System.Numerics.Vector3 NlerpTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float percent = 0.5f)
      => System.Numerics.Vector3.Normalize(System.Numerics.Vector3.Lerp(source, target, percent));

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalised, and does not normalise the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static System.Numerics.Vector3 Orthogonal(this System.Numerics.Vector3 source)
      => System.Math.Abs(source.X) > System.Math.Abs(source.Z) ? new System.Numerics.Vector3(-source.Y, source.X, 0) : new System.Numerics.Vector3(0, -source.Z, source.Y);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector3 RotateAroundAxis(this System.Numerics.Vector3 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector3 RotateAroundWorldAxes(this System.Numerics.Vector3 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static System.Numerics.Vector3 SlerpTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float percent = 0.5f)
    {
      var dot = System.Math.Clamp(System.Numerics.Vector3.Dot(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = System.MathF.Acos(dot) * percent; // Angle between start and desired.
      var relative = System.Numerics.Vector3.Normalize(target - source * dot);
      return source * System.MathF.Cos(theta) + relative * System.MathF.Sin(theta);
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<System.Numerics.Vector3>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple(true, (leading, trailing, index) => (leading, trailing)))
      {
        midpointPolygon.Add(pair.leading.midpoint);

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { pair.leading.trailing, pair.trailing.midpoint, pair.leading.midpoint };
      }

      yield return midpointPolygon;
    }

    public enum TriangulationType
    {
      Sequential,
      SmallestAngle,
      LargestAngle,
      MostSquare,
      LeastSquare,
      Randomized,
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, TriangulationType mode)
    {
      var copy = source.ToList();

      (System.Numerics.Vector3 leading, System.Numerics.Vector3 midling, System.Numerics.Vector3 trailing, int index) triplet = default;

      while (copy.Count >= 3)
      {
        switch (mode)
        {
          case TriangulationType.Sequential:
            triplet = copy.PartitionTuple(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).First();
            break;
          case TriangulationType.Randomized:
            triplet = copy.PartitionTuple(2, (leading, midling, trailing, i) => (leading, midling, trailing, i)).Skip(Random.NumberGenerator.Crypto.Next(copy.Count - 1)).First();
            break;
          case TriangulationType.SmallestAngle:
            var ascendingAngle = GetAnglesEx(copy).Aggregate((a, b) => a.angle < b.angle ? a : b);
            triplet = (ascendingAngle.Item1, ascendingAngle.Item2, ascendingAngle.Item3, ascendingAngle.index);
            break;
          case TriangulationType.LargestAngle:
            var descendingAngle = GetAnglesEx(copy).Aggregate((a, b) => a.angle > b.angle ? a : b);
            triplet = (descendingAngle.Item1, descendingAngle.Item2, descendingAngle.Item3, descendingAngle.index);
            break;
          case TriangulationType.LeastSquare:
            var leastSquare = GetAnglesEx(copy).Aggregate((System.Func<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
            triplet = (leastSquare.Item1, leastSquare.Item2, leastSquare.Item3, leastSquare.index);
            break;
          case TriangulationType.MostSquare:
            var mostSquare = GetAnglesEx(copy).Aggregate((System.Func<(System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle), (System.Numerics.Vector3, System.Numerics.Vector3, System.Numerics.Vector3, int index, double angle)>)((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b));
            triplet = (mostSquare.Item1, mostSquare.Item2, mostSquare.Item3, mostSquare.index);
            break;
          default:
            throw new System.Exception();
        }

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { triplet.midling, triplet.trailing, triplet.leading };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 5 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple(GetMidpoints(source), true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, leading, trailing }) : throw new System.InvalidOperationException();
    //=> source.ComputeCentroid() is System.Numerics.Vector3 sc ? GetMidpointsEx(source).PartitionTuple(true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { sc, leading.midpoint, leading.pair.Item2, trailing.midpoint }) : throw new System.InvalidOperationException();
    /// <summary>Returns a sequence of triangles from the centroid to all vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 3 and 10 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple(source, true, (leading, trailing, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, leading, trailing }) : throw new System.InvalidOperationException();

    /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitInHalf(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      var half1 = new System.Collections.Generic.List<System.Numerics.Vector3>();
      var half2 = new System.Collections.Generic.List<System.Numerics.Vector3>();

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (half1.Count > half2.Count)
        {
          half2.Add(half1[0]);

          half1.RemoveAt(0);
        }

        half1.Add(item);
      }

      if (half2.Count < 3)
      {
        var midpoint = System.Numerics.Vector3.Lerp(half1[half1.Count - 1], half2[0], 0.5f);

        half1.Add(midpoint);
        half2.Insert(0, midpoint);
      }

      half1.Add(half2[0]);
      half2.Add(half1[0]);

      yield return half1;
      yield return half2;
    }

    /// <summary>Returns a sequence of triangles from the specified polygon index to all midpoints, splitting all triangles at their midpoint along the polygon perimeter. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitVertexToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
    {
      var angles = GetAnglesEx(source).ToList();

      if (index < 0 || index > angles.Count - 1)
      {
        index = (index == -1) ? angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index : throw new System.ArgumentOutOfRangeException(nameof(index));
      }

      var vertex = angles[index].Item2;

      var startIndex = index + 1;
      var count = startIndex + angles.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        var triplet = angles[i % angles.Count];
        var midpoint23 = System.Numerics.Vector3.Lerp(triplet.Item2, triplet.Item3, 0.5f);

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, triplet.Item2, midpoint23 };
        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, midpoint23, triplet.Item3 };
      }
    }
    /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitVertexToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
    {
      var angles = GetAnglesEx(source).ToList();

      if (index < 0 || index > angles.Count - 1)
      {
        index = (index == -1) ? (angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index - 1 + angles.Count) % angles.Count : throw new System.ArgumentOutOfRangeException(nameof(index));
      }

      var vertex = angles[index].Item2;

      var startIndex = index + 1;
      var count = startIndex + angles.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, angles[i % angles.Count].Item2, angles[(i + 1) % angles.Count].Item2 };
      }
    }

    /// <summary>Convert a 3D vector to a point.</summary>
    public static System.Drawing.Point ToPoint(this in System.Numerics.Vector3 source)
      => new System.Drawing.Point((int)source.X, (int)source.Y);
    /// <summary>Convert a 3D vector to a 2D vector.</summary>
    public static System.Numerics.Vector2 ToVector2(this System.Numerics.Vector3 source)
      => new System.Numerics.Vector2(source.X, source.Y);

    /// <summary>Determines the inclusion of a point in the 3D planar polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside. (2D/3D)</summary>
    //public static int InsidePolygon(this System.Numerics.Vector3 source, System.Collections.Generic.IList<System.Numerics.Vector3> polygon)
    //{
    //  var normal = polygon.SurfaceNormal();

    //  var x = System.Math.Abs(normal.X);
    //  var y = System.Math.Abs(normal.Y);
    //  var z = System.Math.Abs(normal.Z);

    //  if (z > x && z > y)
    //  {
    //    return source.InsidePolygonXY(polygon);
    //  }

    //  if (x > y)
    //  {
    //    return source.InsidePolygonXY(polygon.Select(v => new System.Numerics.Vector3() { X = v.Y, Y = v.Z }).ToList());
    //  }
    //  else
    //  {
    //    return source.InsidePolygonXY(polygon.Select(v => new System.Numerics.Vector3() { X = v.X, Y = v.Z }).ToList());
    //  }
    //}
    /// <summary>Determines the inclusion of a point in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    //public static int InsidePolygonXY(this System.Numerics.Vector3 source, System.Collections.Generic.IList<System.Numerics.Vector3> polygon)
    //{
    //  int wn = 0;

    //  for (int i = 0; i < polygon.Count; i++)
    //  {
    //    var a = polygon[i];
    //    var b = (i == polygon.Count - 1 ? polygon[0] : polygon[i + 1]);

    //    if (a.Y <= source.Y)
    //    {
    //      if (b.Y > source.Y && source.SideTestXY(a, b) > 0)
    //      {
    //        wn++;
    //      }
    //    }
    //    else
    //    {
    //      if (b.Y <= source.Y && source.SideTestXY(a, b) < 0)
    //      {
    //        wn--;
    //      }
    //    }
    //  }

    //  return wn;
    //}
  }
}
