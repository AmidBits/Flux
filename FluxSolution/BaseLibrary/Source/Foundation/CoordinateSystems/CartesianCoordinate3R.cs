namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static double AngleBetween(this CartesianCoordinate3R source, CartesianCoordinate3R before, CartesianCoordinate3R after)
      => CartesianCoordinate3R.AngleBetween(before - source, after - source);

    /// <summary>Compute the sum angle of all vectors.</summary>
    public static double AngleSum(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source, CartesianCoordinate3R vector)
      => source.AggregateTuple2(0.0, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static double ComputeAreaSigned(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.AggregateTuple2(0.0, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
    /// <summary>Compute the surface area of the polygon.</summary>
    public static double ComputeArea(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => System.Math.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static CartesianCoordinate3R ComputeCentroid(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.Aggregate(CartesianCoordinate3R.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static double ComputePerimeter(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.AggregateTuple2(0.0, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<double> GetAngles(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3R v1, CartesianCoordinate3R v2, CartesianCoordinate3R v3, int index, double angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate3R> GetMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1 + v2) / 2);
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3R v1, CartesianCoordinate3R vm, CartesianCoordinate3R v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => source.PartitionTuple2(true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
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
    public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
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
    public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).EuclideanLength()).GetEnumerator();

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
    {
      var midpointPolygon = new System.Collections.Generic.List<CartesianCoordinate3R>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<CartesianCoordinate3R>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source, Geometry.TriangulationType mode, System.Random rng)
    {
      var copy = new System.Collections.Generic.List<CartesianCoordinate3R>(source);

      (CartesianCoordinate3R v1, CartesianCoordinate3R v2, CartesianCoordinate3R v3, int index, double angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(rng),
          Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) > System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => System.Math.Abs(a.angle - Maths.PiOver2) < System.Math.Abs(b.angle - Maths.PiOver2) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<CartesianCoordinate3R>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3R>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3R>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitInHalf(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R> source)
    {
      var polygon1 = new System.Collections.Generic.List<CartesianCoordinate3R>();
      var polygon2 = new System.Collections.Generic.List<CartesianCoordinate3R>();

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
        var midpoint = CartesianCoordinate3R.Nlerp(polygon1[^1], polygon2[0], 0.5);

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R>> SplitVertexToVertices(this System.Collections.Generic.IList<CartesianCoordinate3R> source, int index)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<CartesianCoordinate3R>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    public static CartesianCoordinate3R ToCartesianCoordinate3(this CartesianCoordinate3I source)
      => new(source.X, source.Y, source.Z);
    public static CartesianCoordinate3R ToCartesianCoordinate3(this System.Numerics.Vector3 source)
      => new(source.X, source.Y, source.Z);
    public static CartesianCoordinate3I ToPoint3(this CartesianCoordinate3R source, System.Func<double, double> transformSelector)
      => new(System.Convert.ToInt32(transformSelector(source.X)), System.Convert.ToInt32(transformSelector(source.Y)), System.Convert.ToInt32(transformSelector(source.Z)));
    public static CartesianCoordinate3I ToPoint3(this CartesianCoordinate3R source, HalfRoundingBehavior behavior)
      => new(System.Convert.ToInt32(Maths.Round(source.X, behavior)), System.Convert.ToInt32(Maths.Round(source.Y, behavior)), System.Convert.ToInt32(Maths.Round(source.Z, behavior)));
    public static System.Numerics.Vector3 ToVector3(this CartesianCoordinate3R source)
      => new((float)source.X, (float)source.Y, (float)source.Z);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct CartesianCoordinate3R
    : /*System.IEquatable<CartesianCoordinate3R>, */ICartesianCoordinate3<double>
  {
    public static readonly CartesianCoordinate3R Zero;

    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;

    public CartesianCoordinate3R(double x, double y, double z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    [System.Diagnostics.Contracts.Pure] public double X { get => m_x; init => m_x = value; }
    [System.Diagnostics.Contracts.Pure] public double Y { get => m_y; init => m_y = value; }
    [System.Diagnostics.Contracts.Pure] public double Z { get => m_z; init => m_z = value; }

    ///// <summary>Returns the axes angles to the 3D X-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (double toYaxis, double toZaxis) AnglesToAxisX
    //  => (System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2)), m_x), System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_z, 2)), m_x));
    ///// <summary>Returns the axes angles to the 3D Y-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (double toXaxis, double toZaxis) AnglesToAxisY
    //  => (System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2)), m_y), System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_z, 2)), m_y));
    ///// <summary>Returns the axes angles to the 3D Z-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (double toXaxis, double toYaxis) AnglesToAxisZ
    //  => (System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2)), m_z), System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2)), m_z));

    ///// <summary>Returns the angle to the 3D X-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double AngleToAxisX
    //  => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_y, 2) + System.Math.Pow(m_z, 2)), m_x);
    ///// <summary>Returns the angle to the 3D Y-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double AngleToAxisY
    //  => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_z, 2) + System.Math.Pow(m_x, 2)), m_y);
    ///// <summary>Returns the angle to the 3D Z-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public double AngleToAxisZ
    //  => System.Math.Atan2(System.Math.Sqrt(System.Math.Pow(m_x, 2) + System.Math.Pow(m_y, 2)), m_z);

    /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public double ChebyshevLength(double edgeLength = 1)
      => Maths.Max(System.Math.Abs(m_x / edgeLength), System.Math.Abs(m_y / edgeLength), System.Math.Abs(m_z / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public double EuclideanLengthSquared()
      => m_x * m_x + m_y * m_y + m_z * m_z;

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public double ManhattanLength(double edgeLength = 1)
      => System.Math.Abs(m_x / edgeLength) + System.Math.Abs(m_y / edgeLength) + System.Math.Abs(m_z / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R Normalized()
      => EuclideanLength() is var m && m != 0 ? this / m : this;

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public int OrthantNumber(CartesianCoordinate3R center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => m_z >= center.m_z ? (m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2)) : (m_y >= center.m_y ? (m_x >= center.m_x ? 7 : 6) : (m_x >= center.m_x ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2) + (m_z >= center.m_z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2) + (m_z < center.m_z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R Orthogonal()
      => System.Math.Abs(m_x) > System.Math.Abs(m_z) ? new CartesianCoordinate3R(-m_y, m_x, 0) : new CartesianCoordinate3R(0, -m_x, m_y);

    #region To..

    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="CartesianCoordinate3I"/> using the specified <see cref="System.MidpointRounding"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3I ToCartesianCoordinate3I(System.MidpointRounding rounding)
      => new(System.Convert.ToInt32(System.Math.Round(m_x, rounding)), System.Convert.ToInt32(System.Math.Round(m_y, rounding)), System.Convert.ToInt32(System.Math.Round(m_z, rounding)));

    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="CartesianCoordinate3I"/> using the specified <see cref="Flux.FullRoundingBehavior"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3I ToCartesianCoordinate3I(Flux.FullRoundingBehavior rounding)
      => new(System.Convert.ToInt32(Maths.Round(m_x, rounding)), System.Convert.ToInt32(Maths.Round(m_y, rounding)), System.Convert.ToInt32(Maths.Round(m_z, rounding)));

    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="CartesianCoordinate3I"/> using the specified <see cref="Flux.HalfRoundingBehavior"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3I ToCartesianCoordinate3I(Flux.HalfRoundingBehavior rounding)
      => new(System.Convert.ToInt32(Maths.Round(m_x, rounding)), System.Convert.ToInt32(Maths.Round(m_y, rounding)), System.Convert.ToInt32(Maths.Round(m_z, rounding)));

    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CylindricalCoordinate ToCylindricalCoordinate()
      => new(System.Math.Sqrt(m_x * m_x + m_y * m_y), (System.Math.Atan2(m_y, m_x) + Maths.PiX2) % Maths.PiX2, m_z);

    /// <summary>Returns a quaternion from two vectors.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/>
    [System.Diagnostics.Contracts.Pure]
    public Quaternion ToQuaternion(CartesianCoordinate3R rotatingTo)
      => Quaternion.FromTwoVectors(this, rotatingTo);

    /// <summary>Converts the <see cref="CartesianCoordinate3R"/> to a <see cref="SphericalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public SphericalCoordinate ToSphericalCoordinate()
    {
      var x2y2 = m_x * m_x + m_y * m_y;

      return new SphericalCoordinate(System.Math.Sqrt(x2y2 + m_z * m_z), System.Math.Atan2(System.Math.Sqrt(x2y2), m_z) + System.Math.PI, System.Math.Atan2(m_y, m_x) + System.Math.PI);
    }

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, Z, <paramref name="w"/>].</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Runtime.Intrinsics.Vector256<double> ToVector256(double w)
      => System.Runtime.Intrinsics.Vector256.Create(m_x, m_y, m_z, w);

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, Z, 0].</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Runtime.Intrinsics.Vector256<double> ToVector256()
      => ToVector256(0);

    /// <summary>Converts the <see cref="CartesianCoordinate3I"/> to a <see cref="System.Numerics.Vector3"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Vector3 ToVector3()
      => new((float)m_x, (float)m_y, (float)m_z);

    #endregion

    #region Static methods
    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    [System.Diagnostics.Contracts.Pure]
    public static double AngleBetween(CartesianCoordinate3R a, CartesianCoordinate3R b)
      => System.Math.Acos(System.Math.Clamp(DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -1, 1));

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R ConvertEclipticToEquatorial(CartesianCoordinate3R ecliptic, double obliquityOfTheEcliptic)
      => Flux.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, System.Math.Cos(obliquityOfTheEcliptic), -System.Math.Sin(obliquityOfTheEcliptic), 0, 0, System.Math.Sin(obliquityOfTheEcliptic), System.Math.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), ecliptic);
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R ConvertEquatorialToEcliptic(CartesianCoordinate3R equatorial, double obliquityOfTheEcliptic)
      => Flux.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, System.Math.Cos(obliquityOfTheEcliptic), System.Math.Sin(obliquityOfTheEcliptic), 0, 0, -System.Math.Sin(obliquityOfTheEcliptic), System.Math.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), equatorial);

    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R CrossProduct(CartesianCoordinate3R a, CartesianCoordinate3R b)
      => new(a.m_y * b.m_z - a.m_z * b.m_y, a.m_z * b.m_x - a.m_x * b.m_z, a.m_x * b.m_y - a.m_y * b.m_x);

    /// <summary>Returns the dot product of two 3D vectors.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double DotProduct(CartesianCoordinate3R a, CartesianCoordinate3R b)
      => a.m_x * b.m_x + a.m_y * b.m_y + a.m_z * b.m_z;

    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R FromRandomAbsolute(double toExclusiveX, double toExclusiveY, double toExclusiveZ, System.Random rng)
      => new(rng.NextDouble(toExclusiveX), rng.NextDouble(toExclusiveY), rng.NextDouble(toExclusiveZ));
    /// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R FromRandomCenterZero(double toExclusiveX, double toExclusiveY, double toExclusiveZ, System.Random rng)
      => new(rng.NextDouble(-toExclusiveX, toExclusiveX), rng.NextDouble(-toExclusiveY, toExclusiveY), rng.NextDouble(-toExclusiveZ, toExclusiveZ));

    /// <summary>Returns the direction cosines.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R GetDirectionCosines(CartesianCoordinate3R source, CartesianCoordinate3R target)
      => (target - source).Normalized();
    /// <summary>Returns the direction ratios.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R GetDirectionRatios(CartesianCoordinate3R source, CartesianCoordinate3R target)
      => target - source;

    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R InterpolateCosine(CartesianCoordinate3R p1, CartesianCoordinate3R p2, double mu)
      => new(CosineInterpolation.Interpolate(p1.m_x, p2.m_x, mu), CosineInterpolation.Interpolate(p1.m_y, p2.m_y, mu), CosineInterpolation.Interpolate(p1.Z, p2.Z, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R InterpolateCubic(CartesianCoordinate3R p0, CartesianCoordinate3R p1, CartesianCoordinate3R p2, CartesianCoordinate3R p3, double mu)
      => new(CubicInterpolation.Interpolate(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu), CubicInterpolation.Interpolate(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu), CubicInterpolation.Interpolate(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R InterpolateHermite2(CartesianCoordinate3R p0, CartesianCoordinate3R p1, CartesianCoordinate3R p2, CartesianCoordinate3R p3, double mu, double tension, double bias)
      => new(HermiteInterpolation.Interpolate(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu, tension, bias), HermiteInterpolation.Interpolate(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu, tension, bias), HermiteInterpolation.Interpolate(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu, tension, bias));
    /// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R InterpolateLinear(CartesianCoordinate3R p1, CartesianCoordinate3R p2, double mu)
      => new(LinearInterpolation.Interpolate(p1.m_x, p2.m_x, mu), LinearInterpolation.Interpolate(p1.m_y, p2.m_y, mu), LinearInterpolation.Interpolate(p1.m_z, p2.m_z, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R Lerp(CartesianCoordinate3R source, CartesianCoordinate3R target, double mu)
    {
      var imu = 1 - mu;

      return new CartesianCoordinate3R(source.m_x * imu + target.m_x * mu, source.m_y * imu + target.m_y * mu, source.m_z * imu + target.m_z * mu);
    }

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R Nlerp(CartesianCoordinate3R source, CartesianCoordinate3R target, double mu)
      => Lerp(source, target, mu).Normalized();

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    [System.Diagnostics.Contracts.Pure]
    public static double ScalarTripleProduct(CartesianCoordinate3R a, CartesianCoordinate3R b, CartesianCoordinate3R c)
      => DotProduct(a, CrossProduct(b, c));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R Slerp(CartesianCoordinate3R source, CartesianCoordinate3R target, double mu)
    {
      var dp = System.Math.Clamp(DotProduct(source, target), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
      var theta = System.Math.Acos(dp) * mu; // Angle between start and desired.
      var cos = System.Math.Cos(theta);
      var sin = System.Math.Sin(theta);

      return new CartesianCoordinate3R(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin, source.m_z * cos + (target.m_z - source.m_z) * dp * sin);
    }

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R VectorTripleProduct(CartesianCoordinate3R a, CartesianCoordinate3R b, CartesianCoordinate3R c)
      => CrossProduct(a, CrossProduct(b, c));
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator CartesianCoordinate3R(System.ValueTuple<double, double, double> vt3) => new(vt3.Item1, vt3.Item2, vt3.Item3);
    [System.Diagnostics.Contracts.Pure] public static explicit operator System.ValueTuple<double, double, double>(CartesianCoordinate3R cc3) => new(cc3.X, cc3.Y, cc3.Z);

    [System.Diagnostics.Contracts.Pure] public static explicit operator CartesianCoordinate3R(double[] v) => new(v[0], v[1], v[2]);
    [System.Diagnostics.Contracts.Pure] public static explicit operator double[](CartesianCoordinate3R v) => new double[] { v.m_x, v.m_y, v.m_z };

    //[System.Diagnostics.Contracts.Pure] public static bool operator ==(CartesianCoordinate3R a, CartesianCoordinate3R b) => a.Equals(b);
    //[System.Diagnostics.Contracts.Pure] public static bool operator !=(CartesianCoordinate3R a, CartesianCoordinate3R b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator -(CartesianCoordinate3R cc) => new(-cc.X, -cc.Y, -cc.Z);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator --(CartesianCoordinate3R cc) => cc - 1;
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator ++(CartesianCoordinate3R cc) => cc + 1;

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator +(CartesianCoordinate3R cc1, CartesianCoordinate3R cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y, cc1.Z + cc2.Z);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator +(CartesianCoordinate3R cc, double scalar) => new(cc.X + scalar, cc.Y + scalar, cc.Z + scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator +(double scalar, CartesianCoordinate3R cc) => new(scalar + cc.X, scalar + cc.Y, scalar + cc.Z);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator -(CartesianCoordinate3R cc1, CartesianCoordinate3R cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y, cc1.Z - cc2.Z);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator -(CartesianCoordinate3R cc, double scalar) => new(cc.X - scalar, cc.Y - scalar, cc.Z - scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator -(double scalar, CartesianCoordinate3R cc) => new(scalar - cc.X, scalar - cc.Y, scalar - cc.Z);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator *(CartesianCoordinate3R cc1, CartesianCoordinate3R cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y, cc1.Z * cc2.Z);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator *(CartesianCoordinate3R cc, double scalar) => new(cc.X * scalar, cc.Y * scalar, cc.Z * scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator *(double scalar, CartesianCoordinate3R cc) => new(scalar * cc.X, scalar * cc.Y, scalar * cc.Z);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator /(CartesianCoordinate3R cc1, CartesianCoordinate3R cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y, cc1.Z / cc2.Z);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator /(CartesianCoordinate3R cc, double scalar) => new(cc.X / scalar, cc.Y / scalar, cc.Z / scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator /(double scalar, CartesianCoordinate3R cc) => new(scalar / cc.X, scalar / cc.Y, scalar / cc.Z);

    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator %(CartesianCoordinate3R cc1, CartesianCoordinate3R cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y, cc1.Z % cc2.Z);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator %(CartesianCoordinate3R cc, double scalar) => new(cc.X % scalar, cc.Y % scalar, cc.Z % scalar);
    [System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3R operator %(double scalar, CartesianCoordinate3R cc) => new(scalar % cc.X, scalar % cc.Y, scalar % cc.Z);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    //[System.Diagnostics.Contracts.Pure] public bool Equals(CartesianCoordinate3R other) => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
    #endregion Implemented interfaces

    #region Object overrides
    //[System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is CartesianCoordinate3R o && Equals(o);
    //[System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_x, m_y, m_z);
    //[System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ X = {m_x}, Y = {m_y}, Z = {m_z}, (Length = {EuclideanLength()}) }}";
    #endregion Object overrides
  }
}
