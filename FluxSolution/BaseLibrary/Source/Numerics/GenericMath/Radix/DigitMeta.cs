namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static void DigitMeta<TSelf>(this TSelf number, TSelf radix, out TSelf count, out System.Collections.Generic.List<TSelf> digits, out TSelf sum)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      count = TSelf.Zero;
      digits = new System.Collections.Generic.List<TSelf>();
      sum = TSelf.Zero;

      while (!TSelf.IsZero(number))
      {
        count++;
        var digit = number % radix;
        digits.Add(digit);
        sum += digit;

        number /= radix;
      }
    }
  }
}
