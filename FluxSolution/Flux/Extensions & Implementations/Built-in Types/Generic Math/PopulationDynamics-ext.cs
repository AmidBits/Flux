namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region BevertonHoltModel

      /// <summary>A classic discrete-time population model which gives the expected number Nt+1 (or density) of individuals in generation t+1 as a function of the number of individuals in the previous generation.</summary>
      /// <param name="population">The number of individuals at time t (Nt).</param>
      /// <param name="growthRate">The proliferation rate per generation (R0).</param>
      /// <param name="carryingCapacity">The carrying capacity in the environment (M).</param>
      /// <returns>The number of individuals at time Nt+1.</returns>
      /// <see href="https://en.wikipedia.org/wiki/Beverton%E2%80%93Holt_model"/>
      /// <seealso cref="RickerModel(double, double, double)" />
      public static TFloat BevertonHoltModel(TFloat population, TFloat growthRate, TFloat carryingCapacity)
        => (growthRate * population) / (TFloat.One + population / carryingCapacity);

      #endregion

      #region BideModel (BIDE model)

      /// <summary>Although BIDE models are conceptually simple, reliable estimates of the 5 variables contained therein (N, B, D, I and E) are often difficult to obtain.</summary>
      /// <param name="population">The number of individuals at time t (Nt).</param>
      /// <param name="births">The number of births within the population between Nt and Nt+1 (B).</param>
      /// <param name="deaths">The number of deaths within the population between Nt and Nt+1 (D).</param>
      /// <param name="immigrated">The number of individuals immigrating into the population between Nt and Nt+1 (I).</param>
      /// <param name="emigrated">The number of individuals emigrating into the population between Nt and Nt+1 (E).</param>
      /// <returns>The number of individuals at time Nt+1.</returns>
      /// <see href="https://en.wikipedia.org/wiki/Matrix_population_models"/>
      public static TFloat BideModel(TFloat population, TFloat births, TFloat immigrated, TFloat deaths, TFloat emigrated)
        => population + births - deaths + immigrated - emigrated;

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IFloatingPointConstants<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region RickerModel

      /// <summary>A classic discrete population model which gives the expected number N t+1 (or density) of individuals in generation t + 1 as a function of the number of individuals in the previous generation.</summary>
      /// <param name="population">The number of individuals in the previous generation (Nt).</param>
      /// <param name="growthRate">The intrinsic growth rate (r).</param>
      /// <param name="carryingCapacity">The carrying capacity of the environment (k).</param>
      /// <returns>The expected number (or density) of individuals in (the next) generation (Nt + 1).</returns>
      /// <see href="https://en.wikipedia.org/wiki/Ricker_model"/>
      public static TFloat RickerModel(TFloat population, TFloat growthRate, TFloat carryingCapacity)
          => population * TFloat.Pow(TFloat.E, growthRate * (TFloat.One - (population / carryingCapacity)));

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IExponentialFunctions<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
    {
      #region Gompertz

      public static TFloat Gompertz(TFloat population, TFloat growthRate, TFloat carryingCapacity)
        => population * TFloat.Exp(growthRate * TFloat.Log(carryingCapacity / population));

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumberBase<TNumber>
    {
      #region LogisticMap

      /// <summary>
      /// <para>This nonlinear difference equation is intended to capture two effects.
      /// <list type="number">
      /// <item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item>
      /// <item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item>
      /// </list>
      /// </para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logistic_map"/></para>
      /// <seealso cref="FloatingPoint.RickerModel{TFloat}(TFloat, TFloat, TFloat)"/>
      /// </summary>
      /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
      /// <param name="r">A value in the range [0, 4] (r).</param>
      /// <returns>The ratio of population to max possible population in the next generation (Xn+1)</returns>
      public static TNumber LogisticMap(TNumber Xn, TNumber r)
        => r * Xn * (TNumber.One - Xn);

      #endregion
    }
  }
}
