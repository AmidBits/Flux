namespace Flux
{
  public static partial class Bits
  {
    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    public static TSelf ReverseBytes<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.
      value.WriteLittleEndian(bytes);
      System.Array.Reverse(bytes, 0, bytes.Length); // Reverse all bytes.

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType()));
    }
  }
}
