//using System.Diagnostics.CodeAnalysis;
//using System.Globalization;
//using System.Numerics;

namespace Flux
{
  //#region ExtensionMethods
  //public static partial class ExtensionMethods
  //{
  //  /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
  //  public static TSelf AngleBetween(this CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> before, CartesianCoordinate3<TSelf> after)
  //    => CartesianCoordinate3<TSelf>.AngleBetween(before - source, after - source);

  //  /// <summary>Compute the sum angle of all vectors.</summary>
  //  public static TSelf AngleSum(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source, CartesianCoordinate3<TSelf> vector)
  //    => source.AggregateTuple2(0.0, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

  //  /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
  //  public static TSelf ComputeAreaSigned(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.AggregateTuple2(0.0, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / 2);
  //  /// <summary>Compute the surface area of the polygon.</summary>
  //  public static TSelf ComputeArea(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => TSelf.Abs(ComputeAreaSigned(source));

  //  /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
  //  public static CartesianCoordinate3<TSelf> ComputeCentroid(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.Aggregate(() => CartesianCoordinate3<TSelf>.Zero, (acc, e, i) => acc + e, (acc, c) => acc / c);

  //  /// <summary>Compute the perimeter length of the polygon.</summary>
  //  public static TSelf ComputePerimeter(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.AggregateTuple2(0.0, true, (a, e1, e2, i) => a + (e2 - e1).EuclideanLength(), (a, c) => a);

  //  /// <summary>Returns a sequence triplet angles.</summary>
  //  public static System.Collections.Generic.IEnumerable<TSelf> GetAngles(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
  //  /// <summary>Returns a sequence triplet angles.</summary>
  //  public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3<TSelf> v1, CartesianCoordinate3<TSelf> v2, CartesianCoordinate3<TSelf> v3, int index, TSelf angle)> GetAnglesEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

  //  /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
  //  public static System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> GetMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.PartitionTuple2(true, (v1, v2, index) => (v1 + v2) / 2);
  //  /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
  //  public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3<TSelf> v1, CartesianCoordinate3<TSelf> vm, CartesianCoordinate3<TSelf> v2, int index)> GetMidpointsEx(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => source.PartitionTuple2(true, (v1, v2, index) => (v1, (v1 + v2) / 2, v2, index));

  //  /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
  //  public static bool IsConvexPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //  {
  //    bool negative = false, positive = false;

  //    foreach (var angle in GetAngles(source))
  //    {
  //      if (angle < 0)
  //        negative = true;
  //      else
  //        positive = true;

  //      if (negative && positive)
  //        return false;
  //    }

  //    return negative ^ positive;
  //  }
  //  /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
  //  public static bool IsEquiangularPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source, IEqualityApproximatable<TSelf> mode)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    mode ??= new Flux.ApproximateEquality.ApproximateEqualityByAbsoluteTolerance<TSelf>(1E-15);

  //    using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

  //    if (e.MoveNext())
  //    {
  //      var initialAngle = e.Current;

  //      while (e.MoveNext())
  //        if (!mode.IsApproximatelyEqual(initialAngle, e.Current))
  //          return false;
  //    }

  //    return true;
  //  }
  //  /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
  //  public static bool IsEquilateralPolygon(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source, IEqualityApproximatable<TSelf> mode)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    mode ??= new Flux.ApproximateEquality.ApproximateEqualityByRelativeTolerance<TSelf>(1E-15);

  //    using var e = source.PartitionTuple2(true, (v1, v2, index) => (v2 - v1).EuclideanLength()).GetEnumerator();

  //    if (e.MoveNext())
  //    {
  //      var initialLength = e.Current;

  //      while (e.MoveNext())
  //        if (!mode.IsApproximatelyEqual(initialLength, e.Current))
  //          return false;
  //    }

  //    return true;
  //  }

  //  /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitAlongMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //  {
  //    var midpointPolygon = new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>();

