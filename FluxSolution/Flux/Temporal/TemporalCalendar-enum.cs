namespace Flux.Temporal
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
