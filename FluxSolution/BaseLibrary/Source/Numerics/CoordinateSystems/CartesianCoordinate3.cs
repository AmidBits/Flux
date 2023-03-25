namespace Flux
{
  #region ExtensionMethods
  public static partial class CoordinateSystems
  {
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
    //}
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>A 3-dimensional cartesian coordinate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct CartesianCoordinate3<TSelf>
      : System.IFormattable, System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>, ICartesianCoordinate3<TSelf>
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

      public void Deconstruct(out TSelf x, out TSelf y, out TSelf z) { x = m_x; y = m_y; z = m_z; }

      public TSelf X { get => m_x; init => m_x = value; }
      public TSelf Y { get => m_y; init => m_y = value; }
      public TSelf Z { get => m_z; init => m_z = value; }

      #region Static methods

      //public static CartesianCoordinate3<TSelf> ConvertEclipticToEquatorial(CartesianCoordinate3<TSelf> ecliptic, TSelf obliquityOfTheEcliptic)
      //  => Numerics.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, TSelf.Cos(obliquityOfTheEcliptic), -TSelf.Sin(obliquityOfTheEcliptic), 0, 0, TSelf.Sin(obliquityOfTheEcliptic), TSelf.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), ecliptic);
      //public static CartesianCoordinate3<TSelf> ConvertEquatorialToEcliptic(CartesianCoordinate3<TSelf> equatorial, TSelf obliquityOfTheEcliptic)
      //  => Flux.Matrix4.Transform(new Flux.Matrix4(1, 0, 0, 0, 0, TSelf.Cos(obliquityOfTheEcliptic), TSelf.Sin(obliquityOfTheEcliptic), 0, 0, -TSelf.Sin(obliquityOfTheEcliptic), TSelf.Cos(obliquityOfTheEcliptic), 0, 0, 0, 0, 1), equatorial);

      ///// <summary>Create a new random vector using the crypto-grade rng.</summary>
      //public static CartesianCoordinate3<TSelf> FromRandomAbsolute(TSelf toExclusiveX, TSelf toExclusiveY, TSelf toExclusiveZ, System.Random rng)
      //  => new(rng.NextTSelf(toExclusiveX), rng.NextTSelf(toExclusiveY), rng.NextTSelf(toExclusiveZ));
      ///// <summary>Create a new random vector in the range (-toExlusive, toExclusive) using the crypto-grade rng.</summary>
      //public static CartesianCoordinate3<TSelf> FromRandomCenterZero(TSelf toExclusiveX, TSelf toExclusiveY, TSelf toExclusiveZ, System.Random rng)
      //  => new(rng.NextTSelf(-toExclusiveX, toExclusiveX), rng.NextTSelf(-toExclusiveY, toExclusiveY), rng.NextTSelf(-toExclusiveZ, toExclusiveZ));

      #endregion Static methods

      #region Implemented interfaces

      public static explicit operator CartesianCoordinate3<TSelf>(System.ValueTuple<TSelf, TSelf, TSelf> vt) => new(vt.Item1, vt.Item2, vt.Item3);
      public static explicit operator System.ValueTuple<TSelf, TSelf, TSelf>(CartesianCoordinate3<TSelf> cc) => new(cc.X, cc.Y, cc.Z);

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
        if (o.ImplementsNumber())
        {
          var v = TSelf.CreateChecked(o);
          return new(v, v, v);
        }
        else if (o is CartesianCoordinate3<TSelf> cc)
          return new(cc.X, cc.Y, TSelf.Zero);

        throw new System.NotSupportedException();
      }
      public static CartesianCoordinate3<TSelf> CreateSaturating<TOther>(TOther o)
        where TOther : System.Numerics.INumberBase<TOther>
      {
        if (o.ImplementsNumber())
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
        if (o.ImplementsNumber())
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
        result = default!;
        return false;
      }
      static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertToSaturating<TOther>(CartesianCoordinate3<TSelf> value, out TOther result)
      {
        result = default!;
        return false;
      }
      static bool System.Numerics.INumberBase<CartesianCoordinate3<TSelf>>.TryConvertToTruncating<TOther>(CartesianCoordinate3<TSelf> value, out TOther result)
      {
        result = default!;
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

      //// IEquatable<>, System.Numerics.IEqualityOperators<>
      //public bool Equals(CartesianCoordinate3<TSelf> other) => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
      //public static bool operator ==(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => a.Equals(b);
      //public static bool operator !=(CartesianCoordinate3<TSelf> a, CartesianCoordinate3<TSelf> b) => !a.Equals(b);
      //public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CartesianCoordinate3<TSelf> o && Equals(o);
      //public override int GetHashCode() => System.HashCode.Combine(m_x, m_y, m_z);

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

      string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().GetNameEx()} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)} }}";
      #endregion Implemented interfaces

      public override string ToString() => $"<{m_x}, {m_y}, {m_z}>";
    }
  }
}
