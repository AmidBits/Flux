namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
    public static (TSelf x, TSelf y, TSelf z) Orthogonal<TSelf>(TSelf x, TSelf y, TSelf z)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(x) > TSelf.Abs(z) ? (-y, x, TSelf.Zero) : (TSelf.Zero, -x, y);
  }
}
