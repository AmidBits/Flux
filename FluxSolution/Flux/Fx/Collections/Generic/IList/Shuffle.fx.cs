namespace Flux
{
  public static partial class Fx
  {
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random? rng = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      rng ??= System.Random.Shared;

      for (var i = source.Count - 1; i > 0; i--)
        source.Swap(i, rng.Next(i + 1));
    }
  }
}
