namespace Flux
{
  public static partial class Fx
  {
    /// <summary>In-place shuffle (randomized) of the span. Uses the specified rng.</summary>
    public static System.Span<T> Shuffle<T>(this System.Span<T> source, System.Random? rng = null)
    {
      rng ??= new System.Random();

      for (var index = 0; index < source.Length; index++)
        Swap(source, index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return source;
    }
  }
}
