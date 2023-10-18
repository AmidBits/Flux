namespace Flux
{
  //  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.GetByteCount(TSelf)"/>.</summary>
    public static int GetByteCount<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetByteCount();

#else

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetByteCount(this System.Numerics.BigInteger value) => value.GetByteCount();

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetByteCount(this byte value) => 1;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetByteCount(this short value) => 2;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetByteCount(this int value) => 4;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    public static int GetByteCount(this long value) => 8;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetByteCount(this sbyte value) => 1;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetByteCount(this ushort value) => 2;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetByteCount(this uint value) => 4;

    /// <summary>Gets the size, in bits, of the type, based on byte count.</summary>
    [System.CLSCompliant(false)] public static int GetByteCount(this ulong value) => 8;

#endif
  }
}
