namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }
  }
}
