namespace Flux
{
  public static partial class XtensionsDecimal
  {
    /// <summary>Strips the fractional part of the floating point value, resulting in only the integer part.</summary>
    public static decimal IntegerPart(this decimal source)
      => System.Math.Truncate(source);
  }
}
