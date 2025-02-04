namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="number"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="commonRatio"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <param name="commonRatio"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> GeometricSequence<TSelf>(this TSelf number, TSelf commonRatio)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (TSelf.IsZero(number)) throw new System.ArgumentOutOfRangeException(nameof(number));
      if (TSelf.IsZero(commonRatio)) throw new System.ArgumentOutOfRangeException(nameof(commonRatio));

      while (true)
      {
        checked
        {
          yield return number;

          number *= commonRatio;
        }
      }
    }
  }
}
