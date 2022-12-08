namespace Flux.Net.Nmea
{
  public sealed class NmeaGprmc
    : NmeaSentence
  {
    public NmeaGprmc(string sentence)
      : base(sentence)
    { }

    public NmeaDataStatus DataStatus
      => ParseDataStatus(m_values[2]);
    public Latitude Latitude
      => new(NmeaSentence.ParseDecimalLatitude(m_values[3], m_values[4]));
    public Longitude Longitude
      => new(NmeaSentence.ParseDecimalLongitude(m_values[5], m_values[6]));
    public Quantities.LinearVelocity SpeedOverGround
      => new(m_values.Length > 7 && double.TryParse(m_values[7], out var result) ? result : 0, Quantities.LinearVelocityUnit.Knot);
    public Azimuth CourseOverGround
      => new(m_values.Length > 8 && double.TryParse(m_values[8], out var result) ? result : 0);
    public System.DateTime UtcDateTime
      => ParseUtcDate(m_values.Length > 9 ? m_values[9] : null) + ParseUtcTime(m_values[1]).TimeOfDay;
  }
}
