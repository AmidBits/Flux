#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns whether <paramref name="number"/> is a perfect square of <paramref name="root"/>.</summary>
    public static bool IsPerfectSquare<TSelf>(this TSelf number, TSelf root)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => number / root == root && number % root == TSelf.Zero;
  }
}
#endif
