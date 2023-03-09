namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DigitCount<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      var count = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;

        number /= rdx;
      }

      return count;
    }
  }
}
