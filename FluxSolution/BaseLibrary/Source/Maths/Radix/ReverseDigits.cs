namespace Flux
{
  public static partial class Maths
  {
    public static System.Numerics.BigInteger ReverseDigits(System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      if (value < 0) return -ReverseDigits(-value, radix);
      else if (radix <= 1) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = System.Numerics.BigInteger.Zero;

      while (value > 0)
      {
        value = System.Numerics.BigInteger.DivRem(value, radix, out var remainder);

        reverse = (reverse * radix + remainder);
      }

      return reverse;
    }

    public static int ReverseDigits(int value, int radix)
    {
      if (value < 0) return -ReverseDigits(-value, radix);
      else if (radix <= 1) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0;

      while (value > 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        reverse = (reverse * radix + remainder);
      }

      return reverse;
    }
    public static long ReverseDigits(long value, long radix)
    {
      if (value < 0) return -ReverseDigits(-value, radix);
      if (radix <= 1) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0L;

      while (value > 0)
      {
        value = System.Math.DivRem(value, radix, out var remainder);

        reverse = (reverse * radix + remainder);
      }

      return reverse;
    }

    [System.CLSCompliant(false)]
    public static uint ReverseDigits(uint value, uint radix)
    {
      if (radix <= 1) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0U;

      while (value > 0)
      {
        reverse = (reverse * radix) + (value % radix);

        value /= radix;
      }

      return reverse;
    }
    [System.CLSCompliant(false)]
    public static ulong ReverseDigits(ulong value, ulong radix)
    {
      if (radix <= 1) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0UL;

      while (value > 0)
      {
        reverse = (reverse * radix) + (value % radix);

        value /= radix;
      }

      return reverse;
    }
  }
}
