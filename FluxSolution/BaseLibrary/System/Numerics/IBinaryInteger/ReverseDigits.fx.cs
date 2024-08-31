namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse the digits of the <paramref name="value"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TValue ReverseDigits<TValue>(this TValue value, TValue radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      Quantities.Radix.AssertMember(radix);

      var reversed = TValue.Zero;

      while (!TValue.IsZero(value))
      {
        reversed = (reversed * radix) + (value % radix);

        value /= radix;
      }

      return reversed;
    }
  }
}
