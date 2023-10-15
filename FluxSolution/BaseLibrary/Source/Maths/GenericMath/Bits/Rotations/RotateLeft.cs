namespace Flux
{
  public static partial class Bits
  {
    /// <summary>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.RotateLeft(TSelf, int)"/>.</summary>
    public static TSelf RotateLeft<TSelf>(this TSelf value, int rotateAmount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.RotateLeft(value, rotateAmount);
  }
}
