#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
  /// <param name="v0">Pre-source point.</param>
  /// <param name="v1">Source point.</param>
  /// <param name="v2">Target point.</param>
  /// <param name="v3">Post-target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
  /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct HermiteInterpolation<T>
    : IInterpolatable<T>, System.IEquatable<HermiteInterpolation<T>>
    where T : System.Numerics.INumber<T>
  {
    private readonly T m_v0, m_v1, m_v2, m_v3;

    private readonly T m_bias, m_tension;

    public HermiteInterpolation(T v0, T v1, T v2, T v3, T bias, T tension)
    {
      m_v0 = v0;
      m_v1 = v1;
      m_v2 = v2;
      m_v3 = v3;

      m_bias = bias;
      m_tension = tension;
    }
    public HermiteInterpolation(T v0, T v1, T v2, T v3)
      : this(v0, v1, v2, v3, T.Zero, T.Zero)
    {
    }

    public T V0 { get => m_v0; init => m_v0 = value; }
    public T V1 { get => m_v1; init => m_v1 = value; }
    public T V2 { get => m_v2; init => m_v2 = value; }
    public T V3 { get => m_v3; init => m_v3 = value; }

    public T Bias { get => m_bias; init => m_bias = value; }
    public T Tension { get => m_tension; init => m_tension = value; }

    [System.Diagnostics.Contracts.Pure]
    public T GetInterpolation(T mu)
      => Interpolate(m_v0, m_v1, m_v2, m_v3, mu, m_tension, m_bias);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static T Interpolate(T v0, T v1, T v2, T v3, T mu, T tension, T bias)
    {
      var one = T.One;
      var two = one + one;
      var three = two + one;

      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var biasP = (one + bias) * (one - tension);
      var biasM = (one - bias) * (one - tension);

      var m0 = (v1 - v0) * biasP / two + (v2 - v1) * biasM / two;
      var m1 = (v2 - v1) * biasP / two + (v3 - v2) * biasM / two;

      var a0 = (two * mu3 - three * mu2 + one);
      var a1 = (mu3 - two * mu2 + mu);
      var a2 = (mu3 - mu2);
      var a3 = (-two * mu3 + three * mu2);

      return a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2;
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(HermiteInterpolation<T> a, HermiteInterpolation<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(HermiteInterpolation<T> a, HermiteInterpolation<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(HermiteInterpolation<T> other) => m_v0 == other.m_v0 && m_v1 == other.m_v1 && m_v2 == other.m_v2 && m_v3 == other.m_v3 && m_bias == other.m_bias && m_tension == other.m_tension;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is HermiteInterpolation<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_v0, m_v1, m_v2, m_v3, m_bias, m_tension);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ V0 = {m_v0}, V1 = {m_v1}, V2 = {m_v2}, V3 = {m_v3}, Bias = {m_bias}, Tension = {m_tension} }}";
    #endregion Object overrides
  }
}
#endif
