namespace Flux
{
  public static class TrigonometricFunctions
  {
    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {
      public TFloat RadianToPercentSlope()
        => TFloat.Tan(x);
    }

    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>Returns the cotangent of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public TFloat Cot()
        => TFloat.One / TFloat.Tan(x);

      /// <summary>Returns the secant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public TFloat Sec()
        => TFloat.One / TFloat.Cos(x);

      /// <summary>Returns the cosecant of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
      public TFloat Csc()
        => TFloat.One / TFloat.Sin(x);

      /// <summary>Returns the versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Vsin()
        => TFloat.One - TFloat.Cos(x);

      /// <summary>Returns the versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Vcos()
        => TFloat.One + TFloat.Cos(x);

      /// <summary>Returns the coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Cvsin()
        => TFloat.One - TFloat.Sin(x);

      /// <summary>Returns the coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Cvcos()
        => TFloat.One + TFloat.Sin(x);

      /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Hvsin()
        => (TFloat.One - TFloat.Cos(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Hvcos()
        => (TFloat.One + TFloat.Cos(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the hacoversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Hcvsin()
        => (TFloat.One - TFloat.Sin(x)) / TFloat.CreateChecked(2);

      /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
      public TFloat Hcvcos()
        => (TFloat.One + TFloat.Sin(x)) / TFloat.CreateChecked(2);
    }

    extension<TFloat>(TFloat y)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>
      /// <para>Implementation of Atan2(y, x) resulting in [-Pi, +Pi].</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      public TFloat Atan2(TFloat x)
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
      public TFloat Atan2Ccw(TFloat x)
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
      public TFloat Atan2Cw(TFloat x)
      {
        var xx = double.CreateChecked(x);
        var yy = double.CreateChecked(y);

        var atan2 = double.Atan2(xx, yy); // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.

        if (atan2 < 0)  // The positive range is already 0..+Pi, if so, no adjustments needed.
          atan2 = (atan2 + double.Tau) % double.Tau; // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full clock-wise turn (0..+Tau) starting at noon.

        return TFloat.CreateChecked(atan2);
      }
    }

    extension<TFloat>(TFloat y)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>
      /// <para>Returns the inverse cotangent of the specified angle, range <c>[-PI/2, PI/2]</c> (matches <c>Atan(1/x)</c>).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
      /// </summary>
      public TFloat Acot1()
        => TFloat.IsZero(y) ? y : TFloat.Atan(TFloat.One / y);

      /// <summary>
      /// <para>Returns the inverse cotangent of the specified angle, range <c>[0, PI]</c>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
      /// </summary>
      public TFloat Acot2()
        => TFloat.Pi / TFloat.CreateChecked(2) - TFloat.Atan(y);

      /// <summary>
      /// <para>Returns the inverse secant of the specified angle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// </summary>
      public TFloat Asec()
        => TFloat.Acos(TFloat.One / y);

      /// <summary>
      /// <para>Returns the inverse cosecant of the specified angle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
      /// </summary>
      public TFloat Acsc()
        => TFloat.Asin(TFloat.One / y);

      /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Avsin()
        => TFloat.Acos(TFloat.One - y);

      /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Avcos()
        => TFloat.Acos(y - TFloat.One);

      /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Acvsin()
        => TFloat.Asin(TFloat.One - y);

      /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Acvcos()
        => TFloat.Asin(y - TFloat.One);

      /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Ahvsin()
        => TFloat.Acos(TFloat.One - TFloat.CreateChecked(2) * y); // An extra subtraction saves a call to the Sqrt function: 2 * double.Asin(double.Sqrt(y));

      /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Ahvcos()
        => TFloat.Acos(TFloat.CreateChecked(2) * y - TFloat.One); // An extra subtraction saves a call to the Sqrt function: 2 * double.Acos(double.Sqrt(y));

      /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Ahcvsin()
        => TFloat.Asin(TFloat.One - TFloat.CreateChecked(2) * y);

      /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
      public TFloat Ahcvcos()
        => TFloat.Asin(TFloat.CreateChecked(2) * y - TFloat.One);
    }
  }
}
