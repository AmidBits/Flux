namespace Flux.Services.Nmea
{
  public class NmeaGpgga
    : NmeaSentence
  {
    public NmeaGpgga(string sentence)
      : base(sentence)
    { }

    public System.DateTime UtcTime
      => NmeaSentence.ParseUtcTime(m_values[1]);
    public Quantity.Latitude Latitude
      => new Quantity.Latitude(NmeaSentence.ParseDecimalLatitude(m_values[2], m_values[3]));
    public Quantity.Longitude Longitude
      => new Quantity.Longitude(NmeaSentence.ParseDecimalLongitude(m_values[4], m_values[5]));
    public NmeaPositionFixIndicator PositionFixIndicator
      => m_values.Length > 6 && int.TryParse(m_values[6], out var result) ? (NmeaPositionFixIndicator)result : NmeaPositionFixIndicator.Unknown;
    public int SatellitesUsed
      => m_values.Length > 7 && int.TryParse(m_values[7], out var result) ? result : -1;
  }
}
