namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Shuffles all bytes of an integer.</para>
    /// </summary>
    public static TNumber ShuffleBytes<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var bytes = new byte[value.GetByteCount()];
      value.WriteLittleEndian(bytes);
      System.Security.Cryptography.RandomNumberGenerator.Shuffle<byte>(bytes);
      return TNumber.ReadLittleEndian(bytes, value.GetType().IsUnsignedNumericType());
    }
  }
}
