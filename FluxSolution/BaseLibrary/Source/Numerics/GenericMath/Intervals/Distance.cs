#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the 1-dimensional distance between the two specified values.</summary>
    public static TSelf Distance<TSelf>(this TSelf self, TSelf other)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => TSelf.Abs(other - self);
  }
}
#endif
