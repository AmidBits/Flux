namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TValue"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TValue> GetDigits<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      if (TValue.IsNegative(value))
        value = TValue.Abs(value);

      var list = new System.Collections.Generic.List<TValue>();

      if (TValue.IsZero(value))
        list.Add(TValue.Zero);
      else
        while (!TValue.IsZero(value))
        {
          list.Insert(0, value % rdx);

          value /= rdx;
        }

      return list;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TValue"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TValue> GetDigitsReversed<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      if (TValue.IsNegative(value))
        value = TValue.Abs(value);

      var list = new System.Collections.Generic.List<TValue>();

      if (TValue.IsZero(value))
        list.Add(TValue.Zero);
      else
        while (!TValue.IsZero(value))
        {
          list.Add(value % rdx);

          value /= rdx;
        }

      return list;
    }
  }
}
