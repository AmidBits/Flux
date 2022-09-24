#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Creates a new sequence of consecutive numbers between the min and max from the specified collection.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> CreateConsecutive<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> collection)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (collection is null) throw new System.ArgumentNullException(nameof(collection));

      var (minItem, minIndex, maxItem, maxIndex) = collection.Extrema(s => s);

      return Loop.Between(minItem, maxItem, TSelf.One);
    }
  }
}
#endif
