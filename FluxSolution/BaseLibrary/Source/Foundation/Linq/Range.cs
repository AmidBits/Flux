namespace Flux
{
  public static partial class Liq
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Range(this System.Numerics.BigInteger start, System.Numerics.BigInteger count, System.Numerics.BigInteger step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<decimal> Range(this decimal start, decimal count, decimal step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<double> Range(this double start, double count, double step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<float> Range(this float start, float count, float step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<int> Range(this int start, int count, int step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<long> Range(this long start, long count, long step)
    {
      for (; count > 0; count--, start += step)
        yield return start;
    }
  }
}
