namespace Flux.Units
{
  /// <summary>
  /// <para>Plane angle, unit of radian. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Angle"/></para>
  /// </summary>
  public readonly partial record struct Angle
    : System.IComparable, System.IComparable<Angle>, System.IFormattable, ISiUnitValueQuantifiable<double, AngleUnit>
  {
    public static readonly Angle FullTurn = new(double.Tau);
    public static readonly Angle HalfTurn = new(double.Pi);
    public static readonly Angle QuarterTurn = new(double.Pi / 2);

    public static readonly Angle Zero;

    /// <summary>Angle in radians.</summary>
    private readonly double m_value;

    public Angle(double value, AngleUnit unit = AngleUnit.Radian) => m_value = ConvertFromUnit(unit, value);

    public Angle(MetricPrefix prefix, double radian) => m_value = prefix.ConvertPrefix(radian, MetricPrefix.Unprefixed);

    public double InDegrees => double.RadiansToDegrees(m_value);

    public AngleNames GetAngleNames()
    {
      var angleNames = AngleNames.Unknown;

      if (AngleNames.ZeroAngle.IsNamedAngle(this)) angleNames |= AngleNames.ZeroAngle;
      if (AngleNames.AcuteAngle.IsNamedAngle(this)) angleNames |= AngleNames.AcuteAngle;
      if (AngleNames.RightAngle.IsNamedAngle(this)) angleNames |= AngleNames.RightAngle;
      if (AngleNames.ObtuseAngle.IsNamedAngle(this)) angleNames |= AngleNames.ObtuseAngle;
      if (AngleNames.StraightAngle.IsNamedAngle(this)) angleNames |= AngleNames.StraightAngle;
      if (AngleNames.ReflexAngle.IsNamedAngle(this)) angleNames |= AngleNames.ReflexAngle;
      if (AngleNames.PerigonAngle.IsNamedAngle(this)) angleNames |= AngleNames.PerigonAngle;
      if (AngleNames.ObliqueAngle.IsNamedAngle(this)) angleNames |= AngleNames.ObliqueAngle;

      return angleNames;
    }

    #region Static methods

    #region Conversion methods
    public static double ConvertAngleToPercentSlope(double radAngle)
      => double.Tan(radAngle);

    /// <summary>
    /// <para>Convert decimal degrees to the traditional sexagsimal unit subdivisions, a.k.a. DMS notation.</para>
    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="decimalDegrees"></param>
    /// <returns></returns>
    public static (int degrees, double decimalMinutes, int minutes, double decimalSeconds) ConvertDecimalDegreesToSexagesimalUnitSubdivisions(double decimalDegrees)
    {
      var absDegrees = double.Abs(decimalDegrees);
      var floorAbsDegrees = double.Floor(absDegrees);
      var degrees = double.CopySign(floorAbsDegrees, decimalDegrees);
      var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);
      var absMinutes = double.Abs(decimalMinutes);
      var minutes = double.Floor(absMinutes);
      var decimalSeconds = 60 * (absMinutes - minutes);

      return (int.CreateChecked(degrees), decimalMinutes, int.CreateChecked(minutes), decimalSeconds);
    }

    /// <summary>
    /// <para>Convert the traditional sexagsimal unit subdivisions, a.k.a. DMS notation, to decimal degrees.</para>
    /// <para>One degree is divided into 60 minutes (of arc), a.k.a. arcminutes, and one minute into 60 seconds (of arc), a.k.a. arcseconds, represented by degree sign, single prime and double prime.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="degrees"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static double ConvertSexagesimalUnitSubdivisionsToDecimalDegrees(double degrees, double minutes, double seconds)
      => degrees + minutes / 60d + seconds / 3600d;

    ///// <summary>Convert the angle specified in arcminutes to radians.</summary>
    //public static double ConvertArcminuteToRadian(double arcminAngle) => arcminAngle / 3437.7467707849396;

