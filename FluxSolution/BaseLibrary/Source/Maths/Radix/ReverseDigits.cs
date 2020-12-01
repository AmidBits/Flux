namespace Flux
{
  public static partial class Maths
  {
    public static System.Numerics.BigInteger ReverseDigits(System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));
      if (value < 0) return -ReverseDigits(-value, radix);

      var reverse = System.Numerics.BigInteger.Zero;
      while (value > 0)
      {
        value = System.Numerics.BigInteger.DivRem(value, radix, out var remainder);
        reverse = reverse * radix + remainder;
      }
      return reverse;
    }

    public static int ReverseDigits(int value, int radix)
      => value < 0 ? -(int)ReverseDigits((uint)-value, radix) : (int)ReverseDigits((uint)value, radix);
    public static long ReverseDigits(long value, int radix)
      => value < 0 ? -(long)ReverseDigits((ulong)-value, radix) : (long)ReverseDigits((ulong)value, radix);

    [System.CLSCompliant(false)]
    public static uint ReverseDigits(uint value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0U;
      while (value > 0)
      {
        reverse = (reverse * (uint)radix) + (value % (uint)radix);
        value /= (uint)radix;
      }
      return reverse;
    }
    [System.CLSCompliant(false)]
    public static ulong ReverseDigits(ulong value, int radix)
    {
      if (radix < 2) throw new System.ArgumentOutOfRangeException(nameof(radix));

      var reverse = 0UL;
      while (value > 0)
      {
        reverse = (reverse * (ulong)radix) + (value % (ulong)radix);
        value /= (ulong)radix;
      }
      return reverse;
    }
  }
}
