namespace Flux
{
#if NET7_0_OR_GREATER
  public class GroupedFactorial<TSelf>
    : IFactorialComputable<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
  {
    private TSelf m_threadCount;

    public GroupedFactorial(TSelf threadCount) => m_threadCount = TSelf.Max(TSelf.One, threadCount);
    public GroupedFactorial() : this(TSelf.Max(TSelf.One, TSelf.CreateChecked(System.Environment.ProcessorCount) - TSelf.One)) { }

    public TSelf ThreadCount { get => m_threadCount; init => m_threadCount = value; }

#else
  public class GroupedFactorial
    : IFactorialComputable<System.Numerics.BigInteger>
  {
    private System.Numerics.BigInteger m_threadCount;

    public GroupedFactorial(System.Numerics.BigInteger threadCount) => m_threadCount = System.Numerics.BigInteger.Max(System.Numerics.BigInteger.One, threadCount);
    public GroupedFactorial() : this(System.Numerics.BigInteger.Max(System.Numerics.BigInteger.One, System.Environment.ProcessorCount - System.Numerics.BigInteger.One)) { }

    public System.Numerics.BigInteger ThreadCount { get => m_threadCount; init => m_threadCount = value; }

#endif

#if NET7_0_OR_GREATER

    public TSelf ComputeFactorial(TSelf x)
    {
      if (TSelf.IsNegative(x)) return -ComputeFactorial(TSelf.Abs(x));

      var groupCount = m_threadCount;

      var (quotient, remainder) = TSelf.DivRem(x, groupCount);

      var list = new System.Collections.Generic.List<System.Threading.Tasks.Task<TSelf>>();

      for (var index = TSelf.Zero; index < groupCount; index++)
        list.Add(PartialFactorialAsync(index * quotient + TSelf.One, index * quotient + quotient));

      if (remainder > TSelf.Zero)
        list.Add(PartialFactorialAsync(groupCount * quotient + TSelf.One, groupCount * quotient + remainder));

      System.Threading.Tasks.Task.Run(() => { System.Threading.Tasks.Task.WhenAll(list).ConfigureAwait(false); }).ConfigureAwait(false);

      return list.AsParallel().Aggregate(TSelf.One, (acc, task) => acc * task.Result);
    }

    public static async System.Threading.Tasks.Task<TSelf> PartialFactorialAsync(TSelf loValue, TSelf hiValue)
    {
      var f = loValue;

      await System.Threading.Tasks.Task.Run(() =>
      {
        for (var i = loValue + TSelf.One; i <= hiValue; i++)
          f *= i;
      });

      return f;
    }

#else

    public System.Numerics.BigInteger ComputeFactorial(System.Numerics.BigInteger x)
    {
      if (x < 0) return -ComputeFactorial(System.Numerics.BigInteger.Abs(x));

      var groupCount = m_threadCount;

      var quotient = System.Numerics.BigInteger.DivRem(x, groupCount, out var remainder);

      var list = new System.Collections.Generic.List<System.Threading.Tasks.Task<System.Numerics.BigInteger>>();

      for (var index = System.Numerics.BigInteger.Zero; index < groupCount; index++)
        list.Add(PartialFactorialAsync(index * quotient + System.Numerics.BigInteger.One, index * quotient + quotient));

      if (remainder > System.Numerics.BigInteger.Zero)
        list.Add(PartialFactorialAsync(groupCount * quotient + System.Numerics.BigInteger.One, groupCount * quotient + remainder));

      System.Threading.Tasks.Task.Run(() => { System.Threading.Tasks.Task.WhenAll(list).ConfigureAwait(false); }).ConfigureAwait(false);

      return list.AsParallel().Aggregate(System.Numerics.BigInteger.One, (acc, task) => acc * task.Result);
    }

    public static async System.Threading.Tasks.Task<System.Numerics.BigInteger> PartialFactorialAsync(System.Numerics.BigInteger loValue, System.Numerics.BigInteger hiValue)
    {
      var f = loValue;

      await System.Threading.Tasks.Task.Run(() =>
      {
        for (var i = loValue + System.Numerics.BigInteger.One; i <= hiValue; i++)
          f *= i;
      });

      return f;
    }

#endif
  }
}
