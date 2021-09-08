namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static double GetFraction(this double source)
      => source % 1;
  }
}
