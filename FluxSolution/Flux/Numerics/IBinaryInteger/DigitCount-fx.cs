namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TNumber DigitCount<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      var count = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        count++;

        value /= rdx;
      }

      return count;
    }
  }
}
