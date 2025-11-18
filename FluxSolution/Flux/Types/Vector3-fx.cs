namespace Flux
{
  public static partial class Vector3Extensions
  {
    #region 3D vector (non-collection) computations

    public static float AbsoluteSum(this System.Numerics.Vector3 source)
      => float.Abs(source.X) + float.Abs(source.Y) + float.Abs(source.Z);

    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector3 source, System.Numerics.Vector3 before, System.Numerics.Vector3 after)
      => AngleTo(before - source, after - source);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
      => double.Acos(double.Clamp(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(source), System.Numerics.Vector3.Normalize(target)), -1, 1));
    //    //{
    //    //  var cross = System.Numerics.Vector3.Cross(source, target);

    //    //  return System.Math.Atan2(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(cross), cross), System.Numerics.Vector3.Dot(source, target));
    //    //}

    //    //public static double AngleToAxisX(this System.Numerics.Vector3 source)
    //    //  => System.Math.Atan2(System.Math.Sqrt(source.Y * source.Y + source.Z * source.Z), source.X);
    //    //public static double AngleToAxisY(this System.Numerics.Vector3 source)
    //    //  => System.Math.Atan2(System.Math.Sqrt(source.Z * source.Z + source.X * source.X), source.Y);
    //    //public static double AngleToAxisZ(this System.Numerics.Vector3 source)
    //    //  => System.Math.Atan2(System.Math.Sqrt(source.X * source.X + source.Y * source.Y), source.Z);

    /// <summary>Compute the Chebyshev length of a vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static float ChebyshevLength(this System.Numerics.Vector3 source, float edgeLength = 1)
      => float.Max(float.Max(source.X, source.Y), source.Z) / edgeLength;

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static float DotProductEx(this System.Numerics.Vector3 a, System.Numerics.Vector3 b)
      => (float)(System.Numerics.Vector3.Dot(a, b) / double.Sqrt(a.LengthSquared() * b.LengthSquared()));

    public static int GetOctant(this System.Numerics.Vector3 source, System.Numerics.Vector3 center)
      => source.X < center.X is var xneg && source.Y < center.Y is var yneg && source.Z < center.Z ? (yneg ? (xneg ? 5 : 4) : (xneg ? 6 : 7)) : (yneg ? (xneg ? 2 : 3) : (xneg ? 1 : 0));

    public static int GetOctantPositiveAs1(this System.Numerics.Vector3 source, System.Numerics.Vector3 center)
      => (source.X > center.X ? 1 : 0) + (source.Y > center.Y ? 2 : 0) + (source.Z > center.Z ? 4 : 0);

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified <paramref name="center"/> and orthant <paramref name="numbering"/>.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    public static int GetOrthantNumber(this System.Numerics.Vector3 source, System.Numerics.Vector3 center, Geometry.OrthantNumbering numbering)
      => numbering switch
      {
        Geometry.OrthantNumbering.Traditional => source.GetOctant(center),
        Geometry.OrthantNumbering.BinaryNegativeAs1 => (source.X < center.X ? 1 : 0) + (source.Y < center.Y ? 2 : 0) + (source.Z < center.Z ? 4 : 0),
        Geometry.OrthantNumbering.BinaryPositiveAs1 => source.GetOctantPositiveAs1(center),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static float ManhattanLength(this System.Numerics.Vector3 source, float edgeLength = 1)
      => (float.Abs(source.X) + float.Abs(source.Y) + float.Abs(source.Z)) / edgeLength;

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static System.Numerics.Vector3 Nlerp(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float mu)
      => System.Numerics.Vector3.Normalize(System.Numerics.Vector3.Lerp(source, target, mu));

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalised, and does not normalise the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static System.Numerics.Vector3 Orthogonal(this System.Numerics.Vector3 source)
      => double.Abs(source.X) > double.Abs(source.Z) ? new System.Numerics.Vector3(-source.Y, source.X, 0) : new System.Numerics.Vector3(0, -source.Z, source.Y);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector3 RotateAroundAxis(this System.Numerics.Vector3 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector3 RotateAroundWorldAxes(this System.Numerics.Vector3 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector3.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Create a new scalar by computing the scalar triple product, i.e. the dot product of one of the vectors with the cross product of the other two.</summary>
    /// <remarks>This is the signed volume of the parallelepiped defined by the three vectors given.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static float ScalarTripleProduct(System.Numerics.Vector3 a, System.Numerics.Vector3 b, System.Numerics.Vector3 c)
      => System.Numerics.Vector3.Dot(a, System.Numerics.Vector3.Cross(b, c));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static System.Numerics.Vector3 Slerp(this System.Numerics.Vector3 source, System.Numerics.Vector3 target, float percent = 0.5f)
    {
      var dot = float.Clamp(System.Numerics.Vector3.Dot(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = float.Acos(dot) * percent; // Angle between start and desired.
      var relative = System.Numerics.Vector3.Normalize(target - source * dot);
      return source * float.Cos(theta) + relative * float.Sin(theta);
    }

    /// <summary>Returns a quaternion from two vectors.
    /// <para><see href="http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final"/></para>
    /// <para><see href="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/></para>
    /// </summary>
    public static System.Numerics.Quaternion ToQuaternion(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
    {
      var norm_u_norm_v = double.Sqrt(System.Numerics.Vector3.Dot(source, source) * System.Numerics.Vector3.Dot(target, target));
      var real_part = (float)norm_u_norm_v + System.Numerics.Vector3.Dot(source, target);

      System.Numerics.Vector3 w;

      if (real_part < 1E-7 * norm_u_norm_v)
      {
        real_part = 0;

        // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
        w = double.Abs(source.X) > double.Abs(source.Z) ? new System.Numerics.Vector3(-source.Y, source.X, 0) : new System.Numerics.Vector3(0, -source.Z, source.Y);
      }
      else
      {
        w = System.Numerics.Vector3.Cross(source, target);
      }

      return System.Numerics.Quaternion.Normalize(new System.Numerics.Quaternion(w.X, w.Y, w.Z, real_part));
    }

    /// <summary>Create a new vector by computing the vector triple product (Lagrange's formula), i.e. the cross product of one vector with the cross product of the other two.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static System.Numerics.Vector3 VectorTripleProduct(System.Numerics.Vector3 a, System.Numerics.Vector3 b, System.Numerics.Vector3 c)
      => System.Numerics.Vector3.Cross(a, System.Numerics.Vector3.Cross(b, c));

    #endregion // 3D vector (non-collection) computations

    #region 3D vector collection (shape) algorithms

    //    /// <summary>Creates a new sequence with the midpoints added in-between the vertices in the sequence.</summary>
    //    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> AddMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    //    {
    //      if (source is null) throw new System.ArgumentNullException(nameof(source));

    //      using var ev = source.GetEnumerator();
    //      using var em = source.GetMidpoints().GetEnumerator();

    //      while (ev.MoveNext() && em.MoveNext())
    //      {
    //        yield return ev.Current;
    //        yield return em.Current;
    //      }
    //    }

    public static double AngleSum(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, System.Numerics.Vector3 vector)
      => source.PartitionTuple2(true, (v1, v2, i) => AngleBetween(vector, v1, v2)).Sum();

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise. (2D/3D)</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e1.X * e2.Y - e2.X * e1.Y), (a, i) => a / 2);
    //   => source.PartitionTuple2(true, (e1, e2, i) => e1.X * e2.Y - e2.X * e1.Y).Sum() / 2;

    /// <summary>Compute the surface area of the polygon. (2D/3D)</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => double.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static System.Numerics.Vector3 ComputeCentroid(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.Aggregate(System.Numerics.Vector3.Zero, (a, e, index) => a + e, (a, count) => a / count);

    ///// <summary>Compute the surface normal of the polygon, which is simply the cross product of three vertices (as in a subtriangle of the polygon). (2D/3D)</summary>
    ////  Modified from http://www.fullonsoftware.co.uk/snippets/content/Math_-_Calculating_Face_Normals.pdf
    //public static System.Numerics.Vector3 ComputeNormal(this System.Collections.Generic.IReadOnlyList<System.Numerics.Vector3> source)
    //  => source is null ? throw new System.ArgumentNullException(nameof(source)) : (source.Count >= 3 ? System.Numerics.Vector3.Cross(source[1] - source[0], source[2] - source[0]) : throw new System.ArgumentOutOfRangeException(nameof(source)));

    /// <summary>Compute the perimeter length of the polygon. (2D/3D)</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple2(true, (e1, e2, i) => (e2 - e1).Length()).Sum();
    //=> source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e2 - e1).Length(), (a, i) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 v1, System.Numerics.Vector3 v2, System.Numerics.Vector3 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> GetMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple2(true, (e1, e2, index) => (e2 + e1) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector3 v1, System.Numerics.Vector3 vm, System.Numerics.Vector3 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => source.PartitionTuple2(true, (e1, e2, index) => (e1, (e2 + e1) / 2, e2, index));

    public static bool InsidePolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, System.Numerics.Vector3 vector)
      => double.Abs(AngleSum(source, vector)) > 1;

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
      System.ArgumentNullException.ThrowIfNull(source);

      using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

      if (e.MoveNext())
      {
        var initialAngle = e.Current;

        while (e.MoveNext())
          if (initialAngle != e.Current)
            return false;
      }

      return true;
    }

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).Length()).GetEnumerator();

      if (e.MoveNext())
      {
        var initialLength = e.Current;

        while (e.MoveNext())
          if (initialLength != e.Current)
            return false;
      }

      return true;
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, Geometry.TriangulationType mode, System.Random? rng = null)
    {
      const double halfPi = double.Pi / 2;

      var copy = source.ToList();

      (System.Numerics.Vector3 v1, System.Numerics.Vector3 v2, System.Numerics.Vector3 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).First(),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).Random(rng),
          Geometry.TriangulationType.SmallestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => GetAnglesEx(copy).Aggregate((a, b) => double.Abs(a.angle - halfPi) > double.Abs(b.angle - halfPi) ? a : b),
          Geometry.TriangulationType.MostSquare => GetAnglesEx(copy).Aggregate((a, b) => double.Abs(a.angle - halfPi) < double.Abs(b.angle - halfPi) ? a : b),
          _ => throw new System.Exception(),
        };

        yield return new System.Collections.Generic.List<System.Numerics.Vector3>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 5 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a sequence of triangles from the centroid to all vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 3 and 10 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector3>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitInHalf(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitVertexToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector3>> SplitVertexToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source, int index)
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

    #endregion // 3D vector collection (shape) algorithms
  }
}
