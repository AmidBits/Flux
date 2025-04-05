using System.Runtime.Intrinsics;

namespace Flux
{
  public static partial class Intrinsics
  {
    //internal const byte ShuffleYXYX = (0 << 6) | (1 << 4) | (0 << 2) | 1;
    internal const byte ShuffleYZXW = (3 << 6) | (0 << 4) | (2 << 2) | 1;
    internal const byte ShuffleZXYW = (3 << 6) | (1 << 4) | (0 << 2) | 2;

    public static Vector128<double> One128D => Vector128.Create(1d);
    public static Vector256<double> One256D => Vector256.Create(1d);
#if NET8_0_OR_GREATER
    public static Vector512<double> One512D => Vector512.Create(1d);
#endif

#if NET8_0_OR_GREATER
    public static Vector512<long> One512I64 => Vector512.Create(1L);
#endif

    public static Vector128<double> Two128D => Vector128.Create(2d);
    public static Vector256<double> Two256D => Vector256.Create(2d);
    //    public static Vector512<double> Two512D => Vector512.Create(2L).AsDouble();

    public static Vector128<double> Zero128D => Vector128.Create(0L).AsDouble();
    public static Vector256<double> Zero256D => Vector256.Create(0L).AsDouble();
    //public static Vector512<double> Zero512D => Vector512.Create(0L).AsDouble();

    //public static Vector128<long> SignMask128Int64 => Vector128.Create(~long.MaxValue);
    //public static Vector256<long> SignMask256Int64 => Vector256.Create(~long.MaxValue);
#if NET8_0_OR_GREATER
    public static Vector512<long> SignMask512Int64 => Vector512.Create(~long.MaxValue);
#endif

    //public static Vector128<long> AbsMask128Int64 => Vector128.Create(long.MaxValue);
    //public static Vector256<long> AbsMask256Int64 => Vector256.Create(long.MaxValue);
#if NET8_0_OR_GREATER
    public static Vector512<long> AbsMask512Int64 => Vector512.Create(long.MaxValue);
#endif

    public static Vector128<double> AbsMask128Double => Vector128.Create(long.MaxValue).AsDouble();
    public static Vector256<double> AbsMask256Double => Vector256.Create(long.MaxValue).AsDouble();
    //    public static Vector512<double> AbsMask512Double => Vector512.Create(long.MaxValue).AsDouble();

    public static Vector128<double> SignMask128Double => Vector128.Create(~long.MaxValue).AsDouble();
    public static Vector256<double> SignMask256Double => Vector256.Create(~long.MaxValue).AsDouble();
#if NET8_0_OR_GREATER
    public static Vector512<double> SignMask512Double => Vector512.Create(~long.MaxValue).AsDouble();
#endif

    public static Vector256<double> BitMask256DoubleX1Y1Z1W0 => Vector256.Create(-1, -1, -1, +0).AsDouble();
    public static Vector256<double> BitMask256DoubleX1Y1Z0W0 => Vector256.Create(-1, -1, +0, +0).AsDouble();

    public static Vector256<double> UnitX256D => Vector256.Create(1d, 0d, 0d, 0d);
    public static Vector256<double> UnitY256D => Vector256.Create(0d, 1d, 0d, 0d);
    public static Vector256<double> UnitZ256D => Vector256.Create(0d, 0d, 1d, 0d);
    public static Vector256<double> UnitW256D => Vector256.Create(0d, 0d, 0d, 1d);

    #region Abs

