namespace Flux
{
  public static partial class Int16Extensions
  {
    extension(System.Int16)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static short MaxPrimeNumber => 32749;

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a short, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
      /// </summary>
      public static short ReverseBits(short value)
      {
        var v = (ushort)value;

        ushort.ReverseBitsInPlace(ref v);

        return unchecked((short)v);
      }

      #endregion
    }
  }
}
