namespace Flux
{
  /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
  public sealed class EqualityByAbsoluteTolerance
    : IEqualityApproximatable
  {
    private readonly double m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(double absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;
    public EqualityByAbsoluteTolerance()
      : this(1E-15)
    {
    }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(double a, double b)
      => IsApproximatelyEqual(a, b, m_absoluteTolerance);

    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(double a, double b, double absoluteTolerance = 1E-15)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);
  }
}
