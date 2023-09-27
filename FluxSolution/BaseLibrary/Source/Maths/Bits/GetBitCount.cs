namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Gets the storage size, in bits, of the binary integer, based on byte-count.</summary>
    public static int GetBitCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetByteCount() * 8;

#else

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetBitCount(this System.Numerics.BigInteger value) => value.GetByteCount() * 8;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetBitCount(this byte value) => 8;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetBitCount(this short value) => 16;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetBitCount(this int value) => 32;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetBitCount(this long value) => 64;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetBitCount(this sbyte value) => 8;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetBitCount(this ushort value) => 16;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetBitCount(this uint value) => 32;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetBitCount(this ulong value) => 64;

#endif
  }
}
