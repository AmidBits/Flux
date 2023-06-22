namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>https://en.wikipedia.org/wiki/Vincenty%27s_formulae</summary>
    public static TSelf HelmertsExpansionParameterK1<TSelf>(this TSelf u)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(TSelf.One + u * u) is var k ? (k - TSelf.One) / (k + TSelf.One) : throw new System.ArithmeticException();

#else

    /// <summary>https://en.wikipedia.org/wiki/Vincenty%27s_formulae</summary>
    public static double HelmertsExpansionParameterK1(this double u)
      => System.Math.Sqrt(1 + u * u) is var k ? (k - 1) / (k + 1) : throw new System.ArithmeticException();

#endif
  }
}
