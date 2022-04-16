namespace Flux
{
  public static partial class DecimalEm
  {
    /// <summary>Deconstructs the decimal number into its basic components as a named ValueType for use.</summary>
    [System.CLSCompliant(false)]
    public static decimal GetParts(this decimal source, out uint[] bits, out bool isNegative, out decimal mantissa, out byte scale)
    {
      bits = (uint[])(object)decimal.GetBits(source);

      isNegative = (bits[3] & 0x80000000) == 0x80000000;

      mantissa = (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];

      scale = (byte)((bits[3] >> 16) & 31); // The number of total decimals points.

      return source;
    }
  }
}
