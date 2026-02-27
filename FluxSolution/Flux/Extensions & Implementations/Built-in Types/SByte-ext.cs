namespace Flux
{
  public static partial class SByteExtensions
  {
    extension(System.SByte)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static sbyte MaxPrimeNumber => 127;

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of an sbyte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
      /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
      /// </summary>
      [System.CLSCompliant(false)]
      public static sbyte ReverseBits(sbyte value)
      {
        var v = (byte)value;

        byte.ReverseBitsInPlace(ref v);

        return unchecked((sbyte)v);
      }

      #endregion
    }
  }
}
