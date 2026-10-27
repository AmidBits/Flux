namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>, System.Numerics.IMinMaxValue<TInteger>
    {
      #region MaxPrime

      /// <summary>
      /// <para>The largest prime number representable by <typeparamref name="TNumber"/>.</para>
      /// </summary>
      public static TInteger MaxPrime
        => typeof(TInteger) == typeof(System.Byte)
        ? TInteger.CreateChecked(251)
        : typeof(TInteger) == typeof(System.Int128)
        ? TInteger.CreateChecked(System.Int128.Parse("170141183460469231731687303715884105727"))
        : typeof(TInteger) == typeof(System.Int16)
        ? TInteger.CreateChecked(32749)
        : typeof(TInteger) == typeof(System.Int32) || (typeof(TInteger) == typeof(System.IntPtr) && System.IntPtr.Size == 4)
        ? TInteger.CreateChecked(2147483647)
        : typeof(TInteger) == typeof(System.Int64) || (typeof(TInteger) == typeof(System.IntPtr) && System.IntPtr.Size == 8)
        ? TInteger.CreateChecked(9223372036854775783)
        : typeof(TInteger) == typeof(System.SByte)
        ? TInteger.CreateChecked(127)
        : typeof(TInteger) == typeof(System.UInt128)
        ? TInteger.CreateChecked(System.UInt128.Parse("340282366920938463463374607431768211297"))
        : typeof(TInteger) == typeof(System.UInt16)
        ? TInteger.CreateChecked(65521)
        : typeof(TInteger) == typeof(System.UInt32) || (typeof(TInteger) == typeof(System.UIntPtr) && System.UIntPtr.Size == 4)
        ? TInteger.CreateChecked(4294967291u)
        : typeof(TInteger) == typeof(System.UInt64) || (typeof(TInteger) == typeof(System.UIntPtr) && System.UIntPtr.Size == 8)
        ? TInteger.CreateChecked(18446744073709551557ul)
        : throw new System.NotImplementedException(typeof(TInteger).Name);

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IMinMaxValue<TFloat>
    {
      #region MaxExactInteger

      /// <summary>
      /// <para>The largest exact integer representable by <typeparamref name="TFloat"/>.</para>
      /// </summary>
      public static TFloat MaxExactInteger
        => typeof(TFloat) == typeof(System.Decimal)
        ? TFloat.CreateChecked(79228162514264337593543950335m)
        : typeof(TFloat) == typeof(System.Double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8)
        ? TFloat.CreateChecked(9007199254740992)
        : typeof(TFloat) == typeof(System.Half)
        ? TFloat.CreateChecked(2048)
        : typeof(TFloat) == typeof(System.Single) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4)
        ? TFloat.CreateChecked(16777216)
#if NET11_0_OR_GREATER
        : typeof(TFloat) == typeof(System.Numerics.BFloat16)
        ? TFloat.CreateChecked(128)
        : typeof(TFloat) == typeof(System.Numerics.Decimal128)
        ? TFloat.CreateChecked(System.Numerics.Decimal128.Parse("9999999999999999999999999999999999")
        : typeof(TFloat) == typeof(System.Numerics.Decimal32)
        ? TFloat.CreateChecked(System.Numerics.Decimal32.Parse("9999999"))
        : typeof(TFloat) == typeof(System.Numerics.Decimal64)
        ? TFloat.CreateChecked(System.Numerics.Decimal64.Parse("9999999999999999"))
#endif
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      #endregion

      #region MinExactInteger

      /// <summary>
      /// <para>The smallest exact integer representable by <typeparamref name="TFloat"/>.</para>
      /// </summary>
      public static TFloat MinExactInteger
        => -get_MaxExactInteger<TFloat>();

      #endregion

      #region MaxExactPrime

      /// <summary>
      /// <para>The largest exact prime number representable by <typeparamref name="TNumber"/>.</para>
      /// </summary>
      public static TFloat MaxExactPrime
        => typeof(TFloat) == typeof(System.Decimal)
        ? TFloat.CreateChecked(79228162514264337593543950297m)
        : typeof(TFloat) == typeof(System.Double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8)
        ? TFloat.CreateChecked(9007199254740881d)
        : typeof(TFloat) == typeof(System.Half)
        ? TFloat.CreateChecked(2039)
        : typeof(TFloat) == typeof(System.Single) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4)
        ? TFloat.CreateChecked(16777213)
#if NET11_0_OR_GREATER
        : typeof(TFloat) == typeof(System.Numerics.BFloat16)
        ? TFloat.CreateChecked(127)
        : typeof(TFloat) == typeof(System.Numerics.Decimal128)
        ? TFloat.CreateChecked(System.Numerics.Decimal128.Parse("9999999999999999999999999999999989")
        : typeof(TFloat) == typeof(System.Numerics.Decimal32)
        ? TFloat.CreateChecked(System.Numerics.Decimal32.Parse("9999991"))
        : typeof(TFloat) == typeof(System.Numerics.Decimal64)
        ? TFloat.CreateChecked(System.Numerics.Decimal64.Parse("9999999999999989"))
#endif
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region MinPrime

      /// <summary>
      /// <para>The smallest prime number representable by <typeparamref name="TNumber"/>.</para>
      /// </summary>
      public static TNumber MinPrime
        => NumberConstants<TNumber>.Two;

      #endregion
    }
  }
}
