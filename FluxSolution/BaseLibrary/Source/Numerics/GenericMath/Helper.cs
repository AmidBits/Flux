namespace Flux
{
  public static partial class GenericMath
  {
    //public static TSelf Div2<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.INumberBase<TSelf>
    //  => value / (TSelf.One + TSelf.One);
    //public static TSelf Div3<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.INumberBase<TSelf>
    //  => value / (TSelf.One + TSelf.One + TSelf.One);

    public static TResult Div<TSelf, TScalar, TResult>(this TSelf value, TScalar scalar, out TResult result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TScalar : System.Numerics.INumber<TScalar>
      where TResult : System.Numerics.INumber<TResult>
      => result = TResult.CreateChecked(value) / TResult.CreateChecked(scalar);
    public static TSelf Div2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => Div(value, 2, out TSelf _);

    public static TResult Mul<TSelf, TScalar, TResult>(this TSelf value, TScalar scalar, out TResult result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TScalar : System.Numerics.INumber<TScalar>
      where TResult : System.Numerics.INumber<TResult>
      => result = TResult.CreateChecked(value) * TResult.CreateChecked(scalar);

    public static TSelf Mul2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => Mul(value, 2, out TSelf _);

    //public static TSelf Mul2<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.INumberBase<TSelf>
    //  => value * (TSelf.One + TSelf.One);

    public static TSelf Pow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value * value;
    public static TSelf Pow8<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => Pow2(Pow2(Pow2(value)));
    public static TSelf Pow10<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => Pow2(value * Pow2(Pow2(value)));
    public static TSelf Pow16<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => Pow2(Pow8(value));
  }
}
