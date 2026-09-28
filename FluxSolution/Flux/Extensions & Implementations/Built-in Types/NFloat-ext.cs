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
      public static System.Runtime.InteropServices.NFloat EngineeringEpsilon
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.EngineeringEpsilon)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.EngineeringEpsilon)
        : throw new System.NotImplementedException();

      public static System.Runtime.InteropServices.NFloat MachineEpsilon
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MachineEpsilon)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MachineEpsilon)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat MaxExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MaxExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MaxExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Runtime.InteropServices.NFloat"/> without losing precision.</para>
      /// </summary>
      public static System.Runtime.InteropServices.NFloat MinExactInteger
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MinExactInteger)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MinExactInteger)
        : throw new System.NotImplementedException();

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Runtime.InteropServices.NFloat"/>.</para>
      /// </summary>
      public static double MaxExactPrimeNumber
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? System.Runtime.InteropServices.NFloat.CreateChecked(double.MaxExactPrimeNumber)
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? System.Runtime.InteropServices.NFloat.CreateChecked(float.MaxExactPrimeNumber)
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

      #region NFloatUlpDistance

      public static long NFloatUlpDistance(System.Runtime.InteropServices.NFloat left, System.Runtime.InteropServices.NFloat right)
        => System.Runtime.InteropServices.NFloat.Size == 8
        ? double.DoubleUlpDistance(double.CreateChecked(left), double.CreateChecked(right))
        : System.Runtime.InteropServices.NFloat.Size == 4
        ? float.SingleUlpDistance(float.CreateChecked(left), float.CreateChecked(right))
        : throw new System.NotImplementedException();

      #endregion
    }
  }
}
