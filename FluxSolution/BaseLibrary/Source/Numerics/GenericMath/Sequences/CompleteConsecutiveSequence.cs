namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Creates a new sequence of consecutive numbers between the min and max from the specified collection.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> CompleteConsecutiveSequence<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> collection)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (collection is null) throw new System.ArgumentNullException(nameof(collection));

      var (minItem, minIndex, maxItem, maxIndex) = collection.Extrema(s => s);

      return Loops.Range<TSelf>.CreateBetween(minItem, maxItem, TSelf.One);
    }
  }
}
