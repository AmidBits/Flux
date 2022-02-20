namespace Flux
{
  public static partial class Maths
  {
    public static System.Numerics.BigInteger ReverseDigits(this System.Numerics.BigInteger value, int radix)
    {
      AssertRadix(radix);

      var reverse = System.Numerics.BigInteger.Zero;

      while (value != 0)
      {
        value = System.Numerics.BigInteger.DivRem(value, radix, out var remainder);

        reverse = reverse * radix + remainder;
      }

      return reverse;
    }

    public static int ReverseDigits(this int value, int radix)
    {
      AssertRadix(radix);

      var reverse = 0;

      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        reverse = reverse * radix + remainder;
      }

      return reverse;
    }

    public static long ReverseDigits(this long value, int radix)
    {
      AssertRadix(radix);

      var reverse = 0L;

      while (value != 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        reverse = reverse * radix + remainder;
      }

      return reverse;
    }
  }
}
