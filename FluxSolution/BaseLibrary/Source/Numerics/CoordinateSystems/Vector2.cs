namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this Vector2 source, Vector2 before, Vector2 after)
      => Vector2.AngleBetween(before - source, after - source);

    /// <summary>Compute the sum angle of all vectors.</summary>
    public static double AngleSum(this System.Collections.Generic.IEnumerable<Vector2> source, Vector2 vector)
      => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<Vector2> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static Vector2 ComputeCentroid(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.Aggregate(() => Vector2.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.AggregateTuple2(0d, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(Vector2 v1, Vector2 v2, Vector2 v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<Vector2> GetMidpoints(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(Vector2 v1, Vector2 vm, Vector2 v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<Vector2> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines the inclusion of a vector in the polygon (2D planar). This Winding Number method counts the number of times the polygon winds around the point. The point is outside only when this "winding number" is 0, otherwise the point is inside.</summary>
    /// <see cref="http://geomalgorithms.com/a03-_inclusion.html#wn_PnPoly"/>
    public static int InsidePolygon(this System.Collections.Generic.IList<Vector2> source, Vector2 vector)
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
          if (b.Y > vector.Y && Vector2.SideTest(vector, a, b) > 0)
            wn++;
        }
        else
        {
          if (b.Y <= vector.Y && Vector2.SideTest(vector, a, b) < 0)
            wn--;
        }
      }

      return wn;
    }

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<Vector2> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<Vector2> source, IEqualityApproximatable<double> mode)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      mode ??= new Flux.ApproximateEquality.ApproximateEqualityByAbsoluteTolerance<double>(1E-15);

      using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

      if (e.MoveNext())
      {
        var initialAngle = e.Current;

        while (e.MoveNext())
          if (!mode.IsApproximatelyEqual(initialAngle, e.Current))
            return false;
      }

      return true;
    }
    /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<Vector2> source, IEqualityApproximatable<double> mode)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      mode ??= new Flux.ApproximateEquality.ApproximateEqualityByRelativeTolerance<double>(1E-15);

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).EuclideanLength()).GetEnumerator();

      if (e.MoveNext())
      {
        var initialLength = e.Current;

        while (e.MoveNext())
          if (!mode.IsApproximatelyEqual(initialLength, e.Current))
            return false;
      }

      return true;
    }

    /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<Vector2> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<Vector2>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<Vector2>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<Vector2> source, Geometry.TriangulationType mode, System.Random rng)
    {
      var copy = new System.Collections.Generic.List<Vector2>(source);

      (Vector2 v1, Vector2 v2, Vector2 v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(rng),
          Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Constants.PiOver2) > System.Math.Abs(b.angle - Constants.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Constants.PiOver2) < System.Math.Abs(b.angle - Constants.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<Vector2>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<Vector2> source)
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<Vector2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<Vector2> source)
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<Vector2>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitInHalf(this System.Collections.Generic.IEnumerable<Vector2> source)
    {
      var polygon1 = new System.Collections.Generic.List<Vector2>();
      var polygon2 = new System.Collections.Generic.List<Vector2>();

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
        var midpoint = Vector2.Nlerp(polygon1[^1], polygon2[0], 0.5);

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<Vector2>> SplitVertexToVertices(this System.Collections.Generic.IList<Vector2> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<Vector2>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    public static Vector2 ToCartesianCoordinate2(this Point2 source)
      => new(source.X, source.Y);
    public static Vector2 ToCartesianCoordinate2(this System.Numerics.Vector2 source)
      => new(source.X, source.Y);
    public static Point2 ToPoint2(this Vector2 source, System.Func<double, double> transformSelector)
      => new(System.Convert.ToInt32(transformSelector(source.X)), System.Convert.ToInt32(transformSelector(source.Y)));
    public static Point2 ToPoint2(this Vector2 source, RoundingMode behavior)
      => new(System.Convert.ToInt32(Flux.Rounding<double>.Round(source.X, behavior)), System.Convert.ToInt32(Flux.Rounding<double>.Round(source.Y, behavior)));
    public static System.Numerics.Vector2 ToVector2(this Vector2 source)
      => new((float)source.X, (float)source.Y);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct Vector2
    : IVector2<double>
  {
    public readonly static Vector2 Zero;

    private readonly double m_x;
    private readonly double m_y;

    public Vector2(double x, double y)
    {
      m_x = x;
      m_y = y;
    }

    [System.Diagnostics.Contracts.Pure] public double X { get => m_x; init => m_x = value; }
    [System.Diagnostics.Contracts.Pure] public double Y { get => m_y; init => m_y = value; }

    ///// <summary>Returns the angle to the 2D X-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double AngleToAxisX
    //  => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2)), m_x);
    ///// <summary>Returns the angle to the 2D Y-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double AngleToAxisY
    //  => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2)), m_y);

    ///// <summary>Returns the X-slope of the line to the point (x, y).</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double LineSlopeX
    //  => System.Math.CopySign(m_x / m_y, m_x);
    ///// <summary>Returns the Y-slope of the line to the point (x, y).</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double LineSlopeY
    //  => System.Math.CopySign(m_y / m_x, m_y);

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public double ChebyshevLength(double edgeLength = 1)
      => System.Math.Max(System.Math.Abs(m_x / edgeLength), System.Math.Abs(m_y / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLengthSquared()
      => m_x * m_x + m_y * m_y;

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public double ManhattanLength(double edgeLength = 1)
      => System.Math.Abs(m_x / edgeLength) + System.Math.Abs(m_y / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public Vector2 Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public int OrthantNumber(Vector2 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Vector2 PerpendicularCcw()
      => new(-m_y, m_x);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Vector2 PerpendicularCw()
      => new(m_y, -m_x);

    #region To..

    /// <summary>Converts the <see cref="Vector2"/> to a <see cref="Point2"/> using the specified <see cref="System.MidpointRounding"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Point2 ToCartesianCoordinate2I(System.MidpointRounding rounding)
      => new(System.Convert.ToInt32(System.Math.Round(m_x, rounding)), System.Convert.ToInt32(System.Math.Round(m_y, rounding)));

    /// <summary>Converts the <see cref="Vector2"/> to a <see cref="Point2"/> using the specified <see cref="Flux.RoundingMode"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Point2 ToCartesianCoordinate2I(Flux.RoundingMode rounding)
      => new(System.Convert.ToInt32(Flux.Rounding<double>.Round(m_x, rounding)), System.Convert.ToInt32(Flux.Rounding<double>.Round(m_y, rounding)));

    /// <summary>Converts the <see cref="Vector2"/> to a <see cref="EllipseGeometry"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public EllipseGeometry ToEllipseGeometry()
      => new(m_x, m_y);

    ///// <summary>Converts the <see cref="Vector2"/> to a <see cref="PolarCoordinate"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public PolarCoordinate ToPolarCoordinate()
    //  => new(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));

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

    #endregion

    #region Static methods
    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    [System.Diagnostics.Contracts.Pure]
    public static double AngleBetween(Vector2 a, Vector2 b)
      => System.Math.Acos(System.Math.Clamp(DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1, 1));

    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertCartesian2ToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Constants.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertCartesian2ToRotationAngleEx(double x, double y)
      => Constants.PiX2 - ConvertCartesian2ToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

    /// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double CrossProduct(Vector2 a, Vector2 b)
      => a.X * b.Y - a.Y * b.X;

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double DotProduct(Vector2 a, Vector2 b)
      => a.X * b.X + a.Y * b.Y;

    /// <summary>Create a new random vector in the range [(0, 0), (toExlusiveX, toExclusiveY)] using the specified rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 FromRandomAbsolute(double toExclusiveX, double toExclusiveY, System.Random rng)
      => new(rng.NextDouble(toExclusiveX), rng.NextDouble(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 FromRandomCenterZero(double toExclusiveX, double toExclusiveY, System.Random rng)
      //=> new(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY * 2 - 1) - (toExclusiveY - 1));
      => new(rng.NextDouble(-toExclusiveX, toExclusiveX), rng.NextDouble(-toExclusiveY, toExclusiveY));

    /// <summary>Returns the direction cosines.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 GetDirectionCosines(Vector2 source, Vector2 target)
      => (target - source).Normalized();
    /// <summary>Returns the direction ratios.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 GetDirectionRatios(Vector2 source, Vector2 target)
      => target - source;

    /// <summary>Returns the average rate of change, or simply the slope between two points.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Geometry.LineSlope GetLineSlope(Vector2 source, Vector2 target)
      => new(source.X, source.Y, target.X, target.Y);

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 Interpolate(Vector2 p1, Vector2 p2, double mu, I2NodeInterpolatable<double, double> mode)
    {
      mode ??= new Interpolation.LinearInterpolation<double, double>();

      return new(System.Convert.ToInt32(mode.Interpolate2Node(p1.X, p2.X, mu)), System.Convert.ToInt32(mode.Interpolate2Node(p1.Y, p2.Y, mu)));
    }

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    public static Point2 Interpolate(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, double mu, I4NodeInterpolatable<double, double> mode)
    {
      mode ??= new Interpolation.CubicInterpolation<double, double>();

      return new(System.Convert.ToInt32(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu)), System.Convert.ToInt32(mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu)));
    }

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector2 InterpolateCosine(Vector2 y1, Vector2 y2, double mu)
    //  => new(CosineInterpolation.Interpolate(y1.m_x, y2.m_x, mu), CosineInterpolation.Interpolate(y1.m_y, y2.m_y, mu));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector2 InterpolateCubic(Vector2 y0, Vector2 y1, Vector2 y2, Vector2 y3, double mu)
    //  => new(CubicInterpolation.Interpolate(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu), CubicInterpolation.Interpolate(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector2 InterpolateHermite2(Vector2 y0, Vector2 y1, Vector2 y2, Vector2 y3, double mu, double tension, double bias)
    //  => new(HermiteInterpolation.Interpolate(y0.m_x, y1.m_x, y2.m_x, y3.m_x, mu, tension, bias), HermiteInterpolation.Interpolate(y0.m_y, y1.m_y, y2.m_y, y3.m_y, mu, tension, bias));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector2 InterpolateLinear(Vector2 y1, Vector2 y2, double mu)
    //  => new(LinearInterpolation.Interpolate(y1.m_x, y2.m_x, mu), LinearInterpolation.Interpolate(y1.m_y, y2.m_y, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 Lerp(Vector2 source, Vector2 target, double mu)
    {
      var imu = 1 - mu;

      return new Vector2(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    [System.Diagnostics.Contracts.Pure]
    public static Vector2 Nlerp(Vector2 source, Vector2 target, double mu)
      => Lerp(source, target, mu).Normalized();

    /// <summary>Returns the perpendicular distance from the 2D point (x, y) to the to the 2D line (x1, y1) to (x2, y2).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double PerpendicularDistanceTo(Vector2 source, Vector2 a, Vector2 b)
    {
      var cc21 = b - a;

      return (cc21 * (source - a)).EuclideanLength() / cc21.EuclideanLength();
    }

    /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
    /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
    /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
    /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
    /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
    /// <param name="source">A given point.</param>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 PerpendicularFoot(Vector2 source, double a, double b, double c)
    {
      var m = -1 * (a * source.m_x + b * source.m_y + c) / (a * a + b * b);

      return new Vector2(m * (a + source.m_x), m * (b + source.m_y));
    }

    /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
    [System.Diagnostics.Contracts.Pure]
    public static int SideTest(Vector2 source, Vector2 a, Vector2 b)
      => System.Math.Sign((source.m_x - b.m_x) * (a.m_y - b.m_y) - (source.m_y - b.m_y) * (a.m_x - b.m_x));

    /// <summary>Slerp is a sherical linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0). Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    [System.Diagnostics.Contracts.Pure]
    public static Vector2 Slerp(Vector2 source, Vector2 target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new Vector2(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin);
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator Vector2(System.ValueTuple<double, double> vt2) => new(vt2.Item1, vt2.Item2);
    [System.Diagnostics.Contracts.Pure] public static explicit operator System.ValueTuple<double, double>(Vector2 cc2) => new(cc2.X, cc2.Y);

    [System.Diagnostics.Contracts.Pure] public static explicit operator Vector2(double[] v) => new(v[0], v[1]);
    [System.Diagnostics.Contracts.Pure] public static explicit operator double[](Vector2 v) => new double[] { v.m_x, v.m_y };

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator -(Vector2 cc) => new(-cc.X, -cc.Y);

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator --(Vector2 cc) => cc - 1;
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator ++(Vector2 cc) => cc + 1;

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator +(Vector2 cc1, Vector2 cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator +(Vector2 cc, double scalar) => new(cc.X + scalar, cc.Y + scalar);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator +(double scalar, Vector2 cc) => new(scalar + cc.X, scalar + cc.Y);

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator -(Vector2 cc1, Vector2 cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator -(Vector2 cc, double scalar) => new(cc.X - scalar, cc.Y - scalar);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator -(double scalar, Vector2 cc) => new(scalar - cc.X, scalar - cc.Y);

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator *(Vector2 cc1, Vector2 cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator *(Vector2 cc, double scalar) => new(cc.X * scalar, cc.Y * scalar);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator *(double scalar, Vector2 cc) => new(scalar * cc.X, scalar * cc.Y);

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator /(Vector2 cc1, Vector2 cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator /(Vector2 cc, double scalar) => new(cc.X / scalar, cc.Y / scalar);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator /(double scalar, Vector2 cc) => new(scalar / cc.X, scalar / cc.Y);

    [System.Diagnostics.Contracts.Pure] public static Vector2 operator %(Vector2 cc1, Vector2 cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator %(Vector2 cc, double scalar) => new(cc.X % scalar, cc.Y % scalar);
    [System.Diagnostics.Contracts.Pure] public static Vector2 operator %(double scalar, Vector2 cc) => new(scalar % cc.X, scalar % cc.Y);
    #endregion Overloaded operators
  }
}
