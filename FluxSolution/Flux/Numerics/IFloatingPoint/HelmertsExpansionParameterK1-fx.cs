namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>
    /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
    /// </summary>
    public static TFloat HelmertsExpansionParameterK1<TFloat>(this TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
      => TFloat.Sqrt(TFloat.One + value * value) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();
  }
}
