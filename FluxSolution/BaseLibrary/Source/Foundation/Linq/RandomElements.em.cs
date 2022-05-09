namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the specified percent of random elements from the sequence. Uses the specified random number generator.</summary>
    /// <param name="probability">Probability as a percent value in the range [0, 1].</param>
    /// <param name="random">The random number generator to use.</param>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random random)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      if (probability <= 0 && probability > 1) throw new System.ArgumentOutOfRangeException(nameof(probability));

      foreach (var element in source)
        if (random.NextDouble() < probability)
          yield return element;
    }
    /// <summary>Returns the specified percent of random elements from the sequence. Uses the .NET cryptographic random number generator.</summary>
    /// <param name="probability">Percent (or probability) as a value in the range [0, 1].</param>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability)
      => RandomElements(source, probability, Randomization.NumberGenerator.Crypto);
  }
}
