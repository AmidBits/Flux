namespace Flux
{
  public static partial class TemporalExtensionMethods
  {
    public static double AverageDaysInYear(this ConversionCalendar source)
      => source switch
      {
        ConversionCalendar.GregorianCalendar => 365.2425,
        ConversionCalendar.JulianCalendar => 365.25,
        _ => throw new System.NotImplementedException(),
      };
  }

  /// <summary>Conversion calendar enum for Julian Date (JD) and MomentUtc conversions.</summary>
  public enum ConversionCalendar
  {
    GregorianCalendar, // Refers to the Gregorian calendar era with the epoch of 1582/10/15.
    JulianCalendar, // Refers to the Julian calendar era with an ending date of 1582/10/04.
  }
}
