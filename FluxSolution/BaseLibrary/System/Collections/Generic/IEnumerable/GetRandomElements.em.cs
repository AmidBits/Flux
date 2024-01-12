namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns approximately the specified percent (<paramref name="probability"/>) of random elements from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (default if null).</summary>
    /// <param name="probability">Probability as a percent value in the range [0, 1].</param>
    /// <param name="rng">The random number generator to use.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static System.Collections.Generic.IEnumerable<T> GetRandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random? rng = null)
    {
      if (probability <= 0 && probability > 1) throw new System.ArgumentOutOfRangeException(nameof(probability));

      rng ??= System.Random.Shared;

      foreach (var item in source)
        if (rng.NextDouble() < probability)
          yield return item;
    }
  }
}
