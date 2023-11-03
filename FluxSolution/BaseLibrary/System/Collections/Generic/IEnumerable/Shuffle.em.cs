namespace Flux
{
  public static partial class ExtensionMethodsIEnumerableT
  {
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    {
      rng ??= new System.Random();

      return source.ToList().OrderBy(t => rng.NextDouble());
    }
  }
}
