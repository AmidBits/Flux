namespace Flux
{
  public static partial class FloatingPoint
  {
    public const int DefaultMaxUlps = 11;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region IsNearInteger

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsNearInteger(TFloat value, out TFloat nearestInteger, int maxUlps = DefaultMaxUlps)
      {
        nearestInteger = RoundToNearestInteger(value);

        return Number.IsWithinUlps(value, nearestInteger, maxUlps);
      }

      #endregion

      #region SnapNearInteger/Try

      /// <summary>
      /// <para>Snap a <typeparamref name="TFloat"/> value to the nearest integer if it is within one ULP of that integer.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat SnapNearInteger(TFloat value, int maxUlps = DefaultMaxUlps)
        => IsNearInteger(value, out var nearestInteger, maxUlps) ? nearestInteger : value;

      public static bool TrySnapNearInteger(TFloat value, out TFloat nearestInteger, int maxUlps = DefaultMaxUlps)
      {
        var isNearInteger = IsNearInteger(value, out nearestInteger, maxUlps);

        if (!isNearInteger)
          nearestInteger = value;

        return isNearInteger;
      }

      #endregion
    }
  }

  public static partial class Number
  {
    public const int DefaultMaxUlps = 11;

    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsNearMultiple

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsNearMultiple(TNumber value, TNumber baseValue, out TNumber nearestMultiple, int maxUlps = DefaultMaxUlps)
      {
        nearestMultiple = IntegerDivRemRounded(value, baseValue).Quotient * baseValue;

        return IsWithinUlps(value, nearestMultiple, maxUlps);
      }

      #endregion

      #region IsWithinUlps

      /// <summary>
      /// <para>Determines whether two numeric values differ by no more than a specified number of ULPs (units in the last place).</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="left">The first value to compare.</param>
      /// <param name="right">The second value to compare.</param>
      /// <param name="maxUlps">The maximum allowed difference measured in units in the last place (ULPs).</param>
      /// <returns>true if the ULP distance between left and right is less than or equal to maxUlps; otherwise false.</returns>
      public static bool IsWithinUlps(TNumber left, TNumber right, int maxUlps = 1)
        => UlpDistance(left, right) <= maxUlps;

      #endregion

      #region SnapNearMultiple/Try

      public static TNumber SnapNearMultiple(TNumber value, TNumber baseValue, int maxUlps = DefaultMaxUlps)
        => IsNearMultiple(value, baseValue, out var nearestMultiple, maxUlps) ? nearestMultiple : value;

      public static bool TrySnapNearMultiple(TNumber value, TNumber baseValue, out TNumber nearestMultiple, int maxUlps = DefaultMaxUlps)
      {
        var isNearMultiple = IsNearMultiple(value, baseValue, out nearestMultiple, maxUlps);

        if (!isNearMultiple)
          nearestMultiple = value;

        return isNearMultiple;
      }

      #endregion

      #region Ulp/TryGet

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber Ulp(TNumber value)
      {
        if (TNumber.IsNaN(value))
          throw new System.ArithmeticException("NaN has no meaningful ULP.");

        if (TNumber.IsInfinity(value))
          throw new System.ArithmeticException("Infinity has no meaningful ULP.");

        if (typeof(TNumber) == typeof(decimal))
        {
          var tv = decimal.CreateChecked(value);
          var bits = decimal.GetBits(tv);
          var scale = ((bits[3] >> 16) & 0xFF);
          var ulp = decimal.Pow10(-scale);
          return TNumber.CreateChecked(ulp);
        }
        else if ((typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8)))
        {
          var tv = double.CreateChecked(value);
          var ulp = tv == double.MaxValue ? tv - double.BitDecrement(tv) : double.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if ((typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4)))
        {
          var tv = float.CreateChecked(value);
          var ulp = tv == float.MaxValue ? tv - float.BitDecrement(tv) : float.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber) == typeof(System.Half))
        {
          var tv = System.Half.CreateChecked(value);
          var ulp = tv == System.Half.MaxValue ? tv - System.Half.BitDecrement(tv) : System.Half.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber).ImplementsIBinaryInteger())
          return TNumber.One;
        else
          throw new System.NotImplementedException(typeof(TNumber).Name);
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="ulp"></param>
      /// <returns></returns>
      public static bool TryGetUlp(TNumber value, out TNumber ulp)
      {
        ulp = Ulp(value);

        return TNumber.IsFinite(ulp);
      }

      #endregion

      #region UlpDecrement

      /// <summary>
      /// <para>Increments a value by <paramref name="count"/> ULPs.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpDecrement(TNumber value, long count)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          if (typeof(TNumber) == typeof(decimal)) // Decrement by ulps * ULP(value).
            return checked(value - Ulp(value) * TNumber.CreateChecked(count));
          else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          {
            var ordered = double.ToOrderedBits(double.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(double.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          {
            var ordered = float.ToOrderedBits(float.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(float.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber) == typeof(System.Half))
          {
            var ordered = System.Half.ToOrderedBits(System.Half.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(System.Half.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber).ImplementsIBinaryInteger()) // Is integer, decrement by ulps.
            return value - TNumber.CreateChecked(count);
          else // Unrecognized type, throw an exception.
            throw new System.NotImplementedException(typeof(TNumber).Name);
        }
      }

      /// <summary>
      /// <para>Increments a value by one ULP.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpDecrement(TNumber value) => UlpDecrement(value, 1);

      #endregion

      #region UlpDistance

      /// <summary>
      /// <para>Returns the distance in adjacent representable values.</para>
      /// <list type="bullet">
      /// <item>For integer types, the result is the absolute difference between the values.</item>
      /// <item>For decimal, the result is the distance between normalized decimal significands.</item>
      /// <item>For IEEE floating-point types, the result is measured in ULPs (Units in the Last Place).</item>
      /// </list>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type="bullet">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="left"></param>
      /// <param name="right"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public static System.Numerics.BigInteger UlpDistance(TNumber left, TNumber right)
      {
        if (TNumber.IsNaN(left) || TNumber.IsNaN(right))
          throw new System.ArithmeticException("NaN has no meaningful ULP distance.");

        if (left == right)
          return System.Numerics.BigInteger.Zero;

        System.Numerics.BigInteger leftOrdered, rightOrdered;

        if (typeof(TNumber) == typeof(decimal))
        {
          var (_, leftScale, leftSignificand) = decimal.ExtractDecimalFields(decimal.CreateChecked(left));
          var (_, rightScale, rightSignificand) = decimal.ExtractDecimalFields(decimal.CreateChecked(right));
          var scale = int.Max(leftScale, rightScale);
          leftOrdered = leftSignificand * (System.Numerics.BigInteger)decimal.Pow10(scale - leftScale);
          rightOrdered = rightSignificand * (System.Numerics.BigInteger)decimal.Pow10(scale - rightScale);
        }
        else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        {
          leftOrdered = System.BitConverter.DoubleToInt64Bits(double.CreateChecked(left));
          rightOrdered = System.BitConverter.DoubleToInt64Bits(double.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)long.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)long.MinValue - rightOrdered;
        }
        else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        {
          leftOrdered = System.BitConverter.SingleToInt32Bits(float.CreateChecked(left));
          rightOrdered = System.BitConverter.SingleToInt32Bits(float.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)int.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)int.MinValue - rightOrdered;
        }
        else if (typeof(TNumber) == typeof(System.Half))
        {
          leftOrdered = System.BitConverter.HalfToInt16Bits(System.Half.CreateChecked(left));
          rightOrdered = System.BitConverter.HalfToInt16Bits(System.Half.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)short.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)short.MinValue - rightOrdered;
        }
        else if (typeof(TNumber).ImplementsIBinaryInteger())
        {
          leftOrdered = System.Numerics.BigInteger.CreateChecked(left);
          rightOrdered = System.Numerics.BigInteger.CreateChecked(right);
        }
        else throw new System.NotImplementedException(typeof(TNumber).Name);

        return System.Numerics.BigInteger.Abs(leftOrdered - rightOrdered);
      }

      #endregion

      #region UlpIncrement

      /// <summary>
      /// <para>Increments a value by <paramref name="count"/> ULPs.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpIncrement(TNumber value, long count)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          if (typeof(TNumber) == typeof(decimal)) // Increment by ulps * ULP(value).
            return value + Ulp(value) * TNumber.CreateChecked(count);
          else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          {
            var ordered = double.ToOrderedBits(double.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(double.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          {
            var ordered = float.ToOrderedBits(float.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(float.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber) == typeof(System.Half))
          {
            var ordered = System.Half.ToOrderedBits(System.Half.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(System.Half.FromOrderedBits(ordered));
          }
          else if (typeof(TNumber).ImplementsIBinaryInteger()) // Is integer, increment by ulps.
            return value + TNumber.CreateChecked(count);
          else // Unrecognized type, throw an exception.
            throw new System.NotImplementedException(typeof(TNumber).Name);
        }
      }

      /// <summary>
      /// <para>Increments a value by one ULP.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpIncrement(TNumber value) => UlpIncrement(value, 1);

      #endregion
    }
  }
}
