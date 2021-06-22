namespace Flux
{
  public class NmeaSentence
  {
    private static System.Text.RegularExpressions.Regex m_reSentence = new System.Text.RegularExpressions.Regex(@"(?<StartOfSentence>\$)?(?<SentenceValues>[A-Z]{5}\,[^\*]+)(?<EndOfSentence>\*)?(?<Checksum>[A-Fa-f0-9]+)?(?<Termination>\r\n)?");

    protected string m_sentenceRaw;

    private System.Collections.Generic.IDictionary<string, string> m_sentenceMetaData;

    private string[] m_sentenceValues;

    public NmeaSentence(string sentence)
    {
      m_sentenceRaw = sentence;

      m_sentenceMetaData = m_reSentence.Match(m_sentenceRaw).GetNamedGroups();

      m_sentenceValues = m_sentenceMetaData[@"SentenceValues"].Split(',');
    }

    public string SentenceCode
      => m_sentenceValues[0];

    public System.Collections.Generic.IReadOnlyDictionary<string, string> SentenceMetaData
      => (System.Collections.Generic.IReadOnlyDictionary<string, string>)m_sentenceMetaData;

    public System.Collections.Generic.IReadOnlyList<string> SentenceValues
      => m_sentenceValues;

    public static double GetDecimalLatitude(string latitude_DDMM_MMMM, string latitude_Indicator)
      => int.Parse(latitude_DDMM_MMMM.Substring(0, 2)) + double.Parse(latitude_DDMM_MMMM.Substring(2)) / 60 is var ld && latitude_Indicator == @"S" ? -ld : ld;
    public static double GetDecimalLongitude(string longitude_DDDMM_MMMM, string longitude_Indicator)
      => int.Parse(longitude_DDDMM_MMMM.Substring(0, 3)) + double.Parse(longitude_DDDMM_MMMM.Substring(3)) / 60 is var ld && longitude_Indicator == @"W" ? -ld : ld;

    //public static System.DateTime GetUtcDateTime(string utc)
    //  => int.Parse( // System.DateTime.ParseExact(utc, @"hhmmss.fff", null);
  }

  public class NmeaGll
  {
    private readonly NmeaSentence m_sentence;

    public NmeaGll(string sentence)
    {
      m_sentence = new NmeaSentence(sentence);
    }

    public string Latitude
      => m_sentence.SentenceValues[1];
    public string LatitudeIndicator
      => m_sentence.SentenceValues[2];
    public string Longitude
      => m_sentence.SentenceValues[3];
    public string LongitudeIndicator
      => m_sentence.SentenceValues[4];
    public string UtcTime
      => m_sentence.SentenceValues[5];

    public double LatitudeDecimal
      => NmeaSentence.GetDecimalLatitude(Latitude, LatitudeIndicator);
    public double LongitudeDecimal
      => NmeaSentence.GetDecimalLongitude(Longitude, LongitudeIndicator);
    public System.DateTime UtcDateTime
      => NmeaSentence.GetUtcDateTime(UtcTime);
  }
}
