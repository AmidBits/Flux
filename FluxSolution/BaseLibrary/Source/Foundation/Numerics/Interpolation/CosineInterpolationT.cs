#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
  /// <param name="v1">Source point.</param>
  /// <param name="v2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct CosineInterpolation<T>
    : IInterpolatable<T>, System.IEquatable<CosineInterpolation<T>>
    where T : System.Numerics.INumber<T>, System.Numerics.ITrigonometricFunctions<T>
  {
    private readonly T m_v1, m_v2;

    public CosineInterpolation(T v1, T v2)
    {
      m_v1 = v1;
      m_v2 = v2;
    }

    public T V1 { get => m_v1; init => m_v1 = value; }
    public T V2 { get => m_v2; init => m_v2 = value; }

    [System.Diagnostics.Contracts.Pure]
    public T GetInterpolation(T mu)
      => Interpolate(m_v1, m_v2, mu);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static T Interpolate(T v1, T v2, T mu)
    {
      var mu2 = (T.One - T.CosPi(mu)) / (T.One + T.One);

      return v1 * (T.One - mu2) + v2 * mu2;
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CosineInterpolation<T> a, CosineInterpolation<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CosineInterpolation<T> a, CosineInterpolation<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(CosineInterpolation<T> other) => m_v1 == other.m_v1 && m_v2 == other.m_v2;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CosineInterpolation<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_v1, m_v2);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ V1 = {m_v1}, V2 = {m_v2} }}";
    #endregion Object overrides
  }
}
#endif
