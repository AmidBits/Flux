namespace Flux
{
  public static partial class SpecialFunctions
  {
    /// <summary>
    /// <para>Compute the nth Sheffer polynomial at x.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static TFloat ShefferPolynomial<TInteger, TFloat>(TInteger n, TFloat x)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TFloat : System.Numerics.IBinaryInteger<TFloat>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      if (TInteger.IsZero(n)) return TFloat.One; // P_0(x) = 1
      if (n == TInteger.One) return x; // P_1(x) = x

      // Recurrence relation: P_n(x) = x * P_(n-1)(x) - (n-1) * P_(n-2)(x)

      var prev1 = x; // P_1(x)
      var prev2 = TFloat.One; // P_0(x)
      var current = TFloat.Zero;

      for (var i = TInteger.CreateChecked(2); i <= n; i++)
      {
        current = x * prev1 - TFloat.CreateChecked(i - TInteger.One) * prev2;
        prev2 = prev1;
        prev1 = current;
      }

      return current;
    }
  }
}
