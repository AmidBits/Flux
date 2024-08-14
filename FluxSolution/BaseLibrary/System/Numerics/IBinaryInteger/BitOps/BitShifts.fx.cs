namespace Flux
{
  public static partial class BitOps
  {
    public static TSelf BitShiftLeft<TSelf>(this TSelf source, bool lsb)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (source << 1) | (lsb ? TSelf.One : TSelf.Zero);

    public static TSelf BitShiftRight<TSelf>(this TSelf source, bool msb)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (msb ? TSelf.RotateRight(TSelf.One, 1) : TSelf.Zero) | (source >> 1);
  }
}
