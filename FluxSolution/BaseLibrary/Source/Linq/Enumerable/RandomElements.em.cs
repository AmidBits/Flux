namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the specified percent of random elements from the sequence. Uses the specified random number generator.</summary>
    /// <param name="probability">Probability as a percent value in the range [0, 1].</param>
    /// <param name="rng">The random number generator to use.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random? rng = null)
    {
      if (probability <= 0 && probability > 1) throw new System.ArgumentOutOfRangeException(nameof(probability));

      rng ??= new System.Random();

      foreach (var item in source)
      {
        if (rng.NextDouble() < probability)
          yield return item;
      }
    }
  }
}
