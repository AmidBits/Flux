namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TValue"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TValue> GetDigits<TValue>(this TValue value, TValue radix, int count = int.MaxValue)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      var reversed = GetDigitsReversed(value, radix, count);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TValue"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TValue> GetDigitsReversed<TValue>(this TValue value, TValue radix, int count = int.MaxValue)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      Quantities.Radix.AssertMember(radix);

      if (TValue.IsNegative(value))
        value = TValue.Abs(value);

      var list = new System.Collections.Generic.List<TValue>();

      if (TValue.IsZero(value))
        list.Add(TValue.Zero);
      else
        while (!TValue.IsZero(value) && list.Count < count)
        {
          list.Add(value % radix);

          value /= radix;
        }

      return list;
    }
  }
}