  //    foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
  //    {
  //      midpointPolygon.Add(pair.v1.vm);

  //      yield return new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
  //    }

  //    yield return midpointPolygon;
  //  }

  //  /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitByTriangulation(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source, Geometry.TriangulationType mode, System.Random rng)
  //  {
  //    var copy = new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>(source);

  //    (CartesianCoordinate3<TSelf> v1, CartesianCoordinate3<TSelf> v2, CartesianCoordinate3<TSelf> v3, int index, TSelf angle) triplet = default;

  //    while (copy.Count >= 3)
  //    {
  //      triplet = mode switch
  //      {
  //        Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d))),
  //        Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, 0d)).RandomElement(rng),
  //        Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
  //        Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
  //        Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => TSelf.Abs(a.angle - Constants.PiOver2) > TSelf.Abs(b.angle - Constants.PiOver2) ? a : b),
  //        Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => TSelf.Abs(a.angle - Constants.PiOver2) < TSelf.Abs(b.angle - Constants.PiOver2) ? a : b),
  //        _ => throw new System.Exception(),
  //      };
  //      yield return new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>() { triplet.v2, triplet.v3, triplet.v1 };

  //      copy.RemoveAt((triplet.index + 1) % copy.Count);
  //    }
  //  }

  //  /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitCentroidToMidpoints(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

  //  /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitCentroidToVertices(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //    => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

  //  /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitInHalf(this System.Collections.Generic.IEnumerable<CartesianCoordinate3<TSelf>> source)
  //  {
  //    var polygon1 = new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>();
  //    var polygon2 = new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>();

  //    foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
  //    {
  //      polygon2.Add(item);

  //      if (polygon2.Count > polygon1.Count)
  //      {
  //        polygon1.Add(polygon2[0]);
  //        polygon2.RemoveAt(0);
  //      }
  //    }

  //    if (polygon1.Count > polygon2.Count)
  //    {
  //      var midpoint = CartesianCoordinate3<TSelf>.Nlerp(polygon1[^1], polygon2[0], 0.5);

  //      polygon1.Add(midpoint);
  //      polygon2.Insert(0, midpoint);
  //    }
  //    else if (polygon1.Count == polygon2.Count)
  //    {
  //      polygon1.Add(polygon2[0]);
  //    }

  //    polygon2.Add(polygon1[0]);

  //    yield return polygon1;
  //    yield return polygon2;
  //  }

  //  /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
  //  /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
  //  /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
  //  public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3<TSelf>>> SplitVertexToVertices(this System.Collections.Generic.IList<CartesianCoordinate3<TSelf>> source, int index)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var vertex = source[index];

  //    var startIndex = index + 1;
  //    var count = startIndex + source.Count - 2;

  //    for (var i = startIndex; i < count; i++)
  //    {
  //      yield return new System.Collections.Generic.List<CartesianCoordinate3<TSelf>>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
  //    }
  //  }
  //}
  //#endregion ExtensionMethods

  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct CartesianCoordinate3<TSelf>
    : ICartesianCoordinate3<TSelf>
    , System.IEquatable<CartesianCoordinate3<TSelf>>
    , System.Numerics.IAdditionOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IAdditiveIdentity<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IDecrementOperators<CartesianCoordinate3<TSelf>>
    , System.Numerics.IDivisionOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IEqualityOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, bool>
    , System.Numerics.IIncrementOperators<CartesianCoordinate3<TSelf>>
    , System.Numerics.IModulusOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IMultiplicativeIdentity<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IMultiplyOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>
    , System.Numerics.ISubtractionOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IUnaryNegationOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    , System.Numerics.IUnaryPlusOperators<CartesianCoordinate3<TSelf>, CartesianCoordinate3<TSelf>>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly TSelf m_x;
    private readonly TSelf m_y;
    private readonly TSelf m_z;

