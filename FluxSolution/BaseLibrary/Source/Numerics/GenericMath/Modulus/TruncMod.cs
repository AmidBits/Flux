namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the integer (i.e. truncated) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }
  }
}
