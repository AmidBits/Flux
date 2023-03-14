//namespace Flux
//{
//  public static partial class GenericMath
//  {
//    public static bool IsSequenceConsecutive<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      using var e = source.ThrowIfNullOrEmpty().PartitionTuple2(false, (a, b, i) => b - a).GetEnumerator();

//      var count = 0;

//      if (e.MoveNext())
//      {
//        var gap = e.Current;

//        do
//        {
//          count++;
//        }
//        while (e.MoveNext() && e.Current == gap);
//      }

//      return count > 1 ? true : throw new System.ArgumentException("Sequence does not have enough elements to determine consecutiveness.", nameof(source));
//    }
//  }
//}