    ///// <summary>Convert the angle specified in arcseconds to radians.</summary>
    //public static double ConvertArcsecondToRadian(double arcsecAngle) => arcsecAngle / 206264.80624709636;

    ///// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (degrees, decimalMinutes), e.g. (32, 13.3).</summary>
    //public static (int degrees, double minutes) ConvertDecimalDegreesToDm(double decimalDegrees)
    //{
    //  var absDegrees = double.Abs(decimalDegrees);
    //  var floorAbsDegrees = System.Convert.ToInt32(double.Floor(absDegrees));

    //  var degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
    //  var decimalMinutes = 60 * (absDegrees - floorAbsDegrees);

    //  return (degrees, decimalMinutes);
    //}

    ///// <summary>Converts a <paramref name="decimalDegrees"/>, e.g. 32.221667, to sexagesimal unit subdivisions (degrees, minutes, decimalSeconds), e.g. (32, 13, 18), and returns the <paramref name="decimalMinutes"/>, e.g. 13.3, as an out parameter.</summary>
    //public static (int degrees, int minutes, double seconds) ConvertDecimalDegreesToDms(double decimalDegrees, out double decimalMinutes)
    //{
    //  (var degrees, decimalMinutes) = ConvertDecimalDegreesToDm(decimalDegrees);

    //  var (minutes, decimalSeconds) = ConvertDecimalMinutesToMs(decimalMinutes);

    //  return (degrees, minutes, decimalSeconds);
    //}

    ///// <summary>Converts a <paramref name="decimalMinutes"/>, e.g. 13.3, to sexagesimal unit subdivisions (minutes, decimalSeconds), e.g. (13, 18).</summary>
    //private static (int minutes, double seconds) ConvertDecimalMinutesToMs(double decimalMinutes)
    //{
    //  var absMinutes = double.Abs(decimalMinutes);

    //  var minutes = System.Convert.ToInt32(double.Floor(absMinutes));
    //  var decimalSeconds = 60 * (absMinutes - minutes);

    //  return (minutes, decimalSeconds);
    //}

    //public static (double decimalDegrees, double degrees, double decimalMinutes, double minutes, double decimalSeconds) ConvertDecimalDegreeToSexagesimalDegree(double decimalDegrees)
    //{
    //  var absDegrees = double.Abs(decimalDegrees);
    //  var floorAbsDegrees = double.Floor(absDegrees);
    //  var fractionAbsDegrees = absDegrees - floorAbsDegrees;

    //  var degrees = double.Sign(decimalDegrees) * floorAbsDegrees;
    //  var decimalMinutes = 60 * fractionAbsDegrees;
    //  var minutes = double.Floor(decimalMinutes);
    //  var decimalSeconds = 3600 * fractionAbsDegrees - 60 * minutes;

    //  return (decimalDegrees, System.Convert.ToInt32(degrees), decimalMinutes, System.Convert.ToInt32(minutes), decimalSeconds);
    //}

    ///// <summary>Convert the angle specified in degrees to gradians (grads).</summary>
    //public static double ConvertDegreeToGradian(double degAngle) => degAngle * (10.0 / 9.0);

    ///// <summary>Convert the angle specified in degrees to radians.</summary>
    //public static double ConvertDegreeToRadian(double degAngle) => degAngle * (double.Pi / 180);

    //public static double ConvertDegreeToTurn(double degAngle) => degAngle / 360;

    ///// <summary>Convert the angle specified in gradians (grads) to degrees.</summary>
    //public static double ConvertGradianToDegree(double gradAngle) => gradAngle * 0.9;

    ///// <summary>Convert the angle specified in gradians (grads) to radians.</summary>
    //public static double ConvertGradianToRadian(double gradAngle) => gradAngle * (double.Pi / 200);

    //public static double ConvertGradianToTurn(double gradAngle) => gradAngle / 400;

    //public static double ConvertMilliradianToRadian(double milliradAngle) => milliradAngle * 1000;

