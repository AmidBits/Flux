namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Compute log(1+x) without losing precision for small values of x.</summary>
    /// <see href="https://www.johndcook.com/blog/stand_alone_code/"/>
    /// <see href="https://www.johndcook.com/blog/csharp_expm1/"/>
    public static TSelf ExpM1<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.Abs(x) < TSelf.CreateChecked(1e-5) ? x + TSelf.One.Divide(2) * x * x : TSelf.Exp(x) - TSelf.One;
  }
}
