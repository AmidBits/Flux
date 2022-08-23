#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
  /// <param name="v1">Source point.</param>
  /// <param name="v2">Target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct LinearInterpolation<T>
    : IInterpolatable<T>, System.IEquatable<LinearInterpolation<T>>
    where T : System.Numerics.INumber<T>
  {
    private readonly T m_v1, m_v2;

    public LinearInterpolation(T v1, T v2)
    {
      m_v1 = v1;
      m_v2 = v2;
    }

    public T V1 { get => m_v1; init => m_v1 = value; }
    public T V2 { get => m_v2; init => m_v2 = value; }

    [System.Diagnostics.Contracts.Pure]
    public T GetInterpolation(T mu)
      => Interpolate(m_v1, m_v2, mu);

    [System.Diagnostics.Contracts.Pure]
    public static T ImputeUnit(T v1, T v2, T v12)
      => T.One / (v2 - v1) * (v12 - v1);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static T Interpolate(T v1, T v2, T mu)
      => v1 * (T.One - mu) + v2 * mu;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(LinearInterpolation<T> a, LinearInterpolation<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(LinearInterpolation<T> a, LinearInterpolation<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(LinearInterpolation<T> other) => m_v1 == other.m_v1 && m_v2 == other.m_v2;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is LinearInterpolation<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_v1, m_v2);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ V1 = {m_v1}, V2 = {m_v2} }}";
    #endregion Object overrides
  }
}
#endif