    public CartesianCoordinate3(TSelf x, TSelf y, TSelf z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    public TSelf X { get => m_x; init => m_x = value; }
    public TSelf Y { get => m_y; init => m_y = value; }
    public TSelf Z { get => m_z; init => m_z = value; }

    ///// <summary>Returns the axes angles to the 3D X-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (TSelf toYaxis, TSelf toZaxis) AnglesToAxisX
    //  => (TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_y, 2)), m_x), TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_z, 2)), m_x));
    ///// <summary>Returns the axes angles to the 3D Y-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (TSelf toXaxis, TSelf toZaxis) AnglesToAxisY
    //  => (TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_x, 2)), m_y), TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_z, 2)), m_y));
    ///// <summary>Returns the axes angles to the 3D Z-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public (TSelf toXaxis, TSelf toYaxis) AnglesToAxisZ
    //  => (TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_x, 2)), m_z), TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_y, 2)), m_z));

    ///// <summary>Returns the angle to the 3D X-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public TSelf AngleToAxisX
    //  => TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_y, 2) + TSelf.Pow(m_z, 2)), m_x);
    ///// <summary>Returns the angle to the 3D Y-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public TSelf AngleToAxisY
    //  => TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_z, 2) + TSelf.Pow(m_x, 2)), m_y);
    ///// <summary>Returns the angle to the 3D Z-axis.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public TSelf AngleToAxisZ
    //  => TSelf.Atan2(TSelf.Sqrt(TSelf.Pow(m_x, 2) + TSelf.Pow(m_y, 2)), m_z);

    ///// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //public TSelf ChebyshevLength(TSelf edgeLength = 1)
    //  => GenericMath.Max(TSelf.Abs(m_x / edgeLength), TSelf.Abs(m_y / edgeLength), TSelf.Abs(m_z / edgeLength));

    ///// <summary>Compute the Euclidean length of the vector.</summary>
    //public TSelf EuclideanLength()
    //  => TSelf.Sqrt(EuclideanLengthSquared());

    ///// <summary>Compute the Euclidean length squared of the vector.</summary>
    //public TSelf EuclideanLengthSquared()
    //  => m_x * m_x + m_y * m_y + m_z * m_z;

    ///// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //public TSelf ManhattanLength(TSelf edgeLength = 1)
    //  => TSelf.Abs(m_x / edgeLength) + TSelf.Abs(m_y / edgeLength) + TSelf.Abs(m_z / edgeLength);

    //public CartesianCoordinate3<TSelf> Normalized()
    //  => EuclideanLength() is var m && m != 0 ? this / m : this;

    ///// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    //public int OrthantNumber(CartesianCoordinate3<TSelf> center, OrthantNumbering numbering)
    //  => numbering switch
    //  {
    //    OrthantNumbering.Traditional => m_z >= center.m_z ? (m_y >= center.m_y ? (m_x >= center.m_x ? 0 : 1) : (m_x >= center.m_x ? 3 : 2)) : (m_y >= center.m_y ? (m_x >= center.m_x ? 7 : 6) : (m_x >= center.m_x ? 4 : 5)),
    //    OrthantNumbering.BinaryNegativeAs1 => (m_x >= center.m_x ? 0 : 1) + (m_y >= center.m_y ? 0 : 2) + (m_z >= center.m_z ? 0 : 4),
    //    OrthantNumbering.BinaryPositiveAs1 => (m_x < center.m_x ? 0 : 1) + (m_y < center.m_y ? 0 : 2) + (m_z < center.m_z ? 0 : 4),
    //    _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
    //  };

    ///// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    ///// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    //public CartesianCoordinate3<TSelf> Orthogonal()
    //  => TSelf.Abs(m_x) > TSelf.Abs(m_z) ? new CartesianCoordinate3<TSelf>(-m_y, m_x, 0) : new CartesianCoordinate3<TSelf>(0, -m_x, m_y);

    /// <summary>Converts the <see cref="CartesianCoordinate3<TSelf>"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    //public CylindricalCoordinate<TSelf> ToCylindricalCoordinate()
    //  => new(
    //    TSelf.Sqrt(m_x * m_x + m_y * m_y),
    //    (TSelf.Atan2(m_y, m_x) + TSelf.Tau) % TSelf.Tau,
    //    m_z
    //  );

    /////// <summary>Returns a quaternion from two vectors.</summary>
    /////// <see cref="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/>
    ////[System.Diagnostics.Contracts.Pure]
    ////public Quaternion ToQuaternion(CartesianCoordinate3<TSelf> rotatingTo)
    ////  => Quaternion.FromTwoVectors(this, rotatingTo);

    ///// <summary>Converts the <see cref="CartesianCoordinate3<TSelf>"/> to a <see cref="SphericalCoordinate"/>.</summary>
    //public SphericalCoordinate ToSphericalCoordinate()
    //{
    //  var x2y2 = m_x * m_x + m_y * m_y;

    //  return new SphericalCoordinate(TSelf.Sqrt(x2y2 + m_z * m_z), TSelf.Atan2(TSelf.Sqrt(x2y2), m_z) + TSelf.Pi, TSelf.Atan2(m_y, m_x) + TSelf.Pi);
    //}

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, Z, <paramref name="w"/>].</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256(TSelf w)
    //  => System.Runtime.Intrinsics.Vector256.Create(m_x, m_y, m_z, w);

