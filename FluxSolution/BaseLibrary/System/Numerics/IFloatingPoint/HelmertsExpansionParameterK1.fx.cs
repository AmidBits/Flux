namespace Flux
{
  public static partial class Fx
  {
    /// <summary>https://en.wikipedia.org/wiki/Vincenty%27s_formulae</summary>
    public static TValue HelmertsExpansionParameterK1<TValue>(this TValue u)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IRootFunctions<TValue>
      => TValue.Sqrt(TValue.One + u * u) is var k ? (k - TValue.One) / (k + TValue.One) : throw new System.ArithmeticException();
  }
}
