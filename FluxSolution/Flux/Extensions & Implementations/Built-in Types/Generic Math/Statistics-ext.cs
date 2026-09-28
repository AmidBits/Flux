namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Quantiles

      /// <summary>
      /// <para>Computes by linear interpolation of the EDF.</para>
      /// </summary>
      /// <param name="ordered"></param>
      /// <param name="h"></param>
      /// <returns>An estimated value.</returns>
      /// <exception cref="System.ArgumentNullException"/>
      private static TFloat QuantileEdfLerp<TFloat>(System.Span<TNumber> ordered, TFloat h)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        var fh = TFloat.Floor(h); // Floor of h.
        var ch = TFloat.Ceiling(h); // Ceiling of h.

        var fhi = System.Convert.ToInt32(fh);
        var chi = System.Convert.ToInt32(ch);

        var maxIndex = ordered.Length - 1;

        // Ensure roundings are clamped to quantile rank [0, maxIndex] range (variable 'h' on Wikipedia). There are no adjustments for 0-based indexing.
        fhi = int.Clamp(fhi, 0, maxIndex);
        chi = int.Clamp(chi, 0, maxIndex);

        var fv = ordered[fhi]; // Value at fhi.
        var cv = ordered[chi]; // Value at chi.

        return TFloat.CreateChecked(fv) + (h - fh) * TFloat.CreateChecked(cv - fv); // Linear interpolation between floor and ceiling using difference between h and fh (making it [0-1]).
      }

      /// <summary>
      /// <para>An empirical distribution function (commonly also called an empirical Cumulative Distribution Function, eCDF) is the distribution function associated with the empirical measure of a sample.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Empirical_distribution_function"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="ordered"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileEdf<TFloat>(System.Span<TNumber> ordered, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = probability * TFloat.CreateChecked(ordered.Length + 1);

        return QuantileEdfLerp(ordered, h - TFloat.One);
      }

      /// <summary>
      /// Computes the nearest-rank quantile.
      /// </summary>
      /// <typeparam name="T">The numeric type.</typeparam>
      /// <param name="sortedData">
      /// Data sorted in ascending order.
      /// </param>
      /// <param name="probability">
      /// Quantile probability in the range [0, 1].
      /// </param>
      /// <returns>
      /// The nearest-rank quantile value.
      /// </returns>
      public static TFloat QuantileNearestRank<TFloat>(System.Span<TNumber> ordered, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        if (ordered.Length == 0) throw new System.ArgumentException("Sequence must not be empty.", nameof(ordered));

        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        if (TFloat.IsZero(probability))
          return TFloat.CreateChecked(ordered[0]);

        var rank = int.CreateChecked(TFloat.Ceiling(probability * TFloat.CreateChecked(ordered.Length)));

        return TFloat.CreateChecked(ordered[rank - 1]);
      }

      /// <summary>
      /// <para>Inverse of empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR1<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length) * probability;

        var index = System.Convert.ToInt32(TFloat.Ceiling(h));

        index = int.Clamp(index, 1, numbers.Length) - 1; // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

        return TFloat.CreateChecked(numbers[index]);
      }

      /// <summary>
      /// <para>The same as R1, but with averaging at discontinuities.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR2<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var half = TFloat.CreateChecked(0.5);

        var h = TFloat.CreateChecked(numbers.Length) * probability + half;

        var chi = System.Convert.ToInt32(TFloat.Ceiling(h - half)); // ceiling(h - 0.5).
        var fhi = System.Convert.ToInt32(TFloat.Floor(h + half)); // floor(h + 0.5).

        // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.
        chi = int.Clamp(chi, 1, numbers.Length) - 1;
        fhi = int.Clamp(fhi, 1, numbers.Length) - 1;

        return TFloat.CreateChecked(numbers[chi] + numbers[fhi]) / TFloat.CreateChecked(2);
      }

      /// <summary>
      /// <para>The observation numbered closest to Np. Here, h indicates rounding to the nearest integer, choosing the even integer in the case of a tie.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR3<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length) * probability - TFloat.CreateChecked(0.5);

        var index = System.Convert.ToInt32(TFloat.Round(h, System.MidpointRounding.ToEven)); // Round h to the nearest integer, choosing the even integer in the case of a tie.

        index = int.Clamp(index, 0, numbers.Length - 1); // Ensure roundings are clamped to quantile rank [1, count] range (variable 'h' on Wikipedia) and then adjust to 0-based index.

        return TFloat.CreateChecked(numbers[index]);
      }

      /// <summary>
      /// <para>Linear interpolation of the empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR4<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length) * probability;

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Piecewise linear function where the knots are the values midway through the steps of the empirical distribution function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR5<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length) * probability + TFloat.CreateChecked(0.5);

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the expectations for the order statistics for the uniform distribution on [0,1]. That is, it is the linear interpolation between points (ph, xh), where ph = h/(N+1) is the probability that the last of (N+1) randomly drawn values will not exceed the h-th smallest of the first N randomly drawn values.</para>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <remarks>
      /// <list type="bullet">
      /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0">Percentile, Third variant C = 0</see> - Microsoft Excel PERCENTILE.EXC function - Python's default "exclusive" method - Primary variant recommended by NIST.</item>
      /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_4">Quartile, Method 4</see> - Microsoft Excel QUARTILE.EXC function</item>
      /// </list>
      /// </remarks>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR6<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length + 1) * probability;

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the modes for the order statistics for the uniform distribution on [0, 1].</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Equivalent to</para>
      /// <list type="bullet">
      /// <item><see href="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1">Percentile - Second variant, C = 1</see> - Microsoft Excel PERCENTILE.INC function - Python's optional "inclusive" method - Noted as an alternative by NIST</item>
      /// <item><see href="https://en.wikipedia.org/wiki/Quartile#Method_3">Quartile Method 3</see> - Microsoft Excel	QUARTILE.INC function</item>
      /// </list>
      /// </remarks>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR7<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = TFloat.CreateChecked(numbers.Length - 1) * probability + TFloat.One;

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>Linear interpolation of the approximate medians for order statistics.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR8<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = (TFloat.CreateChecked(numbers.Length) + TFloat.CreateChecked(1d / 3d)) * probability + TFloat.CreateChecked(1d / 3d);

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      /// <summary>
      /// <para>The resulting quantile estimates are approximately unbiased for the expected order statistics if x is normally distributed.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quantile#Estimating_quantiles_from_a_sample"/></para>
      /// </summary>
      /// <typeparam name="TPercent"></typeparam>
      /// <param name="numbers"></param>
      /// <param name="probability"></param>
      /// <returns></returns>
      public static TFloat QuantileR9<TFloat>(System.Span<TNumber> numbers, TFloat probability)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(probability);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, TFloat.One);

        var h = (TFloat.CreateChecked(numbers.Length) + TFloat.CreateChecked(1d / 4d)) * probability + TFloat.CreateChecked(3d / 8d);

        return QuantileEdfLerp(numbers, h - TFloat.One); // Adjust for 0-based indexing.
      }

      #endregion
    }

    #region MeanMedianAbsoluteDeviation

    /// <summary>
    /// <para>The mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</para>
    /// <para>In this function, both M(AD)ean, M(AD)edian and the regular mode are computed.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Average_absolute_deviation"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static (double MeanAbsoluteDeviation, double MedianAbsoluteDeviation) MeanMedianAbsoluteDeviation<TNumber>(this System.Collections.Generic.IList<TNumber> values)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var mmo = new Statistics.OnlineMeanMedianMode<TNumber>(values);

      var madMean = 0d;
      var madMedian = 0d;

      foreach (var value in values.Select(v => double.CreateChecked(v)))
      {
        madMean += double.Abs(value - mmo.Mean);
        madMedian += double.Abs(value - mmo.Median);
      }

      madMean /= mmo.Count;
      madMedian /= mmo.Count;

      return (madMean, madMedian);
    }

    #endregion
  }
}
