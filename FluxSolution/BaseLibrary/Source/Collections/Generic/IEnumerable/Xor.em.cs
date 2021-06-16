namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static int Xor<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int> selector, int seed = 0)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      var index = 0;

      var xor = seed;

      foreach (var element in source)
        xor ^= selector(element, index++);

      return xor;
    }
    public static int Xor<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int> selector, int seed = 0)
      => Xor(source, (e, i) => selector(e), seed);
    public static int Xor<TSource>(this System.Collections.Generic.IEnumerable<int> source, int seed = 0)
      => Xor(source, (e, i) => e, seed);
  }
}
