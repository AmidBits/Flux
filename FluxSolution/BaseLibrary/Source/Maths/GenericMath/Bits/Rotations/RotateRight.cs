namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
    /// <summary>Using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.RotateRight(TSelf, int)"/>.</summary>
    public static TSelf RotateRight<TSelf>(this TSelf value, int rotateAmount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.RotateRight(value, rotateAmount);
  }
}
