namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static TSelf DigitCount<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      var count = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;

        number /= radix;
      }

      return count;
    }
  }
}
