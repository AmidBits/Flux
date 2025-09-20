namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Reverse the digits of the <paramref name="value"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TInteger ReverseDigits<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      var reversed = TInteger.Zero;

      while (!TInteger.IsZero(value))
      {
        reversed = (reversed * rdx) + (value % rdx);

        value /= rdx;
      }

      return reversed;
    }
  }
}
