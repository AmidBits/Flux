namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
    /// </summary>
    public static TNumber HelmertsExpansionParameterK1<TNumber>(this TNumber u)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IRootFunctions<TNumber>
      => TNumber.Sqrt(TNumber.One + u * u) is var k ? (k - TNumber.One) / (k + TNumber.One) : throw new System.ArithmeticException();
  }
}
