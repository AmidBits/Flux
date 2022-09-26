
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled(System.Numerics.BigInteger value, int radix)
    {
      while (value != 0)
      {
        value = System.Numerics.BigInteger.DivRem(value, radix, out var digit);

        if (value == 0)
          break;
        else if (System.Numerics.BigInteger.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }

    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled(int value, int radix)
    {
      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var digit);

        if (value == 0)
          break;
        else if (System.Math.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled(long value, int radix)
    {
      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var digit);

        if (value == 0)
          break;
        else if (System.Math.Abs((value % radix) - digit) > 1) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }
  }
}
