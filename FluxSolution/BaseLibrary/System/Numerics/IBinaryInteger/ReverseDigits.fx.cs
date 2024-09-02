namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse the digits of the <paramref name="value"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TValue ReverseDigits<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      var reversed = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        reversed = (reversed * rdx) + (value % rdx);

        value /= rdx;
      }

      return reversed;
    }
  }
}
