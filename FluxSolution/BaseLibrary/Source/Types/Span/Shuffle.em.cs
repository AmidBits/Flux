namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>In-place shuffle (randomized) of the span. Uses the specified rng.</summary>
    public static System.Span<T> Shuffle<T>(this System.Span<T> source, System.Random? rng = null)
    {
      rng ??= new System.Random();

      for (var index = source.Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(source, index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return source;
    }
  }
}
