namespace Flux
{
  public static partial class Fx
  {

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="mode"></param>
    /// <param name="sqrt"></param>
    /// <returns>The resulting integer-sqrt.</returns>
    public static TNumber FastIntegerSqrt<TNumber>(this TNumber number, UniversalRounding mode, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      checked
      {
        sqrt = double.Sqrt(double.CreateChecked(TNumber.Abs(number)));

        return TNumber.CopySign(TNumber.CreateChecked(sqrt.RoundUniversal(mode)), number);
      }
    }
  }
}
