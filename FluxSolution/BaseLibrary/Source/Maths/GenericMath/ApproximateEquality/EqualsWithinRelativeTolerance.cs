namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance<TValue>(this TValue a, TValue b, double relativeTolerance)
      where TValue : System.Numerics.INumber<TValue>
      => a == b || (double.CreateChecked(TValue.Abs(a - b)) <= double.CreateChecked(TValue.Max(TValue.Abs(a), TValue.Abs(b))) * relativeTolerance);

#else

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance(this decimal a, decimal b, decimal relativeTolerance)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance(this double a, double b, double relativeTolerance)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance(this System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger relativeTolerance)
      => a == b || (System.Numerics.BigInteger.Abs(a - b) <= System.Numerics.BigInteger.Max(System.Numerics.BigInteger.Abs(a), System.Numerics.BigInteger.Abs(b)) * relativeTolerance);

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance(this int a, int b, int relativeTolerance)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);

    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool EqualsWithinRelativeTolerance(this long a, long b, long relativeTolerance)
      => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);

#endif
  }
}
