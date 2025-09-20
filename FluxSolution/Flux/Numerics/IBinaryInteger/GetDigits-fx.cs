namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TInteger"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TInteger> GetDigits<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      if (TInteger.IsNegative(value))
        value = TInteger.Abs(value);

      var list = new System.Collections.Generic.List<TInteger>();

      if (TInteger.IsZero(value))
        list.Add(TInteger.Zero);
      else
        while (!TInteger.IsZero(value))
        {
          list.Insert(0, value % rdx);

          value /= rdx;
        }

      return list;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TInteger"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TInteger> GetDigitsReversed<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      if (TInteger.IsNegative(value))
        value = TInteger.Abs(value);

      var list = new System.Collections.Generic.List<TInteger>();

      if (TInteger.IsZero(value))
        list.Add(TInteger.Zero);
      else
        while (!TInteger.IsZero(value))
        {
          list.Add(value % rdx);

          value /= rdx;
        }

      return list;
    }
  }
}
