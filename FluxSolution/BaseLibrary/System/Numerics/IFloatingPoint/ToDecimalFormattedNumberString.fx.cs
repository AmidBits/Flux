namespace Flux
{
  public static partial class Fx
  {
    public static string ToDecimalFormattedNumberString<TNumber>(this TNumber number)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>
      => number.ToString("0." + new string('#', 339), null);
  }
}
