namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static TNumber TruncMod<TNumber>(this TNumber dividend, TNumber divisor, out TNumber remainder)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }
  }
}
