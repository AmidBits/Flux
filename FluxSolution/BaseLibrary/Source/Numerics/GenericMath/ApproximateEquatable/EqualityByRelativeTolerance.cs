#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool IsApproximatelyEqualRelative<TSelf>(this TSelf a, TSelf b, TSelf percentTolerance)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => new Equality.EqualityByRelativeTolerance<TSelf>(percentTolerance).IsApproximatelyEqual(a, b);
  }

  namespace Equality
  {
    /// <summary>PREVIEW! Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public record class EqualityByRelativeTolerance<TSelf>
      : IEqualityApproximatable<TSelf>
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      private readonly TSelf m_relativeTolerance;

      public EqualityByRelativeTolerance(TSelf relativeTolerance)
        => m_relativeTolerance = relativeTolerance;

      /// <summary>The relative tolerance, i.e. tolerance as a percentage, a proportional property.</summary>
      public TSelf RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

      #region Static methods
      public static bool IsApproximatelyEqual(TSelf a, TSelf b, TSelf relativeTolerance)
       => a == b || (TSelf.Abs(a - b) <= TSelf.Max(TSelf.Abs(a), TSelf.Abs(b)) * relativeTolerance);
      #endregion Static methods

      #region Implemented interfaces
      public bool IsApproximatelyEqual(TSelf a, TSelf b)
        => IsApproximatelyEqual(a, b, m_relativeTolerance);
      #endregion Implemented interfaces
    }
  }
}
#endif
