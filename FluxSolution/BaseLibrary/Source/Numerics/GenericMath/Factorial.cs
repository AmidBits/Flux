#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! The factorial of a non-negative integer value, x, is the product of all positive integers less than or equal to the value.</summary>
    /// <remarks>These implementations have been extended to accomodate negative integers. They are simply computed with the sign removed and then re-applied.</remarks>
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
      var processorCount = TSelf.CreateChecked(System.Environment.ProcessorCount);

      var size = x / processorCount + TSelf.One;

      var list = new System.Collections.Generic.List<Flux.Loops.Range<TSelf>>(System.Environment.ProcessorCount);

      for (var index = TSelf.One; index <= x; index += size)
        list.Add(new Flux.Loops.Range<TSelf>(index, TSelf.Min(size, x - index), TSelf.One));

      return list.AsParallel().Select(l => l.Aggregate(TSelf.One, (a, b) => a * b)).Aggregate(TSelf.One, (a, b) => a * b);
    }
  }
}
#endif
