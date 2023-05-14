namespace Flux
{
  public static partial class TemporalExtensionMethods
  {
    public static double AverageDaysInYear(this TemporalCalendar source)
      => source switch
      {
        TemporalCalendar.GregorianCalendar => 365.2425,
        TemporalCalendar.JulianCalendar => 365.25,
        _ => throw new System.NotImplementedException(),
      };
  }

  /// <summary>Conversion calendar enum for Julian Date (JD) and MomentUtc conversions.</summary>
  public enum TemporalCalendar
  {
    /// <summary>
    /// <para>The Gregorian calendar era with the epoch of 1582/10/15.</para>
    /// </summary>
    GregorianCalendar,
    /// <summary>
    /// <para>The Julian calendar era with an ending date of 1582/10/04.</para>
    /// </summary>
    JulianCalendar,
  }
}
