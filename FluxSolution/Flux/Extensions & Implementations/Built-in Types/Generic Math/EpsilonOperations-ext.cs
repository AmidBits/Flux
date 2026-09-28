namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region GetEpsilons

      public static (TFloat MachineEpsilon, TFloat RelativeEpsilon) GetEpsilons(TFloat value)
      {
        var machineEpsilon = (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(decimal.MachineEpsilon)
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(double.MachineEpsilon)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(float.MachineEpsilon)
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked(System.Half.MachineEpsilon)
        : throw new System.NotImplementedException(typeof(TFloat).Name);

        var relativeEpsilon = machineEpsilon * TFloat.Max(TFloat.One, TFloat.Abs(value));

        return (machineEpsilon, relativeEpsilon);
      }

      #endregion

      #region IsEpsilonNear

      public static bool IsEpsilonNear(TFloat value, TFloat target, TFloat epsilonFactor)
      {
        epsilonFactor = epsilonFactor == TFloat.Zero
            ? TFloat.CreateChecked(4)
            : epsilonFactor;

        var magnitude = TFloat.Max(TFloat.One, TFloat.Max(TFloat.Abs(value), TFloat.Abs(target)));

        var epsilon = GetEpsilons(value).RelativeEpsilon * magnitude * epsilonFactor;

        return TFloat.Abs(value - target) <= epsilon;
      }

      #endregion

      #region IsEpsilonNearInteger

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within absolute or relative epsilon.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="epsilonAbsolute"></param>
      /// <param name="epsilonRelative"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearInteger(TFloat value, TFloat epsilonFactor, out TFloat nearestInteger)
      {
        nearestInteger = RoundToNearestInteger(value);

        return IsEpsilonNear(value, nearestInteger, epsilonFactor);
      }

      /// <summary>
      /// <para>Uses an epsilon factor of 11.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearInteger(TFloat value, out TFloat nearestInteger) => IsEpsilonNearInteger(value, TFloat.CreateChecked(11), out nearestInteger);

      #endregion

      #region IsEpsilonNearMultiple

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near a multiple of a number within absolute or relative epsilon.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="epsilonAbsolute"></param>
      /// <param name="epsilonRelative"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearMultiple(TFloat value, TFloat baseValue, TFloat epsilonFactor, out TFloat nearestMultiple)
      {
        var k = RoundToNearestInteger(value / baseValue);

        nearestMultiple = k * baseValue;

        return IsEpsilonNear(value, nearestMultiple, epsilonFactor);
      }

      /// <summary>
      /// <para>Uses an epsilon factor of 11.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsEpsilonNearMultiple(TFloat value, TFloat baseValue, out TFloat nearestMultiple) => IsEpsilonNearMultiple(value, baseValue, TFloat.CreateChecked(11), out nearestMultiple);

      #endregion
    }
  }
}
