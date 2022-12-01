namespace Flux
{
  public static partial class GenericMath
  {
    public static TSelf Add<TSelf, TScalar>(this TSelf value, TScalar scalar)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TScalar : System.Numerics.INumberBase<TScalar>
      => value + TSelf.CreateChecked(scalar);

    public static TSelf Divide<TSelf, TScalar>(this TSelf value, TScalar scalar)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TScalar : System.Numerics.INumberBase<TScalar>
      => value / TSelf.CreateChecked(scalar);

    public static TSelf Multiply<TSelf, TScalar>(this TSelf value, TScalar scalar)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TScalar : System.Numerics.INumberBase<TScalar>
      => value * TSelf.CreateChecked(scalar);

    public static TSelf Subtract<TSelf, TScalar>(this TSelf value, TScalar scalar)
      where TSelf : System.Numerics.INumberBase<TSelf>
      where TScalar : System.Numerics.INumberBase<TScalar>
      => value - TSelf.CreateChecked(scalar);

    public static TSelf Pow2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => value * value;
    public static TSelf Pow3<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => value * value * value;
    public static TSelf Pow4<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => value * value * value * value;
    public static TSelf Pow5<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => value * value * value * value * value;

    public static TSelf Pow8<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => Pow2(Pow4(value));

    public static TSelf Pow10<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => Pow2(Pow5(value));

    public static TSelf Pow16<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IMultiplyOperators<TSelf, TSelf, TSelf>
      => Pow4(Pow4(value));
  }
}
