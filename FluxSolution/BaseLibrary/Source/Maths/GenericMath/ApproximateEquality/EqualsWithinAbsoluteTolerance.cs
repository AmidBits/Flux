namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance<TValue>(this TValue a, TValue b, TValue absoluteTolerance)
      where TValue : System.Numerics.INumber<TValue>
      => a == b || (TValue.Abs(a - b) <= absoluteTolerance);

#else

    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance(this decimal a, decimal b, decimal absoluteTolerance)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);

    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance(this double a, double b, double absoluteTolerance)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);

    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance(this System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger absoluteTolerance)
      => a == b || (System.Numerics.BigInteger.Abs(a - b) <= absoluteTolerance);

    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance(this int a, int b, int absoluteTolerance)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);

    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool EqualsWithinAbsoluteTolerance(this long a, long b, long absoluteTolerance)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);

#endif
  }
}