    //public static double ConvertNatoMilToRadian(double milAngle) => milAngle * double.Pi / 3200;

    ///// <summary>Convert the angle specified in radians to arcminutes.</summary>
    //public static double ConvertRadianToArcminute(double radAngle) => radAngle * 3437.7467707849396;

    ///// <summary>Convert the angle specified in radians to arcseconds.</summary>
    //public static double ConvertRadianToArcsecond(double radAngle) => radAngle * 206264.80624709636;

    ///// <summary>Convert the angle specified in radians to degrees.</summary>
    //public static double ConvertRadianToDegree(double radAngle) => radAngle * (180 / double.Pi);

    ///// <summary>Convert the angle specified in radians to gradians (grads).</summary>
    //public static double ConvertRadianToGradian(double radAngle) => radAngle * (200 / double.Pi);

    //public static double ConvertRadianToMilliradian(double radAngle) => radAngle / 1000;

    //public static double ConvertRadianToNatoMil(double radAngle) => radAngle * 3200 / double.Pi;

    //public static double ConvertRadianToTurn(double radAngle) => radAngle / double.Tau;

    //public static double ConvertRadianToWarsawPactMil(double radAngle) => radAngle * 3000 / double.Pi;

    //public static double ConvertSexagesimalDegreeToDecimalDegree(double degrees, double minutes, double seconds)
    //  => degrees + minutes / 60 + seconds / 3600;

    //public static double ConvertTurnToRadian(double revolutions) => revolutions * double.Tau;

    //public static double ConvertWarsawPactMilToRadian(double milAngle) => milAngle * double.Pi / 3000;

    public static bool TryConvertFormatToDmsNotation(string? format, out AngleDmsNotation result)
    {
      if (!string.IsNullOrWhiteSpace(format))
      {
        if (format.StartsWith(AngleDmsNotation.DegreesMinutesDecimalSeconds.GetAcronym()))
        {
          result = AngleDmsNotation.DegreesMinutesDecimalSeconds;
          return true;
        }

        if (format.StartsWith(AngleDmsNotation.DegreesDecimalMinutes.GetAcronym()))
        {
          result = AngleDmsNotation.DegreesDecimalMinutes;
          return true;
        }

        if (format.StartsWith(AngleDmsNotation.DecimalDegrees.GetAcronym()))
        {
          result = AngleDmsNotation.DecimalDegrees;
          return true;
        }
      }

      result = default;
      return false;
    }

    #endregion // Conversion methods

