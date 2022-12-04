namespace Flux
{
  #region ExtensionMethods
  public static partial class CoordinateSystems
  {
    public static TSelf AbsoluteSum<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X) + TSelf.Abs(source.Y) + TSelf.Abs(source.Z);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static TSelf AngleTo<TSelf>(this ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.Clamp(ICartesianCoordinate3<TSelf>.DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -TSelf.One, TSelf.One));

    /// <summary>Convert a 'mapped' unique index to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    /// <remarks>An index can be uniquely mapped to 3D cartesian coordinates using a <paramref name="gridWidth"/> and <paramref name="gridHeight"/>. The 3D cartesian coordinates can also be converted back to a unique index with the same grid width and height values.</remarks>
    public static CartesianCoordinate3<TSelf> AsUniqueIndexToCartesianCoordinate3<TSelf>(this TSelf uniqueIndex, TSelf gridWidth, TSelf gridHeight)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var xy = gridWidth * gridHeight;
      var irxy = uniqueIndex % xy;

      return new(
        irxy % gridWidth,
        irxy / gridWidth,
        uniqueIndex / xy
      );
    }

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static TSelf ChebyshevLength<TSelf>(this ICartesianCoordinate3<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => GenericMath.Max(TSelf.Abs(source.X / edgeLength), TSelf.Abs(source.Y / edgeLength), TSelf.Abs(source.Z / edgeLength));

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static TSelf DotProductEx<TSelf>(this ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => ICartesianCoordinate3<TSelf>.DotProduct(a, b) / TSelf.Sqrt(a.EuclideanLengthSquared() * b.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static TSelf EuclideanLength<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.X * source.X + source.Y * source.Y + source.Z * source.Z;

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate3<TSelf> InterpolateLinear<TSelf>(this ICartesianCoordinate3<TSelf> p1, ICartesianCoordinate3<TSelf> p2, TSelf mu, I2NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.LinearInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate2Node(p1.X, p2.X, mu), mode.Interpolate2Node(p1.Y, p2.Y, mu), mode.Interpolate2Node(p1.Z, p2.Z, mu));
    //}

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate3<TSelf> Interpolate<TSelf>(this ICartesianCoordinate3<TSelf> p0, ICartesianCoordinate3<TSelf> p1, ICartesianCoordinate3<TSelf> p2, ICartesianCoordinate3<TSelf> p3, TSelf mu, I4NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.CubicInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu), mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu), mode.Interpolate4Node(p0.Z, p1.Z, p2.Z, p3.Z, mu));
    //}

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static CartesianCoordinate3<TSelf> Lerp<TSelf>(this ICartesianCoordinate3<TSelf> source, ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var imu = TSelf.One - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu, source.Z * imu + target.Z * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static TSelf ManhattanLength<TSelf>(this ICartesianCoordinate3<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X / edgeLength) + TSelf.Abs(source.Y / edgeLength) + TSelf.Abs(source.Z / edgeLength);

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static CartesianCoordinate3<TSelf> Nlerp<TSelf>(this ICartesianCoordinate3<TSelf> source, ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => Lerp(source, target, mu).Normalized();

    /// <summary>Creates a new normalized <see cref="CartesianCoordinate2{TSelf}"/> from a <see cref="ICartesianCoordinate2{TSelf}"/>.</summary>
    public static CartesianCoordinate3<TSelf> Normalized<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => source.EuclideanLength() is var m && m != TSelf.Zero ? source.ToCartesianCoordinate3() / m : source.ToCartesianCoordinate3();

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber<TSelf>(this ICartesianCoordinate3<TSelf> source, ICartesianCoordinate3<TSelf> center, OrthantNumbering numbering)
      where TSelf : System.Numerics.INumber<TSelf>
      => numbering switch
      {
        OrthantNumbering.Traditional => source.Z >= center.Z ? (source.Y >= center.Y ? (source.X >= center.X ? 0 : 1) : (source.X >= center.X ? 3 : 2)) : (source.Y >= center.Y ? (source.X >= center.X ? 7 : 6) : (source.X >= center.X ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (source.X >= center.X ? 0 : 1) + (source.Y >= center.Y ? 0 : 2) + (source.Z >= center.Z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (source.X < center.X ? 0 : 1) + (source.Y < center.Y ? 0 : 2) + (source.Z < center.Z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static CartesianCoordinate3<TSelf> Orthogonal<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X) > TSelf.Abs(source.Z)
      ? new(
          -source.Y,
          source.X,
          TSelf.Zero
        )
      : new(
          TSelf.Zero,
          -source.X,
          source.Y
        );

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static CartesianCoordinate3<TSelf> Slerp<TSelf>(this ICartesianCoordinate3<TSelf> source, ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var dp = TSelf.Clamp(ICartesianCoordinate3<TSelf>.DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
      var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
      var cos = TSelf.Cos(theta);
      var sin = TSelf.Sin(theta);

      return new(source.X * cos + (target.X - source.X) * dp * sin, source.Y * cos + (target.Y - source.Y) * dp * sin, source.Z * cos + (target.Z - source.Z) * dp * sin);
    }

    /// <summary>Converts the <see cref="ICartesianCoordinate3{TSelf}"/> to a <see cref="CartesianCoordinate2{TSelf}"/> from the X and Y coordinates.</summary>
    public static CartesianCoordinate2<TSelf> ToCartesianCoordinate2XY<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.X, source.Y);

    /// <summary>Converts the <see cref="ICartesianCoordinate3{TSelf}"/> to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    public static CartesianCoordinate3<TSelf> ToCartesianCoordinate3<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source is CartesianCoordinate3<TSelf> cc ? cc : new(source.X, source.Y, source.Z);

    /// <summary>Converts the <see cref="ICartesianCoordinate3{TSelf}"/> to a <see cref="CartesianCoordinate3{TResult}"/>.</summary>
    public static CartesianCoordinate3<TResult> ToCartesianCoordinate3<TSelf, TResult>(this ICartesianCoordinate3<TSelf> source, INumberRoundable<TSelf, TSelf> rounding, out CartesianCoordinate3<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
      => result = new(TResult.CreateChecked(rounding.RoundNumber(source.X)), TResult.CreateChecked(rounding.RoundNumber(source.Y)), TResult.CreateChecked(rounding.RoundNumber(source.Z)));

    /// <summary>Converts the <see cref="CartesianCoordinate3"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public static CylindricalCoordinate<TSelf> ToCylindricalCoordinate<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.Sqrt(source.X * source.X + source.Y * source.Y),
        (TSelf.Atan2(source.Y, source.X) + TSelf.Tau) % TSelf.Tau,
        source.Z
      );

    /////// <summary>Returns a quaternion from two vectors.</summary>
    /////// <see cref="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/>
    ////[System.Diagnostics.Contracts.Pure]
    ////public Quaternion ToQuaternion(CartesianCoordinate3 rotatingTo)
    ////  => Quaternion.FromTwoVectors(this, rotatingTo);

    /// <summary>Converts the <see cref="ICartesianCoordinate3{TSelf}"/> to a <see cref="SphericalCoordinate{TSelf}"/>.</summary>
    public static SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var x2y2 = source.X * source.X + source.Y * source.Y;

      return new(
        TSelf.Sqrt(x2y2 + source.Z * source.Z),
        TSelf.Atan2(TSelf.Sqrt(x2y2), source.Z) + TSelf.Pi,
        TSelf.Atan2(source.Y, source.X) + TSelf.Pi
      );
    }

    /// <summary>Converts the <see cref="ICartesianCoordinate3{TSelf}"/> to a 'mapped' unique index.</summary>
    /// <remarks>A 3D cartesian coordinate can be uniquely indexed using a <paramref name="gridWidth"/> and <paramref name="gridHeight"/>. The unique index can also be converted back to a 3D cartesian coordinate with the same grid width and height values.</remarks>
    public static TSelf ToUniqueIndex<TSelf>(this ICartesianCoordinate3<TSelf> source, TSelf gridWidth, TSelf gridHeight)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source.X + (source.Y * gridWidth) + (source.Z * gridWidth * gridHeight);
  }
  #endregion ExtensionMethods

  /// <summary>Cartesian 3D coordinate.</summary>
  public interface ICartesianCoordinate3<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
    TSelf Z { get; }

    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    static CartesianCoordinate3<TSelf> CrossProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
     => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    /// <summary>Returns the dot product of two normalized 3D vectors.</summary>
    static TSelf DotProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
     => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    static TSelf ScalarTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c)
      => DotProduct(a, CrossProduct(b, c));

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    static CartesianCoordinate3<TSelf> VectorTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c)
     => CrossProduct(a, CrossProduct(b, c));

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)} }}";
  }
}
