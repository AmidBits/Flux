#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>An interface for approximate comparisons of two real numbers, i.e. when the equality canbe defined by some size difference.</summary>
  public interface IEqualityApproximatable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    /// <summary>Determines if the two values are almost or nearly equal, by some determination defined by the class.</summary>
    bool IsApproximatelyEqual(TSelf a, TSelf b);
  }
}
#endif