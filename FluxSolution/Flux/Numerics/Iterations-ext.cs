namespace Flux
{
  public static class Iterations
  {
    extension<TNumber>(TNumber source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Creates a sequence of numbers based on the standard for statement using <see cref="System.Func{T, TResult}"/> (for <paramref name="source"/>) and <see cref="System.Func{T1, T2, TResult}"/> (for <paramref name="condition"/> and <paramref name="iterator"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="source">Initializes the current-loop-value.</param>
      /// <param name="condition">Conditionally allows/denies the loop to continue. In parameters are (current-loop-value, index).</param>
      /// <param name="iterator">Advances the loop. In parameters (current-loop-value, index).</param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TNumber> LoopCustom(System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> iterator)
      {
        var number = source;

        for (var index = 0; ; index++)
        {
          try
          {
            if (!condition(number, index))
              break;

            number = iterator(number, index);
          }
          catch
          {
            break;
          }

          yield return number;
        }
      }

      /// <summary>
      /// <para>Loop toward or away-from and back-and-forth over <paramref name="center"/>, in <paramref name="stepSize"/> for <paramref name="count"/> times.</para>
      /// <para>E.g. a direction = away-from, mean = 0, stepSize = -3 and count = 5, would yield the sequence [0, -3, 3, -6, 6].</para>
      /// <para>If the loop logic overflows/underflows for any reason, an exception occurs.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TCount"></typeparam>
      /// <param name="source">The order of alternating numbers, either from mean to the outer limit, or from the outer limit to mean.</param>
      /// <param name="center">This is the center of attention which the looping revolves around.</param>
      /// <param name="stepSize">The increasing (positive) and decreasing (negative) step size. Note, the min/max value of the loop inherits the same sign as step-size.</param>
      /// <param name="count">The number of numbers in the sequence.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TNumber> LoopPivot<TCount>(CoordinateSystems.ReferenceRelativeOrientationTAf center, TNumber stepSize, TCount count)
        where TCount : System.Numerics.IBinaryInteger<TCount>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          switch (center)
          {
            case CoordinateSystems.ReferenceRelativeOrientationTAf.AwayFrom:
              if (TCount.IsOddInteger(count)) stepSize = -stepSize;

              for (var index = TCount.One; index <= count; index++)
              {
                yield return source;

                source += stepSize * TNumber.CreateChecked(index);
                stepSize = -stepSize;
              }
              break;
            case CoordinateSystems.ReferenceRelativeOrientationTAf.Toward:
              source += stepSize * TNumber.CreateChecked(count).TruncRem(TNumber.One + TNumber.One).TruncatedQuotient; // Setup the inital outer edge value for inward iteration.

              for (var index = count - TCount.One; index >= TCount.Zero; index--)
              {
                yield return source;

                source -= stepSize * TNumber.CreateChecked(index);
                stepSize = -stepSize;
              }
              break;
            default:
              throw new System.ArgumentOutOfRangeException(nameof(center));
          }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence of <paramref name="count"/> numbers (or as many as possible) using <typeparamref name="TNumber"/> starting at <paramref name="source"/> and spaced by <paramref name="stepSize"/>.</para>
      /// <para>If the loop logic overflows/underflows for any reason, the enumeration is simply terminated, no exceptions are thrown.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TCount"></typeparam>
      /// <param name="source"></param>
      /// <param name="stepSize"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TNumber> LoopRange<TCount>(TNumber stepSize, TCount count)
        where TCount : System.Numerics.IBinaryInteger<TCount>
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        var index = TCount.Zero;

        TNumber number;

        while (index < count)
          checked
          {
            try
            {
              number = source + TNumber.CreateChecked(index) * stepSize;
            }
            catch { break; }

            yield return number;

            try
            {
              index++;
            }
            catch { break; }
          }
      }

      /// <summary>
      /// <para>Creates a new sequence with as many numbers as possible using <typeparamref name="TNumber"/> starting at <paramref name="source"/> and spaced by <paramref name="stepSize"/>.</para>
      /// <para>If the loop logic overflows/underflows for any reason, the enumeration is simply terminated, no exceptions are thrown.</para>
      /// </summary>
      /// <remarks>Please note! If <typeparamref name="TNumber"/> is unlimited in nature (e.g. <see cref="System.Numerics.BigInteger"/>) enumeration is indefinite to the extent of system resources.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="source"></param>
      /// <param name="stepSize"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TNumber> LoopVerge(TNumber stepSize)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);

        var index = TNumber.Zero;

        TNumber number;

        while (true)
          checked
          {
            try
            {
              number = source + index * stepSize;
            }
            catch { break; }

            yield return number;

            try
            {
              index++;
            }
            catch { break; }
          }
      }
    }
  }
}
