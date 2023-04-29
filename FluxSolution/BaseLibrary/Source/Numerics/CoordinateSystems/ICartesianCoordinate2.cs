namespace Flux
{
  #region ExtensionMethods
  public static partial class NumericsExtensionMethods
  {
#if NET7_0_OR_GREATER

    public static TSelf AbsoluteSum<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X) + TSelf.Abs(source.Y);

    /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static TSelf AngleTo<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> a, Numerics.ICartesianCoordinate2<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.Clamp(Numerics.ICartesianCoordinate2<TSelf>.DotProduct(a, b) / (a.EuclideanLength() * b.EuclideanLength()), -TSelf.One, TSelf.One));

    /// <summary>Compute the Chebyshev length of the source vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static TSelf ChebyshevLength<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(TSelf.Abs(source.X / edgeLength), TSelf.Abs(source.Y / edgeLength));

    /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
    /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
    /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
    public static TSelf DotProductEx<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> a, Numerics.ICartesianCoordinate2<TSelf> b)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => Numerics.ICartesianCoordinate2<TSelf>.DotProduct(a, b) / TSelf.Sqrt(a.EuclideanLengthSquared() * b.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public static TSelf EuclideanLength<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(source.EuclideanLengthSquared());

    /// <summary>Compute the Euclidean length squared of the vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.X * source.X + source.Y * source.Y;

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate2<TSelf> InterpolateLinear<TSelf>(this ICartesianCoordinate2<TSelf> p1, ICartesianCoordinate2<TSelf> p2, TSelf mu, I2NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.LinearInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate2Node(p1.X, p2.X, mu), mode.Interpolate2Node(p1.Y, p2.Y, mu));
    //}

    ///// <summary>Creates a new vector by interpolating between the specified vectors and a unit interval [0, 1].</summary>
    //public static CartesianCoordinate2<TSelf> Interpolate<TSelf>(this ICartesianCoordinate2<TSelf> p0, ICartesianCoordinate2<TSelf> p1, ICartesianCoordinate2<TSelf> p2, ICartesianCoordinate2<TSelf> p3, TSelf mu, I4NodeInterpolatable<TSelf, TSelf> mode)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //{
    //  mode ??= new Interpolation.CubicInterpolation<TSelf, TSelf>();

    //  return new(mode.Interpolate4Node(p0.X, p1.X, p2.X, p3.X, mu), mode.Interpolate4Node(p0.Y, p1.Y, p2.Y, p3.Y, mu));
    //}

    /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Numerics.CartesianCoordinate2<TSelf> Lerp<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, Numerics.ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var imu = TSelf.One - mu;

      return new(source.X * imu + target.X * mu, source.Y * imu + target.Y * mu);
    }

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static TSelf ManhattanLength<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(source.X / edgeLength) + TSelf.Abs(source.Y / edgeLength);

    /// <summary>Lerp is a normalized linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
    public static Numerics.CartesianCoordinate2<TSelf> Nlerp<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, Numerics.ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => Lerp(source, target, mu).Normalized();

    /// <summary>Creates a new normalized <see cref="CartesianCoordinate2{TSelf}"/> from a <see cref="ICartesianCoordinate2{TSelf}"/>.</summary>
    public static Numerics.CartesianCoordinate2<TSelf> Normalized<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => source.EuclideanLength() is var m && m != TSelf.Zero ? new Numerics.CartesianCoordinate2<TSelf>(source.X, source.Y) / m : new Numerics.CartesianCoordinate2<TSelf>(source.X, source.Y);

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, Numerics.ICartesianCoordinate2<TSelf> center, OrthantNumbering numbering)
      where TSelf : System.Numerics.INumber<TSelf>
      => numbering switch
      {
        OrthantNumbering.Traditional => source.Y >= center.Y ? (source.X >= center.X ? 0 : 1) : (source.X >= center.X ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (source.X >= center.X ? 0 : 1) + (source.Y >= center.Y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (source.X < center.X ? 0 : 1) + (source.Y < center.Y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static Numerics.CartesianCoordinate2<TSelf> PerpendicularCcw<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        -source.Y,
        source.X
      );

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static Numerics.CartesianCoordinate2<TSelf> PerpendicularCw<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(
        source.Y,
        -source.X
      );

    /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
    public static Numerics.CartesianCoordinate2<TSelf> Slerp<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, Numerics.ICartesianCoordinate2<TSelf> target, TSelf mu)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var dp = TSelf.Clamp(Numerics.ICartesianCoordinate2<TSelf>.DotProduct(source, target), -TSelf.One, TSelf.One); // Ensure precision doesn't exceed acos limits.
      var theta = TSelf.Acos(dp) * mu; // Angle between start and desired.
      var (sin, cos) = TSelf.SinCos(theta);

      return new(source.X * cos + (target.X - source.X) * dp * sin, source.Y * cos + (target.Y - source.Y) * dp * sin);
    }

    /// <summary>Creates a new <see cref="CartesianCoordinate2{TSelf}"/> from a <see cref="Numerics.ICartesianCoordinate2{TResult}"/>.</summary>
    public static Numerics.CartesianCoordinate2<TResult> ToCartesianCoordinate2<TSelf, TResult>(this Numerics.ICartesianCoordinate2<TSelf> source, RoundingMode mode, out Numerics.CartesianCoordinate2<TResult> result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
      => result = new(TResult.CreateChecked(source.X.Round(mode)), TResult.CreateChecked(source.Y.Round(mode)));

    /// <summary>Creates a new <see cref="Numerics.PolarCoordinate{TSelf}"/> from a <see cref=" Numerics.ICartesianCoordinate2{TSelf}"/>.</summary>
    public static Numerics.PolarCoordinate ToPolarCoordinate<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        double.CreateChecked(TSelf.Sqrt(source.X * source.X + source.Y * source.Y)),
        double.CreateChecked(TSelf.Atan2(source.Y, source.X))
      );

    //public static Vector4 ToVector4<TSelf>(this ICartesianCoordinate2<TSelf> source, double z = 0, double w = 0)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => new(double.CreateChecked(source.X), double.CreateChecked(source.Y), z, w);

    /// <summary>Converts the <see cref="Numerics.CartesianCoordinate2{TSelf}"/> to a <see cref="System.Numerics.Vector2"/>.</summary>
    public static System.Numerics.Vector2 ToVector2<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => new(float.CreateChecked(source.X), float.CreateChecked(source.Y));

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector128{double}"/> with the cartesian values as vector elements [X, Y].</summary>
    public static System.Runtime.Intrinsics.Vector128<double> ToVector128<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => System.Runtime.Intrinsics.Vector128.Create(double.CreateChecked(source.X), double.CreateChecked(source.Y));

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256{double}"/> with the cartesian values as vector elements [X, Y, <paramref name="z"/>, <paramref name="w"/>].</summary>
    public static System.Runtime.Intrinsics.Vector256<double> ToVector256<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source, TSelf z, TSelf w)
      where TSelf : System.Numerics.INumber<TSelf>
      => System.Runtime.Intrinsics.Vector256.Create(double.CreateChecked(source.X), double.CreateChecked(source.Y), double.CreateChecked(z), double.CreateChecked(w));

    /// <summary>Creates a new intrinsic vector <see cref="System.Runtime.Intrinsics.Vector256{double}"/> with the cartesian values as vector elements [X, Y, 0, 0].</summary>
    public static System.Runtime.Intrinsics.Vector256<double> ToVector256<TSelf>(this Numerics.ICartesianCoordinate2<TSelf> source)
      where TSelf : System.Numerics.INumber<TSelf>
      => source.ToVector256(TSelf.Zero, TSelf.Zero);