    /// <summary>Returns the vector with absolute element values.</summary>
    public static Vector128<double> Abs(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.And(source, AbsMask128Double)
      : source & AbsMask128Double;

    /// <summary>Returns the vector with absolute element values.</summary>
    public static Vector256<double> Abs(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.And(source, AbsMask256Double)
      : Vector256.Create(Abs(source.GetLower()), Abs(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns the vector with absolute element values.</summary>
    public static Vector512<double> Abs(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.And(source.AsInt64(), AbsMask512Int64).AsDouble()
      : Vector512.Create(Abs(source.GetLower()), Abs(source.GetUpper()));

#endif

    #endregion // Abs

    public static double AbsoluteSum(this Vector128<double> source) => HorizontalSum(Abs(source));
    public static double AbsoluteSum(this Vector256<double> source) => HorizontalSum(Abs(source));

    #region Add

    /// <summary>Returns a new vector with the sum of the vector components.</summary>
    public static Vector128<double> Add(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Add(source, target)
      : Vector128.Create(source[0] + target[0], source[1] + target[1]);

    /// <summary>Returns a new vector with the sum of the vector components.</summary>
    public static Vector256<double> Add(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Add(source, target)
      : Vector256.Create(Add(source.GetLower(), target.GetLower()), Add(source.GetUpper(), target.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the sum of the vector components.</summary>
    public static Vector512<double> Add(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Add(source, target)
      : Vector512.Create(Add(source.GetLower(), target.GetLower()), Add(source.GetUpper(), target.GetUpper()));

#endif

    /// <summary>Returns a new vector with the sum of each vector components and the scalar value.</summary>
    public static Vector128<double> Add(this Vector128<double> source, double scalar) => source.Add(Vector128.Create(scalar));

    /// <summary>Returns a new vector with the sum of each vector components and the scalar value.</summary>
    public static Vector256<double> Add(this Vector256<double> source, double scalar) => source.Add(Vector256.Create(scalar));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the sum of each vector components and the scalar value.</summary>
    public static Vector512<double> Add(this Vector512<double> source, double scalar) => source.Add(Vector512.Create(scalar));

#endif

    #endregion // Add

    #region Angles

    /// <summary>Returns the angle of the source to the other vectors.</summary>>
    public static double AngleBetween(this Vector128<double> source, Vector128<double> before, Vector128<double> after) => AngleTo(Subtract(before, source), Subtract(after, source));

    /// <summary>Returns the angle of the source to the other vectors.</summary>>
    public static double AngleBetween(this Vector256<double> source, Vector256<double> before, Vector256<double> after) => AngleTo(Subtract(before, source), Subtract(after, source));

    /// <summary>(3D) Calculate the angle between the source vector and the target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this Vector128<double> source, Vector128<double> target) => double.Acos(Dot(Normalize(source, out var _), Normalize(target, out var _)));

    /// <summary>(3D) Calculate the angle (in radians) between the source vector and the target vector.
    /// When dot eq 0 then the vectors are perpendicular.
    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
    /// </summary>
    public static double AngleTo(this Vector256<double> source, Vector256<double> target) => double.Acos(Dot(Normalize(source, out var _), Normalize(target, out var _)));

    #endregion // Angles

    #region Ceiling

    /// <summary>Returns a new vector with the smallest integer value that is greater than or equal to the source, for each component.</summary>
    public static Vector128<double> Ceiling(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.Ceiling(source)
      : Vector128.Create(double.Ceiling(source[0]), double.Ceiling(source[1]));

    /// <summary>Returns a new vector with the smallest integer value that is greater than or equal to the source, for each component.</summary>
    public static Vector256<double> Ceiling(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Ceiling(source)
      : Vector256.Create(Ceiling(source.GetLower()), Ceiling(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the smallest integer value that is greater than or equal to the source, for each component.</summary>
    public static Vector512<double> Ceiling(this Vector512<double> source)
      => Vector512.Create(Ceiling(source.GetLower()), Ceiling(source.GetUpper()));

#endif

    #endregion // Ceiling

    #region ChebyshevLength

    /// <summary>Returns a new vector with the Chebyshev length (using the specified edgeLength) of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(this Vector128<double> source, double edgeLength = 1) => HorizontalMax(Divide(Abs(source), edgeLength));

    /// <summary>Returns a new vector with the Chebyshev length (using the specified edgeLength) of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(this Vector256<double> source, double edgeLength = 1) => HorizontalMax(Divide(Abs(source), edgeLength));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the Chebyshev length (using the specified edgeLength) of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevLength(this Vector512<double> source, double edgeLength = 1) => HorizontalMax(Divide(Abs(source), edgeLength));

#endif

    #endregion // ChebyshevLength

    #region Clamp

    /// <summary>Returns a new vector with its components clamped between the corresponding components in min and max.</summary>
    public static Vector128<double> Clamp(this Vector128<double> source, Vector128<double> min, Vector128<double> max)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Max(System.Runtime.Intrinsics.X86.Sse2.Min(source, max), min)
      : Vector128.Create(double.Clamp(source[0], min[0], max[0]), double.Clamp(source[1], min[1], max[1]));

    /// <summary>Returns a new vector with its components clamped between the corresponding components in min and max.</summary>
    public static Vector256<double> Clamp(this Vector256<double> source, Vector256<double> min, Vector256<double> max)
      // We must follow HLSL behavior in the case user specified min value is bigger than max value.
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Max(System.Runtime.Intrinsics.X86.Avx.Min(source, max), min)
      : Vector256.Create(Clamp(source.GetLower(), min.GetLower(), max.GetLower()), Clamp(source.GetUpper(), min.GetUpper(), max.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with its components clamped between the corresponding components in min and max.</summary>
    public static Vector512<double> Clamp(this Vector512<double> source, Vector512<double> min, Vector512<double> max)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Max(System.Runtime.Intrinsics.X86.Avx512F.Min(source, max), min)
      : Vector512.Create(Clamp(source.GetLower(), min.GetLower(), max.GetLower()), Clamp(source.GetUpper(), min.GetUpper(), max.GetUpper()));

#endif

    /// <summary>Returns a new vector with its components clamped between the components min and max.</summary>
    public static Vector128<double> Clamp(this Vector128<double> source, double min, double max) => source.Clamp(Vector128.Create(min), Vector128.Create(max));

    /// <summary>Returns a new vector with its components clamped between the components min and max.</summary>
    public static Vector256<double> Clamp(this Vector256<double> source, double min, double max) => source.Clamp(Vector256.Create(min), Vector256.Create(max));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with its components clamped between the components min and max.</summary>
    public static Vector512<double> Clamp(this Vector512<double> source, double min, double max) => source.Clamp(Vector512.Create(min), Vector512.Create(max));

#endif

    #endregion // Clamp

    #region CopySign

    public static Vector128<double> CopySign(this Vector128<double> source, Vector128<double> sign)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Or(Abs(source), SignMasked(sign))
      : Abs(source) | (sign & SignMask128Double);

    public static Vector256<double> CopySign(this Vector256<double> source, Vector256<double> sign)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Or(Abs(source), SignMasked(sign))
      : Vector256.Create(CopySign(source.GetLower(), sign.GetLower()), CopySign(source.GetUpper(), sign.GetUpper()));

#if NET8_0_OR_GREATER

    public static Vector512<double> CopySign(this Vector512<double> source, Vector512<double> sign)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Or(Abs(source).AsInt64(), SignMaskedInt64(sign)).AsDouble()
      : Vector512.Create(CopySign(source.GetLower(), sign.GetLower()), CopySign(source.GetUpper(), sign.GetUpper()));

#endif

    #endregion // CopySign

    /// <summary>
    /// <para>The cross product for 2-dimensions yield a scalar value.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static double Cross(this Vector128<double> source, Vector128<double> target)
      => (source[0] * target[1] - target[0] * source[1]);

    /// <summary>Returns the cross product of the vector.</summary>
    /// <remarks>
    /// Cross product of A(x, y, z, _) and B(x, y, z, _) is
    ///                    0  1  2  3        0  1  2  3
    ///
    /// '(X = (Ay * Bz) - (Az * By), Y = (Az * Bx) - (Ax * Bz), Z = (Ax * By) - (Ay * Bx)'
    ///           1           2              1           2              1            2
    ///
    /// So we can do (Ay, Az, Ax, _) * (Bz, Bx, By, _) (last elem is irrelevant, as this is for Vector3)
    /// which leaves us with a of the first subtraction element for each (marked 1 above)
    /// Then we repeat with the right hand of subtractions (Az, Ax, Ay, _) * (By, Bz, Bx, _)
    /// which leaves us with the right hand sides (marked 2 above)
    /// Then we subtract them to get the correct vector
    /// We then mask out W to zero, because that is required for the Vector3 representation
    ///
    /// We perform the first 2 multiplications by shuffling the vectors and then multiplying them
    /// Helpers.Shuffle is the same as the C++ macro _MM_SHUFFLE, and you provide the order you wish the elements
    /// to be in *reversed* (no clue why), so here (3, 0, 2, 1) means you have the 2nd elem (1, 0 indexed) in the first slot,
    /// the 3rd elem (2) in the next one, the 1st elem (0) in the next one, and the 4th (3, W/_, unused here) in the last reg
    /// </remarks>
    public static Vector256<double> Cross(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      //? Mask3D(System.Runtime.Intrinsics.X86.Avx.Subtract(System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(source, ShuffleYZXW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(target, ShuffleZXYW)), System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(source, ShuffleZXYW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(target, ShuffleYZXW))))
      // Trying this (below) for 4 dimensional computation, rather than just 3D (above).
      ? System.Runtime.Intrinsics.X86.Avx.Subtract(System.Runtime.Intrinsics.X86.Avx2.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(source, ShuffleYZXW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(target, ShuffleZXYW)), System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(source, ShuffleZXYW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(target, ShuffleYZXW)))
      : Vector256.Create(source[1] * target[2] - source[2] * target[1], source[2] * target[0] - source[0] * target[2], source[0] * target[1] - source[1] * target[0], 0);

    public static void Deconstruct(this Vector128<double> source, out double x, out double y)
    {
      x = source[0];
      y = source[1];
    }

    public static void Deconstruct(this Vector256<double> source, out double x, out double y, out double z, out double w)
    {
      x = source[0];
      y = source[1];
      z = source[2];
      w = source[3];
    }

    #region Divide

    /// <summary>Returns a new vector with the quotient of the vector components.</summary>
    public static Vector128<double> Divide(this Vector128<double> source, Vector128<double> divisor)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Divide(source, divisor)
      : Vector128.Create(source[0] / divisor[0], source[1] / divisor[1]);

    /// <summary>Returns a new vector with the quotient of the vector components.</summary>
    public static Vector256<double> Divide(this Vector256<double> source, Vector256<double> divisor)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Divide(source, divisor)
      : Vector256.Create(Divide(source.GetLower(), divisor.GetLower()), Divide(source.GetUpper(), divisor.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the quotient of the vector components.</summary>
    public static Vector512<double> Divide(this Vector512<double> source, Vector512<double> divisor)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Divide(source, divisor)
      : Vector512.Create(Divide(source.GetLower(), divisor.GetLower()), Divide(source.GetUpper(), divisor.GetUpper()));

#endif

    /// <summary>Returns a new vector with the quotient of each vector components and the scalar value.</summary>
    public static Vector128<double> Divide(this Vector128<double> source, double divisor) => source.Divide(Vector128.Create(divisor));

    /// <summary>Returns a new vector with the quotient of each vector components and the scalar value.</summary>
    public static Vector256<double> Divide(this Vector256<double> source, double divisor) => source.Divide(Vector256.Create(divisor));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the quotient of each vector components and the scalar value.</summary>
    public static Vector512<double> Divide(this Vector512<double> source, double divisor) => source.Divide(Vector512.Create(divisor));

#endif

    #endregion // Divide

    #region Dot

    /// <summary>Returns the dot product of the two given vectors.</summary>
    public static double Dot(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.DotProduct(source, target, 0b_0011_1111)[0] // Multiply the first 2 elements of each and broadcasts it into each element of the returning vector.
      : source[0] * target[0] + source[1] * target[1];

    /// <summary>Returns the dot product of the vector.</summary>
    public static double Dot(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Multiply(source, target).HorizontalSum()
      : Dot(source.GetLower(), target.GetLower()) + Dot(source.GetUpper(), target.GetUpper());

#if NET8_0_OR_GREATER

    /// <summary>Returns the dot product of the vector.</summary>
    public static double Dot(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Multiply(source, target).HorizontalSum() // Normal multiply of the four components = (X, Y, Z, W).
      : Dot(source.GetLower(), target.GetLower()) + Dot(source.GetUpper(), target.GetUpper());

#endif

    #endregion // Dot

    /// <summary>Duplicate the two values in the V128 into four values in a V256.</summary>
    public static Vector256<double> DuplicateV128IntoV256(this Vector128<double> source) => Vector256.Create(source, source);

#if NET8_0_OR_GREATER

    /// <summary>Duplicate the two values in the V128 into four values in a V256.</summary>
    public static Vector512<double> DuplicateV256IntoV512(this Vector256<double> source) => Vector512.Create(source, source);

#endif

    //#region Envelop

    //public static Vector128<double> Envelop(this Vector128<double> source) => RoundAwayFromZero(source);

    //public static Vector256<double> Envelop(this Vector256<double> source) => RoundAwayFromZero(source);

    //public static Vector512<double> Envelop(this Vector512<double> source) => RoundAwayFromZero(source);

    //#endregion // Envelop

    #region EuclideanLength

    /// <summary>Returns a new vector with the Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLength(this Vector128<double> source) => double.Sqrt(EuclideanLengthSquared(source));

    /// <summary>Returns a new vector with the Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLength(this Vector256<double> source) => double.Sqrt(EuclideanLengthSquared(source));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLength(this Vector512<double> source) => double.Sqrt(EuclideanLengthSquared(source));

#endif

    #endregion // EuclideanLength

    #region EuclideanLengthSquared

    /// <summary>Returns a new vector with the squared Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLengthSquared(this Vector128<double> source) => Dot(source, source);

    /// <summary>Returns a new vector with the squared Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLengthSquared(this Vector256<double> source) => Dot(source, source);

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the squared Euclidean length (magnitude) of the vector.</summary>
    public static double EuclideanLengthSquared(this Vector512<double> source) => Dot(source, source);

#endif

    #endregion // EuclideanLengthSquared

    #region Floor

    /// <summary>Returns a new vector with the largest integer value that is less than or equal to the source, for each component.</summary>
    public static Vector128<double> Floor(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.Floor(source)
      : Vector128.Create(double.Floor(source[0]), double.Floor(source[1]));

    /// <summary>Returns a new vector with the largest integer value that is less than or equal to the source, for each component.</summary>
    public static Vector256<double> Floor(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Floor(source)
      : Vector256.Create(Floor(source.GetLower()), Floor(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the largest integer value that is less than or equal to the source, for each component.</summary>
    public static Vector512<double> Floor(this Vector512<double> source)
      => Vector512.Create(Floor(source.GetLower()), Floor(source.GetUpper()));

#endif

    #endregion // Floor

    #region HorizontalSum

    /// <summary>Creates a new value with all components added together.</summary>
    public static double HorizontalSum(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse3.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse3.HorizontalAdd(source, source)[0] // Add component pairs = (X + Y, X + Y).
      : source[0] + source[1];

    /// <summary>Creates a new value with all components added together.</summary>
    public static double HorizontalSum(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Add(source.GetLower(), source.GetUpper()).HorizontalSum()
      : HorizontalSum(source.GetLower()) + HorizontalSum(source.GetUpper());

#if NET8_0_OR_GREATER

    /// <summary>Creates a new value with all components added together.</summary>
    public static double HorizontalSum(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Add(source.GetLower(), source.GetUpper()).HorizontalSum()
      : HorizontalSum(source.GetLower()) + HorizontalSum(source.GetUpper());

#endif

    #endregion // HorizontalSum

    #region HorizontalMax

    /// <summary>Returns a new vector filled with the maximum value of the components in specified vector.</summary>
    public static double HorizontalMax(this Vector128<double> source)
      => double.Max(source[0], source[1]);

    /// <summary>Returns a new vector filled with the maximum value of the components in specified vector.</summary>
    public static double HorizontalMax(this Vector256<double> source)
      => double.Max(HorizontalMax(source.GetLower()), HorizontalMax(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector filled with the maximum value of the components in specified vector.</summary>
    public static double HorizontalMax(this Vector512<double> source)
      => double.Max(HorizontalMax(source.GetLower()), HorizontalMax(source.GetUpper()));

#endif

    #endregion // HorizontalMax

    #region HorizontalMin

    /// <summary>Returns a new vector filled with the minimum value of the components in specified vector.</summary>
    public static double HorizontalMin(this Vector128<double> source)
      => double.Min(source[0], source[1]);

    /// <summary>Returns a new vector filled with the minimum value of the components in specified vector.</summary>
    public static double HorizontalMin(this Vector256<double> source)
      => double.Min(HorizontalMin(source.GetLower()), HorizontalMin(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector filled with the minimum value of the components in specified vector.</summary>
    public static double HorizontalMin(this Vector512<double> source)
      => double.Min(HorizontalMin(source.GetLower()), HorizontalMin(source.GetUpper()));

#endif

    #endregion // HorizontalMin

    #region HorizontalDifference

    public static double HorizontalDifference(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse3.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse3.HorizontalSubtract(source, source)[0] // Add component pairs = (X - Y, X - Y).
      : source[0] - source[1];

    public static double HorizontalDifference(this Vector256<double> source)
      => source[0] - (HorizontalSum(source) - source[0]);

#if NET8_0_OR_GREATER

    /// <summary>Creates a new vector with all components added together.</summary>
    public static double HorizontalDifference(this Vector512<double> source)
      => source[0] - (HorizontalSum(source) - source[0]);

#endif

    #endregion // HorizontalDifference

    #region Lerp

    /// <summary>Returns a new vector that is a linear interpolation of the two specified vectors. All components computed.</summary>
    /// <param name="mu">The weight factor [0, 1]. The resulting vector is, when mu = 0 = v1, mu = 1 = v2, mu = (0, 1) = between v1 and v2.</param>
    public static Vector128<double> Lerp(this Vector128<double> source, Vector128<double> target, double mu)
      => Add(source, Multiply(Subtract(target, source), Vector128.Create(mu))); // General formula of linear interpolation: (from + (to - from) * mu).

    /// <summary>Returns a new vector that is a linear interpolation of the two specified vectors. All components computed.</summary>
    /// <param name="mu">The weight factor [0, 1]. The resulting vector is, when mu = 0 = v1, mu = 1 = v2, mu = (0, 1) = between v1 and v2.</param>
    public static Vector256<double> Lerp(this Vector256<double> source, Vector256<double> target, double mu)
      => Add(source, Multiply(Subtract(target, source), Vector256.Create(mu))); // General formula of linear interpolation: (from + (to - from) * mu).

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector that is a linear interpolation of the two specified vectors. All components computed.</summary>
    /// <param name="mu">The weight factor [0, 1]. The resulting vector is, when mu = 0 = v1, mu = 1 = v2, mu = (0, 1) = between v1 and v2.</param>
    public static Vector512<double> Lerp(this Vector512<double> source, Vector512<double> target, double mu)
      => Add(source, Multiply(Subtract(target, source), Vector512.Create(mu))); // General formula of linear interpolation: (from + (to - from) * mu).

#endif

    #endregion // HorizontalSubtract

    #region ManhattanLength

    /// <summary>Compute the Manhattan length of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(this Vector128<double> source, double edgeLength = 1) => HorizontalSum(Divide(Abs(source), Vector128.Create(edgeLength)));

    /// <summary>Compute the Manhattan length of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(this Vector256<double> source, double edgeLength = 1) => HorizontalSum(Divide(Abs(source), Vector256.Create(edgeLength)));

#if NET8_0_OR_GREATER

    /// <summary>Compute the Manhattan length of the vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanLength(this Vector512<double> source, double edgeLength = 1) => HorizontalSum(Divide(Abs(source), Vector512.Create(edgeLength)));

#endif

    #endregion // ManhattanLength

    /// <summary>Returns a new vector with the first two components of the vector and the last two components set to zero: Z = 0 and W = 0.</summary>
    public static Vector256<double> MaskXY(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.And(source, BitMask256DoubleX1Y1Z0W0)
      : Vector256.Create(source[0], source[1], 0, 0);

    /// <summary>Returns a new vector with the first three components of the vector and the last component set to zero: W = 0.</summary>
    public static Vector256<double> MaskXYZ(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.And(source, BitMask256DoubleX1Y1Z1W0)
      : Vector256.Create(source[0], source[1], source[2], 0);

    #region Max

    /// <summary>Returns a new vector with the maximum value for each component of the two specified vectors.</summary>
    public static Vector128<double> Max(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Max(source, target)
      : Vector128.Create(double.Max(source[0], target[0]), double.Max(source[1], target[1]));

    /// <summary>Returns a new vector with the maximum value for each component of the two specified vectors.</summary>
    public static Vector256<double> Max(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Max(source, target)
      : Vector256.Create(Max(source.GetLower(), target.GetLower()), Max(source.GetUpper(), target.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the maximum value for each component of the two specified vectors.</summary>
    public static Vector512<double> Max(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Max(source, target)
      : Vector512.Create(Max(source.GetLower(), target.GetLower()), Max(source.GetUpper(), target.GetUpper()));

#endif

    #endregion // Max

    #region Min

    /// <summary>Returns a new vector with the minimum value for each component of the two specified vectors.</summary>
    public static Vector128<double> Min(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Min(source, target)
      : Vector128.Create(double.Min(source[0], target[0]), double.Min(source[1], target[1]));

    /// <summary>Returns a new vector with the minimum value for each component of the two specified vectors.</summary>
    public static Vector256<double> Min(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Min(source, target)
      : Vector256.Create(Min(source.GetLower(), target.GetLower()), Min(source.GetUpper(), target.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the minimum value for each component of the two specified vectors.</summary>
    public static Vector512<double> Min(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Min(source, target)
      : Vector512.Create(Min(source.GetLower(), target.GetLower()), Min(source.GetUpper(), target.GetUpper()));

#endif

    #endregion // Min

    #region MinkowskiLength

    /// <summary></summary>
    /// <param name="order">1 or greater.</param>
    /// <see href="https://en.wikipedia.org/wiki/Minkowski_distance"/>
    public static double MinkowskiLength(this Vector128<double> source, int order = 1) => double.Pow(HorizontalSum(Pow(Abs(source), order)), 1d / order);

    /// <summary></summary>
    /// <param name="order">1 or greater.</param>
    /// <see href="https://en.wikipedia.org/wiki/Minkowski_distance"/>
    public static double MinkowskiLength(this Vector256<double> source, int order = 1) => double.Pow(HorizontalSum(Pow(Abs(source), order)), 1d / order);

#if NET8_0_OR_GREATER

    /// <summary></summary>
    /// <param name="order">1 or greater.</param>
    /// <see href="https://en.wikipedia.org/wiki/Minkowski_distance"/>
    public static double MinkowskiLength(this Vector512<double> source, int order = 1) => double.Pow(HorizontalSum(Pow(Abs(source), order)), 1d / order);

#endif

    #endregion // MinkowskiLength

    #region Multiply

    /// <summary>Returns a new vector with the product of the vector components.</summary>
    public static Vector128<double> Multiply(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Multiply(source, target)
      : Vector128.Create(source[0] * target[0], source[1] * target[1]);

    /// <summary>Returns a new vector with the product of the vector components.</summary>
    public static Vector256<double> Multiply(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Multiply(source, target)
      : Vector256.Create(Multiply(source.GetLower(), target.GetLower()), Multiply(source.GetUpper(), target.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the product of the vector components.</summary>
    public static Vector512<double> Multiply(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Multiply(source, target)
      : Vector512.Create(Multiply(source.GetLower(), target.GetLower()), Multiply(source.GetUpper(), target.GetUpper()));

#endif

    /// <summary>Returns a new vector with the product of each vector components and the scalar value.</summary>
    public static Vector128<double> Multiply(this Vector128<double> source, double factor) => source.Multiply(Vector128.Create(factor));

    /// <summary>Returns a new vector with the product of each vector components and the scalar value.</summary>
    public static Vector256<double> Multiply(this Vector256<double> source, double factor) => source.Multiply(Vector256.Create(factor));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the product of each vector components and the scalar value.</summary>
    public static Vector512<double> Multiply(this Vector512<double> source, double factor) => source.Multiply(Vector512.Create(factor));

#endif

    #endregion // Multiply

    #region MultiplyAdd

    /// <summary>Returns (x * y) + z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="add">The vector to be added to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) + z on each element, rounded as one ternary operation</returns>
    public static Vector128<double> MultiplyAdd(this Vector128<double> source, Vector128<double> multiply, Vector128<double> add)
      => System.Runtime.Intrinsics.X86.Fma.IsSupported
      ? System.Runtime.Intrinsics.X86.Fma.MultiplyAdd(source, multiply, add)
      : Add(Multiply(source, multiply), add);

    /// <summary>Returns (x * y) + z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="add">The vector to be added to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) + z on each element, rounded as one ternary operation</returns>
    public static Vector256<double> MultiplyAdd(this Vector256<double> source, Vector256<double> multiply, Vector256<double> add)
      => System.Runtime.Intrinsics.X86.Fma.IsSupported
      ? System.Runtime.Intrinsics.X86.Fma.MultiplyAdd(source, multiply, add)
      : Add(Multiply(source, multiply), add);

#if NET8_0_OR_GREATER

    /// <summary>Returns (x * y) + z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="add">The vector to be added to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) + z on each element, rounded as one ternary operation</returns>
    public static Vector512<double> MultiplyAdd(this Vector512<double> source, Vector512<double> multiply, Vector512<double> add)
      => Add(Multiply(source, multiply), add);

#endif

    #endregion // MultiplyAdd

    #region MultiplySubtract

    /// <summary>Returns (x * y) - z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="add">The vector to be subtracted to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) - z on each element, rounded as one ternary operation</returns>
    public static Vector128<double> MultiplySubtract(this Vector128<double> source, Vector128<double> multiply, Vector128<double> subtract)
      => System.Runtime.Intrinsics.X86.Fma.IsSupported
      ? System.Runtime.Intrinsics.X86.Fma.MultiplySubtract(source, multiply, subtract)
      : Subtract(Multiply(source, multiply), subtract);

    /// <summary>Returns (x * y) - z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="subtract">The vector to be subtracted to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) - z on each element, rounded as one ternary operation</returns>
    public static Vector256<double> MultiplySubtract(this Vector256<double> source, Vector256<double> multiply, Vector256<double> subtract)
      => System.Runtime.Intrinsics.X86.Fma.IsSupported
      ? System.Runtime.Intrinsics.X86.Fma.MultiplySubtract(source, multiply, subtract)
      : Subtract(Multiply(source, multiply), subtract);

#if NET8_0_OR_GREATER

    /// <summary>Returns (x * y) - z on each element of a vector rounded as one ternary operation.</summary>
    /// <param name="source">The vector to be multiplied with <paramref name="multiply"/></param>
    /// <param name="multiply">The vector to be multiplied with <paramref name="source"/></param>
    /// <param name="subtract">The vector to be subtracted to to the infinite precision multiplication of <paramref name="source"/> and <paramref name="multiply"/></param>
    /// <returns>(x * y) - z on each element, rounded as one ternary operation</returns>
    public static Vector512<double> MultiplySubtract(this Vector512<double> source, Vector512<double> multiply, Vector512<double> subtract)
      => Subtract(Multiply(source, multiply), subtract);

#endif

    #endregion // MultiplySubtract

    #region Negate

    /// <summary>Returns a new vector with the components negated.</summary>
    public static Vector128<double> Negate(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Xor(source, SignMask128Double)
      : Vector128.Create(-source[0], -source[1]);

    /// <summary>Returns a new vector with the components negated.</summary>
    public static Vector256<double> Negate(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Xor(source, SignMask256Double)
      : Vector256.Create(Negate(source.GetLower()), Negate(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components negated.</summary>
    public static Vector512<double> Negate(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Xor(source.AsInt64(), SignMask512Double.AsInt64()).AsDouble()
      : Vector512.Create(Negate(source.GetLower()), Negate(source.GetUpper()));

#endif

    #endregion // Negate

    #region Normalize

    /// <summary>Scales the vector to unit length.</summary>
    public static Vector128<double> Normalize(this Vector128<double> source, out double magnitude) => Divide(source, magnitude = EuclideanLength(source));

    /// <summary>Scales the vector to unit length.</summary>
    public static Vector256<double> Normalize(this Vector256<double> source, out double magnitude) => Divide(source, magnitude = EuclideanLength(source));

#if NET8_0_OR_GREATER

    /// <summary>Scales the vector to unit length.</summary>
    public static Vector512<double> Normalize(this Vector512<double> source, out double magnitude) => Divide(source, magnitude = EuclideanLength(source));

#endif

    #endregion // Normalize


    /// <summary>
    /// <para>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</para>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    /// </summary>
    public static int OrthantNumber2D(this Vector128<double> source, Vector128<double> center, Geometry.OrthantNumbering numbering)
    {
      source.Deconstruct(out var sx, out var sy);
      center.Deconstruct(out var cx, out var cy);

      return numbering switch
      {
        Geometry.OrthantNumbering.Traditional => sy >= cy ? (sx >= cx ? 0 : 1) : (sx >= cx ? 3 : 2),
        Geometry.OrthantNumbering.BinaryNegativeAs1 => (sx >= cx ? 0 : 1) + (sy >= cy ? 0 : 2),
        Geometry.OrthantNumbering.BinaryPositiveAs1 => (sx < cx ? 0 : 1) + (sy < cy ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };
    }

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    public static int OrthantNumber3D(this Vector256<double> source, Vector256<double> center, Geometry.OrthantNumbering numbering)
    {
      source.Deconstruct(out var sx, out var sy, out var sz, out var _);
      center.Deconstruct(out var cx, out var cy, out var cz, out var _);

      return numbering switch
      {
        Geometry.OrthantNumbering.Traditional => sz >= cz ? (sy >= cy ? (sx >= cx ? 0 : 1) : (sx >= cx ? 3 : 2)) : (sy >= cy ? (sx >= cx ? 7 : 6) : (sx >= cx ? 4 : 5)),
        Geometry.OrthantNumbering.BinaryNegativeAs1 => (sx >= cx ? 0 : 1) + (sy >= cy ? 0 : 2) + (sz >= cz ? 0 : 4),
        Geometry.OrthantNumbering.BinaryPositiveAs1 => (sx < cx ? 0 : 1) + (sy < cy ? 0 : 2) + (sz < cz ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };
    }


    #region Pow

    /// <summary>Calculates the power of the value and specified exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double source, and if the exponent has a 1 bit at that position, we multiply/accumulate that into the result.</summary>
    public static Vector128<double> Pow(this Vector128<double> source, int exponent)
    {
      if (int.IsNegative(exponent))
        return One128D.Divide(source.Pow(int.Abs(exponent)));

      var pow = One128D;

      while (exponent > 0)
      {
        if ((exponent & 1) == 1)
          pow = pow.Multiply(source);

        source = source.Multiply(source);
        exponent >>= 1;
      }

      return pow;
    }

    /// <summary>Calculates the power of the value and specified exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double source, and if the exponent has a 1 bit at that position, we multiply/accumulate that into the result.</summary>
    public static Vector256<double> Pow(this Vector256<double> source, int exponent)
    {
      if (int.IsNegative(exponent))
        return One256D.Divide(source.Pow(int.Abs(exponent)));

      var pow = One256D;

      while (exponent > 0)
      {
        if ((exponent & 1) == 1)
          pow = pow.Multiply(source);

        source = source.Multiply(source);
        exponent >>= 1;
      }

      return pow;
    }

#if NET8_0_OR_GREATER

    /// <summary>Calculates the power of the value and specified exponent, using exponentiation by repeated squaring. Essentially, we repeatedly double source, and if the exponent has a 1 bit at that position, we multiply/accumulate that into the result.</summary>
    public static Vector512<double> Pow(this Vector512<double> source, int exponent)
    {
      if (int.IsNegative(exponent))
        return One512D.Divide(source.Pow(int.Abs(exponent)));

      var pow = One512D;

      while (exponent > 0)
      {
        if ((exponent & 1) == 1)
          pow = pow.Multiply(source);

        source = source.Multiply(source);
        exponent >>= 1;
      }

      return pow;
    }

#endif

    #endregion // Pow

    #region Reciprocal

    /// <summary>Returns a new vector with the reciprocal (1.0 / x) of each component.</summary>
    public static Vector128<double> Reciprocal(this Vector128<double> source) => One128D.Divide(source);

    /// <summary>Returns a new vector with the reciprocal (1.0 / x) of each component.</summary>
    public static Vector256<double> Reciprocal(this Vector256<double> source) => One256D.Divide(source);

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the reciprocal (1.0 / x) of each component.</summary>
    public static Vector512<double> Reciprocal(this Vector512<double> source) => One512D.Divide(source);

#endif

    #endregion // Reciprocal

    /// <summary>Calculates the reflection of an incident ray. Reflection: (incident - (2 * DotProduct(incident, normal)) * normal)</summary>
    /// <param name="source">The incident ray's vector.</param>
    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
    /// <returns>The vector of the reflected ray.</returns>
    public static Vector128<double> Reflect(this Vector128<double> source, Vector128<double> normal)
      => source.Subtract(Two128D.Multiply(source.Dot(normal)).Multiply(normal)); // reflection = incident - (2 * DotProduct(incident, normal)) * normal

    /// <summary>Calculates the reflection of an incident ray.</summary>
    /// <param name="source">The incident ray's vector.</param>
    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
    /// <returns>The vector of the reflected ray.</returns>
    public static Vector256<double> Reflect(this Vector256<double> source, Vector256<double> normal)
      => source.Subtract(Two256D.Multiply(source.Dot(normal)).Multiply(normal)); // reflection = incident - (2 * DotProduct(incident, normal)) * normal

    #region Remainder

    /// <summary>Returns a new vector with the remainder of the vector components.</summary>
    public static Vector128<double> Remainder(this Vector128<double> source, Vector128<double> divisor)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Subtract(source, System.Runtime.Intrinsics.X86.Sse2.Multiply(System.Runtime.Intrinsics.X86.Sse41.RoundToZero(System.Runtime.Intrinsics.X86.Sse2.Divide(source, divisor)), divisor))
      : Vector128.Create(source[0] % divisor[0], source[1] % divisor[1]);

    /// <summary>Returns a new vector with the remainder of the vector components.</summary>
    public static Vector256<double> Remainder(this Vector256<double> source, Vector256<double> divisor)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Subtract(source, System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx.RoundToZero(System.Runtime.Intrinsics.X86.Avx.Divide(source, divisor)), divisor))
      : Vector256.Create(Remainder(source.GetLower(), divisor.GetLower()), Remainder(source.GetUpper(), divisor.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the remainder of the vector components.</summary>
    public static Vector512<double> Remainder(this Vector512<double> source, Vector512<double> divisor)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Subtract(source, System.Runtime.Intrinsics.X86.Avx512F.Multiply(System.Runtime.Intrinsics.X86.Avx512F.Divide(source, divisor).RoundTowardZero(), divisor))
      : Vector512.Create(Remainder(source.GetLower(), divisor.GetLower()), Remainder(source.GetUpper(), divisor.GetUpper()));

#endif

    /// <summary>Returns a new vector with the remainder of the vector components and the scalar value.</summary>
    public static Vector128<double> Remainder(this Vector128<double> source, double divisor) => source.Remainder(Vector128.Create(divisor));

    /// <summary>Returns a new vector with the remainder of the vector components and the scalar value.</summary>
    public static Vector256<double> Remainder(this Vector256<double> source, double divisor) => source.Remainder(Vector256.Create(divisor));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the remainder of the vector components and the scalar value.</summary>
    public static Vector512<double> Remainder(this Vector512<double> source, double divisor) => source.Remainder(Vector512.Create(divisor));

#endif

    #endregion // Remainder

    #region RoundAwayFromZero

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are away from zero.</summary>
    public static Vector128<double> RoundAwayFromZero(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Or(System.Runtime.Intrinsics.X86.Sse41.RoundToPositiveInfinity(System.Runtime.Intrinsics.X86.Sse2.And(source, AbsMask128Double)), System.Runtime.Intrinsics.X86.Avx.And(source, SignMask128Double))
      : Vector128.Create(double.Round(source[0], MidpointRounding.AwayFromZero), double.Round(source[1], MidpointRounding.AwayFromZero));

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are away from zero.</summary>
    public static Vector256<double> RoundAwayFromZero(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Or(System.Runtime.Intrinsics.X86.Avx.RoundToPositiveInfinity(System.Runtime.Intrinsics.X86.Avx.And(source, AbsMask256Double)), System.Runtime.Intrinsics.X86.Avx.And(source, SignMask256Double))
      : Vector256.Create(RoundAwayFromZero(source.GetLower()), RoundAwayFromZero(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are away from zero.</summary>
    public static Vector512<double> RoundAwayFromZero(this Vector512<double> source)
      => Vector512.Create(RoundAwayFromZero(source.GetLower()), RoundAwayFromZero(source.GetUpper()));

#endif

    #endregion // RoundAwayFromZero

    #region RoundTowardZero

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards zero.</summary>
    public static Vector128<double> RoundTowardZero(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToZero(source)
      : Vector128.Create(double.Round(source[0], System.MidpointRounding.ToZero), double.Round(source[1], System.MidpointRounding.ToZero));

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards zero.</summary>
    public static Vector256<double> RoundTowardZero(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToZero(source)
      : Vector256.Create(RoundTowardZero(source.GetLower()), RoundTowardZero(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards zero.</summary>
    public static Vector512<double> RoundTowardZero(this Vector512<double> source)
      => Vector512.Create(RoundTowardZero(source.GetLower()), RoundTowardZero(source.GetUpper()));

#endif

    #endregion // RoundTowardZero

    #region RoundToEven

    /// <summary>Returns a new vector with the components rounded to their nearest integer (even, or banker's rounding) values.</summary>
    public static Vector128<double> RoundToEven(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(source)
      : Vector128.Create(double.Round(source[0], System.MidpointRounding.ToEven), double.Round(source[1], System.MidpointRounding.ToEven));

    /// <summary>Returns a new vector with the components rounded to their nearest integer (even, or banker's rounding) values.</summary>
    public static Vector256<double> RoundToEven(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToNearestInteger(source)
      : System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(source.GetLower()).ToVector256Unsafe().WithUpper(System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(source.GetUpper()))
      : Vector256.Create(RoundToEven(source.GetLower()), RoundToEven(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components rounded to their nearest integer (even, or banker's rounding) values.</summary>
    public static Vector512<double> RoundToEven(this Vector512<double> source)
      => Vector512.Create(RoundToEven(source.GetLower()), RoundToEven(source.GetUpper()));

#endif

    #endregion // RoundToEven

    #region RoundToNegativeInfinity

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards negative infinity.</summary>
    public static Vector128<double> RoundToNegativeInfinity(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToNegativeInfinity(source)
      : Vector128.Create(double.Round(source[0], System.MidpointRounding.ToNegativeInfinity), double.Round(source[1], System.MidpointRounding.ToNegativeInfinity));

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards negative infinity.</summary>
    public static Vector256<double> RoundToNegativeInfinity(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToNegativeInfinity(source)
      : Vector256.Create(RoundToNegativeInfinity(source.GetLower()), RoundToNegativeInfinity(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards negative infinity.</summary>
    public static Vector512<double> RoundToNegativeInfinity(this Vector512<double> source)
      => Vector512.Create(RoundToNegativeInfinity(source.GetLower()), RoundToNegativeInfinity(source.GetUpper()));

#endif

    #endregion // RoundToNegativeInfinity

    #region RoundToPositiveInfinity

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards positive infinity.</summary>
    public static Vector128<double> RoundToPositiveInfinity(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToPositiveInfinity(source)
      : Vector128.Create(double.Round(source[0], System.MidpointRounding.ToPositiveInfinity), double.Round(source[1], System.MidpointRounding.ToPositiveInfinity));

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards positive infinity.</summary>
    public static Vector256<double> RoundToPositiveInfinity(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToPositiveInfinity(source)
      : Vector256.Create(RoundToPositiveInfinity(source.GetLower()), RoundToPositiveInfinity(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the components rounded to their nearest integer values, that are towards positive infinity.</summary>
    public static Vector512<double> RoundToPositiveInfinity(this Vector512<double> source)
      => Vector512.Create(RoundToPositiveInfinity(source.GetLower()), RoundToPositiveInfinity(source.GetUpper()));

#endif

    #endregion // RoundToPositiveInfinity

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(this Vector256<double> source, Vector256<double> second, Vector256<double> third)
      => Dot(source, Cross(second, third));

    #region Sign

    public static Vector128<double> Sign(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse42.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Or(System.Runtime.Intrinsics.X86.Sse2.Min(System.Runtime.Intrinsics.X86.Sse2.And(source, AbsMask128Double), One128D), SignMasked(source))
      : Vector128.Create((double)double.Sign(source[0]), (double)double.Sign(source[1]));

    public static Vector256<double> Sign(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Or(System.Runtime.Intrinsics.X86.Avx.Min(System.Runtime.Intrinsics.X86.Avx.And(source, AbsMask256Double), One256D), SignMasked(source))
      : Vector256.Create(Sign(source.GetLower()), Sign(source.GetUpper()));

#if NET8_0_OR_GREATER

    public static Vector512<double> Sign(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Or(System.Runtime.Intrinsics.X86.Avx512F.Min(System.Runtime.Intrinsics.X86.Avx512F.And(source.AsInt64(), AbsMask512Int64), One512I64), SignMaskedInt64(source)).AsDouble()
      : Vector512.Create(Sign(source.GetLower()), Sign(source.GetUpper()));

#endif

    #endregion // Sign

    #region SignMasked

    /// <summary>Returns a new vector with the sign of the components.</summary>
    /// <remarks>This is unlike System.Math.Sign(), which is why it's currently marked internal.</remarks>
    internal static Vector128<double> SignMasked(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.And(source, SignMask128Double)
      : source & SignMask128Double;

    /// <summary>Returns a new vector with the sign of the components.</summary>
    /// <remarks>This is unlike System.Math.Sign(), which is why it's currently marked internal.</remarks>
    internal static Vector256<double> SignMasked(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.And(source, SignMask256Double)
      : Vector256.Create(SignMasked(source.GetLower()), SignMasked(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the sign of the components.</summary>
    /// <remarks>This is unlike System.Math.Sign(), which is why it's currently marked internal.</remarks>
    internal static Vector512<long> SignMaskedInt64(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.And(source.AsInt64(), SignMask512Int64)
      : Vector512.Create(SignMasked(source.GetLower()), SignMasked(source.GetUpper())).AsInt64();

#endif

    #endregion // SignMasked

    //#region Slerp

    ///// <summary>Returns a new vector that is a spherical linear interpolation of the two specified vectors. This is the 2D version because slerp use other functionality that is 2D dependent.</summary>
    //public static Vector128<double> Slerp(this Vector128<double> source, Vector128<double> target, double mu)
    //{
    //  var scale0 = 1 - mu;
    //  var scale1 = mu;

    //  if (Dot(source, target) is var dot && dot is var cosTheta && cosTheta != 0)
    //  {
    //    var theta = double.Acos(double.Clamp(cosTheta, -1, 1));
    //    var sinTheta = double.Sin(theta);
    //    scale0 = double.Sin(scale0 * theta) / sinTheta;
    //    scale1 = double.Sin(scale1 * theta) / sinTheta;
    //  }

    //  return Add(Multiply(source, Vector128.Create(scale0)), Multiply(target, Vector128.Create(scale1)));
    //}

    ///// <summary>Returns a new vector that is a spherical linear interpolation of the two specified NORMALIZED vectors. This is a 3D version because slerp use other functionality that is 3D dependent.</summary>
    //public static Vector256<double> Slerp(this Vector256<double> source, Vector256<double> target, double mu)
    //{
    //  var scale0 = 1 - mu;
    //  var scale1 = mu;

    //  if (Dot(source, target) is var dot && dot is var cosTheta && cosTheta != 0)
    //  {
    //    var theta = double.Acos(double.Clamp(cosTheta, -1, 1));
    //    var sinTheta = double.Sin(theta);
    //    scale0 = double.Sin(scale0 * theta) / sinTheta;
    //    scale1 = double.Sin(scale1 * theta) / sinTheta;
    //  }

    //  return Add(Multiply(source, Vector256.Create(scale0)), Multiply(target, Vector256.Create(scale1)));
    //}

    ///// <summary>Returns a new vector that is a spherical linear interpolation of the two specified NORMALIZED vectors. This is a 3D version because slerp use other functionality that is 3D dependent.</summary>
    //public static Vector512<double> Slerp(this Vector512<double> source, Vector512<double> target, double mu)
    //{
    //  var scale0 = 1 - mu;
    //  var scale1 = mu;

    //  if (Dot(source, target) is var dot && dot is var cosTheta && cosTheta != 0)
    //  {
    //    var theta = double.Acos(double.Clamp(cosTheta, -1, 1));
    //    var sinTheta = double.Sin(theta);
    //    scale0 = double.Sin(scale0 * theta) / sinTheta;
    //    scale1 = double.Sin(scale1 * theta) / sinTheta;
    //  }

    //  return Add(Multiply(source, Vector512.Create(scale0)), Multiply(target, Vector512.Create(scale1)));
    //}

    //#endregion // Slerp

    #region Sqrt

    /// <summary>Returns a new vector with the square root of each component.</summary>
    public static Vector128<double> Sqrt(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Sqrt(source)
      : Vector128.Create(double.Sqrt(source[0]), double.Sqrt(source[1]));

    /// <summary>Returns a new vector with the square root of each component.</summary>
    public static Vector256<double> Sqrt(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Sqrt(source)
      : Vector256.Create(Sqrt(source.GetLower()), Sqrt(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the square root of each component.</summary>
    public static Vector512<double> Sqrt(this Vector512<double> source)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Sqrt(source)
      : Vector512.Create(Sqrt(source.GetLower()), Sqrt(source.GetUpper()));

#endif

    #endregion // Sqrt

    #region Subtract

    /// <summary>Returns a new vector with the difference of the vector components.</summary>
    public static Vector128<double> Subtract(this Vector128<double> source, Vector128<double> target)
      => System.Runtime.Intrinsics.X86.Sse2.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse2.Subtract(source, target)
      : Vector128.Create(source[0] - target[0], source[1] - target[1]);

    /// <summary>Returns a new vector with the difference of the vector components.</summary>
    public static Vector256<double> Subtract(this Vector256<double> source, Vector256<double> target)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Subtract(source, target)
      : Vector256.Create(Subtract(source.GetLower(), target.GetLower()), Subtract(source.GetUpper(), target.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the difference of the vector components.</summary>
    public static Vector512<double> Subtract(this Vector512<double> source, Vector512<double> target)
      => System.Runtime.Intrinsics.X86.Avx512F.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx512F.Subtract(source, target)
      : Vector512.Create(Subtract(source.GetLower(), target.GetLower()), Subtract(source.GetUpper(), target.GetUpper()));

#endif

    /// <summary>Returns a new vector with the difference of each vector components and the scalar value.</summary>
    public static Vector128<double> Subtract(this Vector128<double> source, double scalar) => source.Subtract(Vector128.Create(scalar));

    /// <summary>Returns a new vector with the difference of each vector components and the scalar value.</summary>
    public static Vector256<double> Subtract(this Vector256<double> source, double scalar) => source.Subtract(Vector256.Create(scalar));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the difference of each vector components and the scalar value.</summary>
    public static Vector512<double> Subtract(this Vector512<double> source, double scalar) => source.Subtract(Vector512.Create(scalar));

#endif

    #endregion // Subtract

    #region ToString..

    public static string ToStringXY(this Vector128<double> source, string? format, IFormatProvider? formatProvider)
      => $"<{source[0].ToString(format, formatProvider)}, {source[1].ToString(format, formatProvider)}>";

    public static string ToStringXY(this Vector256<double> source, string? format, IFormatProvider? formatProvider)
      => $"<{source[0].ToString(format, formatProvider)}, {source[1].ToString(format, formatProvider)}>";

    public static string ToStringXYZ(this Vector256<double> source, string? format, IFormatProvider? formatProvider)
      => $"<{source[0].ToString(format, formatProvider)}, {source[1].ToString(format, formatProvider)}, {source[2].ToString(format, formatProvider)}>";

    public static string ToStringXYZW(this Vector256<double> source, string? format, IFormatProvider? formatProvider)
      => $"<{source[0].ToString(format, formatProvider)}, {source[1].ToString(format, formatProvider)}, {source[2].ToString(format, formatProvider)}, {source[3].ToString(format, formatProvider)}>";

    #endregion // ToString..

    public static System.Numerics.Vector2 ToVector2(this Vector128<double> source)
      => new((float)source[0], (float)source[1]);

    public static System.Numerics.Vector3 ToVector3(this Vector256<double> source)
      => new((float)source[0], (float)source[1], (float)source[2]);

    #region Truncate

    /// <summary>Returns a new vector with the vector components truncated.</summary>
    public static Vector128<double> Truncate(this Vector128<double> source)
      => System.Runtime.Intrinsics.X86.Sse41.IsSupported
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToZero(source)
      : Vector128.Create(double.Truncate(source[0]), double.Truncate(source[1]));

    /// <summary>Returns a new vector with the vector components truncated.</summary>
    public static Vector256<double> Truncate(this Vector256<double> source)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToZero(source)
      : Vector256.Create(Truncate(source.GetLower()), Truncate(source.GetUpper()));

#if NET8_0_OR_GREATER

    /// <summary>Returns a new vector with the vector components truncated.</summary>
    public static Vector512<double> Truncate(this Vector512<double> source)
      => Vector512.Create(Truncate(source.GetLower()), Truncate(source.GetUpper()));

#endif

    #endregion // Truncate

    #region TruncMod

    public static Vector128<double> TruncMod(this Vector128<double> source, Vector128<double> divisor, out Vector128<double> remainder)
    {
      if (System.Runtime.Intrinsics.X86.Sse41.IsSupported)
      {
        var q = System.Runtime.Intrinsics.X86.Sse41.RoundToZero(System.Runtime.Intrinsics.X86.Sse2.Divide(source, divisor));
        remainder = System.Runtime.Intrinsics.X86.Sse2.Subtract(source, System.Runtime.Intrinsics.X86.Sse2.Multiply(q, divisor));
        return q;
      }
      else
      {
        var s0 = source[0];
        var d0 = divisor[0];
        var r0 = s0 % d0;

        var s1 = source[1];
        var d1 = divisor[1];
        var r1 = s1 % d1;

        remainder = Vector128.Create(r0, r1);

        return Vector128.Create((s0 - r0) / d0, (s1 - r1) / d1);
      }
    }

    public static Vector256<double> TruncMod(this Vector256<double> source, Vector256<double> divisor, out Vector256<double> remainder)
    {
      if (System.Runtime.Intrinsics.X86.Avx.IsSupported)
      {
        var q = System.Runtime.Intrinsics.X86.Avx.RoundToZero(System.Runtime.Intrinsics.X86.Avx.Divide(source, divisor));
        remainder = System.Runtime.Intrinsics.X86.Avx.Subtract(source, System.Runtime.Intrinsics.X86.Avx.Multiply(q, divisor));
        return q;
      }
      else
      {
        var lq = TruncMod(source.GetLower(), divisor.GetLower(), out var lr);
        var uq = TruncMod(source.GetUpper(), divisor.GetUpper(), out var ur);

        remainder = Vector256.Create(lr, ur);

        return Vector256.Create(lq, uq);
      }
    }

#if NET8_0_OR_GREATER

    public static Vector512<double> TruncMod(this Vector512<double> source, Vector512<double> divisor, out Vector512<double> remainder)
    {
      if (System.Runtime.Intrinsics.X86.Avx512F.IsSupported)
      {
        var q = System.Runtime.Intrinsics.X86.Avx512F.Divide(source, divisor).RoundTowardZero();
        remainder = System.Runtime.Intrinsics.X86.Avx512F.Subtract(source, System.Runtime.Intrinsics.X86.Avx512F.Multiply(q, divisor));
        return q;
      }
      else
      {
        var lq = TruncMod(source.GetLower(), divisor.GetLower(), out var lr);
        var uq = TruncMod(source.GetUpper(), divisor.GetUpper(), out var ur);

        remainder = Vector512.Create(lr, ur);

        return Vector512.Create(lq, uq);
      }
    }

#endif

    public static Vector128<double> TruncMod(this Vector128<double> source, double divisor, out Vector128<double> remainder) => source.TruncMod(Vector128.Create(divisor), out remainder);
    public static Vector256<double> TruncMod(this Vector256<double> source, double divisor, out Vector256<double> remainder) => source.TruncMod(Vector256.Create(divisor), out remainder);

#if NET8_0_OR_GREATER

    public static Vector512<double> TruncMod(this Vector512<double> source, double divisor, out Vector512<double> remainder) => source.TruncMod(Vector512.Create(divisor), out remainder);

#endif

    #endregion // TruncMod

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Vector256<double> VectorTripleProduct(this Vector256<double> source, Vector256<double> second, Vector256<double> third)
      => Cross(source, Cross(second, third));

    /// <summary>Returns a new vector with...</summary>
    public static Vector128<double> WithinBounds(this Vector128<double> source, Vector128<double> bound)
      => System.Runtime.Intrinsics.X86.Avx2.And(System.Runtime.Intrinsics.X86.Avx2.Compare(source, bound, System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedLessThanOrEqualSignaling), System.Runtime.Intrinsics.X86.Avx2.Compare(source, Negate(bound), System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedGreaterThanOrEqualSignaling));

    /// <summary>Returns a new vector with...</summary>
    public static Vector256<double> WithinBounds(this Vector256<double> source, Vector256<double> bound)
      => System.Runtime.Intrinsics.X86.Avx2.And(System.Runtime.Intrinsics.X86.Avx2.Compare(source, bound, System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedLessThanOrEqualSignaling), System.Runtime.Intrinsics.X86.Avx2.Compare(source, Negate(bound), System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedGreaterThanOrEqualSignaling));
  }
}

//namespace Flux.Numerics
//{
//  //public static class ExtensionMethodsIntrinsics
//  //{
//  //  public static T X<T>(this Vector256<T> v) where T : struct => v[0];
//  //  public static T Y<T>(this Vector256<T> v) where T : struct => v[1];
//  //  public static T Z<T>(this Vector256<T> v) where T : struct => v[2];
//  //  public static T W<T>(this Vector256<T> v) where T : struct => v[3];
//  //}

//  /// <summary></summary>
//  /// <see href="https://github.com/john-h-k/MathSharp/tree/master/sources/MathSharp/Vector/VectorFloatingPoint/VectorDouble"/>
//  public struct FourD
//    : System.IEquatable<FourD>
//  {
//    public static FourD Empty
//      => new FourD(0, 0, 0, 0);

//    //public static Vector256<double> Epsilon
//    //  => Vector256.Create(double.Epsilon);
//    //public static Vector256<double> NegativeOne
//    //  => Vector256.Create(-1d);
//    public static Vector256<double> One
//      => Vector256.Create(1d);
//    //public static Vector256<double> Zero
//    //  => Vector256<double>.Zero;

//    public static Vector256<double> MaskNotSign
//      => Vector256.Create(~long.MaxValue).AsDouble();
//    //public static readonly Vector256<double> MaskSign = Vector256.Create(long.MaxValue).AsDouble();

//    //public static Vector256<double> MaskX
//    //  => Vector256.Create(+0, -1, -1, -1).AsDouble();
//    //public static Vector256<double> MaskY
//    //  => Vector256.Create(-1, +0, -1, -1).AsDouble();
//    //public static Vector256<double> MaskZ
//    //  => Vector256.Create(-1, -1, +0, -1).AsDouble();
//    public static Vector256<double> MaskW
//      => Vector256.Create(-1, -1, -1, +0).AsDouble();

//    //public static Vector256<double> UnitX
//    //  => Vector256.Create(1, 0, 0, 0).AsDouble();
//    //public static Vector256<double> UnitY
//    //  => Vector256.Create(0, 1, 0, 0).AsDouble();
//    //public static Vector256<double> UnitZ
//    //  => Vector256.Create(0, 0, 1, 0).AsDouble();
//    //public static Vector256<double> UnitW
//    //  => Vector256.Create(0, 0, 0, 1).AsDouble();

//    //public static Vector256<double> OneOverPI
//    //  => Vector256.Create(Maths.PiInto1);
//    public static Vector256<double> OneOverPI2
//      => Vector256.Create(1 / Maths.PiOver2);
//    public static Vector256<double> PI2
//      => Vector256.Create(Maths.PiOver2);

//    private readonly Vector256<double> m_v256;

//    public double X
//      => m_v256[0];
//    public double Y
//      => m_v256[1];
//    public double Z
//      => m_v256[2];
//    public double W
//      => m_v256[3];

//    public FourD(in Vector256<double> xyzw)
//      => m_v256 = xyzw;
//    public FourD(double x, double y, double z, double w)
//      => m_v256 = Vector256.Create(x, y, z, w);
//    public FourD(double x, double y, double z)
//      : this(x, y, z, 0)
//    { }
//    public FourD(double x, double y)
//      : this(x, y, 0)
//    { }

//    public bool IsEmpty
//      => Equals(Empty);

//    /// <summary>Retreives a copy of the Vector256 store for the instance.</summary>
//    public Vector256<double> V256
//      => m_v256;

//    #region Static methods
//    /// <summary>Returns the vector with absolute values.</summary>
//    public static Vector256<double> Abs(in Vector256<double> v)
//      => Max(Subtract(Vector256<double>.Zero, v), v);
//    /// <summary>Returns a new vector with the sum of the two vectors.</summary>
//    public static Vector256<double> Add(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Add(v1, v2)
//      : Vector256.Create(v1[0] + v2[0], v1[1] + v2[1], v1[2] + v2[2], v1[3] + v2[3]);
//    /// <summary>Returns a new vector with the sum of the vector components and the scalar value.</summary>
//    public static Vector256<double> Add(in Vector256<double> v, double scalar)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? Add(v, Vector256.Create(scalar))
//      : Vector256.Create(v[0] + scalar, v[1] + scalar, v[2] + scalar, v[3] + scalar);
//    /// <summary>Returns the vector with its components clamped between min and max.</summary>
//    public static Vector256<double> Clamp(in Vector256<double> v, in Vector256<double> min, in Vector256<double> max)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? Max(Min(v, max), min)
//      : Vector256.Create(System.Math.Clamp(v[0], min[0], max[0]), System.Math.Clamp(v[1], min[1], max[1]), System.Math.Clamp(v[2], min[2], max[2]), System.Math.Clamp(v[3], min[3], max[3]));
//    /// <summary>Returns the vector with the values from clamped between min and max.</summary>
//    public static Vector256<double> CopySign(in Vector256<double> v, in Vector256<double> sign)
//      => System.Runtime.Intrinsics.X86.Avx.Or(Sign(sign), Abs(v));
//    /// <summary>Returns the cross product of the vector.</summary>
//    /// <remarks>
//    /// Cross product of A(x, y, z, _) and B(x, y, z, _) is
//    ///                    0  1  2  3        0  1  2  3
//    ///
//    /// '(X = (Ay * Bz) - (Az * By), Y = (Az * Bx) - (Ax * Bz), Z = (Ax * By) - (Ay * Bx)'
//    ///           1           2              1           2              1            2
//    ///
//    /// So we can do (Ay, Az, Ax, _) * (Bz, Bx, By, _) (last elem is irrelevant, as this is for Vector3)
//    /// which leaves us with a of the first subtraction element for each (marked 1 above)
//    /// Then we repeat with the right hand of subtractions (Az, Ax, Ay, _) * (By, Bz, Bx, _)
//    /// which leaves us with the right hand sides (marked 2 above)
//    /// Then we subtract them to get the correct vector
//    /// We then mask out W to zero, because that is required for the Vector3 representation
//    ///
//    /// We perform the first 2 multiplications by shuffling the vectors and then multiplying them
//    /// Helpers.Shuffle is the same as the C++ macro _MM_SHUFFLE, and you provide the order you wish the elements
//    /// to be in *reversed* (no clue why), so here (3, 0, 2, 1) means you have the 2nd elem (1, 0 indexed) in the first slot,
//    /// the 3rd elem (2) in the next one, the 1st elem (0) in the next one, and the 4th (3, W/_, unused here) in the last reg
//    /// </remarks>
//    public static Vector256<double> CrossProduct3D(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx2.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Subtract(System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v1, ShuffleValues.YZXW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v2, ShuffleValues.ZXYW)), System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v1, ShuffleValues.ZXYW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v2, ShuffleValues.YZXW))), MaskW)
//      : Vector256.Create(v1[1] * v2[2] - v1[2] * v2[1], v1[2] * v2[0] - v1[0] * v2[2], v1[0] * v2[1] - v1[1] * v2[0], 0);
//    /// <summary>Computes the euclidean distance between two vectors.</summary>
//    public static Vector256<double> Distance2D(in Vector256<double> v1, in Vector256<double> v2)
//      => Length2D(Subtract(v1, v2));
//    /// <summary>Returns the distance between the two vectors.</summary>
//    public static Vector256<double> Distance3D(in Vector256<double> v1, in Vector256<double> v2)
//      => Length3D(Subtract(v1, v2));
//    /// <summary>Computes the euclidean distance squared between two vectors.</summary>
//    public static Vector256<double> DistanceSquared2D(in Vector256<double> v1, in Vector256<double> v2)
//      => LengthSquared2D(Subtract(v1, v2));
//    /// <summary>Returns the squared distance between the two vectors.</summary>
//    public static Vector256<double> DistanceSquared3D(in Vector256<double> v1, in Vector256<double> v2)
//      => LengthSquared3D(Subtract(v1, v2));
//    public static Vector256<double> Divide(in Vector256<double> v, in Vector256<double> divisor)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Divide(v, divisor)
//      : Vector256.Create(v[0] / divisor[0], v[1] / divisor[1], v[2] / divisor[2], v[3] / divisor[3]);
//    public static Vector256<double> Divide(in Vector256<double> v, double divisor)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? Subtract(v, Vector256.Create(divisor))
//      : Vector256.Create(v[0] / divisor, v[1] / divisor, v[2] / divisor, v[3] / divisor);
//    /// <summary>Returns the dot product of the two given vectors.</summary>
//    public static Vector256<double> DotProduct2D(in Vector256<double> v1, in Vector256<double> v2)
//    {
//      if (System.Runtime.Intrinsics.X86.Sse41.IsSupported) // SSE4.1 has a native dot product instruction, dppd
//      {
//        var dp = System.Runtime.Intrinsics.X86.Sse41.DotProduct(v1.GetLower(), v2.GetLower(), 0b_0011_1111); // Multiply the first 2 elements of each and broadcasts it into each element of the returning vector.
//        return Vector256.Create(dp, dp);
//      }
//      else if (System.Runtime.Intrinsics.X86.Sse3.IsSupported)
//      {
//        var tmp = System.Runtime.Intrinsics.X86.Sse2.Multiply(v1.GetLower(), v2.GetLower());
//        return DuplicateV128IntoV256(System.Runtime.Intrinsics.X86.Sse3.HorizontalAdd(tmp, tmp));
//      }
//      else if (System.Runtime.Intrinsics.X86.Sse2.IsSupported)
//      {
//        var tmp = System.Runtime.Intrinsics.X86.Sse2.Multiply(v1.GetLower(), v2.GetLower());
//        var dot = System.Runtime.Intrinsics.X86.Sse2.Add(tmp, System.Runtime.Intrinsics.X86.Sse2.Shuffle(tmp, tmp, ShuffleValues.YXYX));
//        return dot.ToVector256Unsafe().WithUpper(dot);
//      }

//      return Vector256.Create(v1[0] * v2[0] + v1[1] * v2[1]);
//    }
//    /// <summary>Returns the dot product of the vector.</summary>
//    public static Vector256<double> DotProduct3D(in Vector256<double> v1, in Vector256<double> v2)
//    {
//      if (System.Runtime.Intrinsics.X86.Avx.IsSupported)
//      {
//        var result = System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Multiply(v1, v2), MaskW); // Normal multiply of the four components and mask out W = (X, Y, Z, 0).
//        result = System.Runtime.Intrinsics.X86.Avx.HorizontalAdd(result, result); // Add component pairs = (X + Y, X + Y, Z + 0, Z + 0).
//        result = System.Runtime.Intrinsics.X86.Avx.Add(result, System.Runtime.Intrinsics.X86.Avx.Permute2x128(result, result, 0b_0000_0001)); // Swap the two halfs and add it to the previous result = (X + Z, Y + W, Z + X, W + Y).
//        return result; // Each element contains the dot product (3D).
//      }

//      return Vector256.Create(v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2]);
//    }
//    /// <summary>Returns the dot product of the vector.</summary>
//    public static Vector256<double> DotProduct4D(in Vector256<double> v1, in Vector256<double> v2)
//    {
//      if (System.Runtime.Intrinsics.X86.Avx.IsSupported)
//      {
//        var result = System.Runtime.Intrinsics.X86.Avx.Multiply(v1, v2); // Normal multiply of the four components = (X, Y, Z, W).
//        result = System.Runtime.Intrinsics.X86.Avx.HorizontalAdd(result, result); // Add component pairs = (X + Y, X + Y, Z + W, Z + W).
//        result = System.Runtime.Intrinsics.X86.Avx.Add(result, System.Runtime.Intrinsics.X86.Avx.Permute2x128(result, result, 0b_0000_0001)); // Swap the two halfs and add it to the previous result = (X + Z, Y + W, Z + X, W + Y).
//        return result; // Each element contains the dot product (4D).
//      }

//      return Vector256.Create(v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2] + v1[3] * v2[3]);
//    }
//    /// <summary>Duplicate the two values in the V128 into four values in a V256.</summary>
//    public static Vector256<double> DuplicateV128IntoV256(in Vector128<double> v)
//      => Vector256.Create(v, v);
//    /// <summary>Creates a new Vector4D from a Vector256<double>.</summary>
//    public static FourD FromV256(Vector256<double> v)
//      => new FourD(v[0], v[1], v[2], v[3]);
//    //public static Vector256<double> HorizontalAdd(in Vector256<double> v1, in Vector256<double> v2)
//    //  => System.Runtime.Intrinsics.X86.Avx.IsSupported
//    //  ? System.Runtime.Intrinsics.X86.Avx.HorizontalAdd(v1, v2)
//    //  : Vector256.Create(v1[0] + v1[1], v2[0] + v2[1], v1[2] + v1[3], v2[2] + v2[3]);
//    /// <summary>Returns the length of the given Vector2D.</summary>
//    public static Vector256<double> Length2D(in Vector256<double> v)
//      => Sqrt(DotProduct2D(v, v));
//    /// <summary>Returns the Euclidean length (magnitude) of the vector.</summary>
//    public static Vector256<double> Length3D(in Vector256<double> v)
//      => Sqrt(DotProduct3D(v, v));
//    /// <summary>Returns the length of the given Vector2D.</summary>
//    public static Vector256<double> LengthSquared2D(in Vector256<double> v)
//      => DotProduct2D(v, v);
//    /// <summary>Returns the squared Euclidean length (magnitude) of the vector.</summary>
//    public static Vector256<double> LengthSquared3D(in Vector256<double> v)
//      => DotProduct3D(v, v);
//    /// <summary>Returns a new vector that is a linear interpolation of the two specified vectors.</summary>
//    /// <param name="weight">The weight factor [0, 1]. The resulting vector is v1 when weight is 0, v2 when weight is 1, otherwise in between.</param>
//    public static Vector256<double> Lerp(in Vector256<double> v1, in Vector256<double> v2, double weight)
//      => Add(v1, Multiply(Subtract(v2, v1), Vector256.Create(weight >= 0 && weight <= 1 ? weight : throw new System.ArgumentOutOfRangeException(nameof(weight))))); // General formula of linear interpolation: (from + (to - from) * weight).
//    public static Vector256<double> Max(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Max(v1, v2)
//      : Vector256.Create(System.Math.Max(v1[0], v2[0]), System.Math.Max(v1[1], v2[1]), System.Math.Max(v1[2], v2[2]), System.Math.Max(v1[3], v2[3]));
//    public static Vector256<double> Min(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Min(v1, v2)
//      : Vector256.Create(System.Math.Min(v1[0], v2[0]), System.Math.Min(v1[1], v2[1]), System.Math.Min(v1[2], v2[2]), System.Math.Min(v1[3], v2[3]));
//    public static Vector256<double> ModPI2(in Vector256<double> v)
//      => Subtract(v, Multiply(Round(Multiply(v, OneOverPI2)), PI2));
//    public static Vector256<double> Multiply(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Multiply(v1, v2)
//      : Vector256.Create(v1[0] * v2[0], v1[1] * v2[1], v1[2] * v2[2], v1[3] * v2[3]);
//    public static Vector256<double> Multiply(in Vector256<double> v, in double scalar)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? Multiply(v, Vector256.Create(scalar))
//      : Vector256.Create(v[0] * scalar, v[1] * scalar, v[2] * scalar, v[3] * scalar);
//    /// <summary>Returns (x * y) + z on each element of a <see cref="Vector256{Double}"/>, rounded as one ternary operation.</summary>
//    /// <param name="x">The vector to be multiplied with <paramref name="y"/></param>
//    /// <param name="y">The vector to be multiplied with <paramref name="x"/></param>
//    /// <param name="z">The vector to be added to to the infinite precision multiplication of <paramref name="x"/> and <paramref name="y"/></param>
//    /// <returns>(x * y) + z on each element, rounded as one ternary operation</returns>
//    public static Vector256<double> MultiplyAdd(in Vector256<double> x, in Vector256<double> y, in Vector256<double> z)
//      => System.Runtime.Intrinsics.X86.Fma.IsSupported
//      ? System.Runtime.Intrinsics.X86.Fma.MultiplyAdd(x, y, z)
//      : Vector256.Create(x[0] * y[0] + z[0], x[1] * y[1] + z[1], x[2] * y[2] + z[2], x[3] * y[3] + z[3]);
//    /// <summary>Returns the values negated.</summary>
//    public static Vector256<double> Negate(in Vector256<double> v)
//      => System.Runtime.Intrinsics.X86.Avx.Xor(MaskNotSign, v);
//    /// <summary>Scales the Vector2D to unit length.</summary>
//    public static Vector256<double> Normalize2D(in Vector256<double> v)
//      => Divide(v, Length2D(v));
//    /// <summary>Scales the Vector3D to unit length.</summary>
//    public static Vector256<double> Normalize3D(in Vector256<double> v)
//      => Divide(v, Length3D(v));
//    /// <summary>Returns a new vector that is a normalized linear interpolation of the two specified vectors. This is the 2D version because nlerp use normalize which is 2D or 3D dependent.</summary>
//    public static Vector256<double> Nlerp2D(in Vector256<double> v1, in Vector256<double> v2, double weight)
//      => Normalize2D(Lerp(v1, v2, weight));
//    /// <summary>Returns a new vector that is a normalized linear interpolation of the two specified vectors. This is the 3D version because nlerp use normalize which is 2D or 3D dependent.</summary>
//    public static Vector256<double> Nlerp3D(in Vector256<double> v1, in Vector256<double> v2, double weight)
//      => Normalize3D(Lerp(v1, v2, weight));
//    public static Vector256<double> Reciprocal(in Vector256<double> v)
//      => Divide(One, v);
//    public static Vector256<double> ReciprocalSqrt(in Vector256<double> v)
//      => Divide(One, Sqrt(v));
//    /// <summary>Calculates the reflection of an incident ray. Reflection: (incident - (2 * DotProduct(incident, normal)) * normal)</summary>
//    /// <param name="incident">The incident ray's vector.</param>
//    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
//    /// <returns>The vector of the reflected ray.</returns>
//    public static Vector256<double> Reflect2D(in Vector256<double> incident, in Vector256<double> normal)
//      => Subtract(incident, Multiply(Multiply(DotProduct2D(incident, normal), 2), normal));
//    /// <summary>Calculates the reflection of an incident ray.</summary>
//    /// <param name="incident">The incident ray's vector.</param>
//    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
//    /// <returns>The vector of the reflected ray.</returns>
//    public static Vector256<double> Reflect3D(in Vector256<double> incident, in Vector256<double> normal)
//      => Subtract(incident, Multiply(Multiply(DotProduct3D(incident, normal), 2), normal)); // reflection = incident - (2 * DotProduct(incident, normal)) * normal
//    public static Vector256<double> Remainder(in Vector256<double> v, in Vector256<double> divisor)
//      => Subtract(v, Multiply(Truncate(Divide(v, divisor)), divisor));
//    public static Vector256<double> Remainder(in Vector256<double> v, double divisor)
//      => Remainder(v, Vector256.Create(divisor));
//    public static Vector256<double> Round(in Vector256<double> v)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.RoundToNearestInteger(v)
//      : System.Runtime.Intrinsics.X86.Sse41.IsSupported && v.GetLower() is var lower && v.GetUpper() is var upper
//      ? System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(lower).ToVector256Unsafe().WithUpper(System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(upper))
//      : Vector256.Create(System.Math.Round(v[0]), System.Math.Round(v[1]), System.Math.Round(v[2]), System.Math.Round(v[3]));
//    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
//    public static Vector256<double> ScalarTripleProduct3D(in Vector256<double> a, in Vector256<double> b, in Vector256<double> c)
//      => DotProduct3D(a, CrossProduct3D(b, c));
//    public static Vector256<double> Sign(in Vector256<double> v)
//      => System.Runtime.Intrinsics.X86.Avx.And(v, MaskNotSign);
//    /// <summary>Returns a new vector that is a spherical linear interpolation of the two specified vectors. This is the 2D version because slerp use other functionality that is 2D dependent.</summary>
//    public static Vector256<double> Slerp2D(in Vector256<double> v1, in Vector256<double> v2, double weight)
//    {
//      var dot = System.Math.Clamp(DotProduct2D(v1, v2)[0], -1, 1); // Yields the cos(angle) between the two vectors. Ensure Acos range [-1, 1] by clamping.

//      var theta = System.Math.Acos(dot) * weight; // Get angle (ensure acos range [-1, 1]) and multiply it by weight to produce angle between v1 and vr (relative vector).

//      var vr = Normalize2D(Subtract(v2, Multiply(v1, dot))); // Intermediate vector normalized (orthonormal basis?).

//      return Add(Multiply(Normalize2D(v1), System.Math.Cos(theta)), Multiply(vr, System.Math.Sin(theta))); // Resulting vector.
//    }
//    /// <summary>Returns a new vector that is a spherical linear interpolation of the two specified vectors. This is a 3D version because slerp use other functionality that is 3D dependent.</summary>
//    public static Vector256<double> SlerpA3D(Vector256<double> v1, Vector256<double> v2, double weight)
//    {
//      v1 = Normalize3D(v1);
//      v2 = Normalize3D(v2);

//      var dot = System.Math.Clamp(DotProduct3D(v1, v2)[0], -1, 1); // Yields the cos(angle) between the two vectors. Ensure Acos range [-1, 1] by clamping.

//      var theta = System.Math.Acos(dot) * weight; // Get angle (acos) and multiply it by weight to produce angle between v1 and vr (relative vector).

//      var vr = Normalize3D(Subtract(v2, Multiply(v1, dot))); // Intermediate vector normalized (orthonormal basis?).

//      return Add(Multiply(Normalize3D(v1), System.Math.Cos(theta)), Multiply(vr, System.Math.Sin(theta))); // Resulting vector.
//    }
//    /// <summary>Returns a new vector that is a spherical linear interpolation of the two specified vectors. This is a 3D version because slerp use other functionality that is 3D dependent.</summary>
//    public static Vector256<double> Slerp3D(Vector256<double> v1, Vector256<double> v2, double weight)
//    {
//      v1 = Normalize3D(v1);
//      v2 = Normalize3D(v2);

//      double scale0, scale1;

//      if (DotProduct3D(v1, v2)[0] is var cosTheta && cosTheta != 0)
//      {
//        var theta = System.Math.Acos(System.Math.Clamp(cosTheta, -1, 1));
//        var sinTheta = System.Math.Sin(theta);
//        scale0 = System.Math.Sin((1 - weight) * theta) / sinTheta;
//        scale1 = System.Math.Sin(weight * theta) / sinTheta;
//      }
//      else
//      {
//        scale0 = 1 - weight;
//        scale1 = weight;
//      }

//      return Add(Multiply(v1, scale0), Multiply(v2, scale1));
//    }
//    public static Vector256<double> Sqrt(in Vector256<double> v)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Sqrt(v)
//      : Vector256.Create(System.Math.Sqrt(v[0]), System.Math.Sqrt(v[1]), System.Math.Sqrt(v[2]), System.Math.Sqrt(v[3]));
//    public static Vector256<double> Square(in Vector256<double> v)
//      => Multiply(v, v);
//    public static Vector256<double> Subtract(in Vector256<double> v1, in Vector256<double> v2)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.Subtract(v1, v2)
//      : Vector256.Create(v1[0] - v2[0], v1[1] - v2[1], v1[2] - v2[2], v1[3] - v2[3]);
//    public static Vector256<double> Subtract(in Vector256<double> v, double scalar)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? Subtract(v, Vector256.Create(scalar))
//      : Vector256.Create(v[0] - scalar, v[1] - scalar, v[2] - scalar, v[3] - scalar);
//    /// <summary>Truncate the values, as in System.Math.Truncate().</summary>
//    public static Vector256<double> Truncate(in Vector256<double> v)
//      => System.Runtime.Intrinsics.X86.Avx.IsSupported
//      ? System.Runtime.Intrinsics.X86.Avx.RoundToZero(v)
//      : System.Runtime.Intrinsics.X86.Sse41.IsSupported && v.GetLower() is var lower && v.GetUpper() is var upper
//      ? System.Runtime.Intrinsics.X86.Sse41.RoundToZero(lower).ToVector256Unsafe().WithUpper(System.Runtime.Intrinsics.X86.Sse41.RoundToZero(upper))
//      : Vector256.Create(System.Math.Truncate(v[0]), System.Math.Truncate(v[1]), System.Math.Truncate(v[2]), System.Math.Truncate(v[3]));
//    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
//    public static Vector256<double> VectorTripleProduct3D(in Vector256<double> a, in Vector256<double> b, in Vector256<double> c)
//      => CrossProduct3D(a, CrossProduct3D(b, c));
//    public static Vector256<double> WithinBounds(in Vector256<double> v, in Vector256<double> bound)
//      => System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Compare(v, bound, System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedLessThanOrEqualSignaling), System.Runtime.Intrinsics.X86.Avx.Compare(v, Negate(bound), System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedGreaterThanOrEqualSignaling));

//    #endregion Static methods

//    #region Overloaded operators
//    public static bool operator ==(FourD a, FourD b)
//      => a.Equals(b);
//    public static bool operator !=(FourD a, FourD b)
//      => !a.Equals(b);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    public bool Equals(FourD other)
//      => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is FourD o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(X, Y, Z, W);
//    public override string ToString()
//      => $"{GetType().Name} {{ X = {X}, Y = {Y}, Z = {Z}, W = {W} }}";
//    #endregion Object overrides
//  }

//  /// <summary>
//  /// Values used in <see cref="Vector.Shuffle(Vector128{float}, Vector128{float}, byte)"/>,
//  /// <see cref="Vector.Shuffle(Vector256{double}, Vector256{double}, byte)"/>,
//  /// <see cref="Vector.Shuffle(System.Runtime.Intrinsics.Vector128{float},byte)"/>,
//  /// <see cref="Vector.Shuffle(System.Runtime.Intrinsics.Vector256{double},byte)"/>,
//  /// methods to dictate the ordering of the resultant vectors
//  /// </summary>
//  /// <example>
//  /// To reverse a vector (x, y, z, w), to (w, z, y, x), you would do
//  /// <code>Vector.Shuffle(vector, <see cref="WZYX"/>)</code>
//  /// </example>
//  public static class ShuffleValues
//  {
//    //#region Byte Mask Constants to Shuffle Values.
//    //public const byte XXXX = (0 << 6) | (0 << 4) | (0 << 2) | 0;
//    //public const byte YXXX = (0 << 6) | (0 << 4) | (0 << 2) | 1;
//    //public const byte ZXXX = (0 << 6) | (0 << 4) | (0 << 2) | 2;
//    //public const byte WXXX = (0 << 6) | (0 << 4) | (0 << 2) | 3;
//    //public const byte XYXX = (0 << 6) | (0 << 4) | (1 << 2) | 0;
//    //public const byte YYXX = (0 << 6) | (0 << 4) | (1 << 2) | 1;
//    //public const byte ZYXX = (0 << 6) | (0 << 4) | (1 << 2) | 2;
//    //public const byte WYXX = (0 << 6) | (0 << 4) | (1 << 2) | 3;
//    //public const byte XZXX = (0 << 6) | (0 << 4) | (2 << 2) | 0;
//    //public const byte YZXX = (0 << 6) | (0 << 4) | (2 << 2) | 1;
//    //public const byte ZZXX = (0 << 6) | (0 << 4) | (2 << 2) | 2;
//    //public const byte WZXX = (0 << 6) | (0 << 4) | (2 << 2) | 3;
//    //public const byte XWXX = (0 << 6) | (0 << 4) | (3 << 2) | 0;
//    //public const byte YWXX = (0 << 6) | (0 << 4) | (3 << 2) | 1;
//    //public const byte ZWXX = (0 << 6) | (0 << 4) | (3 << 2) | 2;
//    //public const byte WWXX = (0 << 6) | (0 << 4) | (3 << 2) | 3;
//    //public const byte XXYX = (0 << 6) | (1 << 4) | (0 << 2) | 0;
//    public const byte YXYX = (0 << 6) | (1 << 4) | (0 << 2) | 1;
//    //public const byte ZXYX = (0 << 6) | (1 << 4) | (0 << 2) | 2;
//    //public const byte WXYX = (0 << 6) | (1 << 4) | (0 << 2) | 3;
//    //public const byte XYYX = (0 << 6) | (1 << 4) | (1 << 2) | 0;
//    //public const byte YYYX = (0 << 6) | (1 << 4) | (1 << 2) | 1;
//    //public const byte ZYYX = (0 << 6) | (1 << 4) | (1 << 2) | 2;
//    //public const byte WYYX = (0 << 6) | (1 << 4) | (1 << 2) | 3;
//    //public const byte XZYX = (0 << 6) | (1 << 4) | (2 << 2) | 0;
//    //public const byte YZYX = (0 << 6) | (1 << 4) | (2 << 2) | 1;
//    //public const byte ZZYX = (0 << 6) | (1 << 4) | (2 << 2) | 2;
//    //public const byte WZYX = (0 << 6) | (1 << 4) | (2 << 2) | 3;
//    //public const byte XWYX = (0 << 6) | (1 << 4) | (3 << 2) | 0;
//    //public const byte YWYX = (0 << 6) | (1 << 4) | (3 << 2) | 1;
//    //public const byte ZWYX = (0 << 6) | (1 << 4) | (3 << 2) | 2;
//    //public const byte WWYX = (0 << 6) | (1 << 4) | (3 << 2) | 3;
//    //public const byte XXZX = (0 << 6) | (2 << 4) | (0 << 2) | 0;
//    //public const byte YXZX = (0 << 6) | (2 << 4) | (0 << 2) | 1;
//    //public const byte ZXZX = (0 << 6) | (2 << 4) | (0 << 2) | 2;
//    //public const byte WXZX = (0 << 6) | (2 << 4) | (0 << 2) | 3;
//    //public const byte XYZX = (0 << 6) | (2 << 4) | (1 << 2) | 0;
//    //public const byte YYZX = (0 << 6) | (2 << 4) | (1 << 2) | 1;
//    //public const byte ZYZX = (0 << 6) | (2 << 4) | (1 << 2) | 2;
//    //public const byte WYZX = (0 << 6) | (2 << 4) | (1 << 2) | 3;
//    //public const byte XZZX = (0 << 6) | (2 << 4) | (2 << 2) | 0;
//    //public const byte YZZX = (0 << 6) | (2 << 4) | (2 << 2) | 1;
//    //public const byte ZZZX = (0 << 6) | (2 << 4) | (2 << 2) | 2;
//    //public const byte WZZX = (0 << 6) | (2 << 4) | (2 << 2) | 3;
//    //public const byte XWZX = (0 << 6) | (2 << 4) | (3 << 2) | 0;
//    //public const byte YWZX = (0 << 6) | (2 << 4) | (3 << 2) | 1;
//    //public const byte ZWZX = (0 << 6) | (2 << 4) | (3 << 2) | 2;
//    //public const byte WWZX = (0 << 6) | (2 << 4) | (3 << 2) | 3;
//    //public const byte XXWX = (0 << 6) | (3 << 4) | (0 << 2) | 0;
//    //public const byte YXWX = (0 << 6) | (3 << 4) | (0 << 2) | 1;
//    //public const byte ZXWX = (0 << 6) | (3 << 4) | (0 << 2) | 2;
//    //public const byte WXWX = (0 << 6) | (3 << 4) | (0 << 2) | 3;
//    //public const byte XYWX = (0 << 6) | (3 << 4) | (1 << 2) | 0;
//    //public const byte YYWX = (0 << 6) | (3 << 4) | (1 << 2) | 1;
//    //public const byte ZYWX = (0 << 6) | (3 << 4) | (1 << 2) | 2;
//    //public const byte WYWX = (0 << 6) | (3 << 4) | (1 << 2) | 3;
//    //public const byte XZWX = (0 << 6) | (3 << 4) | (2 << 2) | 0;
//    //public const byte YZWX = (0 << 6) | (3 << 4) | (2 << 2) | 1;
//    //public const byte ZZWX = (0 << 6) | (3 << 4) | (2 << 2) | 2;
//    //public const byte WZWX = (0 << 6) | (3 << 4) | (2 << 2) | 3;
//    //public const byte XWWX = (0 << 6) | (3 << 4) | (3 << 2) | 0;
//    //public const byte YWWX = (0 << 6) | (3 << 4) | (3 << 2) | 1;
//    //public const byte ZWWX = (0 << 6) | (3 << 4) | (3 << 2) | 2;
//    //public const byte WWWX = (0 << 6) | (3 << 4) | (3 << 2) | 3;
//    //public const byte XXXY = (1 << 6) | (0 << 4) | (0 << 2) | 0;
//    //public const byte YXXY = (1 << 6) | (0 << 4) | (0 << 2) | 1;
//    //public const byte ZXXY = (1 << 6) | (0 << 4) | (0 << 2) | 2;
//    //public const byte WXXY = (1 << 6) | (0 << 4) | (0 << 2) | 3;
//    //public const byte XYXY = (1 << 6) | (0 << 4) | (1 << 2) | 0;
//    //public const byte YYXY = (1 << 6) | (0 << 4) | (1 << 2) | 1;
//    //public const byte ZYXY = (1 << 6) | (0 << 4) | (1 << 2) | 2;
//    //public const byte WYXY = (1 << 6) | (0 << 4) | (1 << 2) | 3;
//    //public const byte XZXY = (1 << 6) | (0 << 4) | (2 << 2) | 0;
//    //public const byte YZXY = (1 << 6) | (0 << 4) | (2 << 2) | 1;
//    //public const byte ZZXY = (1 << 6) | (0 << 4) | (2 << 2) | 2;
//    //public const byte WZXY = (1 << 6) | (0 << 4) | (2 << 2) | 3;
//    //public const byte XWXY = (1 << 6) | (0 << 4) | (3 << 2) | 0;
//    //public const byte YWXY = (1 << 6) | (0 << 4) | (3 << 2) | 1;
//    //public const byte ZWXY = (1 << 6) | (0 << 4) | (3 << 2) | 2;
//    //public const byte WWXY = (1 << 6) | (0 << 4) | (3 << 2) | 3;
//    //public const byte XXYY = (1 << 6) | (1 << 4) | (0 << 2) | 0;
//    //public const byte YXYY = (1 << 6) | (1 << 4) | (0 << 2) | 1;
//    //public const byte ZXYY = (1 << 6) | (1 << 4) | (0 << 2) | 2;
//    //public const byte WXYY = (1 << 6) | (1 << 4) | (0 << 2) | 3;
//    //public const byte XYYY = (1 << 6) | (1 << 4) | (1 << 2) | 0;
//    //public const byte YYYY = (1 << 6) | (1 << 4) | (1 << 2) | 1;
//    //public const byte ZYYY = (1 << 6) | (1 << 4) | (1 << 2) | 2;
//    //public const byte WYYY = (1 << 6) | (1 << 4) | (1 << 2) | 3;
//    //public const byte XZYY = (1 << 6) | (1 << 4) | (2 << 2) | 0;
//    //public const byte YZYY = (1 << 6) | (1 << 4) | (2 << 2) | 1;
//    //public const byte ZZYY = (1 << 6) | (1 << 4) | (2 << 2) | 2;
//    //public const byte WZYY = (1 << 6) | (1 << 4) | (2 << 2) | 3;
//    //public const byte XWYY = (1 << 6) | (1 << 4) | (3 << 2) | 0;
//    //public const byte YWYY = (1 << 6) | (1 << 4) | (3 << 2) | 1;
//    //public const byte ZWYY = (1 << 6) | (1 << 4) | (3 << 2) | 2;
//    //public const byte WWYY = (1 << 6) | (1 << 4) | (3 << 2) | 3;
//    //public const byte XXZY = (1 << 6) | (2 << 4) | (0 << 2) | 0;
//    //public const byte YXZY = (1 << 6) | (2 << 4) | (0 << 2) | 1;
//    //public const byte ZXZY = (1 << 6) | (2 << 4) | (0 << 2) | 2;
//    //public const byte WXZY = (1 << 6) | (2 << 4) | (0 << 2) | 3;
//    //public const byte XYZY = (1 << 6) | (2 << 4) | (1 << 2) | 0;
//    //public const byte YYZY = (1 << 6) | (2 << 4) | (1 << 2) | 1;
//    //public const byte ZYZY = (1 << 6) | (2 << 4) | (1 << 2) | 2;
//    //public const byte WYZY = (1 << 6) | (2 << 4) | (1 << 2) | 3;
//    //public const byte XZZY = (1 << 6) | (2 << 4) | (2 << 2) | 0;
//    //public const byte YZZY = (1 << 6) | (2 << 4) | (2 << 2) | 1;
//    //public const byte ZZZY = (1 << 6) | (2 << 4) | (2 << 2) | 2;
//    //public const byte WZZY = (1 << 6) | (2 << 4) | (2 << 2) | 3;
//    //public const byte XWZY = (1 << 6) | (2 << 4) | (3 << 2) | 0;
//    //public const byte YWZY = (1 << 6) | (2 << 4) | (3 << 2) | 1;
//    //public const byte ZWZY = (1 << 6) | (2 << 4) | (3 << 2) | 2;
//    //public const byte WWZY = (1 << 6) | (2 << 4) | (3 << 2) | 3;
//    //public const byte XXWY = (1 << 6) | (3 << 4) | (0 << 2) | 0;
//    //public const byte YXWY = (1 << 6) | (3 << 4) | (0 << 2) | 1;
//    //public const byte ZXWY = (1 << 6) | (3 << 4) | (0 << 2) | 2;
//    //public const byte WXWY = (1 << 6) | (3 << 4) | (0 << 2) | 3;
//    //public const byte XYWY = (1 << 6) | (3 << 4) | (1 << 2) | 0;
//    //public const byte YYWY = (1 << 6) | (3 << 4) | (1 << 2) | 1;
//    //public const byte ZYWY = (1 << 6) | (3 << 4) | (1 << 2) | 2;
//    //public const byte WYWY = (1 << 6) | (3 << 4) | (1 << 2) | 3;
//    //public const byte XZWY = (1 << 6) | (3 << 4) | (2 << 2) | 0;
//    //public const byte YZWY = (1 << 6) | (3 << 4) | (2 << 2) | 1;
//    //public const byte ZZWY = (1 << 6) | (3 << 4) | (2 << 2) | 2;
//    //public const byte WZWY = (1 << 6) | (3 << 4) | (2 << 2) | 3;
//    //public const byte XWWY = (1 << 6) | (3 << 4) | (3 << 2) | 0;
//    //public const byte YWWY = (1 << 6) | (3 << 4) | (3 << 2) | 1;
//    //public const byte ZWWY = (1 << 6) | (3 << 4) | (3 << 2) | 2;
//    //public const byte WWWY = (1 << 6) | (3 << 4) | (3 << 2) | 3;
//    //public const byte XXXZ = (2 << 6) | (0 << 4) | (0 << 2) | 0;
//    //public const byte YXXZ = (2 << 6) | (0 << 4) | (0 << 2) | 1;
//    //public const byte ZXXZ = (2 << 6) | (0 << 4) | (0 << 2) | 2;
//    //public const byte WXXZ = (2 << 6) | (0 << 4) | (0 << 2) | 3;
//    //public const byte XYXZ = (2 << 6) | (0 << 4) | (1 << 2) | 0;
//    //public const byte YYXZ = (2 << 6) | (0 << 4) | (1 << 2) | 1;
//    //public const byte ZYXZ = (2 << 6) | (0 << 4) | (1 << 2) | 2;
//    //public const byte WYXZ = (2 << 6) | (0 << 4) | (1 << 2) | 3;
//    //public const byte XZXZ = (2 << 6) | (0 << 4) | (2 << 2) | 0;
//    //public const byte YZXZ = (2 << 6) | (0 << 4) | (2 << 2) | 1;
//    //public const byte ZZXZ = (2 << 6) | (0 << 4) | (2 << 2) | 2;
//    //public const byte WZXZ = (2 << 6) | (0 << 4) | (2 << 2) | 3;
//    //public const byte XWXZ = (2 << 6) | (0 << 4) | (3 << 2) | 0;
//    //public const byte YWXZ = (2 << 6) | (0 << 4) | (3 << 2) | 1;
//    //public const byte ZWXZ = (2 << 6) | (0 << 4) | (3 << 2) | 2;
//    //public const byte WWXZ = (2 << 6) | (0 << 4) | (3 << 2) | 3;
//    //public const byte XXYZ = (2 << 6) | (1 << 4) | (0 << 2) | 0;
//    //public const byte YXYZ = (2 << 6) | (1 << 4) | (0 << 2) | 1;
//    //public const byte ZXYZ = (2 << 6) | (1 << 4) | (0 << 2) | 2;
//    //public const byte WXYZ = (2 << 6) | (1 << 4) | (0 << 2) | 3;
//    //public const byte XYYZ = (2 << 6) | (1 << 4) | (1 << 2) | 0;
//    //public const byte YYYZ = (2 << 6) | (1 << 4) | (1 << 2) | 1;
//    //public const byte ZYYZ = (2 << 6) | (1 << 4) | (1 << 2) | 2;
//    //public const byte WYYZ = (2 << 6) | (1 << 4) | (1 << 2) | 3;
//    //public const byte XZYZ = (2 << 6) | (1 << 4) | (2 << 2) | 0;
//    //public const byte YZYZ = (2 << 6) | (1 << 4) | (2 << 2) | 1;
//    //public const byte ZZYZ = (2 << 6) | (1 << 4) | (2 << 2) | 2;
//    //public const byte WZYZ = (2 << 6) | (1 << 4) | (2 << 2) | 3;
//    //public const byte XWYZ = (2 << 6) | (1 << 4) | (3 << 2) | 0;
//    //public const byte YWYZ = (2 << 6) | (1 << 4) | (3 << 2) | 1;
//    //public const byte ZWYZ = (2 << 6) | (1 << 4) | (3 << 2) | 2;
//    //public const byte WWYZ = (2 << 6) | (1 << 4) | (3 << 2) | 3;
//    //public const byte XXZZ = (2 << 6) | (2 << 4) | (0 << 2) | 0;
//    //public const byte YXZZ = (2 << 6) | (2 << 4) | (0 << 2) | 1;
//    //public const byte ZXZZ = (2 << 6) | (2 << 4) | (0 << 2) | 2;
//    //public const byte WXZZ = (2 << 6) | (2 << 4) | (0 << 2) | 3;
//    //public const byte XYZZ = (2 << 6) | (2 << 4) | (1 << 2) | 0;
//    //public const byte YYZZ = (2 << 6) | (2 << 4) | (1 << 2) | 1;
//    //public const byte ZYZZ = (2 << 6) | (2 << 4) | (1 << 2) | 2;
//    //public const byte WYZZ = (2 << 6) | (2 << 4) | (1 << 2) | 3;
//    //public const byte XZZZ = (2 << 6) | (2 << 4) | (2 << 2) | 0;
//    //public const byte YZZZ = (2 << 6) | (2 << 4) | (2 << 2) | 1;
//    //public const byte ZZZZ = (2 << 6) | (2 << 4) | (2 << 2) | 2;
//    //public const byte WZZZ = (2 << 6) | (2 << 4) | (2 << 2) | 3;
//    //public const byte XWZZ = (2 << 6) | (2 << 4) | (3 << 2) | 0;
//    //public const byte YWZZ = (2 << 6) | (2 << 4) | (3 << 2) | 1;
//    //public const byte ZWZZ = (2 << 6) | (2 << 4) | (3 << 2) | 2;
//    //public const byte WWZZ = (2 << 6) | (2 << 4) | (3 << 2) | 3;
//    //public const byte XXWZ = (2 << 6) | (3 << 4) | (0 << 2) | 0;
//    //public const byte YXWZ = (2 << 6) | (3 << 4) | (0 << 2) | 1;
//    //public const byte ZXWZ = (2 << 6) | (3 << 4) | (0 << 2) | 2;
//    //public const byte WXWZ = (2 << 6) | (3 << 4) | (0 << 2) | 3;
//    //public const byte XYWZ = (2 << 6) | (3 << 4) | (1 << 2) | 0;
//    //public const byte YYWZ = (2 << 6) | (3 << 4) | (1 << 2) | 1;
//    //public const byte ZYWZ = (2 << 6) | (3 << 4) | (1 << 2) | 2;
//    //public const byte WYWZ = (2 << 6) | (3 << 4) | (1 << 2) | 3;
//    //public const byte XZWZ = (2 << 6) | (3 << 4) | (2 << 2) | 0;
//    //public const byte YZWZ = (2 << 6) | (3 << 4) | (2 << 2) | 1;
//    //public const byte ZZWZ = (2 << 6) | (3 << 4) | (2 << 2) | 2;
//    //public const byte WZWZ = (2 << 6) | (3 << 4) | (2 << 2) | 3;
//    //public const byte XWWZ = (2 << 6) | (3 << 4) | (3 << 2) | 0;
//    //public const byte YWWZ = (2 << 6) | (3 << 4) | (3 << 2) | 1;
//    //public const byte ZWWZ = (2 << 6) | (3 << 4) | (3 << 2) | 2;
//    //public const byte WWWZ = (2 << 6) | (3 << 4) | (3 << 2) | 3;
//    //public const byte XXXW = (3 << 6) | (0 << 4) | (0 << 2) | 0;
//    //public const byte YXXW = (3 << 6) | (0 << 4) | (0 << 2) | 1;
//    //public const byte ZXXW = (3 << 6) | (0 << 4) | (0 << 2) | 2;
//    //public const byte WXXW = (3 << 6) | (0 << 4) | (0 << 2) | 3;
//    //public const byte XYXW = (3 << 6) | (0 << 4) | (1 << 2) | 0;
//    //public const byte YYXW = (3 << 6) | (0 << 4) | (1 << 2) | 1;
//    //public const byte ZYXW = (3 << 6) | (0 << 4) | (1 << 2) | 2;
//    //public const byte WYXW = (3 << 6) | (0 << 4) | (1 << 2) | 3;
//    //public const byte XZXW = (3 << 6) | (0 << 4) | (2 << 2) | 0;
//    public const byte YZXW = (3 << 6) | (0 << 4) | (2 << 2) | 1;
//    //public const byte ZZXW = (3 << 6) | (0 << 4) | (2 << 2) | 2;
//    //public const byte WZXW = (3 << 6) | (0 << 4) | (2 << 2) | 3;
//    //public const byte XWXW = (3 << 6) | (0 << 4) | (3 << 2) | 0;
//    //public const byte YWXW = (3 << 6) | (0 << 4) | (3 << 2) | 1;
//    //public const byte ZWXW = (3 << 6) | (0 << 4) | (3 << 2) | 2;
//    //public const byte WWXW = (3 << 6) | (0 << 4) | (3 << 2) | 3;
//    //public const byte XXYW = (3 << 6) | (1 << 4) | (0 << 2) | 0;
//    //public const byte YXYW = (3 << 6) | (1 << 4) | (0 << 2) | 1;
//    public const byte ZXYW = (3 << 6) | (1 << 4) | (0 << 2) | 2;
//    //public const byte WXYW = (3 << 6) | (1 << 4) | (0 << 2) | 3;
//    //public const byte XYYW = (3 << 6) | (1 << 4) | (1 << 2) | 0;
//    //public const byte YYYW = (3 << 6) | (1 << 4) | (1 << 2) | 1;
//    //public const byte ZYYW = (3 << 6) | (1 << 4) | (1 << 2) | 2;
//    //public const byte WYYW = (3 << 6) | (1 << 4) | (1 << 2) | 3;
//    //public const byte XZYW = (3 << 6) | (1 << 4) | (2 << 2) | 0;
//    //public const byte YZYW = (3 << 6) | (1 << 4) | (2 << 2) | 1;
//    //public const byte ZZYW = (3 << 6) | (1 << 4) | (2 << 2) | 2;
//    //public const byte WZYW = (3 << 6) | (1 << 4) | (2 << 2) | 3;
//    //public const byte XWYW = (3 << 6) | (1 << 4) | (3 << 2) | 0;
//    //public const byte YWYW = (3 << 6) | (1 << 4) | (3 << 2) | 1;
//    //public const byte ZWYW = (3 << 6) | (1 << 4) | (3 << 2) | 2;
//    //public const byte WWYW = (3 << 6) | (1 << 4) | (3 << 2) | 3;
//    //public const byte XXZW = (3 << 6) | (2 << 4) | (0 << 2) | 0;
//    //public const byte YXZW = (3 << 6) | (2 << 4) | (0 << 2) | 1;
//    //public const byte ZXZW = (3 << 6) | (2 << 4) | (0 << 2) | 2;
//    //public const byte WXZW = (3 << 6) | (2 << 4) | (0 << 2) | 3;
//    //public const byte XYZW = (3 << 6) | (2 << 4) | (1 << 2) | 0;
//    //public const byte YYZW = (3 << 6) | (2 << 4) | (1 << 2) | 1;
//    //public const byte ZYZW = (3 << 6) | (2 << 4) | (1 << 2) | 2;
//    //public const byte WYZW = (3 << 6) | (2 << 4) | (1 << 2) | 3;
//    //public const byte XZZW = (3 << 6) | (2 << 4) | (2 << 2) | 0;
//    //public const byte YZZW = (3 << 6) | (2 << 4) | (2 << 2) | 1;
//    //public const byte ZZZW = (3 << 6) | (2 << 4) | (2 << 2) | 2;
//    //public const byte WZZW = (3 << 6) | (2 << 4) | (2 << 2) | 3;
//    //public const byte XWZW = (3 << 6) | (2 << 4) | (3 << 2) | 0;
//    //public const byte YWZW = (3 << 6) | (2 << 4) | (3 << 2) | 1;
//    //public const byte ZWZW = (3 << 6) | (2 << 4) | (3 << 2) | 2;
//    //public const byte WWZW = (3 << 6) | (2 << 4) | (3 << 2) | 3;
//    //public const byte XXWW = (3 << 6) | (3 << 4) | (0 << 2) | 0;
//    //public const byte YXWW = (3 << 6) | (3 << 4) | (0 << 2) | 1;
//    //public const byte ZXWW = (3 << 6) | (3 << 4) | (0 << 2) | 2;
//    //public const byte WXWW = (3 << 6) | (3 << 4) | (0 << 2) | 3;
//    //public const byte XYWW = (3 << 6) | (3 << 4) | (1 << 2) | 0;
//    //public const byte YYWW = (3 << 6) | (3 << 4) | (1 << 2) | 1;
//    //public const byte ZYWW = (3 << 6) | (3 << 4) | (1 << 2) | 2;
//    //public const byte WYWW = (3 << 6) | (3 << 4) | (1 << 2) | 3;
//    //public const byte XZWW = (3 << 6) | (3 << 4) | (2 << 2) | 0;
//    //public const byte YZWW = (3 << 6) | (3 << 4) | (2 << 2) | 1;
//    //public const byte ZZWW = (3 << 6) | (3 << 4) | (2 << 2) | 2;
//    //public const byte WZWW = (3 << 6) | (3 << 4) | (2 << 2) | 3;
//    //public const byte XWWW = (3 << 6) | (3 << 4) | (3 << 2) | 0;
//    //public const byte YWWW = (3 << 6) | (3 << 4) | (3 << 2) | 1;
//    //public const byte ZWWW = (3 << 6) | (3 << 4) | (3 << 2) | 2;
//    //public const byte WWWW = (3 << 6) | (3 << 4) | (3 << 2) | 3;
//    //#endregion Byte Mask Constants to Shuffle Values.

//    //#region Byte Mask Shortcut Constants for XXXX(X), YYYY(Y), ZZZZ(Z) and WWWW(W) to Shuffle Values.
//    //public const byte X = XXXX;
//    //public const byte Y = YYYY;
//    //public const byte Z = ZZZZ;
//    //public const byte W = WWWW;
//    //#endregion Byte Mask Shortcut Constants for XXXX(X), YYYY(Y), ZZZZ(Z) and WWWW(W) to Shuffle Values.
//  }
//}
