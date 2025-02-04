namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Returns the Catalan number for the specified <paramref name="number"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static TSelf GetCatalanNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number < TSelf.CreateChecked(211)
      ? (number + number).Factorial() / ((number + TSelf.One).Factorial() * number.Factorial())
      : (number + number).SplitFactorial() / ((number + TSelf.One).SplitFactorial() * number.SplitFactorial());

    /// <summary>
    /// <para>Creates a new sequence with Catalan numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetCatalanSequence<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var number = TSelf.Zero; ; number++)
        yield return GetCatalanNumber(number);
    }
  }
}
