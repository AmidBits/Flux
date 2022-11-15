//namespace Flux
//{
//  /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
//  public readonly struct EqualityByRelativeTolerance
//   : IEqualityApproximable, System.IEquatable<EqualityByRelativeTolerance>
//  {
//    private readonly double m_relativeTolerance;

//    public EqualityByRelativeTolerance(double relativeTolerance)
//      => m_relativeTolerance = relativeTolerance;
//    public EqualityByRelativeTolerance()
//      : this(1E-15)
//    {
//    }

//    public double RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

//    [System.Diagnostics.Contracts.Pure]
//    public bool IsApproximatelyEqual(double a, double b)
//      => IsApproximatelyEqual(a, b, m_relativeTolerance);

//    [System.Diagnostics.Contracts.Pure]
//    public static bool IsApproximatelyEqual(double a, double b, double relativeTolerance = 1E-15)
//       => a == b || (System.Math.Abs(a - b) <= System.Math.Max(System.Math.Abs(a), System.Math.Abs(b)) * relativeTolerance);

//    #region Overloaded operators
//    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityByRelativeTolerance a, EqualityByRelativeTolerance b) => a.Equals(b);
//    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityByRelativeTolerance a, EqualityByRelativeTolerance b) => !a.Equals(b);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable<>
//    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityByRelativeTolerance other) => m_relativeTolerance == other.m_relativeTolerance;
//    #endregion Implemented interfaces

//    #region Object overrides
//    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityByRelativeTolerance o && Equals(o);
//    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_relativeTolerance);
//    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ RelativeTolerance = {m_relativeTolerance} }}";
//    #endregion Object overrides}
//  }
//}
