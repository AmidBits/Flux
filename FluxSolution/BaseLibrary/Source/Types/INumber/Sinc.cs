#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the normalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincn<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IFloatingPointIeee754<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => Sincu(TSelf.Pi * x);

    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincu<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => x != TSelf.Zero ? TSelf.Sin(x) / x : TSelf.One;
  }
}
#endif
