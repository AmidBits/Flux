namespace Flux
{
  /// <summary>An interface for approximate comparisons of two real numbers, i.e. when the equality canbe defined by some size difference.</summary>
  public interface IEqualityApproximable
  {
    /// <summary>Determines if the two values are almost or nearly equal, by some determination defined by the class.</summary>
    bool IsApproximatelyEqual(double a, double b);
  }
}
