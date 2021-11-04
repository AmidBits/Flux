namespace Flux.Services.Nmea
{
  // http://aprs.gids.nl/nmea/

  public class NmeaSentence
  {
    private static readonly System.Text.RegularExpressions.Regex m_reSentence = new(@"(?<StartOfSentence>\$)?(?<SentenceContent>[A-Z]{5}[^\*]+)(?<EndOfSentence>\*)?(?<SentenceChecksum>[0-9a-fA-F]+)?(?<SentenceTermination>\r\n)?", System.Text.RegularExpressions.RegexOptions.Compiled);

    protected readonly string m_raw;
    protected readonly System.Collections.Generic.IDictionary<string, string> m_metaData;
    protected readonly string[] m_values;

    public NmeaSentence(string sentence)
    {
      m_raw = sentence;
      m_metaData = m_reSentence.Match(m_raw).GetNamedGroups();
      m_values = SentenceContent.Split(',');

      SentenceChecksumComputed = ComputeChecksum(SentenceContent);
    }

    public string Raw
      => m_raw;
    public System.Collections.Generic.IReadOnlyDictionary<string, string> MetaData
      => (System.Collections.Generic.IReadOnlyDictionary<string, string>)m_metaData;
    public System.Collections.Generic.IReadOnlyList<string> Values
      => m_values;

    public string StartOfSentence
      => m_metaData[nameof(StartOfSentence)];
    public string SentenceContent
      => m_metaData[nameof(SentenceContent)];
    public string EndOfSentence
      => m_metaData[nameof(EndOfSentence)];
    public string SentenceChecksum
      => m_metaData[nameof(SentenceChecksum)];
    public string SentenceChecksumComputed { get; }
    public string SentenceTermination
      => m_metaData[nameof(SentenceTermination)];

    public string Code
      => m_values[0];

    public static string ComputeChecksum(string sentenceContent)
    {
      var checksum = 0U;

      foreach (var c in sentenceContent)
        checksum ^= c;

      return checksum.ToString(@"X2");
    }

    public static NmeaSentence Parse(string sentence)
    {
      var parsed = new NmeaSentence(sentence);

      switch (parsed.Code)
      {
        case @"GPGGA":
          return new NmeaGpgga(sentence);
        case @"GPGLL":
          return new NmeaGpgll(sentence);
        case @"GPRMC":
          return new NmeaGprmc(sentence);
        default:
          return parsed;
      }
    }

    public static NmeaDataStatus ParseDataStatus(string? data_status)
    {
      if (string.IsNullOrEmpty(data_status) || data_status.Length != 1)
        return NmeaDataStatus.Unknown;

      switch (data_status[0])
      {
        case 'A':
          return NmeaDataStatus.Valid;
        case 'V':
          return NmeaDataStatus.Warning;
        default:
          return NmeaDataStatus.Unknown;
      }
    }
    public static double ParseDecimalLatitude(string latitude_DDMM_MMMM, string latitude_Indicator)
    {
      var degrees = int.Parse(latitude_DDMM_MMMM.Substring(0, 2)); // First two (2) digits are degrees.
      var minutes = double.Parse(latitude_DDMM_MMMM.Substring(2)); // Remaining digits (including decimal point) is minutes.

      var decimalValue = degrees + minutes / 60;

      return latitude_Indicator == @"S" ? -decimalValue : decimalValue; // If "S" (south) then negative, otherwise assume "N" (north).
    }
    public static double ParseDecimalLongitude(string longitude_DDDMM_MMMM, string longitude_Indicator)
    {
      var degrees = int.Parse(longitude_DDDMM_MMMM.Substring(0, 3)); // First three (3) digits are degrees.
      var minutes = double.Parse(longitude_DDDMM_MMMM.Substring(3)); // Remaining digits (including decimal point) is minutes.

      var decimalValue = degrees + minutes / 60; // Compute the decimal representation.

      return longitude_Indicator == @"W" ? -decimalValue : decimalValue; // If "W" (west) then negative, otherwise assume "E" (east).
    }
    public static System.DateTime ParseUtcDate(string? utc_DDMMYY)
    {
      if (string.IsNullOrEmpty(utc_DDMMYY) || utc_DDMMYY.Length < 6)
        return default;

      var day = int.Parse(utc_DDMMYY.Substring(0, 2)); // First two digits are day.
      var month = int.Parse(utc_DDMMYY.Substring(2, 2)); // Next two digits are month.
      var year = int.Parse(utc_DDMMYY.Substring(4, 2)); // Next two digits are year.

      year += year > 50 ? 1900 : 2000; // Assume typical conversion to four digits.

      return new System.DateTime(year, month, day, 0, 0, 0, System.DateTimeKind.Utc); // Create as UTC date.
    }
    public static System.DateTime ParseUtcTime(string? utc_HHMMSS_SSS)
    {
      if (string.IsNullOrEmpty(utc_HHMMSS_SSS) || utc_HHMMSS_SSS.Length < 6)
        return default;

      var hours = int.Parse(utc_HHMMSS_SSS.Substring(0, 2)); // First two digits are hours.
      var minutes = int.Parse(utc_HHMMSS_SSS.Substring(2, 2)); // Next two digits are minutes.
      var seconds = int.Parse(utc_HHMMSS_SSS.Substring(4, 2)); // Next two digits are seconds.
      var milliseconds = utc_HHMMSS_SSS.Length >= 8 ? int.Parse(utc_HHMMSS_SSS.Substring(7)) : 0; // Skipping the period, any remaining digits are milliseconds.

      return new System.DateTime(1, 1, 1, hours, minutes, seconds, milliseconds, System.DateTimeKind.Utc);
    }
  }
}

/*
  var nmea = new string[]
  {
    "$GPRMC,161229.487,A,3723.2475,N,12158.3416,W,0.13,309.62,120598,,*10\r\n",
    "$GPGLL,3723.2475,N,12158.3416,W,161229.487,A*2C\r\n",
    "$GPGSA,A,3,,,,,,16,18,,22,24,,,3.6,2.1,2.2*3C",
    "$GPGSV,3,1,11,03,03,111,00,04,15,270,00,06,01,010,00,13,06,292,00*74\r\n",
    "$GPGGA,161229.487,3723.2475,N,12158.3416,W,1,07,1.0,9.0,M,,,,0000*18",
    "$GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62",
    "$GPRMC,225446,A,4916.45,N,12311.12,W,000.5,054.7,191194,020.3,E*68",
  };

  var list = new System.Collections.Generic.List<Flux.Services.Nmea.NmeaSentence>();

  foreach (var s in nmea)
    list.Add(Flux.Services.Nmea.NmeaSentence.Parse(s));
*/
