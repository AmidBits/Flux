namespace Flux
{
  public static partial class Int64Extensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in an <see cref="System.Int64"/>.</para>
    /// </summary>
    public const long MaxPrimeNumber = 9223372036854775783;

    extension(System.Int64)
    {
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
