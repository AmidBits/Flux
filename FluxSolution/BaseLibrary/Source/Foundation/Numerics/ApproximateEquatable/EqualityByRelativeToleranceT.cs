#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Perform a comparison where a tolerance relative to the size of the compared numbers, i.e. a percentage of tolerance.</summary>
  public readonly struct EqualityByRelativeTolerance<T>
   : IEqualityApproximable<T>, System.IEquatable<EqualityByRelativeTolerance<T>>
    where T : System.Numerics.INumber<T>
  {
    private readonly T m_relativeTolerance;

    public EqualityByRelativeTolerance(T relativeTolerance)
      => m_relativeTolerance = relativeTolerance;

    public T RelativeTolerance { get => m_relativeTolerance; init => m_relativeTolerance = value; }

    [System.Diagnostics.Contracts.Pure]
    public bool IsApproximatelyEqual(T a, T b)
      => IsApproximatelyEqual(a, b, m_relativeTolerance);

    [System.Diagnostics.Contracts.Pure]
    public static bool IsApproximatelyEqual(T a, T b, T relativeTolerance)
       => a == b || (T.Abs(a - b) <= T.Max(T.Abs(a), T.Abs(b)) * relativeTolerance);

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(EqualityByRelativeTolerance<T> a, EqualityByRelativeTolerance<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(EqualityByRelativeTolerance<T> a, EqualityByRelativeTolerance<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(EqualityByRelativeTolerance<T> other) => m_relativeTolerance == other.m_relativeTolerance;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is EqualityByRelativeTolerance<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_relativeTolerance);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ RelativeTolerance = {m_relativeTolerance} }}";
    #endregion Object overrides}
  }
}
#endif
