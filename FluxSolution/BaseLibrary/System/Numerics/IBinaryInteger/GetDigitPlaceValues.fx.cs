namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the digit place value components of <paramref name="number"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
    public static System.Collections.Generic.List<TSelf> GetDigitPlaceValues<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var list = number.GetDigitsReversed(radix); // Already asserts radix.

      var power = TSelf.One;

      for (int index = 0; index < list.Count; index++)
      {
        list[index] *= power;

        power *= radix;
      }

      return list;
    }
  }
}
