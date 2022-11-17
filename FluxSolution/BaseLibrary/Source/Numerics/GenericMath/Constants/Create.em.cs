namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the value of <paramref name="source"/> as the type <typeparamref name="TSelf"/> and also in the out parameter <paramref name="result"/>.</summary>
    public static TSelf CreateChecked<TSelf>(this MathematicalConstant source, out TSelf result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => result = TSelf.CreateChecked(source.Value());

    /// <summary>Returns the value of <paramref name="source"/> as the type <typeparamref name="TSelf"/> and also in the out parameter <paramref name="result"/>.</summary>
    public static TSelf CreateSaturating<TSelf>(this MathematicalConstant source, out TSelf result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => result = TSelf.CreateSaturating(source.Value());

    /// <summary>Returns the value of <paramref name="source"/> as the type <typeparamref name="TSelf"/> and also in the out parameter <paramref name="result"/>.</summary>
    public static TSelf CreateTruncating<TSelf>(this MathematicalConstant source, out TSelf result)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => result = TSelf.CreateTruncating(source.Value());
  }
}
