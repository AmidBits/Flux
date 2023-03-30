namespace Flux
{
  public static partial class ApproximateEqualityExtensionMethods
  {
    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool IsApproximatelyEqualRelative<TValue>(this TValue a, TValue b, double percentTolerance)
      where TValue : System.Numerics.INumber<TValue>
      => ApproximateEquality.ByRelativeTolerance<TValue>.IsApproximatelyEqual(a, b, percentTolerance);
  }

  namespace ApproximateEquality
  {
    /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public record class ByRelativeTolerance<TValue>
      : IEqualityApproximatable<TValue>
      where TValue : System.Numerics.INumber<TValue>
    {
      private readonly double m_relativeTolerance;

      public ByRelativeTolerance(double relativeTolerance) => m_relativeTolerance = relativeTolerance;

      /// <summary>The relative tolerance, i.e. tolerance as a percentage, a proportional property.</summary>
      public double RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

      /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
      public static bool IsApproximatelyEqual(TValue a, TValue b, double relativeTolerance) => a == b || (double.CreateChecked(TValue.Abs(a - b)) <= double.CreateChecked(TValue.Max(TValue.Abs(a), TValue.Abs(b))) * relativeTolerance);

      public bool IsApproximatelyEqual(TValue a, TValue b) => IsApproximatelyEqual(a, b, m_relativeTolerance);
    }
  }
}
