namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TValue, TRadix>(this TValue value, TRadix radix)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      var rdx = TValue.CreateChecked(Quantities.Radix.AssertMember(radix));

      while (!TValue.IsZero(value))
      {
        var remainder = value % rdx;

        value /= rdx;

        if (TValue.IsZero(value))
          break;
        else if (TValue.Abs((value % rdx) - remainder) > TValue.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
