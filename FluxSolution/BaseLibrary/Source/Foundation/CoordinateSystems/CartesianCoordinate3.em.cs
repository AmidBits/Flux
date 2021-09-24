namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this CartesianCoordinate3 source, CartesianCoordinate3 before, CartesianCoordinate3 after)
      => CartesianCoordinate3.AngleBetween(before - source, after - source);

    public static double AngleSum(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source, CartesianCoordinate3 vector)
      => AggregateTuple2(source, 0.0, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => AggregateTuple2(source, 0.0, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static CartesianCoordinate3 ComputeCentroid(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => Aggregate(source, CartesianCoordinate3.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => AggregateTuple2(source, 0.0, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3 v1, CartesianCoordinate3 v2, CartesianCoordinate3 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate3> GetMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => PartitionTuple2(source, true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3 v1, CartesianCoordinate3 vm, CartesianCoordinate3 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => PartitionTuple2(source, true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
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
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<CartesianCoordinate3>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<CartesianCoordinate3>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source, Geometry.TriangulationType mode)
    {
      var copy = new System.Collections.Generic.List<CartesianCoordinate3>(source);

      (CartesianCoordinate3 v1, CartesianCoordinate3 v2, CartesianCoordinate3 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(PartitionTuple3(copy, 2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.Randomized => RandomElement(PartitionTuple3(copy, 2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<CartesianCoordinate3>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(GetMidpoints(source), true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(source, true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitInHalf(this System.Collections.Generic.IEnumerable<CartesianCoordinate3> source)
    {
      var polygon1 = new System.Collections.Generic.List<CartesianCoordinate3>();
      var polygon2 = new System.Collections.Generic.List<CartesianCoordinate3>();

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
        var midpoint = CartesianCoordinate3.Nlerp(polygon1[polygon1.Count - 1], polygon2[0], 0.5);

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3>> SplitVertexToVertices(this System.Collections.Generic.IList<CartesianCoordinate3> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<CartesianCoordinate3>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    public static CartesianCoordinate3 ToCartesianCoordinate3(this Geometry.Point3 source)
      => new CartesianCoordinate3(source.X, source.Y, source.Z);
    public static CartesianCoordinate3 ToCartesianCoordinate3(this System.Numerics.Vector3 source)
      => new CartesianCoordinate3(source.X, source.Y, source.Z);
    public static Geometry.Point3 ToPoint3(this CartesianCoordinate3 source, FullRoundingBehavior behavior)
      => new Geometry.Point3((int)Maths.RoundTo(source.X, behavior), (int)Maths.RoundTo(source.Y, behavior), (int)Maths.RoundTo(source.Z, behavior));
    public static Geometry.Point3 ToPoint3(this CartesianCoordinate3 source, HalfRoundingBehavior behavior)
      => new Geometry.Point3((int)Maths.RoundTo(source.X, behavior), (int)Maths.RoundTo(source.Y, behavior), (int)Maths.RoundTo(source.Z, behavior));
    public static System.Numerics.Vector3 ToVector3(this CartesianCoordinate3 source)
      => new System.Numerics.Vector3((float)source.X, (float)source.Y, (float)source.Z);
  }
}
