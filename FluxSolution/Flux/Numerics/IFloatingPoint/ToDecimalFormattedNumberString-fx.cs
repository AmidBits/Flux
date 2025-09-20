namespace Flux
{
  public static partial class FloatingPoint
  {
    public static string ToDecimalFormattedNumberString<TFloat>(this TFloat value, int decimalCount = 339)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => value.ToString("0." + new string('#', decimalCount), null);
  }
}