    /// <summary>
    /// <para>Ensure <paramref name="angle"/> is an azimuth, i.e. a value in the interval [0, 360). Note that 360 as a value is excluded and is represented as 0.</para>
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [0, 360)) are wrapped, i.e. 370 = 10, -10 = 350, etc.</remarks>
    public static Angle AsAzimuth(Angle angle) => new(Number.WrapAround(angle.Value, 0, double.Tau) % double.Tau);

    /// <summary>
    /// <para>Creates an azimuth <see cref="Angle"/> from <paramref name="value"/> and <paramref name="unit"/>, i.e. a value in the interval [0, 360].</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [0, 360] are wrapped, i.e. 370 = 10, -10 = 350, etc.</remarks>
    public static Angle AsAzimuth(double value, AngleUnit unit) => AsAzimuth(new(value, unit));

    /// <summary>
    /// <para>Ensure <paramref name="angle"/> is a latitude, i.e. a value in the interval [-90, +90].</para>
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [-90, +90] are folded, i.e. +100 = +80, -100 = -80, etc.</remarks>
    public static Angle AsLatitude(Angle angle) => new(Number.FoldAcross(angle.Value, double.Pi / -2, double.Pi / 2));

    /// <summary>
    /// <para>Creates a longitude <see cref="Angle"/> from <paramref name="value"/> and <paramref name="unit"/>, i.e. a value in the interval [-90, +90].</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [-90, +90] are folded, i.e. +100 = +80, -100 = -80, etc.</remarks>
    public static Angle AsLatitude(double value, AngleUnit unit) => AsLatitude(new(value, unit));

    /// <summary>
    /// <para>Ensure <paramref name="angle"/> is a longitude, i.e. a value in the interval [-180, +180].</para>
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [-180, +180] are wrapped, i.e. 190 = -170, -190 = +170, etc.</remarks>
    public static Angle AsLongitude(Angle angle) => new(IntervalNotation.Closed.Wrap(angle.Value, -double.Pi, double.Pi));

    /// <summary>
    /// <para>Creates a longitude <see cref="Angle"/> from <paramref name="value"/> and <paramref name="unit"/>, i.e. a value in the interval [-180, +180].</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    /// <remarks>Values outside the interval [-180, +180] are wrapped, i.e. 190 = -170, -190 = +170, etc.</remarks>
    public static Angle AsLongitude(double value, AngleUnit unit) => AsLongitude(new(value, unit));

    #region Trigonometry static methods

    //#region Gudermannian

    ///// <summary>Returns the Gudermannian of the specified value.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    //public static double Gd(double value) => double.Atan(double.Sinh(value));

    //// Inverse function:

    ///// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    ///// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    ///// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    //public static double Agd(double value) => double.Atanh(double.Sin(value));

    //#endregion Gudermannian

    //#region Atan2 options

    ///// <summary>
    ///// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being 3 o'clock and rotating counter-clockwise.</para>
    ///// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
    ///// </summary>
    ///// <param name="y"></param>
    ///// <param name="x"></param>
    ///// <returns></returns>
    ///// <remarks>
    ///// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
    ///// <para>This uses <see cref="double.Atan2(double, double)"/> in the traditional sense, but without any negative return values.</para>
    ///// </remarks>
    //public static double Atan2Ccw(double y, double x)
    //  => double.Atan2(y, x) is var atan2 && atan2 < 0 // Call Atan2 as usual, which means 0 is at 3 o'clock and rotating counter-clockwise.
    //  ? (atan2 + double.Tau) % double.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
    //  : atan2; // The positive range is already 0..+Pi, so return it.

    ///// <summary>
    ///// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being noon and rotating clockwise.</para>
    ///// <para><seealsoww href="https://en.wikipedia.org/wiki/Atan2"/></para>
    ///// </summary>
    ///// <param name="y"></param>
    ///// <param name="x"></param>
    ///// <returns></returns>
    ///// <remarks>
    ///// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
    ///// <para>This the reverse rotation and 90 degree offset is done by passing (x, y) rather than (y, x) into <see cref="double.Atan2(double, double)"/>.</para>
    ///// </remarks>
    //public static double Atan2Cw(double y, double x)
    //  => double.Atan2(x, y) is var atan2s && atan2s < 0 // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.
    //  ? (atan2s + double.Tau) % double.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
    //  : atan2s; // The positive range is already 0..+Pi, so return it.

    //#endregion // Atan2 options

    //#region Hyperbolic Reciprocals/Inverse

    //// Hyperbolic reciprocals (1 divided by):

    ///// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    //public static double Csch(double v) => 1 / double.Sinh(v);

    ///// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    //public static double Sech(double v) => 1 / double.Cosh(v);

    ///// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    //public static double Coth(double v) => double.Cosh(v) / double.Sinh(v);

    //// Inverse hyperbolic reciprocals:

    ///// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    //public static double Acsch(double v) => double.Asinh(1 / v); // Cheaper versions than using Log and Sqrt functions: double.Log(1 / x + double.Sqrt(1 / x * x + 1));

    ///// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    //public static double Asech(double v) => double.Acosh(1 / v); // Cheaper versions than using Log and Sqrt functions: double.Log((1 + double.Sqrt(1 - x * x)) / x);

    ///// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    //public static double Acoth(double v) => double.Atanh(1 / v); // Cheaper versions than using log functions: double.Log((x + 1) / (x - 1)) / 2;

    //#endregion Hyperbolic Reciprocals/Inverse

    //#region Reciprocals/Inverse

    //// Reciprocals (1 divided by):

    ///// <summary>Returns the cosecant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    //public static double Csc(double v) => 1 / double.Sin(v);
    ///// <summary>Returns the secant of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    //public static double Sec(double v) => 1 / double.Cos(v);

    ///// <summary>Returns the cotangent of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    //public static double Cot(double v) => 1 / double.Tan(v);

    //// Inverse reciprocals:

    ///// <summary>
    ///// <para>Returns the inverse cosecant of the specified angle.</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
    ///// </summary>
    //public static double Acsc(double v) => double.Asin(1 / v);

    ///// <summary>
    ///// <para>Returns the inverse secant of the specified angle.</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
    ///// </summary>
    //public static double Asec(double v) => double.Acos(1 / v);

    ///// <summary>
    ///// <para>Returns the inverse cotangent of the specified angle, range [-PI/2, PI/2] (matches Atan(1/x)).</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
    ///// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
    ///// </summary>
    //public static double AcotSymmetrical(double v) => double.Atan2(1, v);

    ///// <summary>
    ///// <para>Returns the inverse cotangent of the specified angle, range [0, PI].</para>
    ///// <para><see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/></para>
    ///// <para><seealso href="https://stackoverflow.com/a/15501536/3178666"/></para>
    ///// </summary>
    //public static double AcotAbsolute(double v) => double.Pi / 2 - double.Atan(v);

    //#endregion Reciprocals/Inverse

    //#region Sinc

    ///// <summary>Returns the normalized sinc of the specified value.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    //public static double Sincn(double value) => Sincu(double.Pi * value);

    ///// <summary>Returns the unnormalized sinc of the specified value.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    //public static double Sincu(double value) => value != 0 ? double.Sin(value) / value : 1;

    //#endregion Sinc

    //#region Versed/Inverse

    //// Versed functions.

    ///// <summary>Returns the versed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Vsin(double value) => 1 - double.Cos(value);
    ///// <summary>Returns the versed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Vcos(double value) => 1 + double.Cos(value);
    ///// <summary>Returns the coversed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Cvsin(double value) => 1 - double.Sin(value);
    ///// <summary>Returns the coversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Cvcos(double value) => 1 + double.Sin(value);

    //// Inverse versed functions:

    ///// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Avsin(double y) => double.Acos(1 - y);
    ///// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Avcos(double y) => double.Acos(y - 1);
    ///// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Acvsin(double y) => double.Asin(1 - y);
    ///// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Acvcos(double y) => double.Asin(y - 1);

    //#endregion Versed/Inverse

    //#region Haversed/Inverse

    //// Haversed functions (half of the versed versions above):

    ///// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Hvsin(double value) => (1 - double.Cos(value)) / 2;
    ///// <summary>Returns the haversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Hvcos(double value) => (1 + double.Cos(value)) / 2;
    ///// <summary>Returns the hacoversed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Hcvsin(double value) => (1 - double.Sin(value)) / 2;
    ///// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    //public static double Hcvcos(double value) => (1 + double.Sin(value)) / 2;

    //// Inversed haversed functions:

    ///// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Ahvsin(double y) => double.Acos(1 - 2 * y); // An extra subtraction saves a call to the Sqrt function: 2 * double.Asin(double.Sqrt(y));
    ///// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Ahvcos(double y) => double.Acos(2 * y - 1); // An extra subtraction saves a call to the Sqrt function: 2 * double.Acos(double.Sqrt(y));
    ///// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Ahcvsin(double y) => double.Asin(1 - 2 * y);
    ///// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    //public static double Ahcvcos(double y) => double.Asin(2 * y - 1);

    //#endregion Versine/Haversine

    #endregion Trigonometry static methods

    [System.Text.RegularExpressions.GeneratedRegex(@"(?<Degrees>\d*\.?\d+\s*[\u00B0\u02DA\u030A]?)\s*(?<Minutes>\d*\.?\d+\s*[\u2032\u0027\u02B9\u00B4])?\s*(?<Seconds>\d*\.?\d+\s*[\u2033\u0022\u02BA\u301E\u201D\u3003])?\s*(?<Direction>[ENWS])?")]
    private static partial System.Text.RegularExpressions.Regex ParseDmsNotationRegex();

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="dmsNotation"></param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static System.Collections.Generic.List<IValueQuantifiable<double>> ParseDmsNotations(string dmsNotation)
    {
      var list = new System.Collections.Generic.List<IValueQuantifiable<double>>();

      var evm = ParseDmsNotationRegex().EnumerateMatches(dmsNotation);

      foreach (var vm in evm)
      {
        System.Console.WriteLine($"({vm.Index}, {vm.Length}) = '{dmsNotation.AsSpan().Slice(vm.Index, vm.Length).ToStringBuilder().RemoveAll(char.IsWhiteSpace).ToString()}'");

        if (ParseDmsNotationRegex().Match(dmsNotation, vm.Index, vm.Length) is var m && m.Success)
        {
          var decimalDegrees = 0.0;

          if (m.Groups["Degrees"] is var g1 && g1.Success && double.TryParse(g1.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var degrees))
            decimalDegrees += degrees;

          if (m.Groups["Minutes"] is var g2 && g2.Success && double.TryParse(g2.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var minutes))
            decimalDegrees += minutes / 60;

          if (m.Groups["Seconds"] is var g3 && g3.Success && double.TryParse(g3.Value.AsSpan().TrimCommonSuffix(c => !char.IsDigit(c)), out var seconds))
            decimalDegrees += seconds / 3600;

          if (m.Groups["Direction"] is var g4 && g4.Success && g4.Length == 1 && g4.Value[0] is 'S' or 'W')
            decimalDegrees = -decimalDegrees;

          if (g4.Success && (g4.Value[0] is 'N' or 'S'))
            list.Add(new PlanetaryScience.Latitude(decimalDegrees));
          else if (g4.Success && (g4.Value[0] is 'E' or 'W'))
            list.Add(new PlanetaryScience.Longitude(decimalDegrees));
          else
            throw new System.InvalidOperationException();
        }
      }

      return list;
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="decimalDegrees"></param>
    /// <param name="dmsNotation"></param>
    /// <param name="axis"></param>
    /// <param name="decimalPoints"></param>
    /// <param name="componentSpacing"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static string ToStringDmsNotation(double decimalDegrees, AngleDmsNotation dmsNotation, PlanetaryScience.CompassCardinalAxis axis, int decimalPoints = -1, UnicodeSpacing componentSpacing = UnicodeSpacing.None)
    {
      var (degrees, decimalMinutes, minutes, decimalSeconds) = ConvertDecimalDegreesToSexagesimalUnitSubdivisions(decimalDegrees);

      var spacingString = componentSpacing.ToSpacingString();

      var directional = axis.ToCardinalDirection(degrees < 0).ToString();

      decimalPoints = decimalPoints >= 0 && decimalPoints <= 15 ? decimalPoints : dmsNotation switch
      {
        AngleDmsNotation.DecimalDegrees => 4,
        AngleDmsNotation.DegreesDecimalMinutes => 2,
        AngleDmsNotation.DegreesMinutesDecimalSeconds => 0,
        _ => throw new NotImplementedException(),
      };

      return dmsNotation switch
      {
        AngleDmsNotation.DecimalDegrees
          => new Units.Angle(double.Abs(decimalDegrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, $"N{decimalPoints}", null) + spacingString + directional, // Show as decimal degrees.
        AngleDmsNotation.DegreesDecimalMinutes
          => new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null) + spacingString + new Units.Angle(decimalMinutes, Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, $"N{decimalPoints}", null) + spacingString + directional, // Show as degrees and decimal minutes.
        AngleDmsNotation.DegreesMinutesDecimalSeconds
          => new Units.Angle(double.Abs(degrees), Units.AngleUnit.Degree).ToUnitString(Units.AngleUnit.Degree, "N0", null) + spacingString + new Units.Angle(double.Abs(minutes), Units.AngleUnit.Arcminute).ToUnitString(Units.AngleUnit.Arcminute, "N0", null) + spacingString + new Units.Angle(decimalSeconds, Units.AngleUnit.Arcsecond).ToUnitString(Units.AngleUnit.Arcsecond, $"N{decimalPoints}", null) + spacingString + directional, // Show as degrees, minutes and decimal seconds.
        _
          => throw new System.ArgumentOutOfRangeException(nameof(dmsNotation)),
      };
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/ISO_6709"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Geographic_coordinate_conversion#Change_of_units_and_format"/></para>
    /// </summary>
    /// <param name="dmsNotation"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParseDmsNotations(string dmsNotation, out System.Collections.Generic.List<IValueQuantifiable<double>> result)
    {
      try
      {
        result = ParseDmsNotations(dmsNotation);
        return true;
      }
      catch
      {
        result = default!;
        return false;
      }
    }

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
    public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
    public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) >= 0;

    public static Angle operator -(Angle v) => new(-v.m_value);
    public static Angle operator *(Angle a, Angle b) => new(a.m_value * b.m_value);
    public static Angle operator /(Angle a, Angle b) => new(a.m_value / b.m_value);
    public static Angle operator %(Angle a, Angle b) => new(a.m_value % b.m_value);
    public static Angle operator +(Angle a, Angle b) => new(a.m_value + b.m_value);
    public static Angle operator -(Angle a, Angle b) => new(a.m_value - b.m_value);
    public static Angle operator *(Angle a, double b) => new(a.m_value * b);
    public static Angle operator /(Angle a, double b) => new(a.m_value / b);
    public static Angle operator %(Angle a, double b) => new(a.m_value % b);
    public static Angle operator +(Angle a, double b) => new(a.m_value + b);
    public static Angle operator -(Angle a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Angle o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Angle other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AngleUnit.Radian.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AngleUnit unit, double value)
      => unit switch
      {
        AngleUnit.Radian => value,

        AngleUnit.Degree => double.DegreesToRadians(value),
        AngleUnit.Gradian => value * double.Pi / 200,
        AngleUnit.NatoMil => value * double.Pi / 3200,
        AngleUnit.WarsawPactMil => value * double.Pi / 3000,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AngleUnit unit, double value)
      => unit switch
      {
        AngleUnit.Radian => value,

        AngleUnit.Degree => double.RadiansToDegrees(value),
        AngleUnit.Gradian => value * 200 / double.Pi,
        AngleUnit.NatoMil => value * 3200 / double.Pi,
        AngleUnit.WarsawPactMil => value * 3000 / double.Pi,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AngleUnit from, AngleUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AngleUnit unit)
      => ConvertToUnit(unit, m_value);

    public string ToUnitString(AngleUnit unit = AngleUnit.Radian, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
          + (fullName
          ? spacing.ToSpacingString() + unit.GetUnitName(Number.IsConsideredPlural(value))
          : (unit.HasUnitSpacing(true) ? spacing : UnicodeSpacing.None).ToSpacingString() + unit.GetUnitSymbol(true)
          );
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Angle.Value"/> property is in <see cref="AngleUnit.Radian"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    //public string ToVerboseString(string? format, System.IFormatProvider? formatProvider)
    //  => Value.ToString(format, formatProvider) + " = " + ToUnitValueSymbolString(AngleUnit.Radian, format, formatProvider) + " = " + ToUnitValueSymbolString(AngleUnit.Degree, format, formatProvider);

    public override string ToString() => ToString(null, null);
  }
}
