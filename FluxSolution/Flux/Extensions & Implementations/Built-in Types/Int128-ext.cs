namespace Flux
{
  public static partial class Int128Extensions
  {
    extension(System.Int128)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static System.Int128 MaxPrimeNumber => new(0x7FFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFFFFul);

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a System.Int128, i.e. trade place of bit 127 with bit 0 and bit 126 with bit 1 and so on.</para>
      /// </summary>
      public static System.Int128 ReverseBits(System.Int128 value)
      {
        var v = (System.UInt128)value;

        System.UInt128.ReverseBitsInPlace(ref v);

        return unchecked((System.Int128)v);
      }

      #endregion  
    }
  }
}
