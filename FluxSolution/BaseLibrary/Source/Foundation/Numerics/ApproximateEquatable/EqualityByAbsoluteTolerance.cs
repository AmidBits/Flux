namespace Flux
{
  /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
  public readonly struct EqualityByAbsoluteTolerance
    : IEqualityApproximatable, System.IEquatable<EqualityByAbsoluteTolerance>
  {
    private readonly double m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(double absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;
    public EqualityByAbsoluteTolerance()
      : this(1E-15)
    {
    }

    public double AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(double a, double b)
      => IsApproximatelyEqual(a, b, m_absoluteTolerance);

    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(double a, double b, double absoluteTolerance = 1E-15)
      => a == b || (System.Math.Abs(a - b) <= absoluteTolerance);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityByAbsoluteTolerance a, EqualityByAbsoluteTolerance b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityByAbsoluteTolerance a, EqualityByAbsoluteTolerance b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityByAbsoluteTolerance other) => m_absoluteTolerance == other.m_absoluteTolerance;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityByAbsoluteTolerance o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_absoluteTolerance);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ AbsoluteTolerance = {m_absoluteTolerance} }}";
    #endregion Object overrides
  }
}
