namespace Flux
{
  public static partial class NFloat
  {
    extension(System.Runtime.InteropServices.NFloat)
    {
      /// <summary>
      /// <para>The base epsilon (1e-12d or 1e-6f) → about the scale where NFloat‑precision (binary64 or binary32) stops reliably distinguishing differences.</para>
      /// <para>A tolerance for “close enough” comparisons — the smallest meaningful difference before rounding noise dominates.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat NFloatEngineeringEpsilon
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleEngineeringEpsilon)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleEngineeringEpsilon)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat NFloatMaxExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleMaxExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleMaxExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat NFloatMinExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleMinExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleMinExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Runtime.InteropServices.NFloat"/>.</para>
      /// </summary>
      public static double NFloatMaxExactPrimeNumber
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleMaxExactPrimeNumber)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleMaxExactPrimeNumber)
        : throw new System.NotImplementedException();

      #region NFloatUlp/TryGet

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static System.Runtime.InteropServices.NFloat NFloatUlp(System.Runtime.InteropServices.NFloat value)
        => System.Runtime.InteropServices.NFloat.IsNaN(value)
        ? System.Runtime.InteropServices.NFloat.NaN
        : System.Runtime.InteropServices.NFloat.IsInfinity(value)
        ? System.Runtime.InteropServices.NFloat.PositiveInfinity
        : System.Runtime.InteropServices.NFloat.BitIncrement(value) - value;

      /// <summary>
      /// <para>Try to get the unit in the last place (ULP) of a <see cref="System.Double"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp64FromBitIncrement"></param>
      /// <returns></returns>
      public static bool TryGetNFloatUlp(System.Runtime.InteropServices.NFloat value, out System.Runtime.InteropServices.NFloat ulpNFloat)
      {
        ulpNFloat = NFloatUlp(value);

        return System.Runtime.InteropServices.NFloat.IsFinite(ulpNFloat);
      }

      #endregion

      #region NFloatUlpDecrement

      /// <summary>
      /// <para>Decrement a double by n ULPs (n can be negative for increment).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulps"></param>
      /// <returns></returns>
      public static System.Runtime.InteropServices.NFloat NFloatUlpDecrement(System.Runtime.InteropServices.NFloat value, long ulps = 1)
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleUlpDecrement(double.CreateChecked(value), ulps))
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleUlpDistance(float.CreateChecked(value), int.CreateChecked(ulps)))
        : throw new System.NotImplementedException();


      #endregion

      #region NFloatUlpDistance

      public static long NFloatUlpDistance(System.Runtime.InteropServices.NFloat left, System.Runtime.InteropServices.NFloat right)
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? double.DoubleUlpDistance(double.CreateChecked(left), double.CreateChecked(right))
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? float.SingleUlpDistance(float.CreateChecked(left), float.CreateChecked(right))
        : throw new System.NotImplementedException();

      #endregion

      #region SingleUlpIncrement

      /// <summary>
      /// <para>Increment a double by n ULPs (n can be negative for decrement).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulps"></param>
      /// <returns></returns>
      public static System.Runtime.InteropServices.NFloat SingleUlpIncrement(System.Runtime.InteropServices.NFloat value, long ulps = 1)
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.DoubleUlpIncrement(double.CreateChecked(value), ulps))
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.SingleUlpIncrement(float.CreateChecked(value), int.CreateChecked(ulps)))
        : throw new System.NotImplementedException();

      #endregion
    }
  }
}