    ///// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256"/> with the cartesian values as vector elements [X, Y, Z, 0].</summary>
    //public System.Runtime.Intrinsics.Vector256<TSelf> ToVector256()
    //  => ToVector256(0);

    ///// <summary>Converts the <see cref="Point3"/> to a <see cref="System.Numerics.Vector3"/>.</summary>
    //public System.Numerics.Vector3 ToVector3()
    //  => new((float)m_x, (float)m_y, (float)m_z);

    #region Static methods
    ///// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    ///// When dot eq 0 then the vectors are perpendicular.
    ///// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    ///// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    ///// </summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static TSelf AngleBetween(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b)
    //  => TSelf.Acos(TSelf.Clamp(DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -TSelf.One, TSelf.One));

    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> ConvertEclipticToEquatorial(CartesianCoordinate3<TSelf> ecliptic, TSelf obliquityOfTheEcliptic)
    //  => Flux.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, TSelf.Cos(obliquityOfTheEcliptic), -TSelf.Sin(obliquityOfTheEcliptic), 0, 0, TSelf.Sin(obliquityOfTheEcliptic), TSelf.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), ecliptic);
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> ConvertEquatorialToEcliptic(CartesianCoordinate3<TSelf> equatorial, TSelf obliquityOfTheEcliptic)
    //  => Flux.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, TSelf.Cos(obliquityOfTheEcliptic), TSelf.Sin(obliquityOfTheEcliptic), 0, 0, -TSelf.Sin(obliquityOfTheEcliptic), TSelf.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), equatorial);

    ///// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> CrossProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
    //  => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    ///// <summary>Returns the dot product of two 3D vectors.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static TSelf DotProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
    //  => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    ///// <summary>Create a new random vector using the crypto-grade rng.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> FromRandomAbsolute(TSelf toExclusiveX, TSelf toExclusiveY, TSelf toExclusiveZ, System.Random rng)
    //  => new(rng.NextTSelf(toExclusiveX), rng.NextTSelf(toExclusiveY), rng.NextTSelf(toExclusiveZ));
    ///// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> FromRandomCenterZero(TSelf toExclusiveX, TSelf toExclusiveY, TSelf toExclusiveZ, System.Random rng)
    //  => new(rng.NextTSelf(-toExclusiveX, toExclusiveX), rng.NextTSelf(-toExclusiveY, toExclusiveY), rng.NextTSelf(-toExclusiveZ, toExclusiveZ));

