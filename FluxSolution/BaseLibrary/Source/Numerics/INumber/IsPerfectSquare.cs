#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the integer (floor for floating point) quotient and also returns the remainder as an output parameter.</summary>
    public static bool IsPerfectSquare<TSelf>(this TSelf self)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TryISqrt(self, out var _);
  }
}
#endif
