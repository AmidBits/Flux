namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region LogisticMap

      /// <summary>
      /// <para>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logistic_map"/></para>
      /// <seealso cref="Statistics.PopulationModelRicker(double, double, double)"/>
      /// </summary>
      /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
      /// <param name="r">A value in the range [0, 4] (r).</param>
      /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
      public static TFloat LogisticMap(TFloat Xn, TFloat r)
        => r * Xn * (TFloat.One - Xn);

      #endregion

      #region ProbabilityToOdds

      /// <summary>
      /// <para>Computes the odds (p / (1 - p)) of a probability p.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
      /// </summary>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probability in the range [-infinity, +infinity].</returns>
      public static Units.Ratio ProbabilityToOdds(TFloat probability)
        => new(double.CreateChecked(probability), double.CreateChecked(TFloat.One - probability));

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IExponentialFunctions<TFloat>
    {
      #region Expit

      /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
      public static TFloat Expit(TFloat x)
      => TFloat.One / (TFloat.Exp(-x) + TFloat.One);

      #endregion

      #region Logistic

      /// <summary>
      /// <para>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logistic_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Sigmoid_function"/></para>
      /// </summary>
      /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
      /// <param name="k">The logistic growth rate or steepness of the curve (k). Default of (1).</param>
      /// <param name="x0">The x-value of the sigmoid's midpoint (x0). Default of (0)</param>
      /// <param name="L">The curve's maximum value (L).</param>
      /// <returns></returns>
      public static TFloat Logistic(TFloat x, TFloat k, TFloat x0, TFloat L)
        => L / (TFloat.Exp(-(k * (x - x0))) + TFloat.One);

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region Logit

      /// <summary>
      /// <para>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
      /// </summary>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probability in the range [-infinity, +infinity].</returns>
      public static TFloat Logit(TFloat probability)
        => TFloat.Log(TFloat.CreateChecked(ProbabilityToOdds(probability).Value));

      #endregion
    }
  }
}
