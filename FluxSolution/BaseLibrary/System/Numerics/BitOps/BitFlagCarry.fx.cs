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
  }
}
