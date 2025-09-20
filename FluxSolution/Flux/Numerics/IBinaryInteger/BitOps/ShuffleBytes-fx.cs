namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Shuffles all bytes of an integer.</para>
    /// </summary>
    public static TInteger ShuffleBytes<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(value.GetByteCount());
      value.WriteLittleEndian(bytes);
      System.Security.Cryptography.RandomNumberGenerator.Shuffle<byte>(bytes);
      var returnValue = TInteger.ReadLittleEndian(bytes, value.GetType().IsUnsignedNumericType());
      System.Buffers.ArrayPool<byte>.Shared.Return(bytes);
      return returnValue;
    }
  }
}
