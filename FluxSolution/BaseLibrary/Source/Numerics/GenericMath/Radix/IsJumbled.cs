#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      while (!TSelf.IsZero(value))
      {
        var remainder = value % radix;

        value /= radix;

        if (TSelf.IsZero(value))
          break;
        else if (TSelf.Abs((value % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
#endif
