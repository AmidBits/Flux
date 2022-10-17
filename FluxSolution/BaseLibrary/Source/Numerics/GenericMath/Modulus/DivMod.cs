#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.IDivisionOperators<TSelf, TSelf, TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }
  }
}
#endif