#else

    /// <summary>Creates a new <see cref="Numerics.PolarCoordinate{TSelf}"/> from a <see cref=" Numerics.ICartesianCoordinate2{TSelf}"/>.</summary>
    public static Numerics.IPolarCoordinate ToPolarCoordinate(this Numerics.ICartesianCoordinate2<double> source)
      => new Numerics.PolarCoordinate(
        System.Math.Sqrt(source.X * source.X + source.Y * source.Y),
        System.Math.Atan2(source.Y, source.X)
      );

#endif
  }
  #endregion ExtensionMethods

  namespace Numerics
  {
    /// <summary>A 2D cartesian coordinate.</summary>
    public interface ICartesianCoordinate2<TSelf>
      : ICartesianCoordinate<TSelf>
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.INumber<TSelf>
#endif
    {
      TSelf X { get; }
      TSelf Y { get; }

#if NET7_0_OR_GREATER
      /// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
      static TSelf CrossProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b) => a.X * b.Y - a.Y * b.X;

      /// <summary>Returns the dot product of two normalized 2D vectors.</summary>
      static TSelf DotProduct(ICartesianCoordinate2<TSelf> a, ICartesianCoordinate2<TSelf> b) => a.X * b.X + a.Y * b.Y;
#endif
    }
  }
}
