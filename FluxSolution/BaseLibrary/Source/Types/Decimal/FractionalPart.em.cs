namespace Flux
{
  public static partial class SystemDecimalEm
  {
    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static decimal FractionalPart(this decimal source)
      => source - System.Math.Truncate(source);
  }
}
