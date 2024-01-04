namespace Flux
{
  public static partial class Fx
  {
    #region 2D vector (non-collection) computations

    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this System.Numerics.Vector2 source, System.Numerics.Vector2 before, System.Numerics.Vector2 after)
      => AngleTo(before - source, after - source);

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this System.Numerics.Vector2 source, System.Numerics.Vector2 target)
      => System.Math.Acos(System.Math.Clamp(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(source), System.Numerics.Vector2.Normalize(target)), -1, 1));

    /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static float ChebyshevLength(this System.Numerics.Vector2 source, float edgeLength = 1)
      => System.Math.Max(System.Math.Abs(source.X / edgeLength), System.Math.Abs(source.Y / edgeLength));

    public static float LineSlopeX(this System.Numerics.Vector2 source)
      => System.MathF.CopySign(source.X / source.Y, source.X);
    public static float LineSlopeY(this System.Numerics.Vector2 source)
      => System.MathF.CopySign(source.Y / source.X, source.Y);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static float ManhattanLength(this System.Numerics.Vector2 source, float edgeLength = 1)
      => System.Math.Abs(source.X / edgeLength) + System.Math.Abs(source.Y / edgeLength);

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCcw(this System.Numerics.Vector2 source)
      => new(-source.Y, source.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static System.Numerics.Vector2 PerpendicularCw(this System.Numerics.Vector2 source)
      => new(source.Y, -source.X);

    /// <summary>Find the perpendicular distance from a point in a 2D plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    public static double PerpendicularDistance(this System.Numerics.Vector2 source, float a, float b, float c)
      => System.Math.Abs(a * source.X + b * source.Y + c) / System.Math.Sqrt(a * a + b * b);

    /// <summary>Perpendicular distance to the to the line.</summary>
    public static double PerpendicularDistanceToLine(this System.Numerics.Vector2 source, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
    {
      var bma = b - a;

      return (bma * (source - a)).Length() / bma.Length();
    }

    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    public static System.Numerics.Vector2 PerpendicularFoot(this System.Numerics.Vector2 source, float a, float b, float c)
      => -1 * (a * source.X + b * source.Y + c) / (a * a + b * b) * new System.Numerics.Vector2(a + source.X, b + source.Y);

    /// <summary>Rotate the vector around the specified axis.</summary>
    public static System.Numerics.Vector2 RotateAroundAxis(this System.Numerics.Vector2 source, System.Numerics.Vector3 axis, float angle)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    /// <summary>Rotate the vector around the world axes.</summary>
    public static System.Numerics.Vector2 RotateAroundWorldAxes(this System.Numerics.Vector2 source, float yaw, float pitch, float roll)
      => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static System.Numerics.Vector2 Slerp(this System.Numerics.Vector2 source, System.Numerics.Vector2 target, float percent = 0.5f)
    {
      var dot = System.Math.Clamp(System.Numerics.Vector2.Dot(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = System.MathF.Acos(dot) * percent; // Angle between start and desired.
      var relative = System.Numerics.Vector2.Normalize(target - source * dot);
      return source * System.MathF.Cos(theta) + relative * System.MathF.Sin(theta);
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this System.Numerics.Vector2 source, System.Numerics.Vector2 a, System.Numerics.Vector2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (source.Y - b.Y) * (a.X - b.X));

    //public static Geometry.Ellipse ToEllipse(this System.Numerics.Vector2 vector2)
    //  => new Geometry.Ellipse(System.Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y), System.Math.Atan2(vector2.Y, vector2.X));

    #endregion // 2D vector (non-collection) computations

    #region 2D vector collection (shape) algorithms

    //    /// <summary>Creates a new sequence with the midpoints added in-between the vertices in the sequence.</summary>
    //    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> AddMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
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

    public static double AngleSum(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source, System.Numerics.Vector2 vector)
      => source.PartitionTuple2(true, (v1, v2, i) => AngleBetween(vector, v1, v2)).Sum();

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple2(true, (e1, e2, i) => e1.X * e2.Y - e2.X * e1.Y).Sum() / 2;

    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static System.Numerics.Vector2 ComputeCentroid(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.Aggregate(System.Numerics.Vector2.Zero, (a, e, index) => a + e, (a, count) => a / count);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple2(true, (e1, e2, i) => (e2 - e1).Length()).Sum();

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> GetMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(System.Numerics.Vector2 v1, System.Numerics.Vector2 vm, System.Numerics.Vector2 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source, System.Func<double, double, bool> equalityPredicate)
    //=> source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).AllEqual(out _);
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

      if (e.MoveNext())
      {
        var initialAngle = e.Current;

        while (e.MoveNext())
          if (!equalityPredicate(initialAngle, e.Current))
            return false;
      }

      return true;
    }

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source, System.Func<float, float, bool> equalityPredicate)
    //=> source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).Length()).AllEqual(out _);
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).Length()).GetEnumerator();

      if (e.MoveNext())
      {
        var initialLength = e.Current;

        while (e.MoveNext())
          if (!equalityPredicate(initialLength, e.Current))
            return false;
      }

      return true;
    }

    /// <summary>Determines the inclusion of a vector in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int IsInsidePolygon(this System.Collections.Generic.IList<System.Numerics.Vector2> source, System.Numerics.Vector2 vector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      int wn = 0;

      for (int i = 0; i < source.Count; i++)
      {
        var a = source[i];
        var b = (i == source.Count - 1 ? source[0] : source[i + 1]);

        if (a.Y <= vector.Y)
        {
          if (b.Y > vector.Y && SideTest(vector, a, b) > 0)
          {
            wn++;
          }
        }
        else
        {
          if (b.Y <= vector.Y && SideTest(vector, a, b) < 0)
          {
            wn--;
          }
        }
      }

      return wn;
    }

    /// <summary>Determines whether the specified polygons A and B intersect.</summary>
    public static bool IsIntersectingPolygon(System.Collections.Generic.IList<System.Numerics.Vector2> a, System.Collections.Generic.IList<System.Numerics.Vector2> b)
    {
      if (a is null) throw new System.ArgumentNullException(nameof(a));
      if (b is null) throw new System.ArgumentNullException(nameof(b));

      if (Geometry.LineSegment.IntersectionTest(a[a.Count - 1], a[0], b[b.Count - 1], b[0]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
        return true;

      for (int i = 1; i < a.Count; i++)
      {
        if (Geometry.LineSegment.IntersectionTest(a[i - 1], a[i], b[b.Count - 1], b[0]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
          return true;

        for (int p = 1; p < b.Count; p++)
        {
          if (Geometry.LineSegment.IntersectionTest(a[i - 1], a[i], b[p - 1], b[p]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
            return true;
        }
      }

      return false;
      //return t.Any(point => point.InsidePolygon(polygon)) || polygon.Any(point => point.InsidePolygon(t));
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitAlongMidpoints(System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<System.Numerics.Vector2>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source, Geometry.TriangulationType mode, System.Random? rng = null)
    {
      var copy = source.ToList();

      (System.Numerics.Vector2 v1, System.Numerics.Vector2 v2, System.Numerics.Vector2 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).First(),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).Random(rng),
          Geometry.TriangulationType.SmallestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => GetAnglesEx(copy).Aggregate((a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => GetAnglesEx(copy).Aggregate((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => GetAnglesEx(copy).Aggregate((a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<System.Numerics.Vector2>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitInHalf(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
    {
      var polygon1 = new System.Collections.Generic.List<System.Numerics.Vector2>();
      var polygon2 = new System.Collections.Generic.List<System.Numerics.Vector2>();

      foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
      {
        polygon2.Add(item);

        if (polygon2.Count > polygon1.Count)
        {
          polygon1.Add(polygon2[0]);
          polygon2.RemoveAt(0);
        }
      }

      if (polygon1.Count > polygon2.Count)
      {
        var midpoint = System.Numerics.Vector2.Lerp(polygon1[^1], polygon2[0], 0.5f);

        polygon1.Add(midpoint);
        polygon2.Insert(0, midpoint);
      }
      else if (polygon1.Count == polygon2.Count)
      {
        polygon1.Add(polygon2[0]);
      }

      polygon2.Add(polygon1[0]);

      yield return polygon1;
      yield return polygon2;
    }

    /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<System.Numerics.Vector2>> SplitVertexToVertices(this System.Collections.Generic.IList<System.Numerics.Vector2> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Numerics.Vector2[] { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    #endregion // 2D vector collection (shape) algorithms
  }
}
