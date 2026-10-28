namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IExponentialFunctions<TFloat>
    {
      #region ExponentialWave

      /// <summary>
      /// <para>Exponential wave function.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="period"></param>
      /// <param name="shape">0 is the linear limit, use 1 for default exponential, 5 for strong exponential, and 10 for very strong exponential.</param>
      /// <returns></returns>
      public static TFloat ExponentialWave(TFloat value, TFloat period, TFloat shape)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period); // Phase in [0, modulus).

        var unitInterval = phase / period; // Normalize to [0, 1).

        var x = shape * unitInterval; // Scale to [0, shape].

        return TFloat.Exp(x);
      }

      #endregion

      #region SawtoothWave

      /// <summary>
      /// Generates a periodic sawtooth wave in the interval [0, 1).
      ///
      /// The wave rises linearly throughout the cycle and
      /// abruptly resets at the beginning of the next period.
      /// </summary>
      public static TFloat SawtoothWave(TFloat value, TFloat period)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        return Number.EuclideanModulo(value, period) / period;
      }

      #endregion

      #region SigmoidWave

      /// <summary>
      /// <para>Generates a periodic sigmoid-shaped wave in the interval [0, 1].</para>
      /// <para>The wave is produced by wrapping the input into a single period, normalizing the phase to [0, 1), and applying a logistic (sigmoid) shaping function.</para>
      /// <para>Small shape values produce a gentle S-curve that is close to a linear ramp. Larger shape values increase the steepness near the center of the cycle while flattening the beginning and end.</para>
      /// <para>Unlike power, exponential, and logarithmic waves, which bias the curve toward one end of the cycle, a sigmoid wave distributes its curvature symmetrically about the midpoint. This produces a characteristic "ease-in/ease-out" shape that starts slowly, accelerates through the middle, and slows again toward the end.</para>
      /// <para>As the shape increases, the wave approaches a step function with its transition centered within the period.</para>
      /// <para>Sigmoid waves are useful for smooth transitions, interpolation, animation easing, and other applications that require gradual acceleration and deceleration within each cycle.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="period"></param>
      /// <param name="shape">Use 2 for almost linear, 6 for a nice S-curve, 10 for strong ease-in/ease-out, 20 for nearly step-like.</param>
      /// <returns></returns>
      public static TFloat SigmoidWave(TFloat value, TFloat period, TFloat shape)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period) / period;

        var oneHalf = TFloat.CreateChecked(0.5);

        var y = TFloat.One / (TFloat.One + TFloat.Exp(-shape * (phase - oneHalf)));

        var ymin = TFloat.One / (TFloat.One + TFloat.Exp(shape * oneHalf));

        var ymax = TFloat.One / (TFloat.One + TFloat.Exp(-shape * oneHalf));

        return (y - ymin) / (ymax - ymin);
      }

      #endregion

      #region SquareWave

      /// <summary>
      /// <para>Generates a periodic square wave in the interval [0, 1].</para>
      /// <para>The wave remains at 0 for the first half of the cycle and at 1 for the second half.</para>
      /// </summary>
      public static TFloat SquareWave(TFloat value, TFloat period)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period) / period;

        return phase < TFloat.CreateChecked(0.5) ? TFloat.Zero : TFloat.One;
      }

      #endregion

      #region TriangleWave

      /// <summary>
      /// <para>Generates a periodic triangle wave in the interval [0, 1].</para>
      /// <para>The wave rises linearly to the midpoint of each cycle and then falls linearly to the beginning of the next.</para>
      /// </summary>
      public static TFloat TriangleWave(TFloat value, TFloat period)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period) / period;

        return TFloat.One - TFloat.Abs(TFloat.CreateChecked(2) * phase - TFloat.One);
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region LogarithmicWave

      /// <summary>
      /// <para>Logarithmic wave function.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="value"></param>
      /// <param name="period"></param>
      /// <param name="shape">0 is the linear limit, use 1 for mild logarithmic, and 5 for strong logarithmic.</param>
      /// <returns></returns>
      public static TFloat LogarithmicWave<T>(TFloat value, TFloat period, TFloat shape)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period); // Phase in [0, modulus).

        var unitInterval = phase / period; // Normalize to [0, 1).

        var x = TFloat.One + shape * unitInterval; // Shift to [1, 1 + shape] to ensure domain is always valid: argument >= 1.

        return TFloat.Log(x);
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region PowerWave

      /// <summary>
      /// <para>Generates a periodic power-shaped wave in the interval [0, 1].</para>
      /// <para>The wave is produced by wrapping the input into a single period, normalizing the phase to [0, 1), and raising it to the specified power.</para>
      /// <para>A shape of 1 produces a linear ramp.</para>
      /// <para>Shapes greater than 1 bias the wave toward the end of the cycle, producing an increasingly "ease-in" curve. As the shape increases, the result becomes qualitatively similar to an exponential wave, remaining near zero for most of the period before rising rapidly.</para>
      /// <para>Shapes between 0 and 1 bias the wave toward the beginning of the cycle, producing an "ease-out" curve. As the shape decreases, the result becomes qualitatively similar to a logarithmic wave, rising rapidly before gradually flattening.</para>
      /// <para>Power waves provide a simple and flexible family of curve shapes that span from logarithmic-like behavior, through linear, to exponential-like behavior using a single shape parameter.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="period"></param>
      /// <param name="shape">Use 1 for linear, > 1 for ease-in, and &lt; 1 for ease-out.</param>
      /// <returns></returns>
      public static TFloat PowerWave(TFloat value, TFloat period, TFloat shape)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period) / period;

        return TFloat.Pow(phase, shape);
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      #region SineWave

      /// <summary>
      /// Generates a periodic sine wave in the interval [0, 1].
      ///
      /// The wave begins at the midpoint, rises smoothly to a
      /// maximum, falls smoothly to a minimum, and returns to
      /// the midpoint at the end of the cycle.
      /// </summary>
      public static TFloat SineWave(TFloat value, TFloat period)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(period);

        var phase = Number.EuclideanModulo(value, period) / period;

        return (TFloat.Sin(TFloat.Tau * phase) + TFloat.One) / TFloat.CreateChecked(2);
      }

      #endregion
    }
  }
}
