namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Implementation see reference.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_phi/"/>
    public static double Phi(this double x)
    {
      var a1 = 0.254829592;
      var a2 = -0.284496736;
      var a3 = 1.421413741;
      var a4 = -1.453152027;
      var a5 = 1.061405429;

      var p = 0.3275911;

      var absx = double.Abs(x) / double.Sqrt(2);

      // A&S formula 7.1.26
      var t = 1 / (1 + p * absx);
      var y = 1 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * double.Exp(-absx * absx);

      return 0.5 * (1 + double.CopySign(y, x));
    }
  }
}
