namespace Flux
{
  public static partial class Fx
  {
    public static bool BitFlagCarryLsb<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => !TSelf.IsZero(source & TSelf.One);

    public static bool BitFlagCarryMsb<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => !TSelf.IsZero(source & TSelf.RotateRight(TSelf.One, 1));

    public static byte BitShiftLeft<TSelf>(this TSelf source, bool lsb)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (source << 1) | (lsb ? 1 : 0);

    public static byte BitShiftRight<TSelf>(this TSelf source, bool msb)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => (msb ? TSelf.RotateRight(TSelf.One, 1) : 0) | (source >> 1);
  }
}
