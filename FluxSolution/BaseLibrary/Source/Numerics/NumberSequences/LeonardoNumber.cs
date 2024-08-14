namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new sequence with Leonardo numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Leonardo_number"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="step"></param>
    /// <returns></returns>
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
