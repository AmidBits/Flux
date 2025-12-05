namespace Flux
{
  public static partial class PlanetaryScienceExtensions
  {
    /// <summary>
    /// <para>Gets the <see cref="Temporal.JulianDate"/> for the <see cref="LunarPhase"/> <paramref name="lunarPhase"/> in the synodic month of the <see cref="System.DateTime"/> <paramref name="source"/>.</para>
    /// <para><see href="http://www.fourmilab.ch/earthview/pacalc.html"/></para>
    /// <para><seealso href="https://stackoverflow.com/a/2531541/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="lunarPhase"></param>
    /// <returns></returns>
    public static System.DateTime LunarTruePhase(this System.DateTime source, PlanetaryScience.LunarPhase lunarPhase, out Temporal.JulianDate julianDate)
    {
      julianDate = new(source.LunarTruePhase(lunarPhase.GetUnitPhase()));

      return new System.DateTime(1900, 1, 1, 6, 13, 26, DateTimeKind.Utc).AddDays(julianDate.Value - 2415020.75933);
    }

    /// <summary>
    /// <para>Gets a JulianDate (JD) for the <paramref name="lunarPhase"/> { 0.0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875 } in the synodic month of the <see cref="System.DateTime"/> <paramref name="source"/>.</para>
    /// <para><see href="http://www.fourmilab.ch/earthview/pacalc.html"/></para>
    /// <para><seealso href="https://stackoverflow.com/a/2531541/3178666"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="lunarPhase">The lunar-phases are floating point fractions of the unit interval [0, 1), i.e. { 0.0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875 }.</param>
    /// <returns></returns>
    public static double LunarTruePhase(this System.DateTime source, double lunarPhase)
    {
      var daysInYear = System.DateTime.IsLeapYear(source.Year) ? 366d : 365d;

      var ks = double.Floor((source.Year - 1900 + source.DayOfYear / daysInYear) * 12.3685);

      var k = ks + lunarPhase; // Add phase to new moon time.
      var t = k / 1236.85; // Time in Julian centuries from 1900 January 0.5.

      var t2 = t * t; // Square for frequent use.
      var t3 = t2 * t; // Cube for frequent use.

      var pt = 2415020.75933 // Mean time of phase.
        + 29.53058868 * k // Synodic month (mean time from new to next new Moon).
        + 0.0001178 * t2
        - 0.000000155 * t3
        + 0.00033 * Dsin(166.56 + 132.87 * t - 0.009173 * t2);

      var m = 359.2242 // Sun's mean anomaly.
        + 29.10535608 * k
        - 0.0000333 * t2
        - 0.00000347 * t3;

      var mprime = 306.0253 // Moon's mean anomaly.
        + 385.81691806 * k
        + 0.0107306 * t2
        + 0.00001236 * t3;

      var f = 21.2964 // Moon's argument of latitude.
        + 390.67050646 * k
        - 0.0016528 * t2
        - 0.00000239 * t3;

      if ((lunarPhase < 0.01) || (double.Abs(lunarPhase - 0.5) < 0.01)) // Corrections for New and Full Moon.
      {
        pt += (0.1734 - 0.000393 * t) * Dsin(m)
          + 0.0021 * Dsin(2 * m)
          - 0.4068 * Dsin(mprime)
          + 0.0161 * Dsin(2 * mprime)
          - 0.0004 * Dsin(3 * mprime)
          + 0.0104 * Dsin(2 * f)
          - 0.0051 * Dsin(m + mprime)
          - 0.0074 * Dsin(m - mprime)
          + 0.0004 * Dsin(2 * f + m)
          - 0.0004 * Dsin(2 * f - m)
          - 0.0006 * Dsin(2 * f + mprime)
          + 0.0010 * Dsin(2 * f - mprime)
          + 0.0005 * Dsin(m + 2 * mprime);
      }
      else if (double.Abs(lunarPhase - 0.25) < 0.01 || double.Abs(lunarPhase - 0.75) < 0.01)
      {
        pt += (0.1721 - 0.0004 * t) * Dsin(m)
          + 0.0021 * Dsin(2 * m)
          - 0.6280 * Dsin(mprime)
          + 0.0089 * Dsin(2 * mprime)
          - 0.0004 * Dsin(3 * mprime)
          + 0.0079 * Dsin(2 * f)
          - 0.0119 * Dsin(m + mprime)
          - 0.0047 * Dsin(m - mprime)
          + 0.0003 * Dsin(2 * f + m)
          - 0.0004 * Dsin(2 * f - m)
          - 0.0006 * Dsin(2 * f + mprime)
          + 0.0021 * Dsin(2 * f - mprime)
          + 0.0003 * Dsin(m + 2 * mprime)
          + 0.0004 * Dsin(m - 2 * mprime)
          - 0.0003 * Dsin(2 * m + mprime);

        if (lunarPhase < 0.5) // First quarter correction.
          pt += 0.0028 - 0.0004 * Dcos(m) + 0.0003 * Dcos(mprime);
        else // Last quarter correction.
          pt += -0.0028 + 0.0004 * Dcos(m) - 0.0003 * Dcos(mprime);
      }

      return pt;

      static double Dsin(double deg) => double.Sin(double.DegreesToRadians(deg));

      static double Dcos(double deg) => double.Cos(double.DegreesToRadians(deg));
    }

    ///// <summary>
    ///// <para>Gets the date for the <paramref name="lunarPhase"/> in the cycle of the <paramref name="source"/> <see cref="System.DateTime"/>.</para>
    ///// <para><see href="http://www.fourmilab.ch/earthview/pacalc.html"/></para>
    ///// <para><seealso href="https://stackoverflow.com/a/2531541/3178666"/></para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <param name="lunarPhase"></param>
    ///// <returns></returns>
    //public static System.DateTime GetLunarPhaseStartDateInSynodicMonth(this System.DateTime source, LunarPhase lunarPhase, out Quantities.JulianDate julianDate)
    //{
    //  var ks = double.Floor((source.Year - 1900 + (source.DayOfYear / (System.DateTime.IsLeapYear(source.Year) ? 366d : 365d))) * 12.3685);

    //  var k = ks + 0.125 * (int)lunarPhase;
    //  var t = k / 1236.85;  // Time in Julian centuries from "1900 January 0.5".

    //  var t2 = t * t; // Square for frequent use.
    //  var t3 = t2 * t; // Cube for frequent use.

    //  var pt = 2415020.75933 // Mean time of phase.
    //    + 29.53058868 * k // Synodic month, the mean time, in days, from a new-moon to next new-moon.
    //    + 0.0001178 * t2
    //    - 0.000000155 * t3
    //    + 0.00033 * Dsin(166.56 + 132.87 * t - 0.009173 * t2);

    //  var m = 359.2242 // Sun's mean anomaly.
    //    + 29.10535608 * k
    //    - 0.0000333 * t2
    //    - 0.00000347 * t3;

    //  var mprime = 306.0253 // Moon's mean anomaly.
    //    + 385.81691806 * k
    //    + 0.0107306 * t2
    //    + 0.00001236 * t3;

    //  var f = 21.2964 // Moon's argument of latitude.
    //    + 390.67050646 * k
    //    - 0.0016528 * t2
    //    - 0.00000239 * t3;

    //  var fmul2 = 2 * f;
    //  var mprimemul2 = 2 * mprime;

    //  var ds2mulm = Dsin(2 * m);
    //  var ds2mulmprime = Dsin(mprimemul2); //Dsin(2 * mprime);
    //  var ds3mulmprime = Dsin(3 * mprime);
    //  var ds2mulf = Dsin(fmul2); //Dsin(2 * f);
    //  var dsmaddmprime = Dsin(m + mprime);
    //  var dsmsubmprime = Dsin(m - mprime);
    //  var ds2mulfaddm = Dsin(fmul2 + m); //Dsin(2 * f + m);
    //  var ds2mulfsubm = Dsin(fmul2 - m); //Dsin(2 * f - m);
    //  var ds2mulfaddmprime = Dsin(fmul2 + mprime); //Dsin(2 * f + mprime);
    //  var ds2mulfsubmprime = Dsin(fmul2 - mprime); //Dsin(2 * f - mprime);
    //  var dsmadd2mulmprime = Dsin(m + mprimemul2); //Dsin(m + 2 * mprime);

    //  if (lunarPhase == LunarPhase.NewMoon || lunarPhase == LunarPhase.FullMoon) // Corrections for new-moon and full-moon.
    //  {
    //    pt += (0.1734 - 0.000393 * t) * Dsin(m)
    //      + 0.0021 * ds2mulm //Dsin(2 * m)
    //      - 0.4068 * Dsin(mprime)
    //      + 0.0161 * ds2mulmprime //Dsin(2 * mprime)
    //      - 0.0004 * ds3mulmprime //Dsin(3 * mprime)
    //      + 0.0104 * ds2mulf //Dsin(2 * f)
    //      - 0.0051 * dsmaddmprime //Dsin(m + mprime)
    //      - 0.0074 * dsmsubmprime //Dsin(m - mprime)
    //      + 0.0004 * ds2mulfaddm //Dsin(2 * f + m)
    //      - 0.0004 * ds2mulfsubm //Dsin(2 * f - m)
    //      - 0.0006 * ds2mulfaddmprime //Dsin(2 * f + mprime)
    //      + 0.0010 * ds2mulfsubmprime //Dsin(2 * f - mprime)
    //      + 0.0005 * dsmadd2mulmprime; //Dsin(m + 2 * mprime);
    //  }
    //  else // Calculate times of either waxing crescent, first quarter, waxing gibbous, waning gibbous, last quarter, or waning crescent.
    //  {
    //    pt += (0.1721 - 0.0004 * t) * Dsin(m)
    //      + 0.0021 * ds2mulm //Dsin(2 * m)
    //      - 0.6280 * Dsin(mprime)
    //      + 0.0089 * ds2mulmprime //Dsin(2 * mprime)
    //      - 0.0004 * ds3mulmprime //Dsin(3 * mprime)
    //      + 0.0079 * ds2mulf //Dsin(2 * f)
    //      - 0.0119 * dsmaddmprime //Dsin(m + mprime)
    //      - 0.0047 * dsmsubmprime //Dsin(m - mprime)
    //      + 0.0003 * ds2mulfaddm //Dsin(2 * f + m)
    //      - 0.0004 * ds2mulfsubm //Dsin(2 * f - m)
    //      - 0.0006 * ds2mulfaddmprime //Dsin(2 * f + mprime)
    //      + 0.0021 * ds2mulfsubmprime //Dsin(2 * f - mprime)
    //      + 0.0003 * dsmadd2mulmprime //Dsin(m + 2 * mprime)
    //      + 0.0004 * Dsin(m - 2 * mprime)
    //      - 0.0003 * Dsin(2 * m + mprime);

    //    if ((int)lunarPhase < 4)  // Waxing
    //      pt += 0.0028 - 0.0004 * Dcos(m) + 0.0003 * Dcos(mprime);
    //    else if ((int)lunarPhase > 4) // Waning
    //      pt += -0.0028 + 0.0004 * Dcos(m) - 0.0003 * Dcos(mprime);
    //  }

    //  julianDate = new Quantities.JulianDate(pt);

    //  return new System.DateTime(1900, 1, 1, 6, 13, 26, DateTimeKind.Utc).AddDays(pt - 2415020.75933);

    //  static double Dsin(double deg) => double.Sin(double.DegreesToRadians(deg));

    //  static double Dcos(double deg) => double.Cos(double.DegreesToRadians(deg));
    //}

    /// <summary>
    /// <para>Gets the lunar phase and start date for the <paramref name="source"/> <see cref="System.DateTime"/>.</para>
    /// </summary>
    /// <param name="source">UT DateTime</param>
    /// <returns></returns>
    public static (PlanetaryScience.LunarPhase LunarPhase, Temporal.JulianDate JulianDate, System.DateTime DateTime) GetLunarPhaseAndDate(this System.DateTime source)
      => (source.LunarTruePhase(PlanetaryScience.LunarPhase.NewMoon, out var jdNewMoon) is var dtNewMoon && source.CompareTo(dtNewMoon) < 0
      // Get the date from the previous waning-crescent, but only if source is before the new-moon phase of the current cycle.
      // Need to subtract enough (but not too much) time so that the time between source (which includes time) and the cutoff for the last phase of the previous cycle.
      // If the source is then greater-than-or-equal-to the previous-waning-crescent, we return that information.
      && source.AddHours(-43).LunarTruePhase(PlanetaryScience.LunarPhase.WaningCrescent, out var jdPreviousWaningCrescent) is var dtPreviousWaningCrescent && source.CompareTo(dtPreviousWaningCrescent) >= 0)
      ? (PlanetaryScience.LunarPhase.WaningCrescent, jdPreviousWaningCrescent, dtPreviousWaningCrescent)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.WaxingCrescent, out var jdWaxingCrescent) is var dtWaxingCrescent && source.CompareTo(dtNewMoon) >= 0 && source.CompareTo(dtWaxingCrescent) < 0)
      ? (PlanetaryScience.LunarPhase.NewMoon, jdNewMoon, dtNewMoon)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.FirstQuarter, out var jdFirstQuarter) is var dtFirstQuarter && source.CompareTo(dtWaxingCrescent) >= 0 && source.CompareTo(dtFirstQuarter) < 0)
      ? (PlanetaryScience.LunarPhase.WaxingCrescent, jdWaxingCrescent, dtWaxingCrescent)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.WaxingGibbous, out var jdWaxingGibbous) is var dtWaxingGibbous && source.CompareTo(dtFirstQuarter) >= 0 && source.CompareTo(dtWaxingGibbous) < 0)
      ? (PlanetaryScience.LunarPhase.FirstQuarter, jdFirstQuarter, dtFirstQuarter)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.FullMoon, out var jdFullMoon) is var dtFullMoon && source.CompareTo(dtWaxingGibbous) >= 0 && source.CompareTo(dtFullMoon) < 0)
      ? (PlanetaryScience.LunarPhase.WaxingGibbous, jdWaxingGibbous, dtWaxingGibbous)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.WaningGibbous, out var jdWaningGibbous) is var dtWaningGibbous && source.CompareTo(dtFullMoon) >= 0 && source.CompareTo(dtWaningGibbous) < 0)
      ? (PlanetaryScience.LunarPhase.FullMoon, jdFullMoon, dtFullMoon)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.LastQuarter, out var jdLastQuarter) is var dtLastQuarter && source.CompareTo(dtWaningGibbous) >= 0 && source.CompareTo(dtLastQuarter) < 0)
      ? (PlanetaryScience.LunarPhase.WaningGibbous, jdWaningGibbous, dtWaningGibbous)
      : (source.LunarTruePhase(PlanetaryScience.LunarPhase.WaningCrescent, out var jdWaningCrescent) is var dtWaningCrescent && source.CompareTo(dtLastQuarter) >= 0 && source.CompareTo(dtWaningCrescent) < 0)
      ? (PlanetaryScience.LunarPhase.LastQuarter, jdLastQuarter, dtLastQuarter)
      : (PlanetaryScience.LunarPhase.WaningCrescent, jdWaningCrescent, dtWaningCrescent);
  }

  #region MoonPA script

  /*
  // FIXANGLE  --  Range reduce angle in degrees.
  private static double fixangle(double a)
  {
    return a - 360.0 * (double.Floor(a / 360.0));
  }

  // SUMSER  --  Sum the series of periodic terms.
  private static double sumser(System.Func<double, double> trig, double D, double M, double F, double T, double[] argtab, double[] coeff, double[] tfix, double[] tfixc)
  {
    var j = 0;
    var n = 0;
    var sum = 0d;

    D = double.DegreesToRadians(fixangle(D));
    M = double.DegreesToRadians(fixangle(M));
    F = double.DegreesToRadians(fixangle(F));

    for (var i = 0; coeff[i] != 0.0; i++)
    {
      var arg = (D * argtab[j]) + (M * argtab[j + 1]) + (F * argtab[j + 2]);
      j += 3;
      var coef = coeff[i];
      if (i == tfix[n])
      {
        coef += T * tfixc[n++];
      }
      sum += coef * trig(arg);
    }

    return sum;
  }

  // MOONPA  --  Calculate perigee or apogee from index number.
  private static (double, double, double) MoonPerigeeOrApogee(int k)
  {
    var EarthRad = 6378.14;

    var t = k - double.Floor(k);

    var apogee = (t > 0.499 && t < 0.501)
          ? true
          : (t > 0.999 || t < 0.001)
          ? false
          : throw new System.ArgumentOutOfRangeException(nameof(k));

    t = k / 1325.55;

    var t2 = t * t;
    var t3 = t * t2;
    var t4 = t * t3;

    // Mean time of perigee or apogee.
    var JDE = 2451534.6698 + 27.55454989 * k
                             - 0.0006691 * t2
                             - 0.000001098 * t3
                             + 0.0000000052 * t4;

    // Mean elongation of the Moon.
    var D = 171.9179 + 335.9106046 * k
                       - 0.0100383 * t2
                       - 0.00001156 * t3
                       + 0.000000055 * t4;

    // Mean anomaly of the Sun.
    var M = 347.3477 + 27.1577721 * k
                       - 0.0008130 * t2
                       - 0.0000010 * t3;

    // Moon's argument of latitude.
    var F = 316.6109 + 364.5287911 * k
                       - 0.0125053 * t2
                       - 0.0000148 * t3;

    JDE += sumser(double.Sin, D, M, F, t,
            apogee ? apoarg : periarg, apogee ? apocoeff : pericoeff,
            apogee ? apotft : peritft, apogee ? apotfc : peritfc);

    var par = sumser(double.Cos, D, M, F, t,
            apogee ? apoparg : periparg, apogee ? apopcoeff : peripcoeff,
            apogee ? apoptft : periptft, apogee ? apoptfc : periptfc);

    par = double.DegreesToRadians(par / 3600.0);

    return (JDE, par, EarthRad / double.Sin(par));
  }


  // We define the perigee and apogee period term arrays statically to avoid re-constructing them on every invocation of moonpa.

  static double[] periarg = new double[]{
  //  D,  M,  F
      2, 0, 0,
      4, 0, 0,
      6, 0, 0,
      8, 0, 0,
      2, -1, 0,
      0, 1, 0,
     10, 0, 0,
      4, -1, 0,
      6, -1, 0,
     12, 0, 0,
      1, 0, 0,
      8, -1, 0,
     14, 0, 0,
      0, 0, 2,
      3, 0, 0,
     10, -1, 0,
     16, 0, 0,
     12, -1, 0,
      5, 0, 0,
      2, 0, 2,
     18, 0, 0,
     14, -1, 0,
      7, 0, 0,
      2, 1, 0,
     20, 0, 0,
      1, 1, 0,
     16, -1, 0,
      4, 1, 0,
      9, 0, 0,
      4, 0, 2,

      2, -2, 0,
      4, -2, 0,
      6, -2, 0,
     22, 0, 0,
     18, -1, 0,
      6, 1, 0,
     11, 0, 0,
      8, 1, 0,
      4, 0, -2,
      6, 0, 2,
      3, 1, 0,
      5, 1, 0,
     13, 0, 0,
     20, -1, 0,
      3, 2, 0,
      4, -2, 2,
      1, 2, 0,
     22, -1, 0,
      0, 0, 4,
      6, 0, -2,
      2, 1, -2,
      0, 2, 0,
      0, -1, 2,
      2, 0, 4,
      0, -2, 2,
      2, 2, -2,
     24, 0, 0,
      4, 0, -4,
      2, 2, 0,
      1, -1, 0
  };

  static double[] pericoeff = new double[]{
      -1.6769,
       0.4589,
      -0.1856,
       0.0883,
      -0.0773,
       0.0502,
      -0.0460,
       0.0422,
      -0.0256,
       0.0253,
       0.0237,
       0.0162,
      -0.0145,
       0.0129,
      -0.0112,
      -0.0104,
       0.0086,
       0.0069,
       0.0066,
      -0.0053,
      -0.0052,
      -0.0046,
      -0.0041,
       0.0040,
       0.0032,
      -0.0032,
       0.0031,
      -0.0029,
       0.0027,
       0.0027,

      -0.0027,
       0.0024,
      -0.0021,
      -0.0021,
      -0.0021,
       0.0019,
      -0.0018,
      -0.0014,
      -0.0014,
      -0.0014,
       0.0014,
      -0.0014,
       0.0013,
       0.0013,
       0.0011,
      -0.0011,
      -0.0010,
      -0.0009,
      -0.0008,
       0.0008,
       0.0008,
       0.0007,
       0.0007,
       0.0007,
      -0.0006,
      -0.0006,
       0.0006,
       0.0005,
       0.0005,
      -0.0004,

      0
  };

  static double[] peritft = new double[]{
      4,
      5,
      7,
      -1
  };

  static double[] peritfc = new double[] {
       0.00019,
      -0.00013,
      -0.00011
  };

  static double[] apoarg = new double[]{
  //  D,  M,  F
      2, 0, 0,
      4, 0, 0,
      0, 1, 0,
      2, -1, 0,
      0, 0, 2,
      1, 0, 0,
      6, 0, 0,
      4, -1, 0,
      2, 0, 2,
      1, 1, 0,
      8, 0, 0,
      6, -1, 0,
      2, 0, -2,
      2, -2, 0,
      3, 0, 0,
      4, 0, 2,

      8, -1, 0,
      4, -2, 0,
     10, 0, 0,
      3, 1, 0,
      0, 2, 0,
      2, 1, 0,
      2, 2, 0,
      6, 0, 2,
      6, -2, 0,
     10, -1, 0,
      5, 0, 0,
      4, 0, -2,
      0, 1, 2,
     12, 0, 0,
      2, -1, 2,
      1, -1, 0
  };

  static double[] apocoeff = new double[]{
       0.4392,
       0.0684,
       0.0456,
       0.0426,
       0.0212,
      -0.0189,
       0.0144,
       0.0113,
       0.0047,
       0.0036,
       0.0035,
       0.0034,
      -0.0034,
       0.0022,
      -0.0017,
       0.0013,

       0.0011,
       0.0010,
       0.0009,
       0.0007,
       0.0006,
       0.0005,
       0.0005,
       0.0004,
       0.0004,
       0.0004,
      -0.0004,
      -0.0004,
       0.0003,
       0.0003,
       0.0003,
      -0.0003,

      0
  };

  static double[] apotft = new double[] {
      2,
      3,
      -1
  };

  static double[] apotfc = new double[]{
      -0.00011,
      -0.00011
  };

  static double[] periparg = new double[]{
  //  D,  M,  F
      0, 0, 0,
      2, 0, 0,
      4, 0, 0,
      2, -1, 0,
      6, 0, 0,
      1, 0, 0,
      8, 0, 0,
      0, 1, 0,
      0, 0, 2,
      4, -1, 0,
      2, 0, -2,
     10, 0, 0,
      6, -1, 0,
      3, 0, 0,
      2, 1, 0,
      1, 1, 0,
     12, 0, 0,
      8, -1, 0,
      2, 0, 2,
      2, -2, 0,
      5, 0, 0,
     14, 0, 0,

     10, -1, 0,
      4, 1, 0,
     12, -1, 0,
      4, -2, 0,
      7, 0, 0,
      4, 0, 2,
     16, 0, 0,
      3, 1, 0,
      1, -1, 0,
      6, 1, 0,
      0, 2, 0,
     14, -1, 0,
      2, 2, 0,
      6, -2, 0,
      2, -1, -2,
      9, 0, 0,
     18, 0, 0,
      6, 0, 2,
      0, -1, 2,
     16, -1, 0,
      4, 0, -2,
      8, 1, 0,
     11, 0, 0,
      5, 1, 0,
     20, 0, 0
  };

  static double[] peripcoeff = new double[]{
    3629.215,
      63.224,
      -6.990,
       2.834,
       1.927,
      -1.263,
      -0.702,
       0.696,
      -0.690,
      -0.629,
      -0.392,
       0.297,
       0.260,
       0.201,
      -0.161,
       0.157,
      -0.138,
      -0.127,
       0.104,
       0.104,
      -0.079,
       0.068,

       0.067,
       0.054,
      -0.038,
      -0.038,
       0.037,
      -0.037,
      -0.035,
      -0.030,
       0.029,
      -0.025,
       0.023,
       0.023,
      -0.023,
       0.022,
      -0.021,
      -0.020,
       0.019,
       0.017,
       0.014,
      -0.014,
       0.013,
       0.012,
       0.011,
       0.010,
      -0.010,

      0
  };

  static double[] periptft = new double[] {
      3,
      7,
      9,
      -1
  };

  static double[] periptfc = new double[](
      -0.0071,
      -0.0017,
       0.0016
  );

  static double[] apoparg = new double[]{
  //  D,  M,  F
      0, 0, 0,
      2, 0, 0,
      1, 0, 0,
      0, 0, 2,
      0, 1, 0,
      4, 0, 0,
      2, -1, 0,
      1, 1, 0,
      4, -1, 0,
      6, 0, 0,
      2, 1, 0,
      2, 0, 2,
      2, 0, -2,
      2, -2, 0,
      2, 2, 0,
      0, 2, 0,
      6, -1, 0,
      8, 0, 0
  };

  static double[] apopcoeff = new double[]{
    3245.251,
      -9.147,
      -0.841,
       0.697,
      -0.656,
       0.355,
       0.159,
       0.127,
       0.065,

       0.052,
       0.043,
       0.031,
      -0.023,
       0.022,
       0.019,
      -0.016,
       0.014,
       0.010,

      0
  };

  static double[] apoptft = new double[] {
      4,
      -1
  };

  static double[] apoptfc = new double[] {
       0.0016,
       -1
  };
  */

  #endregion // MoonPA
}
