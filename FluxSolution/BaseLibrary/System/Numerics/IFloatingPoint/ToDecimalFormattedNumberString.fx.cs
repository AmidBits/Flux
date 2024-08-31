namespace Flux
{
  public static partial class Fx
  {
    public static string ToDecimalFormattedNumberString<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.ToString("0." + new string('#', 339), null);
  }
}
