namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_log_one_plus_x/"/>
    public static double Log1P(this double x)
    {
      if (x <= -1) throw new ArgumentOutOfRangeException(nameof(x));

      if (System.Math.Abs(x) > 1e-4)
        return System.Math.Log(1 + x);

      // Use Taylor approx. log(1 + x) = x - x^2/2 with error roughly x^3/3
      // Since |x| < 10^-4, |x|^3 < 10^-12, relative error less than 10^-8

      return (-0.5 * x + 1) * x;
    }
  }
}
