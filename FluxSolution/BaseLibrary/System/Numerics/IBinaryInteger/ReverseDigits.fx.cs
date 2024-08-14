namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reverse the digits of the <paramref name="number"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      var reversed = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        reversed = (reversed * radix) + (number % radix);

        number /= radix;
      }

      return reversed;
    }
  }
}
