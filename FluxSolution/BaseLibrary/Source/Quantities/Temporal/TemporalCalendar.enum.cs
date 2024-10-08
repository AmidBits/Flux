namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Gets the average number of days in a year of the <paramref name="source"/> <see cref="Quantities.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static double AverageDaysInYear(this Quantities.TemporalCalendar source)
      => source switch
      {
        Quantities.TemporalCalendar.GregorianCalendar => 365.2425,
        Quantities.TemporalCalendar.JulianCalendar => 365.25,
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
    public static bool Contains(this Quantities.TemporalCalendar source, double julianDate)
      => source switch
      {
        Quantities.TemporalCalendar.GregorianCalendar => julianDate >= Quantities.JulianDate.GregorianCalendarEpoch.Value,
        Quantities.TemporalCalendar.JulianCalendar => julianDate >= Quantities.JulianDate.JulianCalendarEpoch.Value && julianDate < Quantities.JulianDate.GregorianCalendarEpoch.Value,
        _ => throw new NotImplementedException(),
      };

    /// <summary>
    /// <para>Indicates whether the <paramref name="julianDayNumber"/> (JDN) can be considered to be in the (regular) <paramref name="source"/> <see cref="Quantities.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="julianDayNumber"></param>
    /// <returns></returns>
    public static bool Contains(this Quantities.TemporalCalendar source, int julianDayNumber) => source.Contains((double)julianDayNumber);

    /// <summary>
    /// <para>Indicates whether the (<paramref name="year"/>, <paramref name="month"/> and <paramref name="day"/>) date components can be considered to be in the (regular) <paramref name="source"/> <see cref="Quantities.TemporalCalendar"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static bool Contains(this Quantities.TemporalCalendar source, int year, int month, int day)
      => source switch
      {
        Quantities.TemporalCalendar.GregorianCalendar => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15))),
        Quantities.TemporalCalendar.JulianCalendar => !(year < -7 || year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day > 4)))),
        _ => throw new NotImplementedException(),
      };
  }

  namespace Quantities
  {
    /// <summary>Conversion calendar enum for Julian Date (JD) and MomentUtc conversions.</summary>
    public enum TemporalCalendar
    {
      /// <summary>
      /// <para>The Gregorian calendar epoch with a start of 1582/10/15.</para>
      /// </summary>
      /// <remarks>This is the "regular" Gregorian calendar, which is still in use, meaning it has no ending reference.</remarks>
      GregorianCalendar,
      /// <summary>
      /// <para>The Julian calendar with an epoch of -7/1/1 to 1582/10/04.</para>
      /// </summary>
      /// <remarks>This is the "regular" Julian calendar.</remarks>
      JulianCalendar,
    }
  }
}
