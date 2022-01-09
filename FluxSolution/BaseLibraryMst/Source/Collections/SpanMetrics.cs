using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Collections.Generic
{
  [TestClass]
  public class SpanMetrics
  {
    private readonly string m_text1a = "CA";
    private readonly string m_text1b = "ABC";

    private readonly string m_text2a = "àèìòùÀÈÌÒÙ äëïöüÄËÏÖÜ âêîôûÂÊÎÔÛ áéíóúÁÉÍÓÚðÐýÝ ãñõÃÑÕšŠžŽçÇåÅøØ";
    private readonly string m_text2b = "aeiouAEIOU aeiouAEIOU aeiouAEIOU aeiouAEIOUdDyY anoANOsSzZcCaAoO";

    private readonly string m_text3a = "SħoñeTets1";
    private readonly string m_text3b = "ShaneTest2";

    private readonly string m_text4a = "Aaaaaaaaaa";
    private readonly string m_text4b = "aaaaaaaaa";


    readonly Flux.StringComparerEx m_comparisonOrdinal = Flux.StringComparerEx.Ordinal;
    readonly Flux.StringComparerEx m_comparisonOrdinalIgnoreCase = Flux.StringComparerEx.OrdinalIgnoreCase;

    readonly Flux.StringComparerEx m_comparableIgnoreNonSpace = new(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);

    readonly Flux.StringComparerEx m_comparerDoNotIgnoreCase = Flux.StringComparerEx.CurrentCulture;

    [TestMethod]
    public void DamerauLevenshteinDistance()
    {
      Assert.AreEqual(2, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text1a, m_text1b));
      Assert.AreEqual(2, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text1a, m_text1b));

      Assert.AreEqual(0, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text2a, m_text2b));
      Assert.AreEqual(60, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparerDoNotIgnoreCase).GetEditDistance(m_text2a, m_text2b));

      Assert.AreEqual(5, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text3a, m_text3b));
      Assert.AreEqual(3, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text3a, m_text3b));

      Assert.AreEqual(1, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text4a, m_text4b));
      Assert.AreEqual(1, new Flux.Metrical.DamerauLevenshteinDistance<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text4a, m_text4b));
    }

    [TestMethod]
    public void HammingDistance()
    {
      Assert.AreEqual(0, new Flux.Metrical.HammingDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text2a, m_text2b));
      Assert.AreEqual(60, new Flux.Metrical.HammingDistance<char>(m_comparerDoNotIgnoreCase).GetEditDistance(m_text2a, m_text2b));

      Assert.AreEqual(6, new Flux.Metrical.HammingDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text3a, m_text3b));
      Assert.AreEqual(4, new Flux.Metrical.HammingDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text3a, m_text3b));
    }

    [TestMethod]
    public void JaroWinklerDistance()
    {
      Assert.AreEqual(0, new Flux.Metrical.JaroWinklerDistance<char>(m_comparisonOrdinal).GetNormalizedSimilarity(m_text1a, m_text1b));
      Assert.AreEqual(0, new Flux.Metrical.JaroWinklerDistance<char>(m_comparisonOrdinalIgnoreCase).GetNormalizedSimilarity(m_text1a, m_text1b));

      Assert.AreEqual(1, new Flux.Metrical.JaroWinklerDistance<char>(m_comparableIgnoreNonSpace).GetNormalizedSimilarity(m_text2a, m_text2b));
      Assert.AreEqual(0.625, 1 - new Flux.Metrical.JaroWinklerDistance<char>(m_comparerDoNotIgnoreCase).GetNormalizedSimilarity(m_text2a, m_text2b));

      Assert.AreEqual(0.362962962962963, 1 - new Flux.Metrical.JaroWinklerDistance<char>(m_comparisonOrdinal).GetNormalizedSimilarity(m_text3a, m_text3b), 1e15);
      Assert.AreEqual(0.222222222222222, 1 - new Flux.Metrical.JaroWinklerDistance<char>(m_comparableIgnoreNonSpace).GetNormalizedSimilarity(m_text3a, m_text3b), 1e15);
    }

    [TestMethod]
    public void LevenshteinDistance()
    {
      Assert.AreEqual(3, new Flux.Metrical.LevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text1a, m_text1b));
      Assert.AreEqual(3, new Flux.Metrical.LevenshteinDistance<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text1a, m_text1b));

      Assert.AreEqual(0, new Flux.Metrical.LevenshteinDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text2a, m_text2b));
      Assert.AreEqual(60, new Flux.Metrical.LevenshteinDistance<char>(m_comparerDoNotIgnoreCase).GetEditDistance(m_text2a, m_text2b));

      Assert.AreEqual(6, new Flux.Metrical.LevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text3a, m_text3b));
      Assert.AreEqual(4, new Flux.Metrical.LevenshteinDistance<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text3a, m_text3b));

      Assert.AreEqual(1, new Flux.Metrical.LevenshteinDistance<char>(m_comparisonOrdinal).GetEditDistance(m_text4a, m_text4b));
      Assert.AreEqual(1, new Flux.Metrical.LevenshteinDistance<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text4a, m_text4b));
    }

    [TestMethod]
    public void LongestCommonSubsequence()
    {
      Assert.AreEqual(1, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparisonOrdinal).GetLengthMetric(m_text1a, m_text1b));
      Assert.AreEqual(1, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparisonOrdinalIgnoreCase).GetLengthMetric(m_text1a, m_text1b));

      Assert.AreEqual(64, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparableIgnoreNonSpace).GetLengthMetric(m_text2a, m_text2b));
      Assert.AreEqual(4, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparerDoNotIgnoreCase).GetLengthMetric(m_text2a, m_text2b));

      Assert.AreEqual(5, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparisonOrdinal).GetLengthMetric(m_text3a, m_text3b));
      Assert.AreEqual(7, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparableIgnoreNonSpace).GetLengthMetric(m_text3a, m_text3b));

      Assert.AreEqual(9, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparisonOrdinal).GetLengthMetric(m_text4a, m_text4b));
      Assert.AreEqual(9, new Flux.Metrical.LongestCommonSubsequence<char>(m_comparisonOrdinalIgnoreCase).GetLengthMetric(m_text4a, m_text4b));
    }

    [TestMethod]
    public void LongestCommonSubstring()
    {
      Assert.AreEqual(1, new Flux.Metrical.LongestCommonSubstring<char>(m_comparisonOrdinal).GetLengthMeasure(m_text1a, m_text1b));
      Assert.AreEqual(1, new Flux.Metrical.LongestCommonSubstring<char>(m_comparisonOrdinalIgnoreCase).GetLengthMeasure(m_text1a, m_text1b));

      Assert.AreEqual(64, new Flux.Metrical.LongestCommonSubstring<char>(m_comparableIgnoreNonSpace).GetLengthMeasure(m_text2a, m_text2b));
      Assert.AreEqual(1, new Flux.Metrical.LongestCommonSubstring<char>(m_comparerDoNotIgnoreCase).GetLengthMeasure(m_text2a, m_text2b));

      Assert.AreEqual(3, new Flux.Metrical.LongestCommonSubstring<char>(m_comparisonOrdinal).GetLengthMeasure(m_text3a, m_text3b));
      Assert.AreEqual(4, new Flux.Metrical.LongestCommonSubstring<char>(m_comparableIgnoreNonSpace).GetLengthMeasure(m_text3a, m_text3b));

      Assert.AreEqual(9, new Flux.Metrical.LongestCommonSubstring<char>(m_comparisonOrdinal).GetLengthMeasure(m_text4a, m_text4b));
      Assert.AreEqual(9, new Flux.Metrical.LongestCommonSubstring<char>(m_comparisonOrdinalIgnoreCase).GetLengthMeasure(m_text4a, m_text4b));
    }

    [TestMethod]
    public void OptimalStringAlignment()
    {
      Assert.AreEqual(3, new Flux.Metrical.OptimalStringAlignment<char>(m_comparisonOrdinal).GetEditDistance(m_text1a, m_text1b));
      Assert.AreEqual(3, new Flux.Metrical.OptimalStringAlignment<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text1a, m_text1b));

      Assert.AreEqual(0, new Flux.Metrical.OptimalStringAlignment<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text2a, m_text2b));
      Assert.AreEqual(60, new Flux.Metrical.OptimalStringAlignment<char>(m_comparerDoNotIgnoreCase).GetEditDistance(m_text2a, m_text2b));

      Assert.AreEqual(5, new Flux.Metrical.OptimalStringAlignment<char>(m_comparisonOrdinal).GetEditDistance(m_text3a, m_text3b));
      Assert.AreEqual(3, new Flux.Metrical.OptimalStringAlignment<char>(m_comparableIgnoreNonSpace).GetEditDistance(m_text3a, m_text3b));

      Assert.AreEqual(1, new Flux.Metrical.OptimalStringAlignment<char>(m_comparisonOrdinal).GetEditDistance(m_text4a, m_text4b));
      Assert.AreEqual(1, new Flux.Metrical.OptimalStringAlignment<char>(m_comparisonOrdinalIgnoreCase).GetEditDistance(m_text4a, m_text4b));
    }
  }
}
