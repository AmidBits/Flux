namespace Flux
{
  public static partial class Fx
  {
    public static string ToDecimalFormattedNumberString<TNumber>(this TNumber value, int decimalCount = 339)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>
      => value.ToString("0." + new string('#', decimalCount), null);
  }
}
