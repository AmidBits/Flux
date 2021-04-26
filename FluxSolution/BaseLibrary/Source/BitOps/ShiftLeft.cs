namespace Flux
{
  public static partial class BitOps
  {
    [System.CLSCompliant(false)]
    public static bool ShiftLeft(ref uint value)
    {
      var carryFlag = (value & 0x80000000U) != 0;
      value <<= 1;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool ShiftLeft(ref ulong value)
    {
      var carryFlag = (value & 0x8000000000000000UL) != 0;
      value <<= 1;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool ShiftLeft(ref uint value, int count)
    {
      var carryFlag = (value & 0x80000000U) != 0;
      value <<= count;
      return carryFlag;
    }

    /// <summary>Shifts the bits one position to the left.</summary>
    /// <returns>Whether the MSB (most significant BIT), or overflow bit, was set.</returns>
    [System.CLSCompliant(false)]
    public static bool ShiftLeft(ref ulong value, int count)
    {
      var carryFlag = (value & 0x8000000000000000UL) != 0;
      value <<= count;
      return carryFlag;
    }
  }
}
