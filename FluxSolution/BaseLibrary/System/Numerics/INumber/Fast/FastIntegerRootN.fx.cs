namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <param name="number"></param>
    /// <param name="nth"></param>
    /// <param name="mode"></param>
    /// <param name="root"></param>
    /// <returns>The resulting integer-nth-root.</returns>
    public static TNumber FastIntegerRootN<TNumber, TNth>(this TNumber number, TNth nth, UniversalRounding mode, out double root)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      checked
      {
        root = double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth));

        return TNumber.CopySign(TNumber.CreateChecked(root.RoundUniversal(mode)), number);
      }
    }
  }
}
