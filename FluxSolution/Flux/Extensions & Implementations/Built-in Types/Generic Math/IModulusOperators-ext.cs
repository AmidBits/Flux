namespace Flux
{
  public static class ModulusOperators
  {
    extension<TModulusOperator>(TModulusOperator)
      where TModulusOperator : System.Numerics.INumber<TModulusOperator>, System.Numerics.IModulusOperators<TModulusOperator, TModulusOperator, TModulusOperator>
    {
      /// <summary>
      /// <para>Computes the half-open left modulo of a number <paramref name="value"/> with respect to a positive modulus <paramref name="modulus"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns>value in the range (0, modulus]</returns>
      public static TModulusOperator HalfOpenLeftModulo(TModulusOperator value, TModulusOperator modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var result = EuclideanModulo(value, modulus);

        return TModulusOperator.IsZero(result) ? modulus : result;
      }

      /// <summary>
      /// <para>Normalizes a number <paramref name="value"/> to the range [0, <paramref name="modulus"/>), where <paramref name="modulus"/> is a positive modulus.</para>
      /// <para>A.K.A. HalfOpenRightModulo(value, modulus)</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static TModulusOperator EuclideanModulo(TModulusOperator value, TModulusOperator modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var rem = value % modulus;

        return TModulusOperator.IsNegative(rem) ? rem + modulus : rem;
      }

      /// <summary>
      /// <para>Computes all remainder interpretations.</para>
      /// <para>Remainder is the standard remainder.</para>
      /// <para>RemainderNoZero is the same as standard except no zero, and instead returns the divisor with the sign of the dividend.</para>
      /// <para>ReverseRemainder is the reverse order of the standard remainder, except for 0, which is still in same.</para>
      /// <para>ReverseRemainderNoZero is the same as ReverseRemainder except no zero, and instead returns the divisor with the sign of dividend.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns></returns>
      public static (TModulusOperator Remainder, TModulusOperator RemainderNoZero, TModulusOperator ReverseRemainder, TModulusOperator ReverseRemainderNoZero) RemainderAnalysis(TModulusOperator value, TModulusOperator modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var remainder = value % modulus;

        var copySign = TModulusOperator.CopySign(modulus, value);

        if (TModulusOperator.IsZero(remainder))
          return (remainder, copySign, remainder, copySign);

        var minusRemainder = copySign - remainder;

        return (remainder, remainder, minusRemainder, minusRemainder);
      }

      /// <summary>
      /// <para>Computes the triangle (folded) modulo of a number <paramref name="value"/> with respect to a positive modulus <paramref name="modulus"/>.</para>
      /// <para>The resulting waveform has period 2 × modulus.</para>
      /// <para>A.K.A. "reflected", "folded" or "mirrored" modulo.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="modulus"></param>
      /// <returns>value in the range [0, modulus]</returns>
      public static TModulusOperator TriangleModulo(TModulusOperator value, TModulusOperator modulus)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(modulus);

        var remainder = EuclideanModulo(value, modulus + modulus);

        return modulus - TModulusOperator.Abs(remainder - modulus);
      }
    }
  }
}
