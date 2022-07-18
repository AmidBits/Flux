namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Shifts the bits one position to the right.</summary>
    /// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static uint ShiftRight(ref this uint value, int count)
    {
      var carry = value << (32 - count);
      value >>= count;
      return carry;
    }

    /// <summary>Shifts the bits one position to the right.</summary>
    /// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static ulong ShiftRight(ref this ulong value, int count)
    {
      var carry = value << (64 - count);
      value >>= count;
      return carry;
    }
  }
}
