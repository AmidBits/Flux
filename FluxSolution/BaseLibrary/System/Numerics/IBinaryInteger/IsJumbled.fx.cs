namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether <paramref name="number"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      Quantities.Radix.AssertMember(radix);

      while (!TSelf.IsZero(number))
      {
        var remainder = number % radix;

        number /= radix;

        if (TSelf.IsZero(number))
          break;
        else if (TSelf.Abs((number % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
