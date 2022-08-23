namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Dirac delta function.
    /// <param name="selectorPositiveW"/>, <paramref name="x"/> = 0
    /// 0, <paramref name="x"/> != 0
    /// </summary>
    /// <param name="selectorPositiveW">Results in a hyperreal number.</param>
    public static TResult DiracDeltaFunction<TSelf, TResult>(this TSelf x, System.Func<TSelf, TResult> selectorPositiveW)
      where TSelf : System.Numerics.INumber<TSelf>
      => x == TSelf.Zero ? selectorPositiveW(x) : default!;

    /// <summary>PREVIEW! Unit step or Heaviside step function.
    /// 1, <paramref name="x"/> > 0
    /// 0, <paramref name="x"/> <= 0
    /// </summary>
    public static TSelf UnitStep<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => x > TSelf.Zero ? TSelf.One : TSelf.Zero;

    /// <summary>PREVIEW! Unit step or Heaviside step function. Alternate form:
    /// 0, <paramref name="x"/> < 0
    /// 1, <paramref name="x"/> >= 0
    /// </summary>
    public static TSelf UnitStepZ<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < TSelf.Zero ? TSelf.Zero : TSelf.One;

    /// <summary>PREVIEW! Unit step or Heaviside step function. Using half-maximum convention:
    /// 0.0, <paramref name="x"/> < 0
    /// 0.5, <paramref name="x"/> = 0
    /// 1.0, <paramref name="x"/> > 0
    /// </summary>
    public static TSelf UnitStepHM<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => x < TSelf.Zero ? TSelf.Zero : x > TSelf.Zero ? TSelf.One : TSelf.One.DivideByTwo();
  }
}
