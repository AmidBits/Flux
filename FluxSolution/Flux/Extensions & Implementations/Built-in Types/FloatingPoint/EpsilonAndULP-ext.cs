namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region Epsilon functions

      /// <summary>
      /// <para>Gets the base absolute and relative epsilons for the specified value.</para>
      /// </summary>
      /// <param name="value">
      /// <para>The value whose local absolute epsilon is required.</para>
      /// </param>
      /// <returns>
      /// <para>
      /// A tuple containing:
      /// </para>
      /// <list type="bullet">
      /// <item>
      /// <description>
      /// <c>EpsilonAbsolute</c>: the local absolute epsilon (ULP) at <paramref name="value"/>.
      /// </description>
      /// </item>
      /// <item>
      /// <description>
      /// <c>EpsilonRelative</c>: the machine epsilon for <typeparamref name="TFloat"/>.
      /// </description>
      /// </item>
      /// </list>
      /// </returns>
      /// <remarks>
      /// <para>
      /// The absolute epsilon depends on the magnitude of <paramref name="value"/> and represents the
      /// smallest representable change at that location in the number line.
      /// </para>
      /// <para>
      /// The relative epsilon is the machine epsilon of <typeparamref name="TFloat"/> and represents
      /// the relative precision of the floating-point type.
      /// </para>
      /// <para>
      /// These values can be used as a basis for floating-point comparison tolerances.
      /// </para>
      /// </remarks>
      public static (TFloat EpsilonAbsolute, TFloat EpsilonRelative) GetBaseEpsilons(TFloat value)
      {
        TryGetUlp(value, out var epsilonAbsolute);

        var epsilonRelative = GetMachineEpsilon<TFloat>();

        return (epsilonAbsolute, epsilonRelative);
      }

      /// <summary>
      /// <para>Gets the engineering epsilon for <typeparamref name="TFloat"/>.</para>
      /// </summary>
      /// <returns>
      /// <para>
      /// A practical floating-point tolerance intended for engineering calculations.
      /// </para>
      /// </returns>
      /// <remarks>
      /// <para>
      /// Engineering epsilon is a user-oriented tolerance chosen to be useful in numerical and
      /// engineering applications where machine epsilon is often unrealistically small.
      /// </para>
      /// <para>
      /// Unlike machine epsilon, engineering epsilon is intended for approximate equality tests,
      /// convergence criteria, and general-purpose numerical tolerances.
      /// </para>
      /// </remarks>
      public static TFloat GetEngineeringEpsilon()
        => (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(decimal.EngineeringEpsilon)
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(double.EngineeringEpsilon)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(float.EngineeringEpsilon)
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked(System.Half.EngineeringEpsilon)
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      /// <summary>
      /// <para>Gets the machine epsilon for <typeparamref name="TFloat"/>.</para>
      /// </summary>
      /// <returns>
      /// <para>
      /// The smallest value ε such that <c>1 + ε != 1</c> for <typeparamref name="TFloat"/>.
      /// </para>
      /// </returns>
      /// <remarks>
      /// <para>
      /// Machine epsilon is a measure of the relative precision of a floating-point type.
      /// </para>
      /// <para>
      /// It is commonly used for numerical analysis and error estimation but is often too small
      /// to serve as a practical comparison tolerance in application code.
      /// </para>
      /// <para>
      /// For most floating-point comparisons, consider using an application-specific tolerance or
      /// <see cref="GetEngineeringEpsilon"/> instead.
      /// </para>
      /// </remarks>
      public static TFloat GetMachineEpsilon()
        => (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(decimal.MachineEpsilon)
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(double.MachineEpsilon)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(float.MachineEpsilon)
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked(System.Half.MachineEpsilon)
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      #endregion

      #region IsEpsilonNear.. functions

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within absolute or relative epsilon.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="epsilonAbsolute"></param>
      /// <param name="epsilonRelative"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearInteger(TFloat value, TFloat epsilonAbsolute, TFloat epsilonRelative, out TFloat nearestInteger)
      {
        nearestInteger = RoundToNearestInteger(value);

        return IsEpsilonNearNumber(value, nearestInteger, epsilonAbsolute, epsilonRelative);
      }

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near a multiple of a number within absolute or relative epsilon.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="epsilonAbsolute"></param>
      /// <param name="epsilonRelative"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearMultiple(TFloat value, TFloat baseValue, TFloat epsilonAbsolute, TFloat epsilonRelative, out TFloat nearestMultiple)
      {
        var k = RoundToNearestInteger(value / baseValue);

        nearestMultiple = k * baseValue;

        return IsEpsilonNearNumber(value, nearestMultiple, epsilonAbsolute, epsilonRelative);
      }

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near a number within absolute or relative epsilon.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="number"></param>
      /// <param name="epsilonAbsolute"></param>
      /// <param name="epsilonRelative"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearNumber(TFloat value, TFloat number, TFloat epsilonAbsolute, TFloat epsilonRelative)
      {
        var difference = TFloat.Abs(value - number);

        var scale = TFloat.Max(TFloat.Abs(value), TFloat.Abs(number));

        return difference <= epsilonAbsolute + epsilonRelative * scale;
      }

      #endregion

      #region IsUlpNear.. functions

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsUlpNearInteger(TFloat value, out TFloat nearestInteger)
      {
        nearestInteger = RoundToNearestInteger(value);

        return IsUlpNearNumber(value, nearestInteger);
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsUlpNearMultiple(TFloat value, TFloat baseValue, out TFloat nearestMultiple)
      {
        var ratio = value / baseValue;

        var m = TFloat.Round(ratio);

        nearestMultiple = m * baseValue;

        var (absoluteEpsilon, relativeEpsilon) = GetBaseEpsilons(value);

        return IsEpsilonNearNumber(value, nearestMultiple, absoluteEpsilon, relativeEpsilon);
      }

      public static bool IsUlpNearNumber(TFloat value, TFloat number)
      {
        TryGetUlp(number, out var ulp);

        return TFloat.Abs(value - number) <= ulp;
      }

      #endregion

      #region SnapUlpNearInteger functions

      /// <summary>
      /// <para>Snap a <typeparamref name="TFloat"/> value to the nearest integer if it is within one ULP of that integer.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat SnapUlpNearInteger(TFloat value)
        => IsUlpNearInteger(value, out var nearestInteger) ? nearestInteger : value;

      public static bool TrySnapUlpNearInteger(TFloat value, out TFloat nearestInteger)
      {
        if (IsUlpNearInteger(value, out nearestInteger))
          return true;

        nearestInteger = value;
        return false;
      }

      #endregion

      #region ULP functions

      /// <summary>
      /// <para>Get the unit in the last place (ULP) of a <typeparamref name="TFloat"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotSupportedException"></exception>
      public static TFloat GetUlp(TFloat value)
        => (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(decimal.GetDecimalUlp(decimal.CreateChecked(value)))
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(double.GetDoubleUlp(double.CreateChecked(value)))
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(float.GetSingleUlp(float.CreateChecked(value)))
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked(System.Half.GetHalfUlp(System.Half.CreateChecked(value)))
        : throw new System.NotSupportedException($"ULP is not defined for type {typeof(TFloat)}.");

      /// <summary>
      /// <para>Try to get the unit in the last place (ULP) of a <typeparamref name="TFloat"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="ulp"></param>
      /// <returns></returns>
      /// <exception cref="System.NotSupportedException"></exception>
      public static bool TryGetUlp(TFloat value, out TFloat ulp)
      {
        ulp = GetUlp(value);

        return !TFloat.IsNaN(value) && !TFloat.IsInfinity(value); ;
      }

      #endregion
    }
  }
}
