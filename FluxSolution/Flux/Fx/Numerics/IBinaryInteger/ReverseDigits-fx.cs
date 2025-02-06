namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse the digits of the <paramref name="value"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TNumber ReverseDigits<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

      var reversed = TNumber.Zero;

      while (!TNumber.IsZero(value))
      {
        reversed = (reversed * rdx) + (value % rdx);

        value /= rdx;
      }

      return reversed;
    }
  }
}
