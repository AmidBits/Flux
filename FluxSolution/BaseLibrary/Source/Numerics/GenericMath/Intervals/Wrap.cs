#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the <paramref name="self"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Wrap<TSelf>(this TSelf self, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => self < min
      ? max - (min - self) % (max - min)
      : self > max
      ? min + (self - min) % (max - min)
      : self;
  }
}
#endif
