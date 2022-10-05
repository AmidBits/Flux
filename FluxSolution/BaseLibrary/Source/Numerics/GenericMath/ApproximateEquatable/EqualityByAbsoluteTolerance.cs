#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool IsApproximatelyEqualAbsolute<TSelf>(this TSelf a, TSelf b, TSelf absoluteTolerance)
      where TSelf : System.Numerics.INumber<TSelf>
      => new EqualityByAbsoluteTolerance<TSelf>(absoluteTolerance).IsApproximatelyEqual(a, b);
  }

  /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
  public record class EqualityByAbsoluteTolerance<TSelf>
    : IEqualityApproximatable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private TSelf m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(TSelf absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;

    public TSelf AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(TSelf a, TSelf b)
      => a == b
      || (m_absoluteTolerance > TSelf.Abs(a - b));
  }
}
#endif
