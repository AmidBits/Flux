namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Reverse the digits of <paramref name="number"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var reverse = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        reverse = (reverse * radix) + (number % radix);

        number /= radix;
      }

      return reverse;
    }
  }
}
