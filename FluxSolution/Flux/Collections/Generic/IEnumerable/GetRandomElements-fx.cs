namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns approximately the specified percent (<paramref name="probability"/>) of random elements from <paramref name="source"/> up to <paramref name="maxCount"/> elements. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="probability">Probability as a percent value in the range (0, 1].</param>
    /// <param name="rng">The random-number-generator to use, or <see cref="System.Random.Shared"/> if null.</param>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<T> GetRandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random? rng = null, int maxCount = int.MaxValue)
    {
      if (maxCount < 1) throw new System.ArgumentOutOfRangeException(nameof(maxCount));

      Units.Probability.AssertMember(probability, IntervalNotation.HalfOpenLeft); // Cannot be zero, but can be one.

      rng ??= System.Random.Shared;

      var count = 0;

      foreach (var item in source)
        if (rng.NextDouble() < probability)
        {
          yield return item;

          if (++count >= maxCount) break;
        }
    }
  }
}
