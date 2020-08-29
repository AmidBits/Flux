using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace IEnumerable
{
  [TestClass]
  public class ExtensionMethodsCollectionsGenericMetric
  {
    string m_text1a = "CA";
    string m_text1b = "ABC";

    string m_text2a = "àèìòùÀÈÌÒÙ äëïöüÄËÏÖÜ âêîôûÂÊÎÔÛ áéíóúÁÉÍÓÚðÐýÝ ãñõÃÑÕšŠžŽçÇåÅøØ";
    string m_text2b = "aeiouAEIOU aeiouAEIOU aeiouAEIOU aeiouAEIOUdDyY anoANOsSzZcCaAoO";

    string m_text3a = "SħoñeTets1";
    string m_text3b = "ShaneTest2";


    Flux.StringComparerEx m_comparisonOrdinal = Flux.StringComparerEx.Ordinal;
    Flux.StringComparerEx m_comparisonOrdinalIgnoreCase = Flux.StringComparerEx.OrdinalIgnoreCase;

    Flux.StringComparerEx m_comparableIgnoreNonSpace = new Flux.StringComparerEx(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);

    Flux.StringComparerEx m_comparerDoNotIgnoreCase = Flux.StringComparerEx.CurrentCulture;

    [TestMethod]
    public void DamerauLevenshteinDistance()
    {
      Assert.AreEqual(2, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(2, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(3, new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void HammingDistance()
    {
      Assert.AreEqual(0, new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void JaroWinklerDistance()
    {
      Assert.AreEqual(0, new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text1a, m_text1b,  m_comparisonOrdinal));
      Assert.AreEqual(0, new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text1a, m_text1b,  m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(1, new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text2a, m_text2b,  m_comparableIgnoreNonSpace));
      Assert.AreEqual(0.625, 1 - new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(0.362962962962963, 1 - new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text3a, m_text3b, m_comparisonOrdinal), 1e15);
      Assert.AreEqual(0.222222222222222, 1 - new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(m_text3a, m_text3b,  m_comparableIgnoreNonSpace), 1e15);
    }

    [TestMethod]
    public void LevenshteinDistance()
    {
      Assert.AreEqual(3, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(3, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void LongestCommonSubsequence()
    {
      Assert.AreEqual(1, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text1a, m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text1a, m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(4, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(7, new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void LongestCommonSubstring()
    {
      Assert.AreEqual(1, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text1a, m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text1a, m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(1, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(3, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void OptimalStringAlignment()
    {
      Assert.AreEqual(3, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(3, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text1a, m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text2a, m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text2a, m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text3a, m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(3, new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(m_text3a, m_text3b, m_comparableIgnoreNonSpace));
    }
  }
}
