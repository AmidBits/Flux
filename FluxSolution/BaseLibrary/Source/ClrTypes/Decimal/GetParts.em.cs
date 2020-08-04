namespace Flux
{
  public static partial class XtensionsDecimal
  {
    /// <summary>Deconstructs the decimal number into its basic components as a named ValueType for use.</summary>
    [System.CLSCompliant(false)]
    public static (uint[] bits, bool isNegative, decimal mantissa, byte scale, decimal originalValue) GetParts(this decimal source)
    {
      var bits = (uint[])(object)decimal.GetBits(source);

      var isNegative = (bits[3] & 0x80000000) == 0x80000000;

      var mantissa = (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];

      var scale = (byte)((bits[3] >> 16) & 31); // The number of total decimals points.

      return (bits, isNegative, mantissa, scale, source);
    }
  }
}
