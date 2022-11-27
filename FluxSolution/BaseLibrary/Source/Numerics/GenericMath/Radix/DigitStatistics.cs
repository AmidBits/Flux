namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static (TSelf count, TSelf sum) DigitStatistics<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertRadix(radix, out TSelf tradix);

      var count = TSelf.Zero;
      var sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;
        sum += number % tradix;

        number /= tradix;
      }

      return (count, sum);
    }
  }
}
