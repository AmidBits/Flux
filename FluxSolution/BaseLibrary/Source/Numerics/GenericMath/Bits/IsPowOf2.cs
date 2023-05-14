namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    public static bool IsPow2<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsPow2(value);

#else

    public static bool IsPow2(this System.Numerics.BigInteger value) 
      => (value & (value - 1)).IsZero;

    public static bool IsPow2(this int value) 
      => System.Numerics.BitOperations.IsPow2(value);

    public static bool IsPow2(this long value) 
      => System.Numerics.BitOperations.IsPow2(value);

#endif
  }
}
