//namespace Flux
//{
//  public static partial class Fx
//  {
//    public static IDictionary<int, int> CompareSequence<TValue>(this System.Collections.Generic.IEnumerable<TValue> source)
//      where TValue : System.IComparable<TValue>
//      //=> source.PartitionTuple2(false, (a, b, i) => (a, b)).Aggregate((lt: 0, eg: 0, gt: 0), (r, t) => t.a.CompareTo(t.b) is var cmp && cmp < 0 ? (r.lt + 1, r.eg, r.gt) : cmp > 0 ? (r.lt, r.eg, r.gt + 1) : (r.lt, r.eg + 1, r.gt), r => r);
//    {
//      return source.PartitionTuple2(false, (a, b, i) => System.Math.Sign(a.CompareTo(b))).GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());
//    }
//    //{
//    //  var lt = 0;
//    //  var eq = 0;
//    //  var gt = 0;

//    //  using var e = source.GetEnumerator();

//    //  if (e.MoveNext() && e.Current is var a)
//    //  {
//    //    while (e.MoveNext() && e.Current is var b)
//    //    {
//    //      var cmp = a.CompareTo(b);

//    //      if (cmp < 0) lt++;
//    //      else if (cmp > 0) gt++;
//    //      else eq++;

//    //      a = b;
//    //    }
//    //  }

//    //  return (lt, eq, gt);
//    //}
//  }
//}
