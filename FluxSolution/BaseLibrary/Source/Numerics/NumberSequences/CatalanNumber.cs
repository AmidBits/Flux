namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>Returns the Catalan number for the specified number.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Catalan_number"/>
    public static TSelf GetCatalanNumber<TSelf>(TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number < TSelf.CreateChecked(200)
      ? Maths.Factorial(number + number) / (Maths.Factorial(number + TSelf.One) * Maths.Factorial(number))
      : Maths.SplitFactorial(number + number) / (Maths.SplitFactorial(number + TSelf.One) * Maths.SplitFactorial(number));

    /// <summary>Creates a new sequence with Catalan numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Catalan_number"/>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    public static System.Collections.Generic.IEnumerable<TSelf> GetCatalanSequence<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      for (var number = TSelf.Zero; ; number++)
        yield return GetCatalanNumber(number);
    }
  }
}
