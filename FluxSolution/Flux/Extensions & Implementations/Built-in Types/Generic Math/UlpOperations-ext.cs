namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region IsUlpNearInteger

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsUlpNearInteger(TFloat value, int maxUlps, out TFloat nearestInteger)
      {
        nearestInteger = RoundToNearestInteger(value);

        return Number.IsUlpNear(value, nearestInteger, maxUlps);
      }

      /// <summary>
      /// <para>Uses a max ulps of 1.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsUlpNearInteger(TFloat value, out TFloat nearestInteger) => IsUlpNearInteger(value, 1, out nearestInteger);

      #endregion

      #region IsUlpNearMultiple

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsUlpNearMultiple(TFloat value, TFloat baseValue, int maxUlps, out TFloat nearestMultiple)
      {
        nearestMultiple = TFloat.Round(value / baseValue) * baseValue;

        return Number.IsUlpNear(value, nearestMultiple, maxUlps);
      }

      public static bool IsUlpNearMultiple(TFloat value, TFloat baseValue, out TFloat nearestMultiple) => IsUlpNearMultiple(value, baseValue, 1, out nearestMultiple);

      #endregion

      #region SnapUlpNearInteger/Try

      /// <summary>
      /// <para>Snap a <typeparamref name="TFloat"/> value to the nearest integer if it is within one ULP of that integer.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat SnapUlpNearInteger(TFloat value, int maxUlps)
        => IsUlpNearInteger(value, maxUlps, out var nearestInteger) ? nearestInteger : value;

      public static TFloat SnapUlpNearInteger(TFloat value) => SnapUlpNearInteger(value, 1);

      public static bool TrySnapUlpNearInteger(TFloat value, int maxUlps, out TFloat nearestInteger)
      {
        if (IsUlpNearInteger(value, maxUlps, out nearestInteger))
          return true;

        nearestInteger = value;
        return false;
      }

      public static bool TrySnapUlpNearInteger(TFloat value, out TFloat nearestInteger) => TrySnapUlpNearInteger(value, 1, out nearestInteger);


      #endregion

      #region SnapUlpNearMultiple/Try

      public static TFloat SnapUlpNearMultiple(TFloat value, TFloat baseValue, int maxUlps)
        => IsUlpNearMultiple(value, baseValue, maxUlps, out var nearestMultiple) ? nearestMultiple : value;

      public static TFloat SnapUlpNearMultiple(TFloat value, TFloat baseValue) => SnapUlpNearMultiple(value, baseValue, 1);

      public static bool TrySnapUlpNearMultiple(TFloat value, TFloat baseValue, int maxUlps, out TFloat nearestMultiple)
      {
        if (IsUlpNearMultiple(value, baseValue, maxUlps, out nearestMultiple))
          return true;

        nearestMultiple = value;
        return false;
      }

      public static bool TrySnapUlpNearMultiple(TFloat value, TFloat baseValue, out TFloat nearestMultiple) => TrySnapUlpNearMultiple(value, baseValue, 1, out nearestMultiple);

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsUlpNear

      public static bool IsUlpNear(TNumber left, TNumber right, int maxUlps)
        => UlpDistance(left, right) <= maxUlps;

      public static bool IsUlpNear(TNumber left, TNumber right) => IsUlpNear(left, right, 1);

      #endregion

      #region Ulp/TryGet

      public static TNumber Ulp(TNumber value)
        => value switch
        {
          decimal dfp128 => TNumber.CreateChecked(decimal.DecimalUlp(dfp128)),
          double bfp64 => TNumber.CreateChecked(double.DoubleUlp(bfp64)),
          float bfp32 => TNumber.CreateChecked(float.SingleUlp(bfp32)),
          System.Half bfp16 => TNumber.CreateChecked(System.Half.HalfUlp(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.NFloatUlp(nf)),
          System.SByte or System.Int16 or System.Int32 or System.Int64 or System.Int128 or System.IntPtr or System.Byte or System.UInt16 or System.UInt32 or System.UInt64 or System.UInt128 or System.UIntPtr or System.Numerics.BigInteger => TNumber.One,
          _ => typeof(TNumber).IsNumericsIBinaryInteger() // If there are new ones.
            ? TNumber.One
            : throw new System.NotImplementedException(typeof(TNumber).Name)
        };

      public static bool TryGetUlp(TNumber value, out TNumber ulp)
      {
        ulp = Ulp(value);

        return TNumber.IsFinite(ulp);
      }

      #endregion

      #region UlpDecrement

      /// <summary>
      /// <para>Decrements a number. If integer, then by +1. If floating-point, then by "bit-decrement". If decimal, by 1e-28m.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpDecrement(TNumber value)
        => value switch
        {
          decimal dfp128 => TNumber.CreateChecked(decimal.DecimalUlpDecrement(dfp128)),
          double bfp64 => TNumber.CreateChecked(double.BitDecrement(bfp64)),
          float bfp32 => TNumber.CreateChecked(float.BitDecrement(bfp32)),
          System.Half bfp16 => TNumber.CreateChecked(System.Half.BitDecrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf)),
          System.SByte or System.Int16 or System.Int32 or System.Int64 or System.Int128 or System.IntPtr or System.Byte or System.UInt16 or System.UInt32 or System.UInt64 or System.UInt128 or System.UIntPtr or System.Numerics.BigInteger => value - TNumber.One,
          _ => typeof(TNumber).IsNumericsIBinaryInteger() // If there are new ones.
            ? value - TNumber.One
            : throw new System.NotImplementedException(typeof(TNumber).Name)
        };

      #endregion

      #region UlpDistance

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="left"></param>
      /// <param name="right"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static System.Numerics.BigInteger UlpDistance(TNumber left, TNumber right)
        => typeof(TNumber) == typeof(decimal)
        ? decimal.DecimalUlpDistance(decimal.CreateChecked(left), decimal.CreateChecked(right))
        : typeof(TNumber) == typeof(double)
        ? double.DoubleUlpDistance(double.CreateChecked(left), double.CreateChecked(right))
        : typeof(TNumber) == typeof(float)
        ? float.SingleUlpDistance(float.CreateChecked(left), float.CreateChecked(right))
        : typeof(TNumber) == typeof(System.Half)
        ? System.Half.HalfUlpDistance(System.Half.CreateChecked(left), System.Half.CreateChecked(right))
        : typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat)
        ? System.Runtime.InteropServices.NFloat.NFloatUlpDistance(System.Runtime.InteropServices.NFloat.CreateChecked(left), System.Runtime.InteropServices.NFloat.CreateChecked(right))
        : typeof(TNumber).IsNumericsIBinaryInteger()
        ? System.Numerics.BigInteger.Abs(System.Numerics.BigInteger.CreateChecked(left) - System.Numerics.BigInteger.CreateChecked(right))
        : throw new System.NotImplementedException(typeof(TNumber).Name);

      #endregion

      #region UlpIncrement

      /// <summary>
      /// <para>Increments a number. If integer, then by -1, otherwise by native-increment.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpIncrement(TNumber value)
        => value switch
        {
          decimal dfp128 => TNumber.CreateChecked(decimal.DecimalUlpIncrement(dfp128)),
          double bfp64 => TNumber.CreateChecked(double.BitIncrement(bfp64)),
          float bfp32 => TNumber.CreateChecked(float.BitIncrement(bfp32)),
          System.Half bfp16 => TNumber.CreateChecked(System.Half.BitIncrement(bfp16)),
          System.Runtime.InteropServices.NFloat nf => TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf)),
          System.SByte or System.Int16 or System.Int32 or System.Int64 or System.Int128 or System.IntPtr or System.Byte or System.UInt16 or System.UInt32 or System.UInt64 or System.UInt128 or System.UIntPtr or System.Numerics.BigInteger => value + TNumber.One,
          _ => typeof(TNumber).IsNumericsIBinaryInteger() // If there are new ones.
            ? value + TNumber.One
            : throw new System.NotImplementedException(typeof(TNumber).Name)
        };

      #endregion
    }
  }
}
