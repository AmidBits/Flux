using System.Runtime.Intrinsics;

namespace Flux.Numerics
{
  //public static class ExtensionMethodsIntrinsics
  //{
  //  public static T X<T>(this Vector256<T> v) where T : struct => v.GetElement(0);
  //  public static T Y<T>(this Vector256<T> v) where T : struct => v.GetElement(1);
  //  public static T Z<T>(this Vector256<T> v) where T : struct => v.GetElement(2);
  //  public static T W<T>(this Vector256<T> v) where T : struct => v.GetElement(3);
  //}

  /// <summary></summary>
  public struct QuaternionIntrinsic
    : System.IEquatable<QuaternionIntrinsic>
  {
    public static QuaternionIntrinsic Empty
      => new QuaternionIntrinsic(0, 0, 0, 0);

    public static Vector256<double> Epsilon
      => Vector256.Create(double.Epsilon);
    public static Vector256<double> NegativeOne
      => Vector256.Create(-1d);
    public static Vector256<double> One
      => Vector256.Create(1d);
    public static Vector256<double> Zero
      => Vector256<double>.Zero;

    public static Vector256<double> MaskNotSign
      => Vector256.Create(~long.MaxValue).AsDouble();
    //public static readonly Vector256<double> MaskSign = Vector256.Create(long.MaxValue).AsDouble();

    public static Vector256<double> MaskX
      => Vector256.Create(+0, -1, -1, -1).AsDouble();
    public static Vector256<double> MaskY
      => Vector256.Create(-1, +0, -1, -1).AsDouble();
    public static Vector256<double> MaskZ
      => Vector256.Create(-1, -1, +0, -1).AsDouble();
    public static Vector256<double> MaskW
      => Vector256.Create(-1, -1, -1, +0).AsDouble();

    public static Vector256<double> UnitX
      => Vector256.Create(1d, 0d, 0d, 0d);
    public static Vector256<double> UnitY
      => Vector256.Create(0d, 1d, 0d, 0d);
    public static Vector256<double> UnitZ
      => Vector256.Create(0d, 0d, 1d, 0d);
    public static Vector256<double> UnitW
      => Vector256.Create(0d, 0d, 0d, 1d);

    public static Vector256<double> OneDivPi
      => Vector256.Create(1 / System.Math.PI);
    public static Vector256<double> OneDiv2Pi
      => Vector256.Create(1 / (2 * System.Math.PI));
    public static Vector256<double> PiMul2
      => Vector256.Create(System.Math.PI * 2);

    private readonly Vector256<double> m_v256;
    /// <summary>Retreives the Vector256 store for the instance.</summary>
    public Vector256<double> V256
      => m_v256;

    public double X
      => m_v256.GetElement(0);
    public double Y
      => m_v256.GetElement(1);
    public double Z
      => m_v256.GetElement(2);
    public double W
      => m_v256.GetElement(3);

    public QuaternionIntrinsic(in Vector256<double> xyzw)
      => m_v256 = xyzw;
    public QuaternionIntrinsic(double x, double y, double z, double w)
      => m_v256 = Vector256.Create(x, y, z, w);
    public QuaternionIntrinsic(double x, double y, double z)
      => m_v256 = Vector256.Create(x, y, z, 1);
    public QuaternionIntrinsic(double x, double y)
      => m_v256 = Vector256.Create(x, y, 1, 1);

    public bool IsEmpty
      => Equals(Empty);

    public double Length()
      => System.Math.Sqrt(LengthSquared());
    public double LengthSquared()
      => DotProduct3D(m_v256, m_v256).GetElement(0);

    public Vector256<double> ToVector256()
      => Vector256.Create(m_v256.GetElement(0), m_v256.GetElement(1), m_v256.GetElement(2), m_v256.GetElement(3));

    #region Static methods
    /// <summary>Returns the vector with absolute values.</summary>
    public static Vector256<double> Abs(in Vector256<double> v)
      => Max(Subtract(Vector256<double>.Zero, v), v);
    /// <summary>Returns a new vector with the sum of the two vectors.</summary>
    public static Vector256<double> Add(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Add(v1, v2)
      : Vector256.Create(v1.GetElement(0) + v2.GetElement(0), v1.GetElement(1) + v2.GetElement(1), v1.GetElement(2) + v2.GetElement(2), v1.GetElement(3) + v2.GetElement(3));
    /// <summary>Returns a new vector with the sum of the vector components and the scalar value.</summary>
    public static Vector256<double> Add(in Vector256<double> v, in double scalar)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? Add(v, Vector256.Create(scalar))
      : Vector256.Create(v.GetElement(0) + scalar, v.GetElement(1) + scalar, v.GetElement(2) + scalar, v.GetElement(3) + scalar);
    /// <summary>Returns the vector with its components clamped between min and max.</summary>
    public static Vector256<double> Clamp(in Vector256<double> v, in Vector256<double> min, in Vector256<double> max)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? Max(Min(v, max), min)
      : Vector256.Create(System.Math.Clamp(v.GetElement(0), min.GetElement(0), max.GetElement(0)), System.Math.Clamp(v.GetElement(1), min.GetElement(1), max.GetElement(1)), System.Math.Clamp(v.GetElement(2), min.GetElement(2), max.GetElement(2)), System.Math.Clamp(v.GetElement(3), min.GetElement(3), max.GetElement(3)));
    /// <summary>Returns the vector with the values from clamped between min and max.</summary>
    public static Vector256<double> CopySign(in Vector256<double> v, in Vector256<double> sign)
      => System.Runtime.Intrinsics.X86.Avx.Or(Sign(sign), Abs(v));
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
    public static Vector256<double> CrossProduct3D(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx2.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Subtract(System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v1, ShuffleValues.YZXW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v2, ShuffleValues.ZXYW)), System.Runtime.Intrinsics.X86.Avx.Multiply(System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v1, ShuffleValues.ZXYW), System.Runtime.Intrinsics.X86.Avx2.Permute4x64(v2, ShuffleValues.YZXW))), MaskW)
      : Vector256.Create(v1.GetElement(1) * v2.GetElement(2) - v1.GetElement(2) * v2.GetElement(1), v1.GetElement(2) * v2.GetElement(0) - v1.GetElement(0) * v2.GetElement(2), v1.GetElement(0) * v2.GetElement(1) - v1.GetElement(1) * v2.GetElement(0), 0);
    /// <summary>Computes the euclidean distance between two vectors.</summary>
    public static Vector256<double> Distance2D(in Vector256<double> v1, in Vector256<double> v2)
      => Length2D(Subtract(v1, v2));
    /// <summary>Returns the distance between the two vectors.</summary>
    public static Vector256<double> Distance3D(in Vector256<double> v1, in Vector256<double> v2)
      => Length3D(Subtract(v1, v2));
    /// <summary>Computes the euclidean distance squared between two vectors.</summary>
    public static Vector256<double> DistanceSquared2D(in Vector256<double> v1, in Vector256<double> v2)
      => LengthSquared2D(Subtract(v1, v2));
    /// <summary>Returns the squared distance between the two vectors.</summary>
    public static Vector256<double> DistanceSquared3D(in Vector256<double> v1, in Vector256<double> v2)
      => LengthSquared3D(Subtract(v1, v2));
    public static Vector256<double> Divide(in Vector256<double> v, in Vector256<double> divisor)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Divide(v, divisor)
      : Vector256.Create(v.GetElement(0) / divisor.GetElement(0), v.GetElement(1) / divisor.GetElement(1), v.GetElement(2) / divisor.GetElement(2), v.GetElement(3) / divisor.GetElement(3));
    public static Vector256<double> Divide(in Vector256<double> v, in double divisor)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? Subtract(v, Vector256.Create(divisor))
      : Vector256.Create(v.GetElement(0) / divisor, v.GetElement(1) / divisor, v.GetElement(2) / divisor, v.GetElement(3) / divisor);
    /// <summary>Returns the dot product of the two given vectors.</summary>
    public static Vector256<double> DotProduct2D(in Vector256<double> v1, in Vector256<double> v2)
    {
      // SSE4.1 has a native dot product instruction, dppd
      if (System.Runtime.Intrinsics.X86.Sse41.IsSupported)
      {
        // This multiplies the first 2 elems of each and broadcasts it into each element of the returning vector.
        const byte control = 0b_0011_1111;
        var dp = System.Runtime.Intrinsics.X86.Sse41.DotProduct(v1.GetLower(), v2.GetLower(), control);
        return Vector256.Create(dp, dp);
      }
      else if (System.Runtime.Intrinsics.X86.Sse3.IsSupported)
      {
        var tmp = System.Runtime.Intrinsics.X86.Sse2.Multiply(v1.GetLower(), v2.GetLower());
        return DuplicateToVector256(System.Runtime.Intrinsics.X86.Sse3.HorizontalAdd(tmp, tmp));
      }
      else if (System.Runtime.Intrinsics.X86.Sse2.IsSupported)
      {
        var tmp = System.Runtime.Intrinsics.X86.Sse2.Multiply(v1.GetLower(), v2.GetLower());
        var dot = System.Runtime.Intrinsics.X86.Sse2.Add(tmp, System.Runtime.Intrinsics.X86.Sse2.Shuffle(tmp, tmp, ShuffleValues.YXYX));
        return dot.ToVector256Unsafe().WithUpper(dot);
      }

      return Vector256.Create(v1.GetElement(0) * v2.GetElement(0) + v1.GetElement(1) * v2.GetElement(1));
    }
    /// <summary>Returns the dot product of the vector.</summary>
    public static Vector256<double> DotProduct3D(in Vector256<double> v1, in Vector256<double> v2)
    {
      if (System.Runtime.Intrinsics.X86.Avx.IsSupported)
      {
        var result = System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Multiply(v1, v2), MaskW);
        // result = (X, Y, Z, 0) let's add them together.
        result = System.Runtime.Intrinsics.X86.Avx.HorizontalAdd(result, result);
        // result = (X + Y, X + Y, Z + 0, Z + 0).
        result = System.Runtime.Intrinsics.X86.Avx.Add(result, System.Runtime.Intrinsics.X86.Avx.Permute2x128(result, result, 0b_0000_0001));
        // We switch the 2 halves, and add that to the original, getting the result in all elements.
        return result;
      }

      return Vector256.Create(v1.GetElement(0) * v2.GetElement(0) + v1.GetElement(1) * v2.GetElement(1) + v1.GetElement(2) * v2.GetElement(2));
    }
    /// <summary>Creates a new Vector4D from a Vector256<double>.</summary>
    public static QuaternionIntrinsic FromVector256(Vector256<double> v)
      => new QuaternionIntrinsic(v.GetElement(0), v.GetElement(1), v.GetElement(2), v.GetElement(3));
    public static Vector256<double> HorizontalAdd(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.HorizontalAdd(v1, v2)
      : Vector256.Create(v1.GetElement(0) + v1.GetElement(1), v2.GetElement(0) + v2.GetElement(1), v1.GetElement(2) + v1.GetElement(3), v2.GetElement(2) + v2.GetElement(3));
    /// <summary>Returns the length of the given Vector2D.</summary>
    public static Vector256<double> Length2D(in Vector256<double> v)
      => Sqrt(DotProduct2D(v, v));
    /// <summary>Returns the Euclidean length (magnitude) of the vector.</summary>
    public static Vector256<double> Length3D(in Vector256<double> v)
      => Sqrt(DotProduct3D(v, v));
    /// <summary>Returns the length of the given Vector2D.</summary>
    public static Vector256<double> LengthSquared2D(in Vector256<double> v)
      => DotProduct2D(v, v);
    /// <summary>Returns the squared Euclidean length (magnitude) of the vector.</summary>
    public static Vector256<double> LengthSquared3D(in Vector256<double> v)
      => DotProduct3D(v, v);
    /// <summary>Returns a new vector that is a linear blend of the two given vectors. Lerp (Linear interpolate) interpolates between two values.</summary>
    /// <param name="weight">The blend factor. a when blend=0, b when blend=1.</param>
    /// <returns>The linear interpolated blend of the two vectors.</returns>
    public static Vector256<double> Lerp(in Vector256<double> v1, in Vector256<double> v2, in double weight)
      => Add(v1, Multiply(Subtract(v2, v1), Vector256.Create(weight >= 0 && weight <= 1
        ? weight
        : throw new System.ArgumentOutOfRangeException(nameof(weight))))); // General formula of linear interpolation: (from + (to - from) * weight).
    public static Vector256<double> Max(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Max(v1, v2)
      : Vector256.Create(System.Math.Max(v1.GetElement(0), v2.GetElement(0)), System.Math.Max(v1.GetElement(1), v2.GetElement(1)), System.Math.Max(v1.GetElement(2), v2.GetElement(2)), System.Math.Max(v1.GetElement(3), v2.GetElement(3)));
    public static Vector256<double> Min(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Min(v1, v2)
      : Vector256.Create(System.Math.Min(v1.GetElement(0), v2.GetElement(0)), System.Math.Min(v1.GetElement(1), v2.GetElement(1)), System.Math.Min(v1.GetElement(2), v2.GetElement(2)), System.Math.Min(v1.GetElement(3), v2.GetElement(3)));
    public static Vector256<double> Mod2Pi(in Vector256<double> v)
      => Subtract(v, Multiply(Round(Multiply(v, OneDiv2Pi)), PiMul2));
    public static Vector256<double> Multiply(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Multiply(v1, v2)
      : Vector256.Create(v1.GetElement(0) * v2.GetElement(0), v1.GetElement(1) * v2.GetElement(1), v1.GetElement(2) * v2.GetElement(2), v1.GetElement(3) * v2.GetElement(3));
    public static Vector256<double> Multiply(in Vector256<double> v, in double scalar)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? Multiply(v, Vector256.Create(scalar))
      : Vector256.Create(v.GetElement(0) * scalar, v.GetElement(1) * scalar, v.GetElement(2) * scalar, v.GetElement(3) * scalar);
    /// <summary>Returns (x * y) + z on each element of a <see cref="Vector256{Double}"/>, rounded as one ternary operation.</summary>
    /// <param name="x">The vector to be multiplied with <paramref name="y"/></param>
    /// <param name="y">The vector to be multiplied with <paramref name="x"/></param>
    /// <param name="z">The vector to be added to to the infinite precision multiplication of <paramref name="x"/> and <paramref name="y"/></param>
    /// <returns>(x * y) + z on each element, rounded as one ternary operation</returns>
    public static Vector256<double> MultiplyAdd(in Vector256<double> x, in Vector256<double> y, in Vector256<double> z)
      => System.Runtime.Intrinsics.X86.Fma.IsSupported
      ? System.Runtime.Intrinsics.X86.Fma.MultiplyAdd(x, y, z)
      : Vector256.Create(x.GetElement(0) * y.GetElement(0) + z.GetElement(0), x.GetElement(1) * y.GetElement(1) + z.GetElement(1), x.GetElement(2) * y.GetElement(2) + z.GetElement(2), x.GetElement(3) * y.GetElement(3) + z.GetElement(3));
    public static Vector256<double> Negate(in Vector256<double> v)
      => System.Runtime.Intrinsics.X86.Avx.Xor(MaskNotSign, v);
    /// <summary>Scales the Vector2D to unit length.</summary>
    public static Vector256<double> Normalize2D(in Vector256<double> v)
      => Divide(v, Length2D(v));
    /// <summary>Scales the Vector3D to unit length.</summary>
    public static Vector256<double> Normalize3D(in Vector256<double> v)
      => Divide(v, Length3D(v));
    public static Vector256<double> Reciprocal(in Vector256<double> v)
      => Divide(One, v);
    public static Vector256<double> ReciprocalSqrt(in Vector256<double> v)
      => Divide(One, Sqrt(v));
    /// <summary>Calculates the reflection of an incident ray. Reflection: (incident - (2 * DotProduct(incident, normal)) * normal)</summary>
    /// <param name="incident">The incident ray's vector.</param>
    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
    /// <returns>The vector of the reflected ray.</returns>
    public static Vector256<double> Reflect2D(in Vector256<double> incident, in Vector256<double> normal)
      => Subtract(incident, Multiply(Multiply(DotProduct2D(incident, normal), 2), normal));
    /// <summary>Calculates the reflection of an incident ray.</summary>
    /// <param name="incident">The incident ray's vector.</param>
    /// <param name="normal">The normal of the mirror upon which the ray is reflecting.</param>
    /// <returns>The vector of the reflected ray.</returns>
    public static Vector256<double> Reflect3D(in Vector256<double> incident, in Vector256<double> normal)
      => Subtract(incident, Multiply(Multiply(DotProduct3D(incident, normal), 2), normal)); // reflection = incident - (2 * DotProduct(incident, normal)) * normal
    public static Vector256<double> Remainder(in Vector256<double> v, in Vector256<double> divisor)
      => Subtract(v, Multiply(Truncate(Divide(v, divisor)), divisor));
    public static Vector256<double> Remainder(in Vector256<double> v, in double divisor)
      => Remainder(v, Vector256.Create(divisor));
    public static Vector256<double> Round(in Vector256<double> v)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToNearestInteger(v)
      : System.Runtime.Intrinsics.X86.Sse41.IsSupported && v.GetLower() is var lower && v.GetUpper() is var upper
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(lower).ToVector256Unsafe().WithUpper(System.Runtime.Intrinsics.X86.Sse41.RoundToNearestInteger(upper))
      : Vector256.Create(System.Math.Round(v.GetElement(0)), System.Math.Round(v.GetElement(1)), System.Math.Round(v.GetElement(2)), System.Math.Round(v.GetElement(3)));
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static Vector256<double> ScalarTripleProduct3D(in Vector256<double> a, in Vector256<double> b, in Vector256<double> c)
      => DotProduct3D(a, CrossProduct3D(b, c));
    public static Vector256<double> Sign(in Vector256<double> v)
      => System.Runtime.Intrinsics.X86.Avx.And(v, MaskNotSign);
    public static Vector256<double> Sqrt(in Vector256<double> v)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Sqrt(v)
      : Vector256.Create(System.Math.Sqrt(v.GetElement(0)), System.Math.Sqrt(v.GetElement(1)), System.Math.Sqrt(v.GetElement(2)), System.Math.Sqrt(v.GetElement(3)));
    public static Vector256<double> Square(in Vector256<double> v)
      => Multiply(v, v);
    public static Vector256<double> Subtract(in Vector256<double> v1, in Vector256<double> v2)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.Subtract(v1, v2)
      : Vector256.Create(v1.GetElement(0) - v2.GetElement(0), v1.GetElement(1) - v2.GetElement(1), v1.GetElement(2) - v2.GetElement(2), v1.GetElement(3) - v2.GetElement(3));
    public static Vector256<double> Subtract(in Vector256<double> v, in double scalar)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? Subtract(v, Vector256.Create(scalar))
      : Vector256.Create(v.GetElement(0) - scalar, v.GetElement(1) - scalar, v.GetElement(2) - scalar, v.GetElement(3) - scalar);
    public static Vector256<double> Truncate(in Vector256<double> v)
      => System.Runtime.Intrinsics.X86.Avx.IsSupported
      ? System.Runtime.Intrinsics.X86.Avx.RoundToZero(v)
      : System.Runtime.Intrinsics.X86.Sse41.IsSupported && v.GetLower() is var lower && v.GetUpper() is var upper
      ? System.Runtime.Intrinsics.X86.Sse41.RoundToZero(lower).ToVector256Unsafe().WithUpper(System.Runtime.Intrinsics.X86.Sse41.RoundToZero(upper))
      : Vector256.Create(System.Math.Truncate(v.GetElement(0)), System.Math.Truncate(v.GetElement(1)), System.Math.Truncate(v.GetElement(2)), System.Math.Truncate(v.GetElement(3)));
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static Vector256<double> VectorTripleProduct3D(in Vector256<double> a, in Vector256<double> b, in Vector256<double> c)
      => CrossProduct3D(a, CrossProduct3D(b, c));
    public static Vector256<double> WithinBounds(in Vector256<double> v, in Vector256<double> bound)
      => System.Runtime.Intrinsics.X86.Avx.And(System.Runtime.Intrinsics.X86.Avx.Compare(v, bound, System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedLessThanOrEqualSignaling), System.Runtime.Intrinsics.X86.Avx.Compare(v, Negate(bound), System.Runtime.Intrinsics.X86.FloatComparisonMode.OrderedGreaterThanOrEqualSignaling));

    public static Vector256<double> DuplicateToVector256(in Vector128<double> v) => Vector256.Create(v, v);
    #endregion Static methods


    #region Overloaded operators
    public static bool operator ==(QuaternionIntrinsic a, QuaternionIntrinsic b)
      => a.Equals(b);
    public static bool operator !=(QuaternionIntrinsic a, QuaternionIntrinsic b)
      => !a.Equals(b);

    public static QuaternionIntrinsic operator +(QuaternionIntrinsic a, QuaternionIntrinsic b)
      => new QuaternionIntrinsic(Add(a.m_v256, b.m_v256));
    public static QuaternionIntrinsic operator -(QuaternionIntrinsic q)
      => new QuaternionIntrinsic(Negate(q.m_v256));
    public static QuaternionIntrinsic operator -(QuaternionIntrinsic a, QuaternionIntrinsic b)
      => new QuaternionIntrinsic(Subtract(a.m_v256, b.m_v256));
    public static QuaternionIntrinsic operator *(QuaternionIntrinsic a, double b)
      => new QuaternionIntrinsic(Multiply(a.m_v256, b));
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(QuaternionIntrinsic other)
      => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is QuaternionIntrinsic o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, W);
    public override string ToString()
      => $"<{GetType().Name}: {X}, {Y}, {Z}, {W}>";
    #endregion Object overrides
  }
}
