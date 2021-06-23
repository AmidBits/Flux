namespace Flux.Nmea
{
  public class NmeaSentence
  {
    private static System.Text.RegularExpressions.Regex m_reSentence = new System.Text.RegularExpressions.Regex(@"(?<StartOfSentence>\$)?(?<SentenceValues>[A-Z]{5}\,[^\*]+)(?<EndOfSentence>\*)?(?<Checksum>[A-Fa-f0-9]+)?(?<Termination>\r\n)?");

    protected string m_sentenceRaw;

    private System.Collections.Generic.IDictionary<string, string> m_sentenceMetaData;

    private string[] m_sentenceData;

    public NmeaSentence(string sentence)
    {
      m_sentenceRaw = sentence;

      m_sentenceMetaData = m_reSentence.Match(m_sentenceRaw).GetNamedGroups();

      m_sentenceData = m_sentenceMetaData[@"SentenceValues"].Split(',');
    }

    public string SentenceCode
      => m_sentenceData[0];

    public System.Collections.Generic.IReadOnlyDictionary<string, string> SentenceMetaData
      => (System.Collections.Generic.IReadOnlyDictionary<string, string>)m_sentenceMetaData;

    public System.Collections.Generic.IReadOnlyList<string> SentenceData
      => m_sentenceData;

    public static double GetDecimalLatitude(string latitude_DDMM_MMMM, string latitude_Indicator)
    {
      var degrees = int.Parse(latitude_DDMM_MMMM.Substring(0, 2));
      var minutes = double.Parse(latitude_DDMM_MMMM.Substring(2));

      var decimalValue = degrees + minutes / 60;

      return latitude_Indicator == @"S" ? -decimalValue : decimalValue;
    }

    public static double GetDecimalLongitude(string longitude_DDDMM_MMMM, string longitude_Indicator)
    {
      var degrees = int.Parse(longitude_DDDMM_MMMM.Substring(0, 3));
      var minutes = double.Parse(longitude_DDDMM_MMMM.Substring(3));

      var decimalValue = degrees + minutes / 60;

      return longitude_Indicator == @"W" ? -decimalValue : decimalValue;
    }

    public static System.DateTime GetUtcTime(string utc_HHMMSS_SSS)
    {
      var now = System.DateTime.Now;

      var hh = int.Parse(utc_HHMMSS_SSS.Substring(0, 2));
      var mm = int.Parse(utc_HHMMSS_SSS.Substring(2, 2));
      var ss = int.Parse(utc_HHMMSS_SSS.Substring(4, 2));
      var ms = int.Parse(utc_HHMMSS_SSS.Substring(7, 3));

      return new System.DateTime(now.Year, now.Month, now.Day, hh, mm, ss, ms, System.DateTimeKind.Utc);
    }
  }

  public class NmeaGga
  {
    private readonly NmeaSentence m_sentence;

    public NmeaGga(string sentence)
    {
      m_sentence = new NmeaSentence(sentence);
    }

    public string LatitudeString
      => m_sentence.SentenceData[1];
    public string LatitudeIndicatorString
      => m_sentence.SentenceData[2];
    public string LongitudeString
      => m_sentence.SentenceData[3];
    public string LongitudeIndicatorString
      => m_sentence.SentenceData[4];
    public string UtcTimeString
      => m_sentence.SentenceData[5];

    public double LatitudeDecimal
      => NmeaSentence.GetDecimalLatitude(LatitudeString, LatitudeIndicatorString);
    public double LongitudeDecimal
      => NmeaSentence.GetDecimalLongitude(LongitudeString, LongitudeIndicatorString);
    public System.DateTime UtcTime
      => NmeaSentence.GetUtcTime(UtcTimeString);
  }

  public class NmeaGll
  {
    private readonly NmeaSentence m_sentence;

    public NmeaGll(string sentence)
    {
      m_sentence = new NmeaSentence(sentence);
    }

    public string LatitudeString
      => m_sentence.SentenceData[1];
    public string LatitudeIndicatorString
      => m_sentence.SentenceData[2];
    public string LongitudeString
      => m_sentence.SentenceData[3];
    public string LongitudeIndicatorString
      => m_sentence.SentenceData[4];
    public string UtcTimeString
      => m_sentence.SentenceData[5];

    public double LatitudeDecimal
      => NmeaSentence.GetDecimalLatitude(LatitudeString, LatitudeIndicatorString);
    public double LongitudeDecimal
      => NmeaSentence.GetDecimalLongitude(LongitudeString, LongitudeIndicatorString);
    public System.DateTime UtcTime
      => NmeaSentence.GetUtcTime(UtcTimeString);
  }
}
