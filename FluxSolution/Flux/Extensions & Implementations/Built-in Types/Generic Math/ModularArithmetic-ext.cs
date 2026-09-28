namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Modular addition.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModAdd(TInteger a, TInteger b, TInteger m)
        => Number.EuclideanModulo(a + b, m);

      /// <summary>
      /// <para>Modular division.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModDiv(TInteger a, TInteger b, TInteger m)
        => ModMul(a, ModInverse(b, m), m);

      /// <summary>
      /// <para>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="m"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <remarks>
      /// <para>A modular multiplicative inverse may not exists for the specified parameters. In that case an arithmetic exception is thrown.</para>
      /// <para><c>var mi = ModInv(4, 7);</c> // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
      /// <para><c>var mi = ModInv(8, 11);</c> // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      public static TInteger ModInverse(TInteger a, TInteger m)
      {
        TInteger t = TInteger.Zero, newT = TInteger.One;
        TInteger r = m, newR = Number.EuclideanModulo(a, m);

        while (newR != TInteger.Zero)
        {
          var q = r / newR;

          (t, newT) = (newT, t - q * newT);
          (r, newR) = (newR, r - q * newR);
        }

        if (r != TInteger.One)
          throw new ArgumentException("Inverse does not exist.");

        return Number.EuclideanModulo(t, m);
      }

      /// <summary>
      /// <para>Modular multiplication.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModMul(TInteger a, TInteger b, TInteger m)
        => Number.EuclideanModulo(a * b, m);

      /// <summary>
      /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_exponentiation"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="b"></param>
      /// <param name="e"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModPow(TInteger a, TInteger e, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(e);

        a = Number.EuclideanModulo(a, m);
        var result = TInteger.One;

        while (e > TInteger.Zero)
        {
          if ((e & TInteger.One) == TInteger.One)
            result = ModMul(result, a, m);

          a = ModMul(a, a, m);
          e >>= 1;
        }

        return result;
      }

      /// <summary>
      /// <para>Modular subtraction.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModSub(TInteger a, TInteger b, TInteger m)
        => Number.EuclideanModulo(a - b, m);
    }
  }
}
