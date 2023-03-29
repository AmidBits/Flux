namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
    public static TSelf AbsoluteSum<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X) + TSelf.Abs(source.Y) + TSelf.Abs(source.Z);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static TSelf AngleTo<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> a, Numerics.ICartesianCoordinate3<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.Clamp(Numerics.ICartesianCoordinate3<TSelf>.DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -TSelf.One, TSelf.One));

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static TSelf ChebyshevLength<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(
        TSelf.Abs(source.X / edgeLength),
        TSelf.Max(
          TSelf.Abs(source.Y / edgeLength),
          TSelf.Abs(source.Z / edgeLength)
        )
      );

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static TSelf DotProductEx<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> a, Numerics.ICartesianCoordinate3<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => Numerics.ICartesianCoordinate3<TSelf>.DotProduct(a, b) / TSelf.Sqrt(a.EuclideanLengthSquared() * b.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static TSelf EuclideanLength<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
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
    public static Numerics.CartesianCoordinate3<TSelf> Lerp<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, Numerics.ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var imu = TSelf.One - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu, source.Z * imu + target.Z * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static TSelf ManhattanLength<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X / edgeLength) + TSelf.Abs(source.Y / edgeLength) + TSelf.Abs(source.Z / edgeLength);

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Numerics.CartesianCoordinate3<TSelf> Nlerp<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, Numerics.ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => Lerp(source, target, mu).Normalized();

    /// <summary>Creates a new normalized <see cref="Numerics.CartesianCoordinate2{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate2{TSelf}"/>.</summary>
    public static Numerics.CartesianCoordinate3<TSelf> Normalized<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => source.EuclideanLength() is var m && m != TSelf.Zero ? new Numerics.CartesianCoordinate3<TSelf>(source.X, source.Y, source.Z) / m : new(source.X, source.Y, source.Z);

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, Numerics.ICartesianCoordinate3<TSelf> center, OrthantNumbering numbering)
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
    public static Numerics.CartesianCoordinate3<TSelf> Orthogonal<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
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
    public static Numerics.CartesianCoordinate3<TSelf> Slerp<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, Numerics.ICartesianCoordinate3<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var dp = TSelf.Clamp(Numerics.ICartesianCoordinate3<TSelf>.DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
      var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
      var (sin, cos) = TSelf.SinCos(theta);

      return new(source.X * cos + (target.X - source.X) * dp * sin, source.Y * cos + (target.Y - source.Y) * dp * sin, source.Z * cos + (target.Z - source.Z) * dp * sin);
    }

    /// <summary>Creates a new <see cref="Numerics.CartesianCoordinate2{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/> using the X and Y.</summary>
    public static Numerics.CartesianCoordinate2<TSelf> ToCartesianCoordinate2XY<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(source.X, source.Y);

    /// <summary>Creates a new <see cref="Numerics.CartesianCoordinate3{TResult}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/> using a <see cref="INumberRoundable{TSelf, TSelf}"/>.</summary>
    public static Numerics.CartesianCoordinate3<TResult> ToCartesianCoordinate3<TSelf, TResult>(this Numerics.ICartesianCoordinate3<TSelf> source, RoundingMode mode, out Numerics.CartesianCoordinate3<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
      => result = new(TResult.CreateChecked(Rounding<TSelf>.Round(source.X, mode)), TResult.CreateChecked(Rounding<TSelf>.Round(source.Y, mode)), TResult.CreateChecked(Rounding<TSelf>.Round(source.Z, mode)));

    /// <summary>Creates a new <see cref="Numerics.CartesianCoordinate3{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/>.</summary>
    public static Numerics.CartesianCoordinate4 ToCartesianCoordinate4<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, double w)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(double.CreateChecked(source.X), double.CreateChecked(source.Y), double.CreateChecked(source.Z), w);

    /// <summary>Creates a new <see cref="Numerics.CylindricalCoordinate{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/>.</summary>
    public static Numerics.CylindricalCoordinate<TSelf> ToCylindricalCoordinate<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.Sqrt(source.X * source.X + source.Y * source.Y),
        (TSelf.Atan2(source.Y, source.X) + TSelf.Tau) % TSelf.Tau,
        source.Z
      );

    /// <summary>Creates a new <see cref="Numerics.HexCoordinate{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/>.</summary>
    public static Numerics.HexCoordinate<TSelf> ToHexCoordinate<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        source.X,
        source.Y,
        source.Z
      );

    /// <summary>Returns a quaternion from two vectors.
    /// <para><see href="http://lolengine.net/blog/2014/02/24/quaternion-from-two-vectors-final"/></para>
    /// <para><see href="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/></para>
    /// </summary>
    public static Numerics.Quaternion<TSelf> ToQuaternion<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, Numerics.ICartesianCoordinate3<TSelf> target)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var norm_u_norm_v = TSelf.Sqrt(Numerics.ICartesianCoordinate3<TSelf>.DotProduct(source, source) * Numerics.ICartesianCoordinate3<TSelf>.DotProduct(target, target));
      var real_part = norm_u_norm_v + Numerics.ICartesianCoordinate3<TSelf>.DotProduct(source, target);

      Numerics.CartesianCoordinate3<TSelf> w;

      if (real_part < TSelf.CreateChecked(GenericMath.Epsilon1E7) * norm_u_norm_v)
      {
        real_part = TSelf.Zero;

        // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
        w = TSelf.Abs(source.X) > TSelf.Abs(source.Z) ? new Numerics.CartesianCoordinate3<TSelf>(-source.Y, source.X, TSelf.Zero) : new Numerics.CartesianCoordinate3<TSelf>(TSelf.Zero, -source.Z, source.Y);
      }
      else
      {
        w = Numerics.ICartesianCoordinate3<TSelf>.CrossProduct(source, target);
      }

      return new Numerics.Quaternion<TSelf>(w.X, w.Y, w.Z, real_part).Normalized();
    }

    /// <summary>Creates a new <see cref="Numerics.SphericalCoordinate{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate3{TSelf}"/>.</summary>
    public static Numerics.SphericalCoordinate<TSelf> ToSphericalCoordinate<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      var x2y2 = source.X * source.X + source.Y * source.Y;

      return new(
        TSelf.Sqrt(x2y2 + source.Z * source.Z),
        TSelf.Atan2(TSelf.Sqrt(x2y2), source.Z) + TSelf.Pi,
        TSelf.Atan2(source.Y, source.X) + TSelf.Pi
      );
    }

    //public static Vector4 ToVector4<TSelf>(this ICartesianCoordinate3<TSelf> source, double w = 0)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => new(double.CreateChecked(source.X), double.CreateChecked(source.Y), double.CreateChecked(source.Z), w);

    /// <summary>Converts the <see cref="Point3"/> to a <see cref="System.Numerics.Vector3"/>.</summary>
    public static System.Numerics.Vector3 ToVector3<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(float.CreateChecked(source.X), float.CreateChecked(source.Y), float.CreateChecked(source.Z));

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256{double}"/> with the cartesian values as vector elements [X, Y, Z, <paramref name="w"/>].</summary>
    public static System.Runtime.Intrinsics.Vector256<double> ToVector256<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source, TSelf w)
      where TSelf : System.Numerics.INumber<TSelf>
      => System.Runtime.Intrinsics.Vector256.Create(double.CreateChecked(source.X), double.CreateChecked(source.Y), double.CreateChecked(source.Z), double.CreateChecked(w));

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256{double}"/> with the cartesian values as vector elements [X, Y, Z, 0].</summary>
    public static System.Runtime.Intrinsics.Vector256<double> ToVector256<TSelf>(this Numerics.ICartesianCoordinate3<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.ToVector256(TSelf.Zero);
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>A 3D cartesian coordinate.</summary>
    public interface ICartesianCoordinate3<TSelf>
      : ICartesianCoordinate<TSelf>/*, System.IFormattable*/
      where TSelf : System.Numerics.INumber<TSelf>
    {
      TSelf X { get; }
      TSelf Y { get; }
      TSelf Z { get; }

      /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
      static CartesianCoordinate3<TSelf> CrossProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b) => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

      /// <summary>Returns the dot product of two normalized 3D vectors.</summary>
      static TSelf DotProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

      /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
      static TSelf ScalarTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c) => DotProduct(a, CrossProduct(b, c));

      /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
      static CartesianCoordinate3<TSelf> VectorTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c) => CrossProduct(a, CrossProduct(b, c));

      //string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      //  => $"{GetType().GetNameEx()} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)} }}";
    }
  }
}
