namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this CartesianCoordinate2 source, CartesianCoordinate2 before, CartesianCoordinate2 after)
      => CartesianCoordinate2.AngleBetween(before - source, after - source);

    /// <summary>Compute the sum angle of all vectors.</summary>
    public static double AngleSum(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source, CartesianCoordinate2 vector)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static CartesianCoordinate2 ComputeCentroid(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.Aggregate(CartesianCoordinate2.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate2 v1, CartesianCoordinate2 v2, CartesianCoordinate2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => PartitionTuple3(source, 2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2> GetMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate2 v1, CartesianCoordinate2 vm, CartesianCoordinate2 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => ExtensionMethods.PartitionTuple2(source, true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines the inclusion of a vector in the polygon (2D planar). This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int InsidePolygon(this System.Collections.Generic.IList<CartesianCoordinate2> source, CartesianCoordinate2 vector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      int wn = 0;

      //for (int i = 0; i < source.Count; i++)
      //{
      //  var a = source[i];
      //  var b = (i == source.Count - 1 ? source[0] : source[i + 1]);

      //  if (a.Y <= vector.Y)
      //  {
      //    if (b.Y > vector.Y && vector.SideTest(a, b) > 0)
      //    {
      //      wn++;
      //    }
      //  }
      //  else
      //  {
      //    if (b.Y <= vector.Y && vector.SideTest(a, b) < 0)
      //    {
      //      wn--;
      //    }
      //  }
      //}

      foreach (var (a, b) in source.PartitionTuple2(true, (a, b, i) => (a, b)))
      {
        if (a.Y <= vector.Y)
        {
          if (b.Y > vector.Y && vector.SideTest(a, b) > 0)
            wn++;
        }
        else
        {
          if (b.Y <= vector.Y && vector.SideTest(a, b) < 0)
            wn--;
        }
      }

      return wn;
    }

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
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
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<CartesianCoordinate2>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<CartesianCoordinate2>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source, Geometry.TriangulationType mode)
    {
      var copy = new System.Collections.Generic.List<CartesianCoordinate2>(source);

      (CartesianCoordinate2 v1, CartesianCoordinate2 v2, CartesianCoordinate2 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(PartitionTuple3(copy, 2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.Randomized => PartitionTuple3(copy, 2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(),
          Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<CartesianCoordinate2>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(GetMidpoints(source), true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => ComputeCentroid(source) is var c ? PartitionTuple2(source, true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitInHalf(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
    {
      var polygon1 = new System.Collections.Generic.List<CartesianCoordinate2>();
      var polygon2 = new System.Collections.Generic.List<CartesianCoordinate2>();

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
        var midpoint = CartesianCoordinate2.Nlerp(polygon1[polygon1.Count - 1], polygon2[0], 0.5);

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitVertexToVertices(this System.Collections.Generic.IList<CartesianCoordinate2> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<CartesianCoordinate2>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    public static CartesianCoordinate2 ToCartesianCoordinate2(this Geometry.Point2 source)
      => new CartesianCoordinate2(source.X, source.Y);
    public static CartesianCoordinate2 ToCartesianCoordinate2(this System.Numerics.Vector2 source)
      => new CartesianCoordinate2(source.X, source.Y);
    public static Geometry.Point2 ToPoint2(this CartesianCoordinate2 source, FullRoundingBehavior behavior)
      => new Geometry.Point2((int)Maths.RoundTo(source.X, behavior), (int)Maths.RoundTo(source.Y, behavior));
    public static Geometry.Point2 ToPoint2(this CartesianCoordinate2 source, HalfRoundingBehavior behavior)
      => new Geometry.Point2((int)Maths.RoundTo(source.X, behavior), (int)Maths.RoundTo(source.Y, behavior));
    public static System.Numerics.Vector2 ToVector2(this CartesianCoordinate2 source)
      => new System.Numerics.Vector2((float)source.X, (float)source.Y);
  }
}
