using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemFx
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


    readonly StringComparerEx m_comparisonOrdinal = StringComparerEx.Ordinal;
    readonly StringComparerEx m_comparisonOrdinalIgnoreCase = StringComparerEx.OrdinalIgnoreCase;

    readonly StringComparerEx m_comparableIgnoreNonSpace = new(System.Globalization.CultureInfo.CurrentCulture, System.Globalization.CompareOptions.IgnoreNonSpace);

    readonly StringComparerEx m_comparerDoNotIgnoreCase = StringComparerEx.CurrentCulture;

    [TestMethod]
    public void DamerauLevenshteinDistance()
    {
      Assert.AreEqual(2, m_text1a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(2, m_text1a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text3a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(1, m_text4a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().GetDamerauLevenshteinDistanceMetric(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void HammingDistance()
    {
      Assert.AreEqual(0, m_text2a.AsSpan().GetHammingDistance(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().GetHammingDistance(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, m_text3a.AsSpan().GetHammingDistance(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().GetHammingDistance(m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void JaroWinklerDistance()
    {
      Assert.AreEqual(0, m_text1a.AsSpan().GetJaroWinklerSimilarity(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(0, m_text1a.AsSpan().GetJaroWinklerSimilarity(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(1, m_text2a.AsSpan().GetJaroWinklerSimilarity(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(0.625, 1 - m_text2a.AsSpan().GetJaroWinklerSimilarity(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(0.362962962962963, 1 - m_text3a.AsSpan().GetJaroWinklerSimilarity(m_text3b, m_comparisonOrdinal), 1e15);
      Assert.AreEqual(0.222222222222222, 1 - m_text3a.AsSpan().GetJaroWinklerSimilarity(m_text3b, m_comparableIgnoreNonSpace), 1e15);
    }

    [TestMethod]
    public void LevenshteinDistance()
    {
      Assert.AreEqual(3, m_text1a.AsSpan().GetLevenshteinDistanceMetric(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text1a.AsSpan().GetLevenshteinDistanceMetric(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().GetLevenshteinDistanceMetric(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().GetLevenshteinDistanceMetric(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, m_text3a.AsSpan().GetLevenshteinDistanceMetric(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().GetLevenshteinDistanceMetric(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(1, m_text4a.AsSpan().GetLevenshteinDistanceMetric(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().GetLevenshteinDistanceMetric(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void LongestCommonSubsequence()
    {
      Assert.AreEqual(1, m_text1a.AsSpan().GetLongestCommonSubsequenceLength(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text1a.AsSpan().GetLongestCommonSubsequenceLength(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, m_text2a.AsSpan().GetLongestCommonSubsequenceLength(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(4, m_text2a.AsSpan().GetLongestCommonSubsequenceLength(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().GetLongestCommonSubsequenceLength(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(7, m_text3a.AsSpan().GetLongestCommonSubsequenceLength(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(9, m_text4a.AsSpan().GetLongestCommonSubsequenceLength(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(9, m_text4a.AsSpan().GetLongestCommonSubsequenceLength(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void LongestCommonSubstring()
    {
      Assert.AreEqual(1, m_text1a.AsSpan().GetLongestCommonSubstringLength(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text1a.AsSpan().GetLongestCommonSubstringLength(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, m_text2a.AsSpan().GetLongestCommonSubstringLength(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(1, m_text2a.AsSpan().GetLongestCommonSubstringLength(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(3, m_text3a.AsSpan().GetLongestCommonSubstringLength(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().GetLongestCommonSubstringLength(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(9, m_text4a.AsSpan().GetLongestCommonSubstringLength(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(9, m_text4a.AsSpan().GetLongestCommonSubstringLength(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void OptimalStringAlignment()
    {
      Assert.AreEqual(3, m_text1a.AsSpan().GetOptimalStringAlignmentMetric(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text1a.AsSpan().GetOptimalStringAlignmentMetric(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().GetOptimalStringAlignmentMetric(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().GetOptimalStringAlignmentMetric(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().GetOptimalStringAlignmentMetric(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text3a.AsSpan().GetOptimalStringAlignmentMetric(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(1, m_text4a.AsSpan().GetOptimalStringAlignmentMetric(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().GetOptimalStringAlignmentMetric(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void ShortestBalancingSubstring()
    {
      var vocab = "ACGT";

      string[] inputs = ["GAAATAAA", "CACCGCTACCGC", "CAGCTAGC", "AAAAAAAA", "GAAAAAAA", "GATGAATAACCA", "ACGT"];
      System.ValueTuple<int, int>[] outputs = [(1, 5), (2, 7), (0, 1), (0, 6), (1, 5), (4, 4), (-1, 0)];

      Assert.AreEqual(inputs.Length, outputs.Length);

      for (var i = 0; i < inputs.Length; i++)
      {
        var vt = inputs[i].AsSpan().ShortestBalancingSubstring(vocab);

        Assert.AreEqual(vt, outputs[i]);
      }
    }
  }
}
