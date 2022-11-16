namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="x"/> (<typeparamref name="TSelf"/>) as type <typeparamref name="TType"/> and also in the out parameter <see cref="result"/>.</summary>
    public static TType ToType<TSelf, TType>(this TSelf x, out TType result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TType : System.Numerics.INumber<TType>
      => result = TType.CreateChecked(x);
  }
}
