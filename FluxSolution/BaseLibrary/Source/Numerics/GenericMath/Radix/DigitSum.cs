namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the sum of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      var sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        sum += number % rdx;

        number /= rdx;
      }

      return sum;
    }
  }
}
