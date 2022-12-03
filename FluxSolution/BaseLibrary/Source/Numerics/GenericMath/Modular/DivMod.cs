namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> values.</remarks>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }
  }
}
