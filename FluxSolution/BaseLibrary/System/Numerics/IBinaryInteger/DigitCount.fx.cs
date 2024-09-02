namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DigitCount<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      var count = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        count++;

        value /= rdx;
      }

      return count;
    }
  }
}
