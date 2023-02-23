namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all bits.</para>
    /// See <see cref="ReverseBytes{TSelf}(TSelf)"/> for byte reversal.
    /// </summary>
    public static TSelf ReverseBits<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the bit reversal.
      value.WriteBigEndian(bytes); // Write as BigEndian ('left-to-right').

      for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
        bytes[i] = MirrorBits(bytes[i]); // Mirror (reverse) bits in each byte.

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType())); // Read as LittleEndian ('right-to-left').
    }
  }
}
