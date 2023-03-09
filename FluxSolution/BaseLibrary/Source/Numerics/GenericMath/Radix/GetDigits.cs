namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the individual digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigits<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var reversed = GetDigitsReversed(number, radix);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>Returns the place value digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      if (TSelf.IsNegative(number))
        number = -number;

      var list = new System.Collections.Generic.List<TSelf>();

      if (TSelf.IsZero(number))
        list.Add(TSelf.Zero);
      else
        while (!TSelf.IsZero(number))
        {
          list.Add(number % rdx);
          number /= rdx;
        }

      return list;
    }
  }
}
