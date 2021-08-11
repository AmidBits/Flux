namespace Flux
{
  public static partial class Maths
  {
    /// <summary>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></summary>
    /// <param name="Xn">The ratio of existing population to maximum possible population.</param>
    /// <param name="r">A value in the range [0, 4].</param>
    /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_map"/>
    /// <seealso cref="RickerModel(double, double, double)"/>
    public static double LogisticMap(double Xn, double r)
      => r * Xn * (1 - Xn);
  }
}
