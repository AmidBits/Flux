namespace Flux.Linq
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> For(System.Numerics.BigInteger initializer, System.Func<System.Numerics.BigInteger, bool> conditionSelector, System.Func<System.Numerics.BigInteger, System.Numerics.BigInteger> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }

    public static System.Collections.Generic.IEnumerable<decimal> For(decimal initializer, System.Func<decimal, bool> conditionSelector, System.Func<decimal, decimal> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<double> For(double initializer, System.Func<double, bool> conditionSelector, System.Func<double, double> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<float> For(float initializer, System.Func<float, bool> conditionSelector, System.Func<float, float> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }

    public static System.Collections.Generic.IEnumerable<int> For(int initializer, System.Func<int, bool> conditionSelector, System.Func<int, int> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }
    public static System.Collections.Generic.IEnumerable<long> For(long initializer, System.Func<long, bool> conditionSelector, System.Func<long, long> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }

    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<uint> For(uint initializer, System.Func<uint, bool> conditionSelector, System.Func<uint, uint> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }
    [System.CLSCompliant(false)]
    public static System.Collections.Generic.IEnumerable<ulong> For(ulong initializer, System.Func<ulong, bool> conditionSelector, System.Func<ulong, ulong> iteratorSelector)
    {
      for (var value = initializer; conditionSelector(value); initializer = iteratorSelector(value))
        yield return value;
    }
  }
}
