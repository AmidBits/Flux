namespace Flux
{
  public static partial class BitOps
  {
    public static bool BitShiftLeft(ref int value)
    {
      var carryFlag = ((uint)value & (1UL << 31)) > 0;
      value = (int)((uint)value << 1);
      return carryFlag;
    }

    [System.CLSCompliant(false)]
    public static bool BitShiftRight(ref uint value)
    {
      var carryFlag = (value & 1) > 0;
      value >>= 1;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftLeft(ref ulong value)
    {
      var carryFlag = (value & (1UL << 63)) > 0;
      value <<= 1;
      return carryFlag;
    }
    /// <summary>Shifts the bits one position to the right.</summary>
    /// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftRight(ref ulong value)
    {
      var carryFlag = (value & 1) > 0;
      value >>= 1;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftLeft(ref uint value, int count)
    {
      var carryFlag = (value & (1UL << 31)) > 0;
      value <<= count;
      return carryFlag;
    }
    /// <summary>Shifts the bits one position to the right.</summary>
    /// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftRight(ref uint value, int count)
    {
      var carryFlag = (value & 1) > 0;
      value >>= count;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftLeft(ref ulong value, int count)
    {
      var carryFlag = (value & (1UL << 63)) > 0;
      value <<= count;
      return carryFlag;
    }
    /// <summary>Shifts the bits one position to the right.</summary>
    /// <returns>Whether the LSB (least significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool BitShiftRight(ref ulong value, int count)
    {
      var carryFlag = (value & 1) > 0;
      value >>= count;
      return carryFlag;
    }
  }
}
