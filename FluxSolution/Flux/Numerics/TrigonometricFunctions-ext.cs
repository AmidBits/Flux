namespace Flux
{
  public static class TrigonometricFunctions
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {
      public static TFloat RadianToPercentSlope(TFloat x)
        => TFloat.Tan(x);

      #region Fn(x)

      /// <summary>Returns the cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static TFloat Cot(TFloat x)
        => TFloat.One / TFloat.Tan(x);

      /// <summary>Returns the secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static TFloat Sec(TFloat x)
        => TFloat.One / TFloat.Cos(x);

      /// <summary>Returns the cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public static TFloat Csc(TFloat x)
        => TFloat.One / TFloat.Sin(x);

      /// <summary>Returns the versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Vsin(TFloat x)
        => TFloat.One - TFloat.Cos(x);

      /// <summary>Returns the versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Vcos(TFloat x)
        => TFloat.One + TFloat.Cos(x);

      /// <summary>Returns the coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Cvsin(TFloat x)
        => TFloat.One - TFloat.Sin(x);

      /// <summary>Returns the coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Cvcos(TFloat x)
        => TFloat.One + TFloat.Sin(x);

      /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Hvsin(TFloat x)
        => (TFloat.One - TFloat.Cos(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Hvcos(TFloat x)
        => (TFloat.One + TFloat.Cos(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the hacoversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Hcvsin(TFloat x)
        => (TFloat.One - TFloat.Sin(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public static TFloat Hcvcos(TFloat x)
        => (TFloat.One + TFloat.Sin(x)) / TFloat.CreateChecked(2);

      #endregion

      #region Sinc..

      /// <summary>
      /// <para>Returns the normalized (using PI) sinc function of the specified x.</para>
      /// <para>The normalization causes the definite integral of the function over the real numbers to equal 1 (whereas the same integral of the unnormalized sinc function has a value of π).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sinc_function"/></para>
      /// </summary>
      public static TFloat Sincn(TFloat x)
        => Sincu(TFloat.Pi * x);

      /// <summary>
      /// <para>Returns the unnormalized sinc of the specified x.</para>
      /// <para>The definite integral of the unnormalized sinc function has a value of π.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sinc_function"/></para>
      /// </summary>
      public static TFloat Sincu(TFloat x)
        => TFloat.IsZero(x)
        ? TFloat.One
        : TFloat.Sin(x) / x;

      #endregion

      #region Fn(y)

      /// <summary>
      /// <para>Returns the inverse cotangent of the specified angle, range <c>[-PI/2, PI/2]</c> (matches <c>Atan(1/x)</c>).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
      /// </summary>
      public static TFloat Acot1(TFloat y)
        => TFloat.IsZero(y) ? y : TFloat.Atan(TFloat.One / y);

      /// <summary>
      /// <para>Returns the inverse cotangent of the specified angle, range <c>[0, PI]</c>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
      /// </summary>
      public static TFloat Acot2(TFloat y)
        => TFloat.Pi / TFloat.CreateChecked(2) - TFloat.Atan(y);

      /// <summary>
      /// <para>Returns the inverse secant of the specified angle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// </summary>
      public static TFloat Asec(TFloat y)
        => TFloat.Acos(TFloat.One / y);

      /// <summary>
      /// <para>Returns the inverse cosecant of the specified angle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// </summary>
      public static TFloat Acsc(TFloat y)
        => TFloat.Asin(TFloat.One / y);

      /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Avsin(TFloat y)
        => TFloat.Acos(TFloat.One - y);

      /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Avcos(TFloat y)
        => TFloat.Acos(y - TFloat.One);

      /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Acvsin(TFloat y)
        => TFloat.Asin(TFloat.One - y);

      /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Acvcos(TFloat y)
        => TFloat.Asin(y - TFloat.One);

      /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Ahvsin(TFloat y)
        => TFloat.Acos(TFloat.One - TFloat.CreateChecked(2) * y); // An extra subtraction saves a call to the Sqrt function: 2 * double.Asin(double.Sqrt(y));

      /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Ahvcos(TFloat y)
        => TFloat.Acos(TFloat.CreateChecked(2) * y - TFloat.One); // An extra subtraction saves a call to the Sqrt function: 2 * double.Acos(double.Sqrt(y));

      /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Ahcvsin(TFloat y)
        => TFloat.Asin(TFloat.One - TFloat.CreateChecked(2) * y);

      /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public static TFloat Ahcvcos(TFloat y)
        => TFloat.Asin(TFloat.CreateChecked(2) * y - TFloat.One);

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      #region Atan(y, x)

      /// <summary>
      /// <para>Implementation of Atan2(y, x) resulting in [-Pi, +Pi].</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      public static TFloat Atan2(TFloat y, TFloat x)
      {
        if (TFloat.IsZero(x))
        {
          if (TFloat.IsZero(y))
            return TFloat.Zero;

          return TFloat.CopySign(TFloat.Pi / TFloat.CreateChecked(2), y);
        }

        var atanYX = TFloat.Atan(y / x);

        if (TFloat.IsNegative(x))
          atanYX += TFloat.CopySign(TFloat.Pi, y);

        return atanYX;
      }

      /// <summary>
      /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being 3 o'clock and rotating counter-clockwise.</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
      /// </summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <remarks>
      /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
      /// <para>This uses <see cref="double.Atan2(double, double)"/> in the traditional sense, but without any negative return values.</para>
      /// </remarks>
      public static TFloat Atan2Ccw(TFloat y, TFloat x)
      {
        var xx = double.CreateChecked(x);
        var yy = double.CreateChecked(y);

        var atan2 = double.Atan2(yy, xx); // Call Atan2 as usual, which means 0 is at 3 o'clock and rotating counter-clockwise.

        if (atan2 < 0)// The positive range is already 0..+Pi, if so, no adjustments needed.
          atan2 = (atan2 + double.Tau) % double.Tau; // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full counter-clock-wise turn (0..+Tau) starting at 3 o'clock.

        return TFloat.CreateChecked(atan2);
      }

      /// <summary>
      /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being noon and rotating clockwise.</para>
      /// <para><seealsoww href="https://en.wikipedia.org/wiki/Atan2"/></para>
      /// </summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      /// <remarks>
      /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
      /// <para>This the reverse rotation and 90 degree offset is done by passing (x, y) rather than (y, x) into <see cref="double.Atan2(double, double)"/>.</para>
      /// </remarks>
      public static TFloat Atan2Cw(TFloat y, TFloat x)
      {
        var xx = double.CreateChecked(x);
        var yy = double.CreateChecked(y);

        var atan2 = double.Atan2(xx, yy); // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.

        if (atan2 < 0)  // The positive range is already 0..+Pi, if so, no adjustments needed.
          atan2 = (atan2 + double.Tau) % double.Tau; // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full clock-wise turn (0..+Tau) starting at noon.

        return TFloat.CreateChecked(atan2);
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IHyperbolicFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      #region Gudermannian

      /// <summary>Returns the Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
      public static TFloat Gd(TFloat x)
        => TFloat.Atan(TFloat.Sinh(x));

      /// <summary>Returns the inverse Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
      /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
      /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
      public static TFloat Agd(TFloat y)
        => TFloat.Asinh(TFloat.Tan(y));

      #endregion
    }
  }
}
