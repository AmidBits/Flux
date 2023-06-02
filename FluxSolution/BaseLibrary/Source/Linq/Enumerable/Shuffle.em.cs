namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
      => source.ToList().OrderBy(t => rng ??= new System.Random());
  }
}
