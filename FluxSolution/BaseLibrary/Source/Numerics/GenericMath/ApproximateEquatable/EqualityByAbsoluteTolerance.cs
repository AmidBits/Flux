#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
  public record class EqualityByAbsoluteTolerance<TValue, TTolerance>
    : IEqualityApproximatable<TValue>
    where TValue : System.Numerics.INumber<TValue>
    where TTolerance : System.Numerics.INumber<TTolerance>, System.Numerics.IComparisonOperators<TTolerance, TValue, bool>
  {
    private TTolerance m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(TTolerance absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;

    public TTolerance AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(TValue a, TValue b)
      => a == b
      || (m_absoluteTolerance > TValue.Abs(a - b));
  }
}
#endif
