namespace Flux
{
  public static partial class SingleExtensions
  {
    extension(System.Single)
    {
      /// <summary>
      /// <para>The base epsilon 1e‑6f → about the scale where single‑precision (binary32) stops reliably distinguishing differences.</para>
      /// <para>A tolerance for “close enough” comparisons — the smallest meaningful difference before rounding noise dominates.</para>
      /// </summary>
      public static float EngineeringEpsilon => 1e-6f;

      /// <summary>
      /// <para></para>
      /// </summary>
      public static float MachineEpsilon => 1.1920929e-7f;

      /// <summary>
      /// <para>The largest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers up to 16,777,216 = <c>(1 &lt;&lt; 24)</c> = 2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static float MaxExactInteger => +16777216;

      /// <summary>
      /// <para>The smallest integer that can be stored in a <see cref="System.Single"/> without losing precision is <c>-16,777,216</c>.</para>
      /// <para>This is because a <see cref="System.Single"/> is a base-2/binary single-precision floating point with a 24-bit mantissa, which means it can precisely represent integers down to -16,777,216 = <c>-(1 &lt;&lt; 24)</c> = -2²⁴, before precision starts to degrade.</para>
      /// </summary>
      public static float MinExactInteger => -16777216;

      /// <summary>
      /// <para>The largest prime integer that precisely fit in a <see cref="System.Single"/>.</para>
      /// </summary>
      public static float MaxExactPrimeNumber => 16777213;

      #region SingleUlp/TryGet

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Single"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static float SingleUlp(float value)
        => float.IsNaN(value)
        ? float.NaN
        : float.IsInfinity(value)
        ? float.PositiveInfinity
        : float.BitIncrement(value) - value;

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Single"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp32FromExponent"></param>
      /// <returns></returns>
      public static bool TryGetSingleUlp(float value, out float ulp32)
      {
        ulp32 = SingleUlp(value);

        return float.IsFinite(ulp32);
      }

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <see cref="System.Single"/> value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp32FromExponent"></param>
      /// <returns></returns>
      public static bool TryGetSingleUlp(float value, out float ulp32FromBitIncrement, out float ulp32FromExponent)
      {
        var hasMeaningfulUlp = TryGetSingleUlp(value, out ulp32FromBitIncrement);

        if (hasMeaningfulUlp)
        {
          var bits = System.BitConverter.SingleToInt32Bits(value);
          var exponent = (bits >> 23) & 0xFF;

          if (exponent == 0) // subnormal: ULP = 2^-149
          {
            ulp32FromExponent = float.BitIncrement(0f);
          }
          else // normal: ULP = 2^(e - 23)
          {
            var unbiased = exponent - 127;
            ulp32FromExponent = System.BitConverter.Int32BitsToSingle((127 + unbiased - 23) << 23);
          }
        }
        else
          ulp32FromExponent = ulp32FromBitIncrement;

        return hasMeaningfulUlp;
      }

      #endregion

      #region SingleUlpDecrement

      /// <summary>
      /// <para>Decrement a double by n ULPs (n can be negative for increment).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulps"></param>
      /// <returns></returns>
      public static float SingleUlpDecrement(float value, int ulps = 1)
      {
        if (ulps < 0) return SingleUlpIncrement(value, -ulps);

        if (value == 0f) return System.BitConverter.Int32BitsToSingle(-ulps); // handle negative zero

        var bits = System.BitConverter.SingleToInt32Bits(value);
        if (value > 0) bits -= ulps;
        else bits += ulps;
        return System.BitConverter.Int32BitsToSingle(bits);
      }

      #endregion

      #region SingleUlpDistance

      public static int SingleUlpDistance(float left, float right)
      {
        var a = BitConverter.SingleToInt32Bits(left);
        var b = BitConverter.SingleToInt32Bits(right);

        if (a < 0)
          a = int.MinValue - a;

        if (b < 0)
          b = int.MinValue - b;

        return int.Abs(a - b);
      }

      #endregion

      #region SingleUlpIncrement

      /// <summary>
      /// <para>Increment a double by n ULPs (n can be negative for decrement).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulps"></param>
      /// <returns></returns>
      public static float SingleUlpIncrement(float value, int ulps = 1)
      {
        if (ulps < 0) return SingleUlpDecrement(value, -ulps);

        if (value == 0f) return System.BitConverter.Int32BitsToSingle(ulps); // handle positive zero

        var bits = System.BitConverter.SingleToInt32Bits(value);
        if (value > 0) bits += ulps;
        else bits -= ulps;
        return System.BitConverter.Int32BitsToSingle(bits);
      }

      #endregion

      #region WrapToInterval

      /// <summary>
      /// <para>Wraps a float value to a specified interval [minValue, maxValue] according to the specified wrap mode and interval notation.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="wrapMode"></param>
      /// <param name="intervalNotation"></param>
      /// <param name="epsilon"></param>
      /// <returns></returns>
      public static float WrapToInterval(float value, float minValue, float maxValue, WrapMode wrapMode = WrapMode.Normalized, IntervalNotation intervalNotation = IntervalNotation.HalfOpenRight, float epsilon = 0f)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(epsilon);

        if (wrapMode == WrapMode.Strict)
          System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);
        else if (wrapMode == WrapMode.Normalized && maxValue < minValue)
          (minValue, maxValue) = (maxValue, minValue);
        else if (wrapMode == WrapMode.Centered)
        {
          var mid = (minValue + maxValue) * 0.5f;
          var half = (maxValue - minValue) * 0.5f;

          return mid + WrapToInterval(value - mid, -half, half, WrapMode.Normalized, intervalNotation, epsilon);
        }

        var range = maxValue - minValue;
        if (range == 0d)
          return minValue;

        var wrapped = (value - minValue) % range;
        if (wrapped < 0d)
          wrapped += range;

        var result = wrapped + minValue;

        TryGetSingleUlp(result, out var ulp32FromBitIncrement, out var _);

        var eps = (epsilon != 0d) ? epsilon : ulp32FromBitIncrement;

        return intervalNotation switch
        {
          IntervalNotation.Closed => result,
          IntervalNotation.HalfOpenRight => result >= maxValue - eps ? minValue : result,
          IntervalNotation.HalfOpenLeft => result <= minValue + eps ? maxValue : result,
          IntervalNotation.Open => (result <= minValue + eps) ? minValue + eps : (result >= maxValue - eps) ? maxValue - eps : result,
          _ => result,
        };
      }

      #endregion
    }
  }
}
