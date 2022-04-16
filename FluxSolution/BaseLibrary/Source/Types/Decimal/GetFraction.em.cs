namespace Flux
{
  public static partial class DecimalEm
  {
    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static decimal GetFraction(this decimal source)
      => source - System.Math.Truncate(source);
  }
}
