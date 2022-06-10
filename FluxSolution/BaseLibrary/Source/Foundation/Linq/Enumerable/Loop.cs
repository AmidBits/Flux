namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Loop(this System.Numerics.BigInteger start, System.Numerics.BigInteger count, System.Numerics.BigInteger step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<decimal> Loop(this decimal start, decimal count, decimal step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<double> Loop(this double start, double count, double step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<float> Loop(this float start, float count, float step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<int> Loop(this int start, int count, int step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<long> Loop(this long start, long count, long step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
  }
}
