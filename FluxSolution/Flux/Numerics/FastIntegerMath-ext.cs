namespace Flux
{
  public static partial class FastIntegerMath
  {
    public static (TFloat Floor, TFloat Ceiling) RoundToFloorAndCeilingWithinTolerance<TFloat>(TFloat value, TFloat absoluteTolerance, TFloat relativeTolerance)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      if (value > default(TFloat)!)
      {
        var ivalue = TFloat.Round(value);

        var eqwt = value.EqualsWithinAbsoluteTolerance(ivalue, absoluteTolerance) || value.EqualsWithinRelativeTolerance(ivalue, relativeTolerance);

        var ivaluec = TFloat.Ceiling(value);
        var ivaluef = TFloat.Floor(value);

        if (eqwt && value < ivalue) // The value and its integer is considered equal, but the specified value is less, so the floor will be off, slightly less, and needs to be equal to ceiling. (The ceiling, same issue, happens to be correct.)
          ivaluef = ivaluec;

        if (eqwt && value > ivalue) // The value and its integer is considered equal, but the specified value is greater, so the ceiling will be off, slightly greater, and needs to be equal to floor. (The floor, same issue, happens to be correct.)
          ivaluec = ivaluef;

        return (ivaluef, ivaluec);
      }

      return (default!, default!);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TInteger FastDigitCount<TInteger, TRadix>(this TInteger value, TRadix radix)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TInteger.One + FastIntegerLog(value, radix, out var _).IlogTz;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="cbrt">Out parameter with the result from <see cref="double.Cbrt(double)"/>.</param>
    /// <returns>The floor and ceiling of the result.</returns>
    public static (TNumber IcbrtTz, TNumber IcbrtAfz) FastIntegerCbrt<TNumber>(this TNumber value, out double cbrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      cbrt = double.Cbrt(double.CreateChecked(value));

      var (icbrtf, icbrtc) = RoundToFloorAndCeilingWithinTolerance(cbrt, 1e-10, 1e-10);

      return (TNumber.CreateChecked(icbrtf), TNumber.CreateChecked(icbrtc));
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="log">Out parameter with the result from <see cref="double.Log(double, double)"/>.</param>
    /// <param name="faultTolerance"></param>
    /// <returns>The floor and ceiling of the result.</returns>
    public static (TNumber IlogTz, TNumber IlogAfz) FastIntegerLog<TNumber, TRadix>(this TNumber value, TRadix radix, out double log, double faultTolerance = 1e-10)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      checked
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        log = TNumber.IsZero(value) ? 0.0 : double.Log(double.CreateChecked(value), double.CreateChecked(Units.Radix.AssertMember(radix)));

        //if (log > 0)
        //{
        var (ilogf, ilogc) = RoundToFloorAndCeilingWithinTolerance(log, 1e-10, 1e-10);

        //var ilogc = double.Ceiling(log);
        //var ilogf = double.Floor(log);

        //var ilog = System.Convert.ToInt64(log);

        //var eqwt = log.EqualsWithinAbsoluteTolerance(ilog, faultTolerance) || log.EqualsWithinRelativeTolerance(ilog, faultTolerance);

        //if (eqwt && log < ilog)
        //  ilogf = ilogc;

        //if (eqwt && log > ilog)
        //  ilogc = ilogf;

        return (TNumber.CreateChecked(ilogf), TNumber.CreateChecked(ilogc));
        //}

        //return (TNumber.Zero, TNumber.Zero);
      }
    }
    //{
    //  checked
    //  {
    //    log = (value < TNumber.One) ? 0.0 : double.Log(double.CreateChecked(value), double.CreateChecked(Units.Radix.AssertMember(radix)));

    //    if (log >= 1.0)
    //      return (TNumber.CreateChecked(double.Floor(log)), TNumber.CreateChecked(double.Ceiling(log)));

    //    return (TNumber.Zero, TNumber.One);
    //  }
    //}

    /// <summary>
    /// <para>Computes a <paramref name="value"/> raised to to a given <paramref name="power"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TPower"></typeparam>
    /// <param name="value"></param>
    /// <param name="power"></param>
    /// <param name="pow">Out parameter with the result from <see cref="double.Pow(double, double)"/>.</param>
    /// <returns>The floor and ceiling of the result.</returns>
    public static (TNumber IpowTz, TNumber IpowAfz) FastIntegerPow<TNumber, TPower>(this TNumber value, TPower power, out double pow)
      where TNumber : System.Numerics.INumber<TNumber>
      where TPower : System.Numerics.INumber<TPower>
    {
      pow = double.Pow(double.CreateChecked(value), double.CreateChecked(power));

      var (ipowf, ipowc) = RoundToFloorAndCeilingWithinTolerance(pow, 1e-10, 1e-10);

      return (TNumber.CreateChecked(ipowf), TNumber.CreateChecked(ipowc));
    }

    /// <summary>
    /// <para>Computes the integer <paramref name="nth"/>-root of a <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <param name="value"></param>
    /// <param name="nth"></param>
    /// <param name="rootn">Out parameter with the result from <see cref="double.RootN(double, int)"/>.</param>
    /// <returns>The floor and ceiling of the result.</returns>
    public static (TNumber IrootnTz, TNumber IrootnAfz) FastIntegerRootN<TNumber, TNth>(this TNumber value, TNth nth, out double rootn)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      rootn = double.RootN(double.CreateChecked(value), int.CreateChecked(nth));

      var (irootnf, irootnc) = RoundToFloorAndCeilingWithinTolerance(rootn, 1e-10, 1e-10);

      return (TNumber.CreateChecked(irootnf), TNumber.CreateChecked(irootnc));
    }

    /// <summary>
    /// <para>Computes the integer square-root of a <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="sqrt">Out parameter with the result from <see cref="double.Sqrt(double)"/>.</param>
    /// <returns>The floor and ceiling of the result.</returns>
    public static (TNumber IsqrtTz, TNumber IsqrtAfz) FastIntegerSqrt<TNumber>(this TNumber value, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      sqrt = double.Sqrt(double.CreateChecked(value));

      var (isqrtf, isqrtc) = RoundToFloorAndCeilingWithinTolerance(sqrt, 1e-10, 1e-10);

      return (TNumber.CreateChecked(isqrtf), TNumber.CreateChecked(isqrtc));
    }

    /// <summary>
    /// <para>Attempts to compute the cube-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="cbrt"/> (double) and <paramref name="icbrt"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The cubed number to find the root of.</param>
    /// <param name="icbrtTz"></param>
    /// <param name="icbrtAfz">The integer cube-root of <paramref name="value"/>.</param>
    /// <param name="cbrt">The floating-point cube-root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerCbrt<TInteger>(this TInteger value, out TInteger icbrtTz, out TInteger icbrtAfz, out double cbrt)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          (icbrtTz, icbrtAfz) = value.FastIntegerCbrt(out cbrt);

          return true;
        }
      }
      catch { }

      icbrtTz = icbrtAfz = TInteger.Zero;
      cbrt = 0.0;

      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the logarithm of a <paramref name="value"/> in the specified <paramref name="radix"/> (base) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="log"/> (double) and <paramref name="ilogTz"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TIRadix"></typeparam>
    /// <typeparam name="TILog"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="ilogTz"></param>
    /// <param name="log"></param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerLog<TInteger, TRadix>(this TInteger value, TRadix radix, out TInteger ilogTz, out TInteger ilogAfz, out double log)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          (ilogTz, ilogAfz) = FastIntegerLog(value, radix, out log);

          return true;
        }
      }
      catch { }

      ilogTz = ilogAfz = TInteger.Zero;
      log = 0.0;

      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the <paramref name="value"/> raised to the given <paramref name="power"/> (exponent) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="pow"/> (double) and <paramref name="integerPow"/> are returned as out parameters.</para>
    /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="power"/>, using the .NET built-in functionality.</para>
    /// <para>This is a faster method, but is limited to integer output less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TIPower"></typeparam>
    /// <typeparam name="TIPow"></typeparam>
    /// <param name="value">The radix (base) to be raised to the power-of-<paramref name="power"/>.</param>
    /// <param name="power">The exponent with which to raise the <paramref name="value"/>.</param>
    /// <param name="mode"></param>
    /// <param name="integerPow"></param>
    /// <param name="pow">The result as an out parameter, if successful. Undefined if unsuccessful.</param>
    /// <returns></returns>
    public static bool TryFastIntegerPow<TInteger, TPower>(this TInteger value, TPower power, out TInteger ipowTz, out TInteger ipowAfz, out double pow)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TPower : System.Numerics.IBinaryInteger<TPower>
    {
      try
      {
        (ipowTz, ipowAfz) = FastIntegerPow(value, power, out pow);

        if (ipowAfz.GetBitLength() <= 53)
          return true;
      }
      catch { }

      ipowTz = ipowAfz = TInteger.One;
      pow = default;

      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the nth-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="rootn"/> (double) and <paramref name="integerRoot"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <typeparam name="TRoot"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">The nth exponent for which to find the root.</param>
    /// <param name="rootn">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerRootN<TInteger, TNth>(this TInteger value, TNth nth, out TInteger irootnTz, out TInteger irootnAfz, out double rootn)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          (irootnTz, irootnAfz) = FastIntegerRootN(value, nth, out rootn);

          return true;
        }
      }
      catch { }

      irootnTz = irootnAfz = TInteger.Zero;
      rootn = 0.0;

      return false;
    }

    /// <summary>
    /// <para>Attempts to compute the square-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="sqrt"/> (double) and <paramref name="isqrt"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TSqrt"></typeparam>
    /// <param name="value">The squared number to find the root of.</param>
    /// <param name="mode"></param>
    /// <param name="isqrt">The integer square-root of <paramref name="value"/>.</param>
    /// <param name="sqrt">The actual floating-point square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerSqrt<TInteger>(this TInteger value, out TInteger isqrtTz, out TInteger isqrtAfz, out double sqrt)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          (isqrtTz, isqrtAfz) = value.FastIntegerSqrt(out sqrt);

          return true;
        }
      }
      catch { }

      isqrtTz = isqrtAfz = TInteger.Zero;
      sqrt = 0.0;

      return false;
    }
  }
}
