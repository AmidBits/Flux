#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
    public static bool IsApproximatelyEqualRelative<TValue, TTolerance>(this TValue a, TValue b, TTolerance percentTolerance)
      where TValue : System.Numerics.INumber<TValue>
      where TTolerance : System.Numerics.IFloatingPoint<TTolerance>, System.Numerics.IComparisonOperators<TTolerance, TValue, bool>, System.Numerics.IMultiplyOperators<TTolerance, TValue, TTolerance>
      => a == b
      || percentTolerance * TValue.Max(TValue.Abs(a), TValue.Abs(b)) > TValue.Abs(a - b);
  }

  /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
  public record class EqualityByRelativeTolerance<TValue, TTolerance>
   : IEqualityApproximatable<TValue>
    where TValue : System.Numerics.INumber<TValue>
    where TTolerance : System.Numerics.IFloatingPoint<TTolerance>, System.Numerics.IComparisonOperators<TTolerance, TValue, bool>, System.Numerics.IMultiplyOperators<TTolerance, TValue, TTolerance>
  {
    private readonly TTolerance m_relativeTolerance;

    public EqualityByRelativeTolerance(TTolerance relativeTolerance)
      => m_relativeTolerance = relativeTolerance;

    public TTolerance RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(TValue a, TValue b)
      => a == b
      || (m_relativeTolerance * TValue.Max(TValue.Abs(a), TValue.Abs(b)) > TValue.Abs(a - b));
  }
}
#endif
