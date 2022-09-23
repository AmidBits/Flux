#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    public static System.Collections.Generic.IEnumerable<TSelf> GetCompleteSet<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var (minItem, minIndex, maxItem, maxIndex) = source.Extrema(s => s);

      return Number.LoopBetween(minItem, maxItem, TSelf.One);
    }
  }
}
#endif
