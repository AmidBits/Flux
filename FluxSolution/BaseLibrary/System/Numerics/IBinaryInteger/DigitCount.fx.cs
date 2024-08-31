namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static TValue DigitCount<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      Quantities.Radix.AssertMember(radix);

      var count = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        count++;

        value /= radix;
      }

      return count;
    }
  }
}
