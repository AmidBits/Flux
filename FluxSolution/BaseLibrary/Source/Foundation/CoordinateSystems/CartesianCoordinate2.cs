namespace Flux
{
  #region ExtensionMethods
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
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + CartesianCoordinate2.EuclideanDistance(e1, e2), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate2 v1, CartesianCoordinate2 v2, CartesianCoordinate2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate2> GetMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate2 v1, CartesianCoordinate2 vm, CartesianCoordinate2 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

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
          if (b.Y > vector.Y && CartesianCoordinate2.SideTest(vector, a, b) > 0)
            wn++;
        }
        else
        {
          if (b.Y <= vector.Y && CartesianCoordinate2.SideTest(vector, a, b) < 0)
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
          if (!EqualityByAbsoluteTolerance.IsApproximatelyEqual(initialAngle, e.Current, Maths.Epsilon1E7))
            return false;
      }

      return true;
    }
    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => CartesianCoordinate2.EuclideanDistance(v1, v2)).GetEnumerator();

      if (e.MoveNext())
      {
        var initialLength = e.Current;

        while (e.MoveNext())
          if (!EqualityByRelativeTolerance.IsApproximatelyEqual(initialLength, e.Current, 1e-15))
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
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(),
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
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<CartesianCoordinate2> source)
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

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
        var midpoint = CartesianCoordinate2.Nlerp(polygon1[^1], polygon2[0], 0.5);

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

    public static CartesianCoordinate2 ToCartesianCoordinate2(this Point2 source)
      => new(source.X, source.Y);
    public static CartesianCoordinate2 ToCartesianCoordinate2(this System.Numerics.Vector2 source)
      => new(source.X, source.Y);
    public static Point2 ToPoint2(this CartesianCoordinate2 source, System.Func<double, double> transformSelector)
      => new(System.Convert.ToInt32(transformSelector(source.X)), System.Convert.ToInt32(transformSelector(source.Y)));
    public static Point2 ToPoint2(this CartesianCoordinate2 source, HalfRounding behavior)
      => new(System.Convert.ToInt32(Maths.Round(source.X, behavior)), System.Convert.ToInt32(Maths.Round(source.Y, behavior)));
    public static System.Numerics.Vector2 ToVector2(this CartesianCoordinate2 source)
      => new((float)source.X, (float)source.Y);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct CartesianCoordinate2
    : System.IEquatable<CartesianCoordinate2>
  {
    public readonly static CartesianCoordinate2 Zero;

    private readonly double m_x;
    private readonly double m_y;

    public CartesianCoordinate2(double x, double y)
    {
      m_x = x;
      m_y = y;
    }

    [System.Diagnostics.Contracts.Pure]
    public double X
      => m_x;
    [System.Diagnostics.Contracts.Pure]
    public double Y
      => m_y;

    /// <summary>Returns the angle to the 2D X-axis.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double AngleToAxisX
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2)), m_x);
    /// <summary>Returns the angle to the 2D Y-axis.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double AngleToAxisY
      => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2)), m_y);

    /// <summary>Returns the X-slope of the line to the point (x, y).</summary>
    [System.Diagnostics.Contracts.Pure]
    public double LineSlopeX
      => System.Math.CopySign(m_x / m_y, m_x);
    /// <summary>Returns the Y-slope of the line to the point (x, y).</summary>
    [System.Diagnostics.Contracts.Pure]
    public double LineSlopeY
      => System.Math.CopySign(m_y / m_x, m_y);

    [System.Diagnostics.Contracts.Pure]
    public EllipseGeometry ToEllipse()
      => new(m_x, m_y);
    [System.Diagnostics.Contracts.Pure]
    public Point2 ToPoint2(System.MidpointRounding midpointRounding = System.MidpointRounding.ToEven)
      => new(System.Convert.ToInt32(System.Math.Round(m_x, midpointRounding)), System.Convert.ToInt32(System.Math.Round(m_y, midpointRounding)));
    [System.Diagnostics.Contracts.Pure]
    public Point2 ToPoint2()
      => ToPoint2(System.MidpointRounding.ToEven);
    [System.Diagnostics.Contracts.Pure]
    public PolarCoordinate ToPolarCoordinate()
      => new(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));
    /// <summary>Return the rotation angle using the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public Angle ToRotationAngle()
      => (Angle)ConvertCartesian2ToRotationAngle(m_x, m_y);
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public Angle ToRotationAngleEx()
      => (Angle)ConvertCartesian2ToRotationAngleEx(m_x, m_y);
    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector128"/> with the cartesian values as vector elements [X, Y].</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Runtime.Intrinsics.Vector128<double> ToVector128()
      => System.Runtime.Intrinsics.Vector128.Create(m_x, m_y);
    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, <paramref name="z"/>, <paramref name="w"/>].</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Runtime.Intrinsics.Vector256<double> ToVector256(double z, double w)
      => System.Runtime.Intrinsics.Vector256.Create(m_x, m_y, z, w);
    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, X, Y], i.e. the values are duplicated.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Runtime.Intrinsics.Vector256<double> ToVector256()
      => ToVector256(m_x, m_y);

    #region Static methods
    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    [System.Diagnostics.Contracts.Pure]
    public static double AngleBetween(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => System.Math.Acos(System.Math.Clamp(DotProduct(a, b) / (CartesianCoordinate2.EuclideanLength(a) * CartesianCoordinate2.EuclideanLength(b)), -1, 1));

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertCartesian2ToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertCartesian2ToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertCartesian2ToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

    /// <summary>Returns the cross product of two 2D vectors.</summary>
    /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    [System.Diagnostics.Contracts.Pure]
    public static double CrossProduct(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.X * b.Y - a.Y * b.X;

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double DotProduct(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.X * b.X + a.Y * b.Y;

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ChebyshevDistance(CartesianCoordinate2 source, CartesianCoordinate2 target, double edgeLength = 1)
      => ChebyshevLength(target - source, edgeLength);
    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ChebyshevLength(CartesianCoordinate2 source, double edgeLength = 1)
      => System.Math.Max(System.Math.Abs(source.m_x / edgeLength), System.Math.Abs(source.m_y / edgeLength));

    /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double EuclideanDistance(CartesianCoordinate2 source, CartesianCoordinate2 target)
      => EuclideanLength(target - source);
    /// <summary>Compute the Euclidean length of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double EuclideanLength(CartesianCoordinate2 source)
      => System.Math.Sqrt(EuclideanLengthSquared(source));
    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double EuclideanLengthSquared(CartesianCoordinate2 source)
      => System.Math.Pow(source.m_x, 2) + System.Math.Pow(source.m_y, 2);

    /// <summary>Create a new random vector in the range [(0, 0), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 FromRandomAbsolute(double toExclusiveX, double toExclusiveY)
      => new(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 FromRandomCenterZero(double toExclusiveX, double toExclusiveY)
      //=> new(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY * 2 - 1) - (toExclusiveY - 1));
      => new(Randomization.NumberGenerator.Crypto.NextDouble(-toExclusiveX, toExclusiveX), Randomization.NumberGenerator.Crypto.NextDouble(-toExclusiveY, toExclusiveY));

    /// <summary>Returns the direction cosines.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 GetDirectionCosines(CartesianCoordinate2 source, CartesianCoordinate2 target)
      => Normalize(target - source);
    /// <summary>Returns the direction ratios.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 GetDirectionRatios(CartesianCoordinate2 source, CartesianCoordinate2 target)
      => target - source;

    /// <summary>Returns the average rate of change, or simply the slope between two points.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Geometry.LineSlope GetLineSlope(CartesianCoordinate2 source, CartesianCoordinate2 target)
      => new(source.X, source.Y, target.X, target.Y);

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 InterpolateCosine(CartesianCoordinate2 y1, CartesianCoordinate2 y2, double mu)
      => new(CosineInterpolation.Interpolate(y1.m_x, y2.m_x, mu), CosineInterpolation.Interpolate(y1.m_y, y2.m_y, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 InterpolateCubic(CartesianCoordinate2 y0, CartesianCoordinate2 y1, CartesianCoordinate2 y2, CartesianCoordinate2 y3, double mu)
      => new(CubicInterpolation.Interpolate(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu), CubicInterpolation.Interpolate(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 InterpolateHermite2(CartesianCoordinate2 y0, CartesianCoordinate2 y1, CartesianCoordinate2 y2, CartesianCoordinate2 y3, double mu, double tension, double bias)
      => new(HermiteInterpolation.Interpolate(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu, tension, bias), HermiteInterpolation.Interpolate(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu, tension, bias));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 InterpolateLinear(CartesianCoordinate2 y1, CartesianCoordinate2 y2, double mu)
      => new(LinearInterpolation.Interpolate(y1.m_x, y2.m_x, mu), LinearInterpolation.Interpolate(y1.m_y, y2.m_y, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 Lerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
    {
      var imu = 1 - mu;

      return new CartesianCoordinate2(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ManhattanDistance(CartesianCoordinate2 source, CartesianCoordinate2 target, double edgeLength = 1)
      => ManhattanLength(target - source, edgeLength);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ManhattanLength(CartesianCoordinate2 source, double edgeLength = 1)
      => System.Math.Abs(source.m_x / edgeLength) + System.Math.Abs(source.m_y / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 Nlerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
      => Normalize(Lerp(source, target, mu));

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 Normalize(CartesianCoordinate2 source)
      => EuclideanLength(source) is var m && m != 0 ? source / m : source;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public static int OrthantNumber(CartesianCoordinate2 source, CartesianCoordinate2 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => source.m_y >= center.m_y ? (source.m_x >= center.m_x ? 0 : 1) : (source.m_x >= center.m_x ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (source.m_x >= center.m_x ? 0 : 1) + (source.m_y >= center.m_y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (source.m_x < center.m_x ? 0 : 1) + (source.m_y < center.m_y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 PerpendicularCcw(CartesianCoordinate2 source)
      => new(-source.m_y, source.m_x);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 PerpendicularCw(CartesianCoordinate2 source)
      => new(source.m_y, -source.m_x);

    /// <summary>Returns the perpendicular distance from the 2D point (x, y) to the to the 2D line (x1, y1) to (x2, y2).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double PerpendicularDistanceTo(CartesianCoordinate2 source, CartesianCoordinate2 a, CartesianCoordinate2 b)
    {
      var cc21 = b - a;

      return EuclideanLength(cc21 * (source - a)) / EuclideanLength(cc21);
    }

    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 PerpendicularFoot(CartesianCoordinate2 source, double a, double b, double c)
    {
      var m = -1 * (a * source.m_x + b * source.m_y + c) / (a * a + b * b);

      return new CartesianCoordinate2(m * (a + source.m_x), m * (b + source.m_y));
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    [System.Diagnostics.Contracts.Pure]
    public static int SideTest(CartesianCoordinate2 source, CartesianCoordinate2 a, CartesianCoordinate2 b)
      => System.Math.Sign((source.m_x - b.m_x) * (a.m_y - b.m_y) - (source.m_y - b.m_y) * (a.m_x - b.m_x));

    /// <summary>Slerp is a sherical linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0). Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate2 Slerp(CartesianCoordinate2 source, CartesianCoordinate2 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new CartesianCoordinate2(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin);
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator CartesianCoordinate2(System.ValueTuple<double, double> vt2) => new(vt2.Item1, vt2.Item2);
    [System.Diagnostics.Contracts.Pure] public static explicit operator System.ValueTuple<double, double>(CartesianCoordinate2 cc2) => new(cc2.X, cc2.Y);

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CartesianCoordinate2 a, CartesianCoordinate2 b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CartesianCoordinate2 a, CartesianCoordinate2 b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc) => new(-cc.X, -cc.Y);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator --(CartesianCoordinate2 cc) => cc - 1;
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator ++(CartesianCoordinate2 cc) => cc + 1;

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator +(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator +(CartesianCoordinate2 cc, double scalar) => new(cc.X + scalar, cc.Y + scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator +(double scalar, CartesianCoordinate2 cc) => new(scalar + cc.X, scalar + cc.Y);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator -(CartesianCoordinate2 cc, double scalar) => new(cc.X - scalar, cc.Y - scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator -(double scalar, CartesianCoordinate2 cc) => new(scalar - cc.X, scalar - cc.Y);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator *(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator *(CartesianCoordinate2 cc, double scalar) => new(cc.X * scalar, cc.Y * scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator *(double scalar, CartesianCoordinate2 cc) => new(scalar * cc.X, scalar * cc.Y);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator /(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator /(CartesianCoordinate2 cc, double scalar) => new(cc.X / scalar, cc.Y / scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator /(double scalar, CartesianCoordinate2 cc) => new(scalar / cc.X, scalar / cc.Y);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator %(CartesianCoordinate2 cc1, CartesianCoordinate2 cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator %(CartesianCoordinate2 cc, double scalar) => new(cc.X % scalar, cc.Y % scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate2 operator %(double scalar, CartesianCoordinate2 cc) => new(scalar % cc.X, scalar % cc.Y);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CartesianCoordinate2 other) => m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CartesianCoordinate2 o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_x, m_y);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ X = {m_x}, Y = {m_y}, (Length = {EuclideanLength(this)}) }}";
    #endregion Object overrides
  }
}
