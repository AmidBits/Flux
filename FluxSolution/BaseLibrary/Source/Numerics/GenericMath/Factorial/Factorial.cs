namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers, which are simply computed with the sign removed and then re-applied.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    public static TSelf Factorial<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => x < TSelf.Zero
      ? -Factorial(-x)
      : x <= TSelf.One
      ? TSelf.One
      : (TSelf.One + TSelf.One).LoopRange(x - TSelf.One, TSelf.One).AsParallel().Aggregate(TSelf.One, (a, b) => a * b);


    public static TSelf FactorialEx<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var groupCount = TSelf.Min(x.IntegerSqrt(), TSelf.CreateChecked(int.Max(1, System.Environment.ProcessorCount - 2))); // Minimum of square root of x and the number of processors on the system, minus 2 of them, and at least 1 :).

      var (quotient, remainder) = TSelf.DivRem(x, groupCount);

      var list = new System.Collections.Generic.List<Flux.Loops.Range<TSelf>>();

      for (var index = TSelf.Zero; index < groupCount; index++)
        list.Add(new Flux.Loops.Range<TSelf>(index * quotient + TSelf.One, quotient, TSelf.One));

      if (remainder > TSelf.Zero)
        list.Add(new Flux.Loops.Range<TSelf>(groupCount * quotient + TSelf.One, remainder, TSelf.One));

      foreach (var range in list)
        System.Console.WriteLine($"{string.Join(',', range)} = {range.Aggregate(TSelf.One, (a, b) => a * b)}");

      return list.AsParallel().Select(l => l.Aggregate(TSelf.One, (a, b) => a * b)).Aggregate(TSelf.One, (a, b) => a * b);
    }
  }
}