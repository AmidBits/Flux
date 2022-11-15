//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Implementation see reference.</summary>
//    /// <see cref="https://www.johndcook.com/blog/2009/01/19/stand-alone-error-function-erf/"/>
//    public static double Erf(double x)
//    {
//      const double a1 = 0.254829592, a2 = -0.284496736, a3 = 1.421413741, a4 = -1.453152027, a5 = 1.061405429;
//      const double p = 0.3275911;

//      var absoluteOfX = System.Math.Abs(x); // Store the absolute value of x.

//      // A&S formula 7.1.26
//      var t = 1.0 / (1.0 + p * absoluteOfX);
//      var y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * System.Math.Exp(-absoluteOfX * absoluteOfX);

//      return System.Math.CopySign(y, x);
//    }
//  }
//}
