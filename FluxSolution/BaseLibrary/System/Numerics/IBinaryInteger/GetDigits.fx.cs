namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigits<TSelf>(this TSelf number, TSelf radix, int count = int.MaxValue)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(number, radix, count);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf>(this TSelf number, TSelf radix, int count = int.MaxValue)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      if (TSelf.IsNegative(number))
        number = TSelf.Abs(number);

      var list = new System.Collections.Generic.List<TSelf>();

      if (TSelf.IsZero(number))
        list.Add(TSelf.Zero);
      else
        while (!TSelf.IsZero(number) && list.Count < count)
        {
          list.Add(number % radix);

          number /= radix;
        }

      return list;
    }
  }
}