    ///// <summary>Returns the direction cosines.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> GetDirectionCosines(CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> target)
    //  => (target - source).Normalized();
    /// <summary>Returns the direction ratios.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3<TSelf> GetDirectionRatios(CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> target)
      => target - source;

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate3<TSelf> Interpolate(CartesianCoordinate3<TSelf> p1, CartesianCoordinate3<TSelf> p2, TSelf mu, I2NodeInterpolatable<TSelf, TSelf> mode)
    //{
    //  mode ??= new Interpolation.LinearInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate2Node(p1.X, p2.X, mu), mode.Interpolate2Node(p1.Y, p2.Y, mu), mode.Interpolate2Node(p1.Z, p2.Z, mu));
    //}

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate3<TSelf> Interpolate(CartesianCoordinate3<TSelf> p0, CartesianCoordinate3<TSelf> p1, CartesianCoordinate3<TSelf> p2, CartesianCoordinate3<TSelf> p3, TSelf mu, I4NodeInterpolatable<TSelf, TSelf> mode)
    //{
    //  mode ??= new Interpolation.CubicInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu), mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu), mode.Interpolate4Node(p0.Z, p1.Z, p2.Z, p3.Z, mu));
    //}

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector3 InterpolateCosine(Vector3 p1, Vector3 p2, TSelf mu)
    //  => new(CosineInterpolation.Interpolate(p1.m_x, p2.m_x, mu), CosineInterpolation.Interpolate(p1.m_y, p2.m_y, mu), CosineInterpolation.Interpolate(p1.Z, p2.Z, mu));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector3 InterpolateCubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, TSelf mu)
    //  => new(CubicInterpolation.Interpolate(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu), CubicInterpolation.Interpolate(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu), CubicInterpolation.Interpolate(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector3 InterpolateHermite2(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, TSelf mu, TSelf tension, TSelf bias)
    //  => new(HermiteInterpolation.Interpolate(p0.m_x, p1.m_x, p2.m_x, p3.m_x, mu, tension, bias), HermiteInterpolation.Interpolate(p0.m_y, p1.m_y, p2.m_y, p3.m_y, mu, tension, bias), HermiteInterpolation.Interpolate(p0.m_z, p1.m_z, p2.m_z, p3.m_z, mu, tension, bias));
    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static Vector3 InterpolateLinear(Vector3 p1, Vector3 p2, TSelf mu)
    //  => new(LinearInterpolation.Interpolate(p1.m_x, p2.m_x, mu), LinearInterpolation.Interpolate(p1.m_y, p2.m_y, mu), LinearInterpolation.Interpolate(p1.m_z, p2.m_z, mu));

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> Lerp(CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> target, TSelf mu)
    //{
    //  var imu = TSelf.One - mu;

    //  return new CartesianCoordinate3<TSelf>(source.m_x * imu + target.m_x * mu, source.m_y * imu + target.m_y * mu, source.m_z * imu + target.m_z * mu);
    //}

    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> Nlerp(CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> target, TSelf mu)
    //  => Lerp(source, target, mu).Normalized();

    ///// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    //[System.Diagnostics.Contracts.Pure]
    //public static TSelf ScalarTripleProduct(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b, CartesianCoordinate3<TSelf> c)
    //  => DotProduct(a, CrossProduct(b, c));

    ///// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> Slerp(CartesianCoordinate3<TSelf> source, CartesianCoordinate3<TSelf> target, TSelf mu)
    //{
    //  var dp = TSelf.Clamp(DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
    //  var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
    //  var cos = TSelf.Cos(theta);
    //  var sin = TSelf.Sin(theta);

    //  return new CartesianCoordinate3<TSelf>(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin, source.m_z * cos + (target.m_z - source.m_z) * dp * sin);
    //}

    ///// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    //[System.Diagnostics.Contracts.Pure]
    //public static CartesianCoordinate3<TSelf> VectorTripleProduct(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b, CartesianCoordinate3<TSelf> c)
    //  => CrossProduct(a, CrossProduct(b, c));
    #endregion Static methods

