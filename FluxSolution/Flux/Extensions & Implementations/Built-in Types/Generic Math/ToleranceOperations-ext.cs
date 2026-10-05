namespace Flux
{
  public static partial class FloatingPoint
  {
    public const double DefaultToleranceFactor = 11.0;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region EffectiveTolerance

      /// <summary>
      /// <para>Computes an effective tolerance for a floating-point value by returning the larger of a scaled machine epsilon and a scaled unit-in-the-last-place (ULP).</para>
      /// </summary>
      /// <remarks>Throws ArgumentOutOfRangeException if toleranceFactor is less than or equal to zero. Uses RelativeEpsilon() for an local spacing component and Number.Ulp(value) for a precision  component.</remarks>
      /// <param name="scale">The floating-point value for which to compute the tolerance.</param>
      /// <param name="toleranceFactor">Factor used to scale both relative epsilon and ULP; must be >= 1.</param>
      /// <returns>The maximum of toleranceFactor * RelativeEpsilon() and toleranceFactor * Ulp(value) where toleranceFactor > 0.</returns>
      public static TFloat EffectiveTolerance(TFloat scale, TFloat toleranceFactor)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toleranceFactor);

        var minimumPrecision = RelativeEpsilon<TFloat>();
        var localSpacing = Number.Ulp(scale);

        return toleranceFactor * TFloat.Max(minimumPrecision, localSpacing);
      }

      #endregion

      #region IsWithinTolerance

      /// <summary>
      /// <para>Determines whether value is within a scale-dependent tolerance of target.</para>
      /// </summary>
      /// <remarks>Tolerance is computed as EffectiveTolerance(Max(|value|, |target|), toleranceFactor) and applied to the absolute difference between the values.</remarks>
      /// <param name="value">Value to compare with the target.</param>
      /// <param name="target">Target value for comparison.</param>
      /// <param name="toleranceFactor">Relative factor used to compute an effective tolerance based on the larger magnitude of value and target.</param>
      /// <returns>True if the absolute difference between value and target is less than or equal to the computed tolerance; otherwise, false.</returns>
      public static bool IsWithinTolerance(TFloat value, TFloat target, TFloat toleranceFactor)
      {
        var scale = TFloat.Max(TFloat.Abs(value), TFloat.Abs(target));

        var tolerance = EffectiveTolerance(scale, toleranceFactor);

        return TFloat.Abs(value - target) <= tolerance;
      }

      /// <summary>
      /// <para>Determines whether value is within a scale-dependent tolerance of target.</para>
      /// </summary>
      /// <remarks>Tolerance is computed as EffectiveTolerance(Max(|value|, |target|), DefaultToleranceFactor) and applied to the absolute difference between the values.</remarks>
      /// <param name="value">Value to compare with the target.</param>
      /// <param name="target">Target value for comparison.</param>
      /// <returns>True if the absolute difference between value and target is less than or equal to the computed tolerance; otherwise, false.</returns>
      public static bool IsWithinTolerance(TFloat value, TFloat target) => IsWithinTolerance(value, target, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion

      #region IsNearZero

      /// <summary>
      /// <para>Returns true when the absolute value of value is less than or equal to toleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>().</para>
      /// </summary>
      /// <remarks>Uses a tolerance based on the floating-point type's relative epsilon. For scale-dependent comparisons or domain-specific thresholds, use an appropriate absolute or scaled tolerance.</remarks>
      /// <param name="value">Value to test for nearness to zero.</param>
      /// <param name="toleranceFactor">Multiplicative factor applied to FloatingPoint.RelativeEpsilon<TFloat>() to form the tolerance; expected to be non-negative.</param>
      /// <returns>True if the absolute value of value is less than or equal to toleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>(); otherwise, false.</returns>
      public static bool IsNearZero(TFloat value, TFloat toleranceFactor)
        => TFloat.Abs(value) <= toleranceFactor * FloatingPoint.RelativeEpsilon<TFloat>();

      /// <summary>
      /// <para>Returns true when the absolute value of value is less than or equal to DefaultToleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>().</para>
      /// </summary>
      /// <remarks>Uses a tolerance based on the floating-point type's relative epsilon. For scale-dependent comparisons or domain-specific thresholds, use an appropriate absolute or scaled tolerance.</remarks>
      /// <param name="value">Value to test for nearness to zero.</param>
      /// <returns>True if the absolute value of value is less than or equal to DefaultToleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>(); otherwise, false.</returns>
      public static bool IsNearZero(TFloat value) => IsNearZero(value, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion

      #region RelativeEpsilon

      public static TFloat RelativeEpsilon()
        => (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(1e-28m)
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(2.220446049250313e-16)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(1.1920929e-7f)
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked((System.Half)0.0009765625)
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      #endregion

      #region SnapNearZero/Try

      public static TFloat SnapNearZero(TFloat value, TFloat toleranceFactor)
        => IsNearZero(value, toleranceFactor) ? TFloat.Zero : value;

      public static TFloat SnapNearZero(TFloat value) => SnapNearZero(value, TFloat.CreateChecked(DefaultToleranceFactor));

      public static bool TrySnapNearZero(TFloat value, out TFloat result, TFloat toleranceFactor)
      {
        var isNearZero = IsNearZero(value, toleranceFactor);

        result = isNearZero ? TFloat.Zero : value;

        return isNearZero;
      }

      public static bool TrySnapNearZero(TFloat value, out TFloat result) => TrySnapNearZero(value, out result, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion
    }
  }
}
