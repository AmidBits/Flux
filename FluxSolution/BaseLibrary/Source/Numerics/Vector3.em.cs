using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new sequence with the midpoints added in-between the vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> AddMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var ev = source.GetEnumerator();
      using var em = source.GetMidpoints().GetEnumerator();

      while (ev.MoveNext() && em.MoveNext())
      {
        yield return ev.Current;
        yield return em.Current;
      }
    }

    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector3 source, System.Numerics.Vector3 before, System.Numerics.Vector3 after)
      => (before - source).AngleTo(after - source);

    public static double AngleSum(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, System.Numerics.Vector3 vector)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + AngleBetween(vector, v1, v2), (a, i) => a);
    public static double AngleSumPB(this System.Collections.Generic.IList<System.Numerics.Vector3> source, System.Numerics.Vector3 vector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var anglesum = 0d;

      var count = source.Count;

      for (var i = 0; i < count; i++)
      {
        var p1 = new System.Numerics.Vector3(source[i].X - vector.X, source[i].Y - vector.Y, source[i].Z - vector.Z);
        var p2 = new System.Numerics.Vector3(source[(i + 1) % count].X - vector.X, source[(i + 1) % count].Y - vector.Y, source[(i + 1) % count].Z - vector.Z);

        var m1m2 = p1.Length() * p2.Length();

        if (m1m2 <= float.Epsilon) return Maths.PiX2; // We are on a node, consider this inside.

        anglesum += System.Math.Acos((p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z) / m1m2); // Add up all acos(costheta) angles.
      }

      return anglesum;
    }

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
      => Maths.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength, (b.Z - a.Z) / edgeLength);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise. (2D/3D)</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IReadOnlyList<System.Numerics.Vector3> source)
      => source.AggregateTuple2(0d, true, (a, va, vb, i) => a + ((va.X * vb.Y - vb.X * va.Y) / 2), (a, i) => a);
    /// <summary>Compute the surface area of the polygon. (2D/3D)</summary>
    public static double ComputeArea(this System.Collections.Generic.IReadOnlyList<System.Numerics.Vector3> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static System.Numerics.Vector3 ComputeCentroid(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.Aggregate(System.Numerics.Vector3.Zero, (acc, vector, index) => acc + vector, (acc, count) => acc / count);

    /// <summary>Compute the surface normal of the polygon, which is simply the cross product of three vertices (as in a subtriangle of the polygon). (2D/3D)</summary>
    //  Modified from http://www.fullonsoftware.co.uk/snippets/content/Math_-_Calculating_Face_Normals.pdf
    public static System.Numerics.Vector3 ComputeNormal(this System.Collections.Generic.IReadOnlyList<System.Numerics.Vector3> source)
      => source is null ? throw new System.ArgumentNullException(nameof(source)) : (source.Count >= 3 ? System.Numerics.Vector3.Cross(source[1] - source[0], source[2] - source[0]) : throw new System.ArgumentOutOfRangeException(nameof(source)));

    /// <summary>Compute the perimeter length of the polygon. (2D/3D)</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + (v2 - v1).Length(), (a, i) => a);

    public static double EuclideanDistanceSquaredTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z);
    public static double EuclideanDistanceTo(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 v1, System.Numerics.Vector3 v2, System.Numerics.Vector3 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> GetMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple2(source, true, (v1, v2, index) => (v2 + v1) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 v1, System.Numerics.Vector3 vm, System.Numerics.Vector3 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => PartitionTuple2(source, true, (v1, v2, index) => (v1, (v2 + v1) / 2, v2, index));

    public static bool InsidePolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, System.Numerics.Vector3 vector)
      => System.Math.Abs(AngleSum(source, vector)) > 1;

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
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

      if (e.MoveNext())
      {
        var tolerance = Maths.Epsilon1E7;
        var angle1 = e.Current;

        while (e.MoveNext())
          if (!Maths.AreAlmostEqual(angle1, e.Current, tolerance))
            return false;
      }

      return true;
    }

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).Length()).GetEnumerator();

      if (e.MoveNext())
      {
        var tolerance = Maths.Epsilon1E7;
        var length1 = e.Current;

        while (e.MoveNext())
          if (!Maths.AreAlmostEqual(length1, e.Current, tolerance))
            return false;
      }

      return true;
    }

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

    /// <summary>Create a new scalar by computing the scalar triple product, i.e. the dot product of one of the vectors with the cross product of the other two.</summary>
    /// <remarks>This is the signed volume of the parallelepiped defined by the three vectors given.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(System.Numerics.Vector3 a, System.Numerics.Vector3 b, System.Numerics.Vector3 c)
      => System.Numerics.Vector3.Dot(a, System.Numerics.Vector3.Cross(b, c));

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

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (a, b, index) => (a, b)))
      {
        midpointPolygon.Add(pair.a.vm);

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { pair.a.v2, pair.b.vm, pair.a.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, Geometry.TriangulationType mode)
    {
      var copy = source.ToList();

      (System.Numerics.Vector3 v1, System.Numerics.Vector3 v2, System.Numerics.Vector3 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).First(),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(),
          Geometry.TriangulationType.SmallestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => GetAnglesEx(copy).Aggregate((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => GetAnglesEx(copy).Aggregate((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 5 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(GetMidpoints(source), true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, v1, v2 }) : throw new System.InvalidOperationException();
    /// <summary>Returns a sequence of triangles from the centroid to all vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 3 and 10 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(source, true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector3>> SplitInHalf(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      var polygon1 = new System.Collections.Generic.List<System.Numerics.Vector3>();
      var polygon2 = new System.Collections.Generic.List<System.Numerics.Vector3>();

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        if (polygon1.Count > polygon2.Count)
        {
          polygon2.Add(polygon1[0]);

          polygon1.RemoveAt(0);
        }

        polygon1.Add(item);
      }

      if (polygon2.Count < 3)
      {
        var midpoint = System.Numerics.Vector3.Lerp(polygon1[^1], polygon2[0], 0.5f);

        polygon1.Add(midpoint);
        polygon2.Insert(0, midpoint);
      }

      polygon1.Add(polygon2[0]);
      polygon2.Add(polygon1[0]);

      yield return polygon1;
      yield return polygon2;
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

      var vertex = angles[index].v2;

      var startIndex = index + 1;
      var count = startIndex + angles.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        var triplet = angles[i % angles.Count];
        var midpoint23 = System.Numerics.Vector3.Lerp(triplet.v2, triplet.v3, 0.5f);

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, triplet.v2, midpoint23 };
        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, midpoint23, triplet.v3 };
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

      var vertex = angles[index].v2;

      var startIndex = index + 1;
      var count = startIndex + angles.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { vertex, angles[i % angles.Count].v2, angles[(i + 1) % angles.Count].v2 };
      }
    }

    /// <summary>Convert a 3D vector to a point.</summary>
    public static System.Drawing.Point ToPoint(this in System.Numerics.Vector3 source)
      => new System.Drawing.Point((int)source.X, (int)source.Y);
    /// <summary>Convert a 3D vector to a 2D vector.</summary>
    public static System.Numerics.Vector2 ToVector2(this System.Numerics.Vector3 source)
      => new System.Numerics.Vector2(source.X, source.Y);

    /// <summary>Create a new vector by computing the vector triple product (Lagrange's formula), i.e. the cross product of one vector with the cross product of the other two.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static System.Numerics.Vector3 VectorTripleProduct(System.Numerics.Vector3 a, System.Numerics.Vector3 b, System.Numerics.Vector3 c)
      => System.Numerics.Vector3.Cross(a, System.Numerics.Vector3.Cross(b, c));

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
  }
}
