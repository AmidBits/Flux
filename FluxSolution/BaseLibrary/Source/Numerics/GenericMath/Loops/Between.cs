#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Loop
  {
    /// <summary>Creates a sequence of numbers from <paramref name="source"/> and <paramref name="target"/> times and increasing/decreasing by <paramref name="step"/> each iteration.</summary>
    public static System.Collections.Generic.IEnumerable<TSelf> Between<TSelf>(TSelf source, TSelf target, TSelf step)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      step = TSelf.Abs(step);

      if (source < target)
        while (source <= target)
        {
          yield return source;

          source += step;
        }
      else if (source > target)
        while (source >= target)
        {
          yield return source;

          source -= step;
        }
      else
        yield return source;
    }
  }
}
#endif
