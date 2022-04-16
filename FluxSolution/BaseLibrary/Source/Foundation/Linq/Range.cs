namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> ForLoop(this System.Numerics.BigInteger start, System.Numerics.BigInteger count, System.Numerics.BigInteger step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<decimal> ForLoop(this decimal start, decimal count, decimal step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<double> ForLoop(this double start, double count, double step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<float> ForLoop(this float start, float count, float step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<int> ForLoop(this int start, int count, int step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<long> ForLoop(this long start, long count, long step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
  }
}
