namespace Flux
{
  public static partial class BitOps
  {
    [System.CLSCompliant(false)]
    public static uint ShiftLeft(ref this uint value, int count)
    {
      var carry = value >> (32 - count);
      value <<= count;
      return carry;
    }

    /// <summary>Shifts the bits count positions to the left, and returns the carry bits.</summary>
    /// <returns>The carry bits, or the overflowing bits.</returns>
    [System.CLSCompliant(false)]
    public static ulong ShiftLeft(ref this ulong value, int count)
    {
      var carry = value >> (64 - count);
      value <<= count;
      return carry;
    }
  }
}
