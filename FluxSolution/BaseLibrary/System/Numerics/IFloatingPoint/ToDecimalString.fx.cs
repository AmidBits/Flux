namespace Flux
{
  public static partial class Fx
  {
    public static string ToDecimalString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => value.ToString("0." + new string('#', 339), null);
  }
}
