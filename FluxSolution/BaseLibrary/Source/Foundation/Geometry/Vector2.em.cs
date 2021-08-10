namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this Geometry.Vector2 source, Geometry.Vector2 before, Geometry.Vector2 after)
      => AngleTo(before - source, after - source);

    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => System.Math.Acos(System.Math.Clamp(DotProduct(source.Normalize(), target.Normalize()), -1, 1));

    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => ChebyshevLength(target - source);
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(this Geometry.Vector2 source)
      => System.Math.Max(System.Math.Abs(source.X), System.Math.Abs(source.Y));

    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(this Geometry.Vector2 source, Geometry.Vector2 target)
      => source.X * target.Y - source.Y * target.X;

    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Geometry.Vector2 DivideCeiling(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Ceiling(source.X / scalar), System.Math.Ceiling(source.Y / scalar));
    /// <summary>Creates a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Geometry.Vector2 DivideFloor(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Floor(source.X / scalar), System.Math.Floor(source.Y / scalar));

    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static double DotProduct(this Geometry.Vector2 source, Geometry.Vector2 target)
      => source.X * target.X + source.Y * target.Y;

    /// <summary>Compute the distance (or magnitude) squared between the two vectors.</summary>
    public static double EuclideanDistanceSquaredTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => EuclideanLengthSquared(target - source);
    /// <summary>Compute the distance (or magnitude) between the two vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => EuclideanLength(target - source);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLength(this Geometry.Vector2 source)
      => System.Math.Sqrt(EuclideanLengthSquared(source));
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanLengthSquared(this Geometry.Vector2 source)
      => source.X * source.X + source.Y * source.Y;

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateCosineTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (source * (1.0 - mu2) + target * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateCubicTo(this Geometry.Vector2 source, Geometry.Vector2 target, Geometry.Vector2 preSource, Geometry.Vector2 postTarget, double mu)
    {
      var mu2 = mu * mu;

      var a0 = postTarget - target - preSource + source;
      var a1 = preSource - source - a0;
      var a2 = target - preSource;
      var a3 = source;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateHermiteTo(this Geometry.Vector2 source, Geometry.Vector2 target, Geometry.Vector2 preSource, Geometry.Vector2 postTarget, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (source - preSource) * onePbias * oneMtension / 2;
      m0 += (target - source) * oneMbias * oneMtension / 2;
      var m1 = (target - source) * onePbias * oneMtension / 2;
      m1 += (postTarget - target) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return source * a0 + m0 * a1 + m1 * a2 + target * a3;
    }
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Geometry.Vector2 InterpolateLinearTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
      => source * (1 - mu) + target * mu;

    /// <summary>Creates a new vector by linear interpolation from source to target.</summary>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 LerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, in double mu)
      => source + (target - source) * mu;

    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistanceTo(this Geometry.Vector2 source, Geometry.Vector2 target)
      => ManhattanLength(target - source);
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(this Geometry.Vector2 source)
      => System.Math.Abs(source.X) + System.Math.Abs(source.Y);

    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Geometry.Vector2 MultiplyCeiling(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Ceiling(source.X * scalar), System.Math.Ceiling(source.Y * scalar));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Geometry.Vector2 MultiplyFloor(this Geometry.Vector2 source, double scalar)
      => new Geometry.Vector2(System.Math.Floor(source.X * scalar), System.Math.Floor(source.Y * scalar));

    /// <summary>Creates a new vector by normalized linear interpolation from source to target.</summary>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 NlerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, in double mu)
      => LerpTo(source, target, mu).Normalize();

    /// <summary>Create a new vector with the source components normalized.</summary>
    public static Geometry.Vector2 Normalize(this Geometry.Vector2 source)
      => source / source.EuclideanLength();

    /// <summary>Create a new vector, 90 degrees perpendicular to the vector, which is the vector rotated 90 degrees counter-clockwise.</summary>
    public static Geometry.Vector2 PerpendicularCcw(this Geometry.Vector2 source)
      => new Geometry.Vector2(-source.Y, source.X);
    /// <summary>Create a new vector, 90 degrees perpendicular to the vector, which is the vector rotated 90 degrees clockwise.</summary>
    public static Geometry.Vector2 PerpendicularCw(this Geometry.Vector2 source)
      => new Geometry.Vector2(source.Y, -source.X);

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    public static int SideTest(this Geometry.Vector2 source, Geometry.Vector2 a, Geometry.Vector2 b)
      => System.Math.Sign((source.X - b.X) * (a.Y - b.Y) - (a.X - b.X) * (source.Y - b.Y));

    /// <summary>Creates a new vector by spherical linear interpolation, which travels the torque-minimal path, i.e. it travels along the straightest path the rounded surface of a sphere.</summary>>
    /// <param name="mu">[0, 1]</param>
    public static Geometry.Vector2 SlerpTo(this Geometry.Vector2 source, Geometry.Vector2 target, double mu)
    {
      var dot = System.Math.Clamp(DotProduct(source, target), -1.0f, 1.0f); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dot) * mu; // Angle between start and desired.

      return source * System.Math.Cos(theta) + (target - source * dot).Normalize() * System.Math.Sin(theta);
    }

    #region IEnumerable<Geometry.Vector2>
    public static double AngleSum(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source, Geometry.Vector2 vector)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + AngleBetween(vector, v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static Geometry.Vector2 ComputeCentroid(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => source.Aggregate(Geometry.Vector2.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(Geometry.Vector2 v1, Geometry.Vector2 v2, Geometry.Vector2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Determines the inclusion of a vector in the (2D planar) polygon. This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int InsidePolygon(this System.Collections.Generic.IList<Geometry.Vector2> source, Geometry.Vector2 vector)
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

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
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
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
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

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source, Geometry.TriangulationType mode)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => ComputeCentroid(source) is var c ? ExtensionMethods.PartitionTuple2(GetMidpoints(source), true, (v1, v2, index) => new System.Collections.Generic.List<Geometry.Point2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
      => ComputeCentroid(source) is var c ? ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => new System.Collections.Generic.List<Geometry.Point2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitInHalf(this System.Collections.Generic.IEnumerable<Geometry.Vector2> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<Geometry.Vector2>> SplitVertexToVertices(this System.Collections.Generic.IList<Geometry.Vector2> source, int index)
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
    #endregion IEnumerable<Geometry.Vector2>
  }
}
