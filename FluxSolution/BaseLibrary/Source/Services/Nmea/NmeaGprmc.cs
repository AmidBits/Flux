namespace Flux.Services.Nmea
{
  public class NmeaGprmc
    : NmeaSentence
  {
    public NmeaGprmc(string sentence)
      : base(sentence)
    { }

    public NmeaDataStatus DataStatus
      => ParseDataStatus(m_values[2]);
    public Quantity.Latitude Latitude
      => new Quantity.Latitude(NmeaSentence.ParseDecimalLatitude(m_values[3], m_values[4]));
    public Quantity.Longitude Longitude
      => new Quantity.Longitude(NmeaSentence.ParseDecimalLongitude(m_values[5], m_values[6]));
    public Quantity.Speed SpeedOverGround
      => Quantity.Speed.FromUnitValue(Quantity.SpeedUnit.Knots, m_values.Length > 7 && double.TryParse(m_values[7], out var result) ? result : 0);
    public Quantity.Bearing CourseOverGround
      => new Quantity.Bearing(m_values.Length > 8 && double.TryParse(m_values[8], out var result) ? result : 0);
    public System.DateTime UtcDateTime
      => ParseUtcDate(m_values.Length > 9 ? m_values[9] : null) + ParseUtcTime(m_values[1]).TimeOfDay;
  }
}