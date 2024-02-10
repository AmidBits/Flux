namespace Flux
{
  public static partial class Fx
  {
    public static (int LessThan, int EqualTo, int GreaterThan) CompareSequence<TValue>(this System.Collections.Generic.IEnumerable<TValue> source)
      where TValue : System.IComparable<TValue>
    {
      var lt = 0;
      var eq = 0;
      var gt = 0;

      using var e = source.GetEnumerator();

      if (e.MoveNext() && e.Current is var a)
      {
        while (e.MoveNext() && e.Current is var b)
        {
          var cmp = a.CompareTo(b);

          if (cmp < 0) lt++;
          else if (cmp > 0) gt++;
          else eq++;

          a = b;
        }
      }

      return (lt, eq, gt);
    }
  }
}
