namespace Flux.Services.Nmea
{
  public class NmeaGpgll
    : NmeaSentence
  {
    public NmeaGpgll(string sentence)
      : base(sentence)
    { }

    public Latitude Latitude
      => new(NmeaSentence.ParseDecimalLatitude(m_values[1], m_values[2]));
    public Longitude Longitude
      => new(NmeaSentence.ParseDecimalLongitude(m_values[3], m_values[4]));
    public System.DateTime UtcTime
      => NmeaSentence.ParseUtcTime(m_values[5]);
    public NmeaDataStatus DataStatus
      => ParseDataStatus(m_values.Length > 6 ? m_values[6] : null);
  }
}
