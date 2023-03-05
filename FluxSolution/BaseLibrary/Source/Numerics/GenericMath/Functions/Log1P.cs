namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_log_one_plus_x/"/>
    public static TSelf Log1P<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
    {
      if (x <= TSelf.NegativeOne) throw new ArgumentOutOfRangeException(nameof(x));

      if (TSelf.Abs(x) > TSelf.CreateChecked(1e-4))
        return TSelf.Log(TSelf.One + x);

      // Use Taylor approx. log(1 + x) = x - x^2/2 with error roughly x^3/3
      // Since |x| < 10^-4, |x|^3 < 10^-12, relative error less than 10^-8

      return (TSelf.NegativeOne.Divide(2) * x + TSelf.One) * x;
    }
  }
}
