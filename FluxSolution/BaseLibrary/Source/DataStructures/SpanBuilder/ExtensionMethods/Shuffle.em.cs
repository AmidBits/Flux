namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Shuffle all values in the builder. Uses the specified <paramref name="rng"/>, or default if null.</summary>
    public static SpanBuilder<T> Shuffle<T>(this SpanBuilder<T> source, System.Random? rng)
    {
      rng ??= new System.Random();

      for (var index = source.Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        source.Swap(index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return source;
    }
  }
}
