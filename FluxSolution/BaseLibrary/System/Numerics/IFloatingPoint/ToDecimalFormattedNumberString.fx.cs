namespace Flux
{
  public static partial class Fx
  {
    public static string ToDecimalFormattedNumberString<TValue>(this TValue value, int count = 339)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.ToString("0." + new string('#', count), null);
  }
}
