namespace Flux
{
  /// <summary>An interface for approximate comparisons of two real numbers, i.e. when the equality canbe defined by some size difference.</summary>
  public interface IEqualityApproximatable<TValue>
#if NET7_0_OR_GREATER
    where TValue : System.Numerics.INumber<TValue>
#endif
  {
    /// <summary>Determines if the two values are almost or nearly equal, by some determination defined by the class.</summary>
    bool IsApproximatelyEqual(TValue a, TValue b);
  }
}
