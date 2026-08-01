namespace Flux
{
  public static partial class Int32Extensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in a <see cref="System.Int32"/>.</para>
    /// </summary>
    public const int MaxPrimeNumber = 2147483647;

    extension(System.Int32)
    {
      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of an int, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</para>
      /// </summary>
      public static int ReverseBits(int value)
      {
        var v = (uint)value;

        uint.ReverseBitsInPlace(ref v);

        return unchecked((int)v);
      }

      #endregion
    }
  }
}
