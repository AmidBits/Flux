namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool IsApproximatelyEqualAbsolute<TSelf>(this TSelf a, TSelf b, TSelf absoluteTolerance)
      where TSelf : System.Numerics.INumber<TSelf>
      => new ApproximateEquality.ApproximateEqualityByAbsoluteTolerance<TSelf>(absoluteTolerance).IsApproximatelyEqual(a, b);
  }

  namespace ApproximateEquality
  {
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public record class ApproximateEqualityByAbsoluteTolerance<TSelf>
      : IEqualityApproximatable<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      private readonly TSelf m_absoluteTolerance;

      public ApproximateEqualityByAbsoluteTolerance(TSelf absoluteTolerance)
        => m_absoluteTolerance = absoluteTolerance;

      /// <summary>The absolute tolerance, i.e. the tolerance in a non-relative term, the tolerance is fixed, not proportional.</summary>
      public TSelf AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

      #region Static methods
      public static bool IsApproximatelyEqual(TSelf a, TSelf b, TSelf absoluteTolerance)
        => a == b || (TSelf.Abs(a - b) <= absoluteTolerance);
      #endregion Static methods

      #region Implemented interfaces
      public bool IsApproximatelyEqual(TSelf a, TSelf b)
        => IsApproximatelyEqual(a, b, m_absoluteTolerance);
      #endregion Implemented interfaces
    }
  }
}
