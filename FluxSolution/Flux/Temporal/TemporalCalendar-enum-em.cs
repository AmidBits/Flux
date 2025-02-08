namespace Flux.Temporal
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Gets the average number of days in a year of the <paramref name="source"/> <see cref="Temporal.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static double AverageDaysInYear(this TemporalCalendar source)
      => source switch
      {
        TemporalCalendar.GregorianCalendar => 365.2425,
        TemporalCalendar.JulianCalendar => 365.25,
        _ => throw new System.NotImplementedException(),
      };

    ///// <summary>
    ///// <para>Gets the epoch (start) of the <paramref name="source"/> <see cref="Quantities.TemporalCalendar"/>.</para>
    ///// </summary>
    ///// <param name="source"></param>
    ///// <returns></returns>
    ///// <exception cref="NotImplementedException"></exception>
    //public static Quantities.JulianDate GetEpoch(this Quantities.TemporalCalendar source)
    //  => source switch
    //  {
    //    Quantities.TemporalCalendar.GregorianCalendar => Quantities.JulianDate.GregorianCalendarEpoch,
    //    Quantities.TemporalCalendar.JulianCalendar => Quantities.JulianDate.JulianCalendarEpoch,
    //    _ => throw new NotImplementedException(),
    //  };

    /// <summary>
    /// <para>Indicates whether the <paramref name="julianDate"/> (JD) can be considered to be in the (regular) <paramref name="source"/> <see cref="Quantities.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="julianDate"></param>
    /// <returns></returns>
    public static bool Contains(this TemporalCalendar source, double julianDate)
      => source switch
      {
        TemporalCalendar.GregorianCalendar => julianDate >= Temporal.JulianDate.GregorianCalendarEpoch.Value,
        TemporalCalendar.JulianCalendar => julianDate >= Temporal.JulianDate.JulianCalendarEpoch.Value && julianDate < Temporal.JulianDate.GregorianCalendarEpoch.Value,
        _ => throw new NotImplementedException(),
      };

    /// <summary>
    /// <para>Indicates whether the <paramref name="julianDayNumber"/> (JDN) can be considered to be in the (regular) <paramref name="source"/> <see cref="Units.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="julianDayNumber"></param>
    /// <returns></returns>
    public static bool Contains(this TemporalCalendar source, int julianDayNumber) => source.Contains((double)julianDayNumber);

    /// <summary>
    /// <para>Indicates whether the (<paramref name="year"/>, <paramref name="month"/> and <paramref name="day"/>) date components can be considered to be in the (regular) <paramref name="source"/> <see cref="Units.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool Contains(this TemporalCalendar source, int year, int month, int day)
      => source switch
      {
        TemporalCalendar.GregorianCalendar => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15))),
        TemporalCalendar.JulianCalendar => !(year < -7 || year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day > 4)))),
        _ => throw new NotImplementedException(),
      };
  }
}
