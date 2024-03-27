namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a new sequence with padovan numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Padovan_sequence"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetPadovanSequence<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf p1 = TSelf.One, p2 = TSelf.One, p3 = TSelf.One;

      yield return p1;
      yield return p2;
      yield return p3;

      while (true)
      {
        var pn = p2 + p3;

        yield return pn;

        p3 = p2;
        p2 = p1;
        p1 = pn;
      }
    }
  }
}
