namespace Flux
{
  public static partial class GenericMath
  {
    public static int UnitSign<TValue>(this TValue number)
      where TValue : System.Numerics.INumber<TValue>
      => TValue.IsNegative(number) ? -1 : 1;

    ///// <summary>Returns the value of <paramref name="x"/> with the sign of <paramref name="y"/>.</summary>
    //public static TValue CopySign<TValue, TSign>(this TValue x, TSign y)
    //  where TValue : System.Numerics.INumber<TValue>
    //  where TSign : System.Numerics.INumber<TSign>
    //  => x.UnitSign() != y.UnitSign() ? -x : x;

    ///// <summary>Returns the value of <paramref name="x"/> with the sign of <paramref name="y"/> and also in the out parameter <paramref name="result"/>.</summary>
    //public static TResult CopySign<TValue, TSign, TResult>(this TValue x, TSign y, out TResult result)
    //  where TValue : System.Numerics.INumber<TValue>
    //  where TSign : System.Numerics.INumber<TSign>
    //  where TResult : System.Numerics.INumber<TResult>
    //  => result = TResult.CreateChecked(TValue.Abs(x)) * TResult.CreateChecked(y.SignumNoZero());
  }
}
