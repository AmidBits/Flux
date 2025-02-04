namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TNumber.CreateChecked(Quantities.Radix.AssertMember(radix));

      while (!TNumber.IsZero(value))
      {
        var remainder = value % rdx;

        value /= rdx;

        if (TNumber.IsZero(value))
          break;
        else if (TNumber.Abs((value % rdx) - remainder) > TNumber.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
