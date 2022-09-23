#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>Creates a sequence of numbers betweeb <paramref name="start"/> and <paramref name="end"/>, inclusive, with the <paramref name="step"/> for each iteration.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> LoopBetween<TSelf>(this TSelf start, TSelf end, TSelf step)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var count = TSelf.Abs(end - start) + TSelf.One;

      if ((start > end && step > TSelf.Zero) || (start < end && step < TSelf.Zero))
        step = -step;

      return Loop(start, count, step);
    }
  }
}
#endif
