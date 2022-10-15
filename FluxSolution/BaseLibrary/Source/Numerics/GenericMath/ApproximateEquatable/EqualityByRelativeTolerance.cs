#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool IsApproximatelyEqualRelative<TSelf>(this TSelf a, TSelf b, TSelf percentTolerance)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new EqualityByRelativeTolerance<TSelf>(percentTolerance).IsApproximatelyEqual(a, b);
  }

  namespace Equality
  {
    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public record class EqualityByRelativeTolerance<TSelf>
      : IEqualityApproximatable<TSelf>
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      private readonly TSelf m_relativeTolerance;

      public EqualityByRelativeTolerance(TSelf relativeTolerance)
        => m_relativeTolerance = relativeTolerance;

      public TSelf RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

      [System.Diagnostics.Contracts.Pure]
      public bool IsApproximatelyEqual(TSelf a, TSelf b)
        => a == b
        || (m_relativeTolerance * TSelf.Max(TSelf.Abs(a), TSelf.Abs(b)) > TSelf.Abs(a - b));
    }
  }
}
#endif
