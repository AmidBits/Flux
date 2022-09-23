#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Loop
  {
    /// <summary>Creates a sequence of numbers from <paramref name="start"/> and <paramref name="count"/> times and increasing/decreasing by <paramref name="step"/> each iteration.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> For<TSelf>(this TSelf start, TSelf count, TSelf step)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (count < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(count));

      while (count > TSelf.Zero)
      {
        yield return start;

        count--;
        start += step;
      }
    }
  }
}
#endif
