namespace Flux
{
  public static partial class Int64Extensions
  {
    extension(System.Int64)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static long MaxPrimeNumber => 9223372036854775783;

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a long, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</para>
      /// </summary>
      public static long ReverseBits(long value)
      {
        var v = (ulong)value;

        ulong.ReverseBitsInPlace(ref v);

        return unchecked((long)v);
      }

      #endregion
    }
  }
}
