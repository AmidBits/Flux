namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      while (!TInteger.IsZero(value))
      {
        var remainder = value % rdx;

        value /= rdx;

        if (TInteger.IsZero(value))
          break;
        else if (TInteger.Abs((value % rdx) - remainder) > TInteger.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
