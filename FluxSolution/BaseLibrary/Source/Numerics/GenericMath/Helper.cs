#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class NumberBase
  {
    public static TSelf Div2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => value / (TSelf.One + TSelf.One);

    public static TSelf Mul2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => value * (TSelf.One + TSelf.One);

    public static TSelf Pow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => value * value;
    public static TSelf Pow8<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => value * value * value * value * value * value * value * value;
    public static TSelf Pow10<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => Pow2(value) * Pow8(value);
    public static TSelf Pow16<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => Pow2(Pow8(value));
  }
}
#endif
