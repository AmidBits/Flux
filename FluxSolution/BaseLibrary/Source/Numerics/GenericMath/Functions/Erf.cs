namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Implementation see reference.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/2009/01/19/stand-alone-error-function-erf/"/>
    public static TSelf Erf<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    {
      var a1 = TSelf.CreateChecked(0.254829592);
      var a2 = TSelf.CreateChecked(-0.284496736);
      var a3 = TSelf.CreateChecked(1.421413741);
      var a4 = TSelf.CreateChecked(-1.453152027);
      var a5 = TSelf.CreateChecked(1.061405429);

      var p = TSelf.CreateChecked(0.3275911);

      var absx = TSelf.Abs(x);

      // A&S formula 7.1.26
      var t = TSelf.One / (TSelf.One + p * absx);
      var y = TSelf.One - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * TSelf.Exp(-absx * absx);

      return TSelf.CopySign(y, x);
    }
  }
}
