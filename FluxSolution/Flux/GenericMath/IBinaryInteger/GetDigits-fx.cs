namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TNumber"/>) of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    public static System.Collections.Generic.List<TNumber> GetDigits<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      if (TNumber.IsNegative(value))
        value = TNumber.Abs(value);

      var list = new System.Collections.Generic.List<TNumber>();

      if (TNumber.IsZero(value))
        list.Add(TNumber.Zero);
      else
        while (!TNumber.IsZero(value))
        {
          list.Insert(0, value % rdx);

          value /= rdx;
        }

      return list;
    }

    /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TNumber"/>) of <paramref name="value"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Collections.Generic.List<TNumber> GetDigitsReversed<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Units.Radix.AssertMember(radix));

      if (TNumber.IsNegative(value))
        value = TNumber.Abs(value);

      var list = new System.Collections.Generic.List<TNumber>();

      if (TNumber.IsZero(value))
        list.Add(TNumber.Zero);
      else
        while (!TNumber.IsZero(value))
        {
          list.Add(value % rdx);

          value /= rdx;
        }

      return list;
    }
  }
}
