namespace Flux
{
  public static partial class Liq
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Range(this System.Numerics.BigInteger start, System.Func<System.Numerics.BigInteger, bool> conditionSelector, System.Func<System.Numerics.BigInteger, System.Numerics.BigInteger> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<decimal> Range(this decimal start, System.Func<decimal, bool> conditionSelector, System.Func<decimal, decimal> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<double> Range(this double start, System.Func<double, bool> conditionSelector, System.Func<double, double> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<float> Range(this float start, System.Func<float, bool> conditionSelector, System.Func<float, float> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }

    public static System.Collections.Generic.IEnumerable<int> Range(this int start, System.Func<int, bool> conditionSelector, System.Func<int, int> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }
    public static System.Collections.Generic.IEnumerable<long> Range(this long start, System.Func<long, bool> conditionSelector, System.Func<long, long> iteratorSelector)
    {
      for (; conditionSelector(start); start = iteratorSelector(start))
        yield return start;
    }
  }
}
