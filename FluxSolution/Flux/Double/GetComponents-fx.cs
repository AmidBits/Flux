namespace Flux
{
  public static partial class Doubles
  {
    /// <summary>
    /// <para>Get the three binary IEEE parts of the floating point both as binary (shifted to LSB) and adjusted (see below).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns>
    /// <para>The three adjusted IEEE parts: <c>(int IeeeSign = 1 or -1, int IeeeExponentUnbiased = [−1022, +1023], long IeeeMantissa53 = [0, 9007199254740991])</c>.</para>
    /// <para>Also returns the three binary IEEE parts, only shifted to LSB, as out parameters: <c><paramref name="ieeeSignBit"/> = 0 or 1 (1-bit as int), <paramref name="ieeeExponentBiased"/> = [0, 2047] (11-bits as int) and <paramref name="ieeeSignificandPrecision52"/> = [0, 4503599627370495]</c>.</para>
    /// </returns>
    public static (int IeeeSign, int IeeeExponentUnbiased, long IeeeMantissa53) GetIeeeComponents(this double source, out int ieeeSignBit, out int ieeeExponentBiased, out long ieeeSignificandPrecision52)
    {
      var bits = System.BitConverter.DoubleToUInt64Bits(source);

      ieeeSignBit = (int)((bits & 0x8000000000000000UL) >>> 63);

      ieeeExponentBiased = (int)((bits & 0x7FF0000000000000UL) >>> 52);

      ieeeSignificandPrecision52 = (long)(bits & 0x000FFFFFFFFFFFFFUL);

      var sign = ieeeSignBit == 0 ? 1 : -1;

      var exponentUnbiased = ieeeExponentBiased - 1023;

      var mantissa53 = 0x0010000000000000L | ieeeSignificandPrecision52; // This is the significandPrecision above with the hidden 53-bit added.

      return (sign, exponentUnbiased, mantissa53);
    }
  }
}