    #region Implemented interfaces

    public static explicit operator CartesianCoordinate3<TSelf>(System.ValueTuple<TSelf, TSelf, TSelf> vt3) => new(vt3.Item1, vt3.Item2, vt3.Item3);
    public static explicit operator System.ValueTuple<TSelf, TSelf, TSelf>(CartesianCoordinate3<TSelf> cc3) => new(cc3.X, cc3.Y, cc3.Z);

    public static explicit operator CartesianCoordinate3<TSelf>(TSelf[] v) => new(v[0], v[1], v[2]);
    public static explicit operator TSelf[](CartesianCoordinate3<TSelf> v) => new TSelf[] { v.m_x, v.m_y, v.m_z };

    // System.Numerics.INumberBase<>
    public static CartesianCoordinate3<TSelf> Zero => new(TSelf.Zero, TSelf.Zero, TSelf.Zero);
    public static int Radix => 2;
    public static CartesianCoordinate3<TSelf> One => new(TSelf.One, TSelf.One, TSelf.One);
    public static CartesianCoordinate3<TSelf> Abs(CartesianCoordinate3<TSelf> cc) => new(TSelf.Abs(cc.X), TSelf.Abs(cc.Y), TSelf.Abs(cc.Z));
    public static CartesianCoordinate3<TSelf> CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateChecked(o);
        return new(v, v, v);
      }
      else if (o is CartesianCoordinate3<TSelf> cc2)
        return new(cc2.X, cc2.Y, TSelf.Zero);

      throw new System.NotSupportedException();
    }
    public static CartesianCoordinate3<TSelf> CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateSaturating(o);
        return new(v, v, v);
      }
      else if (o is CartesianCoordinate3<TSelf> cc2)
        return new(cc2.X, cc2.Y, TSelf.Zero);

      throw new System.NotSupportedException();
    }
    public static CartesianCoordinate3<TSelf> CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsINumber())
      {
        var v = TSelf.CreateTruncating(o);
        return new(v, v, v);
      }
      else if (o is CartesianCoordinate3<TSelf> cc2)
        return new(cc2.X, cc2.Y, TSelf.Zero);

      throw new System.NotSupportedException();
    }
    public static bool IsCanonical(CartesianCoordinate3<TSelf> cc) => true;
    public static bool IsComplexNumber(CartesianCoordinate3<TSelf> cc) => false;
    public static bool IsEvenInteger(CartesianCoordinate3<TSelf> cc) => TSelf.IsEvenInteger(cc.m_x) && TSelf.IsEvenInteger(cc.m_y) && TSelf.IsEvenInteger(cc.m_z);
    public static bool IsFinite(CartesianCoordinate3<TSelf> cc) => !IsInfinity(cc);
    public static bool IsImaginaryNumber(CartesianCoordinate3<TSelf> cc) => false;
    public static bool IsInfinity(CartesianCoordinate3<TSelf> cc) => TSelf.IsInfinity(cc.m_x) || TSelf.IsInfinity(cc.m_y) || TSelf.IsInfinity(cc.m_z);
    public static bool IsInteger(CartesianCoordinate3<TSelf> cc) => TSelf.IsInteger(cc.m_x) && TSelf.IsInteger(cc.m_y) && TSelf.IsInteger(cc.m_z);
    public static bool IsNaN(CartesianCoordinate3<TSelf> cc) => TSelf.IsNaN(cc.m_x) || TSelf.IsNaN(cc.m_y) || TSelf.IsNaN(cc.m_z);
    public static bool IsNegative(CartesianCoordinate3<TSelf> cc) => TSelf.IsNegative(cc.m_x) || TSelf.IsNegative(cc.m_y) || TSelf.IsNegative(cc.m_z);
    public static bool IsNegativeInfinity(CartesianCoordinate3<TSelf> cc) => TSelf.IsNegativeInfinity(cc.m_x) || TSelf.IsNegativeInfinity(cc.m_y) || TSelf.IsNegativeInfinity(cc.m_z);
    public static bool IsNormal(CartesianCoordinate3<TSelf> cc) => false;
    public static bool IsOddInteger(CartesianCoordinate3<TSelf> cc) => TSelf.IsOddInteger(cc.m_x) && TSelf.IsOddInteger(cc.m_y) && TSelf.IsOddInteger(cc.m_z);
    public static bool IsPositive(CartesianCoordinate3<TSelf> cc) => TSelf.IsPositive(cc.m_x) || TSelf.IsPositive(cc.m_y) || TSelf.IsPositive(cc.m_z);
    public static bool IsPositiveInfinity(CartesianCoordinate3<TSelf> cc) => TSelf.IsPositiveInfinity(cc.m_x) || TSelf.IsPositiveInfinity(cc.m_y) || TSelf.IsPositiveInfinity(cc.m_z);
    public static bool IsRealNumber(CartesianCoordinate3<TSelf> cc) => false;
    public static bool IsSubnormal(CartesianCoordinate3<TSelf> cc) => false;
    public static bool IsZero(CartesianCoordinate3<TSelf> cc) => cc.Equals(Zero);
    public static CartesianCoordinate3<TSelf> MaxMagnitude(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate3<TSelf> MaxMagnitudeNumber(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate3<TSelf> MinMagnitude(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate3<TSelf> MinMagnitudeNumber(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => throw new System.NotImplementedException();
    public static CartesianCoordinate3<TSelf> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static CartesianCoordinate3<TSelf> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertFromChecked<TOther>(TOther value, out CartesianCoordinate3<TSelf> result)
    {
      try
      {
        result = CreateChecked(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertFromSaturating<TOther>(TOther value, out CartesianCoordinate3<TSelf> result)
    {
      try
      {
        result = CreateSaturating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertFromTruncating<TOther>(TOther value, out CartesianCoordinate3<TSelf> result)
    {
      try
      {
        result = CreateTruncating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertToChecked<TOther>(CartesianCoordinate3<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertToSaturating<TOther>(CartesianCoordinate3<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertToTruncating<TOther>(CartesianCoordinate3<TSelf> value, out TOther result)
    {
      result = default;
      return false;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out CartesianCoordinate3<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out CartesianCoordinate3<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      charsWritten = default;
      return true;
    }
    public static CartesianCoordinate3<TSelf> Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out CartesianCoordinate3<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public static CartesianCoordinate3<TSelf> Parse(string s, System.IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(string? s, System.IFormatProvider? provider, out CartesianCoordinate3<TSelf> result)
    {
      throw new NotImplementedException();
    }

    // IEquatable<>, System.Numerics.IEqualityOperators<>
    public bool Equals(CartesianCoordinate3<TSelf> other) => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
    public static bool operator ==(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.Equals(b);
    public static bool operator !=(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => !a.Equals(b);
    public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate3<TSelf> o && Equals(o);
    public override int GetHashCode() => System.HashCode.Combine(m_x, m_y, m_z);

    //// IComparable<>, System.Numerics.INumber<>
    //public int CompareTo(CartesianCoordinate3<TSelf> other) => EuclideanLength() is var el && other.EuclideanLength() is var oel && el > oel ? 1 : el < oel ? -1 : 0;
    //public static bool operator >(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.CompareTo(b) > 0;
    //public static bool operator >=(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.CompareTo(b) >= 0;
    //public static bool operator <(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.CompareTo(b) < 0;
    //public static bool operator <=(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.CompareTo(b) <= 0;
    //public int CompareTo([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate3<TSelf> o ? CompareTo(o) : 1;

    // System.Numerics.IUnaryPlusOperators<>
    public static CartesianCoordinate3<TSelf> operator +(CartesianCoordinate3<TSelf> cc) => new(+cc.X, +cc.Y, +cc.Z);

    // System.Numerics.IUnaryNegationOperators<>
    public static CartesianCoordinate3<TSelf> operator -(CartesianCoordinate3<TSelf> cc) => new(-cc.X, -cc.Y, -cc.Z);

    // System.Numerics.IDecrementOperators<>
    public static CartesianCoordinate3<TSelf> operator --(CartesianCoordinate3<TSelf> cc) => cc - TSelf.One;

    // System.Numerics.IIncrementOperators<>
    public static CartesianCoordinate3<TSelf> operator ++(CartesianCoordinate3<TSelf> cc) => cc + TSelf.One;

    // System.Numerics.IAdditiveIdentity<>
    public static CartesianCoordinate3<TSelf> AdditiveIdentity => Zero;

    // System.Numerics.IAdditionOperators<>
    public static CartesianCoordinate3<TSelf> operator +(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y, cc1.Z + cc2.Z);
    public static CartesianCoordinate3<TSelf> operator +(CartesianCoordinate3<TSelf> cc, TSelf scalar) => new(cc.X + scalar, cc.Y + scalar, cc.Z + scalar);
    public static CartesianCoordinate3<TSelf> operator +(TSelf scalar, CartesianCoordinate3<TSelf> cc) => new(scalar + cc.X, scalar + cc.Y, scalar + cc.Z);

    // System.Numerics.ISubtractionOperators<>
    public static CartesianCoordinate3<TSelf> operator -(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y, cc1.Z - cc2.Z);
    public static CartesianCoordinate3<TSelf> operator -(CartesianCoordinate3<TSelf> cc, TSelf scalar) => new(cc.X - scalar, cc.Y - scalar, cc.Z - scalar);
    public static CartesianCoordinate3<TSelf> operator -(TSelf scalar, CartesianCoordinate3<TSelf> cc) => new(scalar - cc.X, scalar - cc.Y, scalar - cc.Z);

    // System.Numerics.IMultiplicativeIdentity<>
    public static CartesianCoordinate3<TSelf> MultiplicativeIdentity => One;

    // System.Numerics.IMultiplyOperators<>
    public static CartesianCoordinate3<TSelf> operator *(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y, cc1.Z * cc2.Z);
    public static CartesianCoordinate3<TSelf> operator *(CartesianCoordinate3<TSelf> cc, TSelf scalar) => new(cc.X * scalar, cc.Y * scalar, cc.Z * scalar);
    public static CartesianCoordinate3<TSelf> operator *(TSelf scalar, CartesianCoordinate3<TSelf> cc) => new(scalar * cc.X, scalar * cc.Y, scalar * cc.Z);

    // System.Numerics.IDivisionOperators<>
    public static CartesianCoordinate3<TSelf> operator /(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y, cc1.Z / cc2.Z);
    public static CartesianCoordinate3<TSelf> operator /(CartesianCoordinate3<TSelf> cc, TSelf scalar) => new(cc.X / scalar, cc.Y / scalar, cc.Z / scalar);
    public static CartesianCoordinate3<TSelf> operator /(TSelf scalar, CartesianCoordinate3<TSelf> cc) => new(scalar / cc.X, scalar / cc.Y, scalar / cc.Z);

    // System.Numerics.IModulusOperators<>
    public static CartesianCoordinate3<TSelf> operator %(CartesianCoordinate3<TSelf> cc1, CartesianCoordinate3<TSelf> cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y, cc1.Z % cc2.Z);
    public static CartesianCoordinate3<TSelf> operator %(CartesianCoordinate3<TSelf> cc, TSelf scalar) => new(cc.X % scalar, cc.Y % scalar, cc.Z % scalar);
    public static CartesianCoordinate3<TSelf> operator %(TSelf scalar, CartesianCoordinate3<TSelf> cc) => new(scalar % cc.X, scalar % cc.Y, scalar % cc.Z);

    #endregion Implemented interfaces
  }
}
