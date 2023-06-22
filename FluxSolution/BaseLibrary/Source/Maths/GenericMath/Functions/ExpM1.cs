namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_expm1/"/>
    public static double ExpM1(this double x)
      => System.Math.Abs(x) < 1e-5 ? x + 0.5 * x * x : System.Math.Exp(x) - 1;
  }
}
