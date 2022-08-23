#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Perform a comparison where the tolerance is the same, no matter how small or large the compared numbers.</summary>
  public readonly struct EqualityByAbsoluteTolerance<T>
    : IEqualityApproximable<T>, System.IEquatable<EqualityByAbsoluteTolerance<T>>
    where T : System.Numerics.INumber<T>
  {
    private readonly T m_absoluteTolerance;

    public EqualityByAbsoluteTolerance(T absoluteTolerance)
      => m_absoluteTolerance = absoluteTolerance;

    public T AbsoluteTolerance { get => m_absoluteTolerance; init => m_absoluteTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(T a, T b)
      => IsApproximatelyEqual(a, b, m_absoluteTolerance);

    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(T a, T b, T absoluteTolerance)
      => a == b || (T.Abs(a - b) <= absoluteTolerance);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityByAbsoluteTolerance<T> a, EqualityByAbsoluteTolerance<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityByAbsoluteTolerance<T> a, EqualityByAbsoluteTolerance<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityByAbsoluteTolerance<T> other) => m_absoluteTolerance == other.m_absoluteTolerance;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityByAbsoluteTolerance<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_absoluteTolerance);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ AbsoluteTolerance = {m_absoluteTolerance} }}";
    #endregion Object overrides
  }
}
#endif
