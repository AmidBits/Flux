#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Number
  {
    /// <summary>Examines whether the ordered sequence is broken.</summary>
    public static bool IsSequenceBroken<TSelf>(this System.Linq.IOrderedEnumerable<TSelf> ordinalSequence)
      where TSelf : System.Numerics.INumberBase<TSelf>
    {
      using var e = ordinalSequence.GetEnumerator();

      if (e.MoveNext())
      {
        var previous = e.Current;

        while (e.MoveNext())
        {
          if (TSelf.Abs(e.Current) - TSelf.Abs(previous) != TSelf.One)
            return true;

          previous = e.Current;
        }
      }

      return false;
    }
  }
}
#endif
