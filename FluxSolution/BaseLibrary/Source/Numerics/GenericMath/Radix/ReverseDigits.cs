namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Reverse the digits of <paramref name="number"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertRadix(radix, out TSelf tradix);

      var reverse = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        reverse = reverse * tradix + (number % tradix);

        number /= tradix;
      }

      return reverse;
    }
  }
}
