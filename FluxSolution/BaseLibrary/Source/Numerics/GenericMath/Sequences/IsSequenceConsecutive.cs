#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns whether the sequence consists of consecutive numbers.</summary>
    public static bool IsSequenceConsecutive<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> collection)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      using var e = collection.GetEnumerator();

      if (e.MoveNext())
      {
        var previous = e.Current;

        while (e.MoveNext())
        {
          if (TSelf.Abs(e.Current) - TSelf.Abs(previous) != TSelf.One)
            return false;

          previous = e.Current;
        }
      }

      return true;
    }
  }
}
#endif
