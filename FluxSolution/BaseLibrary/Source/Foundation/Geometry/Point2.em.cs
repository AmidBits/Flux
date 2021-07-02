using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this Geometry.Point2 source, Geometry.Point2 before, Geometry.Point2 after)
      => AngleTo(before - source, after - source);

    public static double AngleSum(this System.Collections.Generic.IEnumerable<Geometry.Point2> source, Geometry.Point2 vector)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + AngleBetween(vector, v1, v2), (a, c) => a);

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this Geometry.Point2 source, Geometry.Point2 target)
      => System.Math.Acos(System.Math.Clamp(Geometry.Point2.DotProduct(source.Normalize(), target.Normalize()), -1, 1));

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this Geometry.Point2 a, Geometry.Point2 b, double edgeLength = 1)
      => System.Math.Max((b.X - a.X) / edgeLength, (b.Y - a.Y) / edgeLength);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static Geometry.Point2 ComputeCentroid(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => source.Aggregate(Geometry.Point2.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(this Geometry.Point2 p1, Geometry.Point2 p2)
      => p1.X * p2.Y - p1.Y * p2.X;
    /// <summary>Returns the dot product of the two vectors.</summary>
    public static double DotProduct(this Geometry.Point2 p1, Geometry.Point2 p2)
      => p1.X * p2.X + p1.Y * p2.Y;

    public static double EuclideanDistanceSquaredTo(this Geometry.Point2 a, Geometry.Point2 b)
      => (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
    public static double EuclideanDistanceTo(this Geometry.Point2 a, Geometry.Point2 b)
      => System.Math.Sqrt(EuclideanDistanceSquaredTo(a, b));

    /// <summary>Returns the length (squared) of the vector.</summary>
    public static double EuclideanLengthSquared(this Geometry.Point2 v)
      => v.X * v.X + v.Y * v.Y;
    /// <summary>Returns the length of the vector.</summary>
    public static double EuclideanLength(this Geometry.Point2 v)
      => System.Math.Sqrt(EuclideanLengthSquared(v));

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ExtensionMethods.PartitionTuple3(source, 2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(Geometry.Point2 v1, Geometry.Point2 v2, Geometry.Point2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ExtensionMethods.PartitionTuple3(source, 2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<Geometry.Point2> GetMidpoints(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(Geometry.Point2 v1, Geometry.Point2 vm, Geometry.Point2 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines the inclusion of a vector in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int InsidePolygon(this System.Collections.Generic.IList<Geometry.Point2> source, Geometry.Point2 vector)
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

    public static Geometry.Point2 InterpolateCosine(this Geometry.Point2 y1, Geometry.Point2 y2, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (y1 * (1.0 - mu2) + y2 * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    public static Geometry.Point2 InterpolateCubic(this Geometry.Point2 y0, Geometry.Point2 y1, Geometry.Point2 y2, Geometry.Point2 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    public static Geometry.Point2 InterpolateHermite(this Geometry.Point2 y0, Geometry.Point2 y1, Geometry.Point2 y2, Geometry.Point2 y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (y1 - y0) * onePbias * oneMtension / 2;
      m0 += (y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (y2 - y1) * onePbias * oneMtension / 2;
      m1 += (y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
    }
    public static Geometry.Point2 InterpolateLinear(this Geometry.Point2 y1, Geometry.Point2 y2, double mu)
      => y1 * (1 - mu) + y2 * mu;

    /// <summary>Determines whether the specified polygons A and B intersect.</summary>
    public static bool IntersectingPolygon(System.Collections.Generic.IList<Geometry.Point2> a, System.Collections.Generic.IList<Geometry.Point2> b)
    {
      if (a is null) throw new System.ArgumentNullException(nameof(a));
      if (b is null) throw new System.ArgumentNullException(nameof(b));

      if (Geometry.Line.IntersectionTest(a[a.Count - 1], a[0], b[b.Count - 1], b[0]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
        return true;

      for (int i = 1; i < a.Count; i++)
      {
        if (Geometry.Line.IntersectionTest(a[i - 1], a[i], b[b.Count - 1], b[0]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
          return true;

        for (int p = 1; p < b.Count; p++)
        {
          if (Geometry.Line.IntersectionTest(a[i - 1], a[i], b[p - 1], b[p]).Outcome == Geometry.LineTestOutcome.LinesIntersecting)
            return true;
        }
      }

      return false;
      //return t.Any(point => point.InsidePolygon(polygon)) || polygon.Any(point => point.InsidePolygon(t));
    }

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
    //=> source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).AllEqual(out _);
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

      if (e.MoveNext())
      {
        var initialAngle = e.Current;

        while (e.MoveNext())
          if (!Maths.IsAlmostEqual(initialAngle, e.Current, Maths.Epsilon1E7))
            return false;
      }

      return true;
    }

    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
    //=> source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).Length()).AllEqual(out _);
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).EuclideanLength()).GetEnumerator();

      if (e.MoveNext())
      {
        var initialLength = e.Current;

        while (e.MoveNext())
          if (!Maths.IsPracticallyEqual(initialLength, e.Current, 1e-6f, 1e-6f))
            return false;
      }

      return true;
    }

    public static Geometry.Point2 LerpTo(this Geometry.Point2 source, Geometry.Point2 target, in double percent = 0.5)
      => source.NlerpTo(target, percent);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this Geometry.Point2 a, Geometry.Point2 b, float edgeLength = 1)
      => System.Math.Abs(b.X - a.X) / edgeLength + System.Math.Abs(b.Y - a.Y) / edgeLength;

    public static Geometry.Point2 NlerpTo(this Geometry.Point2 source, Geometry.Point2 target, in double percent = 0.5)
      => source.NlerpTo(target, percent).Normalize();

    public static Geometry.Point2 Normalize(this Geometry.Point2 source)
    {
      var length = source.EuclideanLength();

      return source / length;
    }

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static Geometry.Point2 PerpendicularCcw(this Geometry.Point2 source)
      => new Geometry.Point2(-source.Y, source.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static Geometry.Point2 PerpendicularCw(this Geometry.Point2 source)
      => new Geometry.Point2(source.Y, -source.X);

    /// <summary>Perpendicular distance to the to the line.</summary>
    public static double PerpendicularDistance(this Geometry.Point2 source, Geometry.Point2 a, Geometry.Point2 b)
    {
      var ab = b - a;

      return (ab * (source - a)).EuclideanLength() / ab.EuclideanLength();
    }

    /// <summary>Find the perpendicular distance from a point in a 2D plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/perpendicular-distance-between-a-point-and-a-line-in-2-d/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    public static double PerpendicularDistance(this Geometry.Point2 source, double a, double b, double c)
      => System.Math.Abs(a * source.X + b * source.Y + c) / System.Math.Sqrt(a * a + b * b);

    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    //public static Geometry.Point2 PerpendicularFoot(this Geometry.Point2 source, double a, double b, double c)
    //  => -1 * (a * source.X + b * source.Y + c) / (a * a + b * b) * new Geometry.Point2(a + source.X, b + source.Y);

    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Geometry.Point2> QuadrantCenterVectors(this Geometry.Point2 source, Geometry.Size2 subQuadrant)
    {
      yield return new Geometry.Point2(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Geometry.Point2(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Geometry.Point2(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
      yield return new Geometry.Point2(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
    }
    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int QuadrantNumber(this Geometry.Point2 source, Geometry.Point2 center)
      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);

    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
    public static Geometry.Point2 Remainder(this Geometry.Point2 p1, Geometry.Point2 p2)
      => new Geometry.Point2(p1.X % p2.X, p1.Y % p2.Y);
    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
    //public static Geometry.Point2 Remainder(this Geometry.Point2 p, double value)
    //  => new Geometry.Point2(p.Y % value, p.Y % value);

    ///// <summary>Rotate the vector around the specified axis.</summary>
    //public static Geometry.Point2 RotateAroundAxis(this Geometry.Point2 source, System.Numerics.Vector3 axis, float angle)
    //  => Geometry.Point2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
    ///// <summary>Rotate the vector around the world axes.</summary>
    //public static Geometry.Point2 RotateAroundWorldAxes(this Geometry.Point2 source, float yaw, float pitch, float roll)
    //  => Geometry.Point2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this Geometry.Point2 source, Geometry.Point2 a, Geometry.Point2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static Geometry.Point2 SlerpTo(this Geometry.Point2 source, Geometry.Point2 target, double percent = 0.5)
    {
      var dot = System.Math.Clamp(Geometry.Point2.DotProduct(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dot) * percent; // Angle between start and desired.
      var relative = (target - source * dot).Normalize();
      return source * System.Math.Cos(theta) + relative * System.Math.Sin(theta);
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitAlongMidpoints(System.Collections.Generic.IEnumerable<Geometry.Point2> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<Geometry.Point2>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<Geometry.Point2>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<Geometry.Point2> source, Geometry.TriangulationType mode)
    {
      var copy = source.ToList();

      (Geometry.Point2 v1, Geometry.Point2 v2, Geometry.Point2 v3, int index, double angle) triplet = default;

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
        yield return new System.Collections.Generic.List<Geometry.Point2>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ComputeCentroid(source) is var c ? ExtensionMethods.PartitionTuple2(GetMidpoints(source), true, (v1, v2, index) => new System.Collections.Generic.List<Geometry.Point2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
      => ComputeCentroid(source) is var c ? ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => new System.Collections.Generic.List<Geometry.Point2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitInHalf(this System.Collections.Generic.IEnumerable<Geometry.Point2> source)
    {
      var polygon1 = new System.Collections.Generic.List<Geometry.Point2>();
      var polygon2 = new System.Collections.Generic.List<Geometry.Point2>();

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
        var midpoint = polygon1[polygon1.Count - 1].NlerpTo(polygon2[0], 0.5f);

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Point2>> SplitVertexToVertices(this System.Collections.Generic.IList<Geometry.Point2> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new Geometry.Point2[] { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    ///// <summary>Convert a 2D vector to a 3D vector.</summary>
    //public static System.Numerics.Vector3 ToVector3(this Geometry.Point2 source)
    //  => new System.Numerics.Vector3(source, 0);
  }
}
