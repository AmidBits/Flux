namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static (TSelf count, TSelf sum) DigitStatistics<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var count = TSelf.Zero;
      var sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;
        sum += number % radix;

        number /= radix;
      }

      return (count, sum);
    }
  }
}
