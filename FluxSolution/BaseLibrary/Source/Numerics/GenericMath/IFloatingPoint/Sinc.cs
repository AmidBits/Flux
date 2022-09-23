#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class FloatingPoint
  {
    /// <summary>PREVIEW! Returns the normalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincn<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => Sincu(TSelf.Pi * x);

    /// <summary>PREVIEW! Returns the unnormalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincu<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => x != TSelf.Zero ? TSelf.Sin(x) / x : TSelf.One;
  }
}
#endif
