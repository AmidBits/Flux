namespace Flux
{
  public static partial class Fx
  {
    /// <summary>https://en.wikipedia.org/wiki/Vincenty%27s_formulae</summary>
    public static TSelf HelmertsExpansionParameterK1<TSelf>(this TSelf u)
      where TSelf : System.Numerics.INumberBase<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(TSelf.One + u * u) is var k ? (k - TSelf.One) / (k + TSelf.One) : throw new System.ArithmeticException();
  }
}
