namespace Flux
{
  public static partial class ApproximateEqualityExtensionMethods
  {
    /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
    public static bool IsApproximatelyEqualAbsolute<TSelf>(this TSelf a, TSelf b, TSelf absoluteTolerance)
      where TSelf : System.Numerics.INumber<TSelf>
      => ApproximateEquality.ByAbsoluteTolerance<TSelf>.IsApproximatelyEqual(a, b, absoluteTolerance);
  }

  namespace ApproximateEquality
  {
    /// <summary>Perform a comparison where the tolerance is fixed, regardless of how small or large the numbers being compared.</summary>
    public record class ByAbsoluteTolerance<TValue>
      : IEqualityApproximatable<TValue>
      where TValue : System.Numerics.INumber<TValue>
    {
      private readonly TValue m_absoluteTolerance;

      public ByAbsoluteTolerance(TValue absoluteTolerance) => m_absoluteTolerance = absoluteTolerance;

      /// <summary>The absolute tolerance, i.e. the tolerance in a non-relative term, the tolerance is fixed, not proportional.</summary>
      public TValue AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

      /// <summary>Perform a comparison where the tolerance is fixed, regardless of how small or large the numbers being compared.</summary>
      public static bool IsApproximatelyEqual(TValue a, TValue b, TValue absoluteTolerance) => a == b || (TValue.Abs(a - b) <= absoluteTolerance);

      public bool IsApproximatelyEqual(TValue a, TValue b) => IsApproximatelyEqual(a, b, m_absoluteTolerance);
    }
  }
}
