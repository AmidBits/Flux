#if NET7_0_OR_GREATER
namespace Flux
{
  /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
  /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
  /// <param name="v0">Pre-source point.</param>
  /// <param name="v1">Source point.</param>
  /// <param name="v2">Target point.</param>
  /// <param name="v3">Post-target point.</param>
  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  public readonly struct CubicPbInterpolation<T>
    : IInterpolatable<T>, System.IEquatable<CubicPbInterpolation<T>>
    where T : System.Numerics.INumber<T>, System.Numerics.ITrigonometricFunctions<T>
  {
    private readonly T m_v0, m_v1, m_v2, m_v3;

    public CubicPbInterpolation(T v0, T v1, T v2, T v3)
    {
      m_v0 = v0;
      m_v1 = v1;
      m_v2 = v2;
      m_v3 = v3;
    }

    public T V0 { get => m_v0; init => m_v0 = value; }
    public T V1 { get => m_v1; init => m_v1 = value; }
    public T V2 { get => m_v2; init => m_v2 = value; }
    public T V3 { get => m_v3; init => m_v3 = value; }

    [System.Diagnostics.Contracts.Pure]
    public T GetInterpolation(T mu)
      => Interpolate(m_v0, m_v1, m_v2, m_v3, mu);

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static T Interpolate(T v0, T v1, T v2, T v3, T mu)
    {
      var two = T.One + T.One;
      var halfOfOne = T.One / two;
      var oneAndHalf = two - halfOfOne;
      var twoAndHalf = two + halfOfOne;

      var mu2 = mu * mu;

      var a0 = -halfOfOne * v0 + oneAndHalf * v1 - oneAndHalf * v2 + halfOfOne * v3;
      var a1 = v0 - twoAndHalf * v1 + two * v2 - halfOfOne * v3;
      var a2 = -halfOfOne * v0 + halfOfOne * v2;
      var a3 = v1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(CubicPbInterpolation<T> a, CubicPbInterpolation<T> b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(CubicPbInterpolation<T> a, CubicPbInterpolation<T> b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(CubicPbInterpolation<T> other) => m_v0 == other.m_v0 && m_v1 == other.m_v1 && m_v2 == other.m_v2 && m_v3 == other.m_v3;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? obj) => obj is CubicPbInterpolation<T> o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_v0, m_v1, m_v2, m_v3);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ V0 = {m_v0}, V1 = {m_v1}, V2 = {m_v2}, V3 = {m_v3} }}";
    #endregion Object overrides
  }

  //public static partial class Maths
  //{
  //  /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
  //  /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
  //  /// <param name="v0">Pre-source point.</param>
  //  /// <param name="v1">Source point.</param>
  //  /// <param name="v2">Target point.</param>
  //  /// <param name="v3">Post-target point.</param>
  //  /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
  //  /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
  //  public static double InterpolateCubicPB(double v0, double v1, double v2, double v3, double mu)
  //  {
  //    var mu2 = mu * mu;

  //    var a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
  //    var a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
  //    var a2 = -0.5 * v0 + 0.5 * v2;
  //    var a3 = v1;

  //    return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
  //  }
  //}
}
#endif
