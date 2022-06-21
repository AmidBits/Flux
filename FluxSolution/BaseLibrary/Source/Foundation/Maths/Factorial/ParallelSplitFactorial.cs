namespace Flux
{
  public sealed class ParallelSplitFactorial
    : IFactorialFunction
  {
    public static readonly IFactorialFunction Default = new ParallelSplitFactorial();

    public static async System.Threading.Tasks.Task<System.Numerics.BigInteger> ComputeFactorialAsync(System.Numerics.BigInteger number)
    {
      if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));
      else if (number == 0) return 1;
      else if (number <= 2) return number;

      var tasks = new System.Collections.Generic.List<System.Threading.Tasks.Task<System.Numerics.BigInteger>>(128);

      System.Numerics.BigInteger high = number, low = number >> 1, shift = low;

      while ((low + 1) < high)
      {
        tasks.Add(Product(low + 1, high));

        high = low;
        low >>= 1;
        shift += low;
      }

      await System.Threading.Tasks.Task.WhenAll(tasks).ConfigureAwait(false);

      var r = System.Numerics.BigInteger.One;
      var p = System.Numerics.BigInteger.One;

      for (var index = tasks.Count - 1; index >= 0; index--)
      {
        var R = r * p;
        var t = p * tasks[index].Result;

        r = R;
        p = t;
      }

      return (r * p) << (int)shift;

      static async System.Threading.Tasks.Task<System.Numerics.BigInteger> Product(System.Numerics.BigInteger n, System.Numerics.BigInteger m)
      {
        n |= 1; // Round n up to the next odd number
        m = (m - 1) | 1; // Round m down to the next odd number

        if (m == n)
          return m;

        if (m == (n + 2))
          return n * m;

        var k = (n + m) >> 1;

        return await Product(n, k).ConfigureAwait(false) * await Product(k + 1, m).ConfigureAwait(false);
      }
    }

    public System.Numerics.BigInteger ComputeFactorial(System.Numerics.BigInteger number)
    {
      var task = ComputeFactorialAsync(number);
      task.Wait();
      return task.Result;
    }
  }
}
