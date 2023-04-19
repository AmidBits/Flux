namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    public static int ILog2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(TSelf.Log2(value));

#else

    public static int ILog2(this System.Numerics.BigInteger value) => System.Convert.ToInt32(System.Numerics.BigInteger.Log(value, 2));

    public static int ILog2(this int value) => value >= 0 ? System.Numerics.BitOperations.Log2(unchecked((uint)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));

    public static int ILog2(this long value) => value >= 0 ? System.Numerics.BitOperations.Log2(unchecked((ulong)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));

#endif
  }
}
