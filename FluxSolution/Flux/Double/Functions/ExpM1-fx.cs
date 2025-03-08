namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_expm1/"/>
    public static double ExpM1(this double x)
      => double.Abs(x) < 1e-5 ? x + 0.5 * x * x : double.Exp(x) - 1;
  }
}
