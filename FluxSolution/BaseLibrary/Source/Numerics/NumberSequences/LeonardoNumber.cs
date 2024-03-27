namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a new sequence with Leonardo numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Leonardo_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetLeonardoNumbers<TSelf>(TSelf first, TSelf second, TSelf step)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      while (true)
      {
        yield return first;

        (first, second) = (second, first + second + step);
      }
    }
  }
}
