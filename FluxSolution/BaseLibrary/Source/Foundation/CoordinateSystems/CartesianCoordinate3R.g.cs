#if NET7_0_OR_GREATER
namespace Flux
{
  #region ExtensionMethods
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
    public static TSelf AngleBetween<TSelf>(this CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> before, CartesianCoordinate3R<TSelf> after)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => CartesianCoordinate3R<TSelf>.AngleBetween(before - source, after - source);

    /// <summary>Compute the sum angle of all vectors.</summary>
    public static TSelf AngleSum<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source, CartesianCoordinate3R<TSelf> vector)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.AggregateTuple2(TSelf.Zero, true, (a, v1, v2, i) => a + vector.AngleBetween(v1, v2), (a, c) => a);

    /// <summary>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative if clockwise and positive if counterclockwise.</summary>
    public static TSelf ComputeAreaSigned<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.AggregateTuple2(TSelf.Zero, true, (a, e1, e2, i) => a + ((e1.X * e2.Y - e2.X * e1.Y)), (a, c) => a / (TSelf.One + TSelf.One));
    /// <summary>Compute the surface area of the polygon.</summary>
    public static TSelf ComputeArea<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Abs(ComputeAreaSigned(source));

    /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
    public static CartesianCoordinate3R<TSelf> ComputeCentroid<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.Aggregate(CartesianCoordinate3R<TSelf>.Zero, (acc, e, i) => acc + e, (acc, c) => { var count = TSelf.CreateChecked(c); return new CartesianCoordinate3R<TSelf>(acc.X / count, acc.Y / count, acc.Z / count); });

    /// <summary>Compute the perimeter length of the polygon.</summary>
    public static TSelf ComputePerimeter<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.AggregateTuple2(TSelf.Zero, true, (a, e1, e2, i) => a + CartesianCoordinate3R<TSelf>.EuclideanLength(e2 - e1), (a, c) => a);

    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> GetAngles<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3));
    /// <summary>Returns a sequence triplet angles.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3R<TSelf> v1, CartesianCoordinate3R<TSelf> v2, CartesianCoordinate3R<TSelf> v3, int index, TSelf angle)> GetAnglesEx<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, AngleBetween(v2, v1, v3)));

    /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
    public static System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> GetMidpoints<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.PartitionTuple2(true, (v1, v2, index) =>
      {
        var two = (TSelf.One + TSelf.One);
        return new CartesianCoordinate3R<TSelf>((v1.X + v2.X) / two, (v1.Y + v2.Y) / two, (v1.Z + v2.Z) / two);
      });
    /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
    public static System.Collections.Generic.IEnumerable<(CartesianCoordinate3R<TSelf> v1, CartesianCoordinate3R<TSelf> vm, CartesianCoordinate3R<TSelf> v2, int index)> GetMidpointsEx<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => source.PartitionTuple2(true, (v1, v2, index) =>
      {
        var two = (TSelf.One + TSelf.One);
        return (v1, new CartesianCoordinate3R<TSelf>((v1.X + v2.X) / two, (v1.Y + v2.Y) / two, (v1.Z + v2.Z) / two), v2, index);
      });

    /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
    public static bool IsConvexPolygon<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      bool negative = false, positive = false;

      foreach (var angle in GetAngles(source))
      {
        if (angle < TSelf.Zero)
          negative = true;
        else
          positive = true;

        if (negative && positive)
          return false;
      }

      return negative ^ positive;
    }
    /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
    public static bool IsEquiangularPolygon<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
    public static bool IsEquilateralPolygon<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var e = source.PartitionTuple2(true, (v1, v2, index) => CartesianCoordinate3R<TSelf>.EuclideanLength(v2 - v1)).GetEnumerator();

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitAlongMidpoints<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var midpointPolygon = new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>();

      foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (v1, v2, index) => (v1, v2)))
      {
        midpointPolygon.Add(pair.v1.vm);

        yield return new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>() { pair.v1.v2, pair.v2.vm, pair.v1.vm };
      }

      yield return midpointPolygon;
    }

    /// <summary>Returns a sequence of triangles from the vertices of the polygon. Triangles with a vertex angle greater or equal to 0 degrees and less than 180 degrees are extracted first. Triangles are returned in the order of smallest to largest angle. (2D/3D)</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    /// <remarks>Applicable to any shape with more than 3 vertices.</remarks>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitByTriangulation<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source, Geometry.TriangulationType mode, System.Random rng)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var halfPi = TSelf.Pi / (TSelf.One + TSelf.One);

      var copy = new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>(source);

      (CartesianCoordinate3R<TSelf> v1, CartesianCoordinate3R<TSelf> v2, CartesianCoordinate3R<TSelf> v3, int index, TSelf angle) triplet = default;

      while (copy.Count >= 3)
      {
        triplet = mode switch
        {
          Geometry.TriangulationType.Sequential => System.Linq.Enumerable.First(copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, TSelf.Zero))),
          Geometry.TriangulationType.Randomized => copy.PartitionTuple3(2, (v1, v2, v3, i) => (v1, v2, v3, i, TSelf.Zero)).RandomElement(rng),
          Geometry.TriangulationType.SmallestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle < b.angle ? a : b),
          Geometry.TriangulationType.LargestAngle => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => a.angle > b.angle ? a : b),
          Geometry.TriangulationType.LeastSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => TSelf.Abs(a.angle - halfPi) > TSelf.Abs(b.angle - halfPi) ? a : b),
          Geometry.TriangulationType.MostSquare => System.Linq.Enumerable.Aggregate(GetAnglesEx(copy), (a, b) => TSelf.Abs(a.angle - halfPi) < TSelf.Abs(b.angle - halfPi) ? a : b),
          _ => throw new System.Exception(),
        };
        yield return new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>() { triplet.v2, triplet.v3, triplet.v1 };

        copy.RemoveAt((triplet.index + 1) % copy.Count);
      }
    }

    /// <summary>Returns a new set of quadrilaterals from the polygon centroid to its midpoints and their corresponding original vertex. Method 5 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitCentroidToMidpoints<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of triangles from the polygon centroid to its points. Method 3 and 10 in link.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitCentroidToVertices<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

    /// <summary>Returns a new set of polygons by splitting the polygon at two points. Method 2 in link when odd number of vertices. method 9 in link when even number of vertices.</summary>
    /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitInHalf<TSelf>(this System.Collections.Generic.IEnumerable<CartesianCoordinate3R<TSelf>> source)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var polygon1 = new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>();
      var polygon2 = new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>();

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
        var midpoint = CartesianCoordinate3R<TSelf>.Nlerp(polygon1[^1], polygon2[0], TSelf.One / (TSelf.One + TSelf.One));

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
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>> SplitVertexToVertices<TSelf>(this System.Collections.Generic.IList<CartesianCoordinate3R<TSelf>> source, int index)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var vertex = source[index];

      var startIndex = index + 1;
      var count = startIndex + source.Count - 2;

      for (var i = startIndex; i < count; i++)
      {
        yield return new System.Collections.Generic.List<CartesianCoordinate3R<TSelf>>() { vertex, source[i % source.Count], source[(i + 1) % source.Count] };
      }
    }

    //public static CartesianCoordinate3R ToCartesianCoordinate3(this CartesianCoordinate3I source)
    //  => new(source.X, source.Y, source.Z);
    //public static CartesianCoordinate3R ToCartesianCoordinate3(this System.Numerics.Vector3 source)
    //  => new(source.X, source.Y, source.Z);
    //public static CartesianCoordinate3I ToPoint3(this CartesianCoordinate3R source, System.Func<double, double> transformSelector)
    //  => new(System.Convert.ToInt32(transformSelector(source.X)), System.Convert.ToInt32(transformSelector(source.Y)), System.Convert.ToInt32(transformSelector(source.Z)));
    //public static CartesianCoordinate3I ToPoint3(this CartesianCoordinate3R source, HalfRounding behavior)
    //  => new(System.Convert.ToInt32(Maths.Round(source.X, behavior)), System.Convert.ToInt32(Maths.Round(source.Y, behavior)), System.Convert.ToInt32(Maths.Round(source.Z, behavior)));
    //public static System.Numerics.Vector3 ToVector3(this CartesianCoordinate3R source)
    //  => new((float)source.X, (float)source.Y, (float)source.Z);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct CartesianCoordinate3R<TSelf>
    : System.IEquatable<CartesianCoordinate3R<TSelf>>, System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>, System.Numerics.IModulusOperators<CartesianCoordinate3R<TSelf>, CartesianCoordinate3R<TSelf>, CartesianCoordinate3R<TSelf>>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
  {
    private readonly TSelf m_x;
    private readonly TSelf m_y;
    private readonly TSelf m_z;

    public CartesianCoordinate3R(TSelf x, TSelf y, TSelf z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    [System.Diagnostics.Contracts.Pure] public TSelf X { get => m_x; init => m_x = value; }
    [System.Diagnostics.Contracts.Pure] public TSelf Y { get => m_y; init => m_y = value; }
    [System.Diagnostics.Contracts.Pure] public TSelf Z { get => m_z; init => m_z = value; }

    public System.Numerics.Vector3 ToVector3()
    {
      TSelf.TryConvertToSaturating<float>(m_x, out var x);
      TSelf.TryConvertToSaturating<float>(m_y, out var y);
      TSelf.TryConvertToSaturating<float>(m_z, out var z);

      return new(x, y, x);
    }

    #region Static methods
    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf AngleBetween(CartesianCoordinate3R<TSelf> a, CartesianCoordinate3R<TSelf> b)
      => TSelf.Acos(TSelf.Clamp(DotProduct(a, b) / (EuclideanLength(a) * EuclideanLength(b)), -TSelf.One, TSelf.One));

    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> CrossProduct(CartesianCoordinate3R<TSelf> a, CartesianCoordinate3R<TSelf> b)
      => new(a.m_y * b.m_z - a.m_z * b.m_y, a.m_z * b.m_x - a.m_x * b.m_z, a.m_x * b.m_y - a.m_y * b.m_x);

    /// <summary>Returns the dot product of two 3D vectors.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf DotProduct(CartesianCoordinate3R<TSelf> a, CartesianCoordinate3R<TSelf> b)
      => a.m_x * b.m_x + a.m_y * b.m_y + a.m_z * b.m_z;

    /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ChebyshevDistance(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target, TSelf edgeLength)
      => ChebyshevLength(target - source, edgeLength);
    /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ChebyshevLength(CartesianCoordinate3R<TSelf> source, TSelf edgeLength)
      => GenericMath.Max(TSelf.Abs(source.m_x / edgeLength), TSelf.Abs(source.m_y / edgeLength), TSelf.Abs(source.m_z / edgeLength));

    /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf EuclideanDistance(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target)
      => EuclideanLength(target - source);
    /// <summary>Compute the Euclidean length of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf EuclideanLength(CartesianCoordinate3R<TSelf> source)
      => TSelf.Sqrt(EuclideanLengthSquared(source));
    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf EuclideanLengthSquared(CartesianCoordinate3R<TSelf> source)
      => source.m_x * source.m_x + source.m_y * source.m_y + source.m_z * source.m_z;

    /// <summary>Create a new instance of <see cref="CartesianCoordinate3R{TSelf}"/> from the specified <see cref="System.Numerics.Vector3"/>.</summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> From(System.Numerics.Vector3 vector)
    {
      TSelf.TryConvertFromSaturating<float>(vector.X, out var x);
      TSelf.TryConvertFromSaturating<float>(vector.Y, out var y);
      TSelf.TryConvertFromSaturating<float>(vector.Z, out var z);

      return new(x, y, z);
    }

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> Lerp(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target, TSelf mu)
    {
      var imu = TSelf.One - mu;

      return new(source.m_x * imu + target.m_x * mu, source.m_y * imu + target.m_y * mu, source.m_z * imu + target.m_z * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ManhattanDistance(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target, TSelf edgeLength)
      => ManhattanLength(target - source, edgeLength);
    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ManhattanLength(CartesianCoordinate3R<TSelf> source, TSelf edgeLength)
      => TSelf.Abs(source.m_x / edgeLength) + TSelf.Abs(source.m_y / edgeLength) + TSelf.Abs(source.m_z / edgeLength);

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> Nlerp(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target, TSelf mu)
      => Normalize(Lerp(source, target, mu));

    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> Normalize(CartesianCoordinate3R<TSelf> source)
      => EuclideanLength(source) is var m && !TSelf.IsZero(m) ? new(source.m_x / m, source.m_y / m, source.m_z / m) : source;

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    [System.Diagnostics.Contracts.Pure]
    public static int OrthantNumber(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => source.m_z >= center.m_z ? (source.m_y >= center.m_y ? (source.m_x >= center.m_x ? 0 : 1) : (source.m_x >= center.m_x ? 3 : 2)) : (source.m_y >= center.m_y ? (source.m_x >= center.m_x ? 7 : 6) : (source.m_x >= center.m_x ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (source.m_x >= center.m_x ? 0 : 1) + (source.m_y >= center.m_y ? 0 : 2) + (source.m_z >= center.m_z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (source.m_x < center.m_x ? 0 : 1) + (source.m_y < center.m_y ? 0 : 2) + (source.m_z < center.m_z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> Orthogonal(CartesianCoordinate3R<TSelf> source)
      => TSelf.Abs(source.m_x) > TSelf.Abs(source.m_z) ? new CartesianCoordinate3R<TSelf>(-source.m_y, source.m_x, TSelf.Zero) : new CartesianCoordinate3R<TSelf>(TSelf.Zero, -source.m_x, source.m_y);

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ScalarTripleProduct(CartesianCoordinate3R<TSelf> a, CartesianCoordinate3R<TSelf> b, CartesianCoordinate3R<TSelf> c)
      => DotProduct(a, CrossProduct(b, c));

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> Slerp(CartesianCoordinate3R<TSelf> source, CartesianCoordinate3R<TSelf> target, TSelf mu)
    {
      var dp = TSelf.Clamp(DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
      var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
      var cos = TSelf.Cos(theta);
      var sin = TSelf.Sin(theta);

      return new(source.m_x * cos + (target.m_x - source.m_x) * dp * sin, source.m_y * cos + (target.m_y - source.m_y) * dp * sin, source.m_z * cos + (target.m_z - source.m_z) * dp * sin);
    }

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    [System.Diagnostics.Contracts.Pure]
    public static CartesianCoordinate3R<TSelf> VectorTripleProduct(CartesianCoordinate3R<TSelf> a, CartesianCoordinate3R<TSelf> b, CartesianCoordinate3R<TSelf> c)
      => CrossProduct(a, CrossProduct(b, c));

    #endregion Static methods

    #region Overloaded operators
    public static CartesianCoordinate3R<TSelf> operator +(CartesianCoordinate3R<TSelf> value)
      => new(value.X, value.Y, value.Z);

    public static CartesianCoordinate3R<TSelf> operator +(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static CartesianCoordinate3R<TSelf> operator -(CartesianCoordinate3R<TSelf> value)
      => new(-value.X, -value.Y, -value.Z);

    public static CartesianCoordinate3R<TSelf> operator -(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static CartesianCoordinate3R<TSelf> operator ++(CartesianCoordinate3R<TSelf> value)
      => new(value.X + TSelf.One, value.Y + TSelf.One, value.Z + TSelf.One);

    public static CartesianCoordinate3R<TSelf> operator --(CartesianCoordinate3R<TSelf> value)
      => new(value.X - TSelf.One, value.Y - TSelf.One, value.Z - TSelf.One);

    public static CartesianCoordinate3R<TSelf> operator *(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    public static CartesianCoordinate3R<TSelf> operator /(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

    public static CartesianCoordinate3R<TSelf> operator %(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => new(left.X % right.X, left.Y % right.Y, left.Z % right.Z);

    public static bool operator ==(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => left.Equals(right);
    public static bool operator !=(CartesianCoordinate3R<TSelf> left, CartesianCoordinate3R<TSelf> right)
      => !left.Equals(right);

    //[System.Diagnostics.Contracts.Pure] public static explicit operator CartesianCoordinate3RG(System.ValueTuple<double, double, double> vt3) => new(vt3.Item1, vt3.Item2, vt3.Item3);
    //[System.Diagnostics.Contracts.Pure] public static explicit operator System.ValueTuple<double, double, double>(CartesianCoordinate3RG cc3) => new(cc3.X, cc3.Y, cc3.Z);

    //[System.Diagnostics.Contracts.Pure] public static explicit operator CartesianCoordinate3RG(double[] v) => new(v[0], v[1], v[2]);
    //[System.Diagnostics.Contracts.Pure] public static explicit operator double[](CartesianCoordinate3RG v) => new double[] { v.m_x, v.m_y, v.m_z };

    //[System.Diagnostics.Contracts.Pure] public static bool operator ==(CartesianCoordinate3RG a, CartesianCoordinate3RG b) => a.Equals(b);
    //[System.Diagnostics.Contracts.Pure] public static bool operator !=(CartesianCoordinate3RG a, CartesianCoordinate3RG b) => !a.Equals(b);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator -(CartesianCoordinate3RG cc) => new(-cc.X, -cc.Y, -cc.Z);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator --(CartesianCoordinate3RG cc) => cc - 1;
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator ++(CartesianCoordinate3RG cc) => cc + 1;

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator +(CartesianCoordinate3RG cc1, CartesianCoordinate3RG cc2) => new(cc1.X + cc2.X, cc1.Y + cc2.Y, cc1.Z + cc2.Z);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator +(CartesianCoordinate3RG cc, double scalar) => new(cc.X + scalar, cc.Y + scalar, cc.Z + scalar);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator +(double scalar, CartesianCoordinate3RG cc) => new(scalar + cc.X, scalar + cc.Y, scalar + cc.Z);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator -(CartesianCoordinate3RG cc1, CartesianCoordinate3RG cc2) => new(cc1.X - cc2.X, cc1.Y - cc2.Y, cc1.Z - cc2.Z);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator -(CartesianCoordinate3RG cc, double scalar) => new(cc.X - scalar, cc.Y - scalar, cc.Z - scalar);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator -(double scalar, CartesianCoordinate3RG cc) => new(scalar - cc.X, scalar - cc.Y, scalar - cc.Z);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator *(CartesianCoordinate3RG cc1, CartesianCoordinate3RG cc2) => new(cc1.X * cc2.X, cc1.Y * cc2.Y, cc1.Z * cc2.Z);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator *(CartesianCoordinate3RG cc, double scalar) => new(cc.X * scalar, cc.Y * scalar, cc.Z * scalar);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator *(double scalar, CartesianCoordinate3RG cc) => new(scalar * cc.X, scalar * cc.Y, scalar * cc.Z);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator /(CartesianCoordinate3RG cc1, CartesianCoordinate3RG cc2) => new(cc1.X / cc2.X, cc1.Y / cc2.Y, cc1.Z / cc2.Z);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator /(CartesianCoordinate3RG cc, double scalar) => new(cc.X / scalar, cc.Y / scalar, cc.Z / scalar);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator /(double scalar, CartesianCoordinate3RG cc) => new(scalar / cc.X, scalar / cc.Y, scalar / cc.Z);

    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator %(CartesianCoordinate3RG cc1, CartesianCoordinate3RG cc2) => new(cc1.X % cc2.X, cc1.Y % cc2.Y, cc1.Z % cc2.Z);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator %(CartesianCoordinate3RG cc, double scalar) => new(cc.X % scalar, cc.Y % scalar, cc.Z % scalar);
    //[System.Diagnostics.Contracts.Pure] public static CartesianCoordinate3RG operator %(double scalar, CartesianCoordinate3RG cc) => new(scalar % cc.X, scalar % cc.Y, scalar % cc.Z);

    //public static CartesianCoordinate3RG operator +(CartesianCoordinate3RG value)
    //{
    //  throw new NotImplementedException();
    //}
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(CartesianCoordinate3R<TSelf> other) => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;

    // INumberBase<>
    public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> One => new(TSelf.One, TSelf.One, TSelf.One);
    public static int Radix => 2;
    public static CartesianCoordinate3R<TSelf> Zero => new(TSelf.Zero, TSelf.Zero, TSelf.Zero);
    public static CartesianCoordinate3R<TSelf> AdditiveIdentity => Zero;
    public static CartesianCoordinate3R<TSelf> MultiplicativeIdentity => One;
    public static CartesianCoordinate3R<TSelf> Abs(CartesianCoordinate3R<TSelf> value) => new(TSelf.Abs(value.X), TSelf.Abs(value.Y), TSelf.Abs(value.Z));
    public static bool IsCanonical(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsComplexNumber(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsEvenInteger(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsFinite(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsImaginaryNumber(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsInfinity(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsInteger(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsNaN(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsNegative(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsNegativeInfinity(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsNormal(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsOddInteger(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsPositive(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsPositiveInfinity(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsRealNumber(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsSubnormal(CartesianCoordinate3R<TSelf> value) => false;
    public static bool IsZero(CartesianCoordinate3R<TSelf> value) => value == Zero;
    public static CartesianCoordinate3R<TSelf> MaxMagnitude(CartesianCoordinate3R<TSelf> x, CartesianCoordinate3R<TSelf> y) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> MaxMagnitudeNumber(CartesianCoordinate3R<TSelf> x, CartesianCoordinate3R<TSelf> y) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> MinMagnitude(CartesianCoordinate3R<TSelf> x, CartesianCoordinate3R<TSelf> y) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> MinMagnitudeNumber(CartesianCoordinate3R<TSelf> x, CartesianCoordinate3R<TSelf> y) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider provider) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider provider) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> Parse(ReadOnlySpan<char> s, IFormatProvider provider) => throw new NotImplementedException();
    public static CartesianCoordinate3R<TSelf> Parse(string s, IFormatProvider provider) => throw new NotImplementedException();
    public static bool TryParse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider provider, out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string s, System.Globalization.NumberStyles style, IFormatProvider provider, out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    public static bool TryParse([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] string s, IFormatProvider provider, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertFromChecked<TOther>(TOther value, out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertFromSaturating<TOther>(TOther value, out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertFromTruncating<TOther>(TOther value, out CartesianCoordinate3R<TSelf> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertToChecked<TOther>(CartesianCoordinate3R<TSelf> value, out TOther result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertToSaturating<TOther>(CartesianCoordinate3R<TSelf> value, out TOther result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<CartesianCoordinate3R<TSelf>>.TryConvertToTruncating<TOther>(CartesianCoordinate3R<TSelf> value, out TOther result) => throw new NotImplementedException();
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object obj) => obj is CartesianCoordinate3R<TSelf> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_x, m_y, m_z);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ X = {m_x}, Y = {m_y}, Z = {m_z} }}";
    #endregion Object overrides
  }
}
#endif
