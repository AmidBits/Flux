#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf Div2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value / (TSelf.One + TSelf.One);

    public static TSelf Mul2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value * (TSelf.One + TSelf.One);

    public static TSelf Pow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value * value;
  }
}
#endif
