namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    public static int Log2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.Log2(value));

#else

    /// <summary>Also known as "population count" of a binary integer value x is the number of one bits in the value.</summary>
    public static int Log2(this System.Numerics.BigInteger value) => System.Convert.ToInt32(System.Numerics.BigInteger.Log(value, 2));

    public static int Log2(this int value) => unchecked((uint)value).Log2();

    public static int Log2(this long value) => unchecked((ulong)value).Log2();

    [System.CLSCompliant(false)] public static int Log2(this uint value) => System.Numerics.BitOperations.Log2(value);

    [System.CLSCompliant(false)] public static int Log2(this ulong value) => System.Numerics.BitOperations.Log2(value);

#endif
  }
}
