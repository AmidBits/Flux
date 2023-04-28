namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the value of <paramref name="number"/> (<typeparamref name="TSelf"/>) as type <typeparamref name="TResult"/>. The result is also returned out in the parameter <paramref name="result"/>.</summary>
    public static TResult ToType<TSelf, TResult>(this TSelf number, out TResult result)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = TResult.CreateChecked(number);

    /// <summary>Returns the value of <paramref name="number"/> as type <typeparamref name="TResult"/> rounded using the specified <paramref name="mode"/>. The return value is also output in the parameter <paramref name="result"/>.</summary>
    public static TResult ToType<TSelf, TResult>(this TSelf number, out TResult result, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TResult : System.Numerics.INumber<TResult>
      => result = TResult.CreateChecked(number.Round(mode));

#endif
  }
}
