using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetFx.ReadOnlySpan.Special
{
  [TestClass]
  public partial class SpanMetrics
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
    public void DamerauLevenshteinDistance2()
    {
      Assert.AreEqual(2, m_text1a.AsSpan().DamerauLevenshteinDistanceMetric(m_text1b, out var _, m_comparisonOrdinal));
      Assert.AreEqual(2, m_text1a.AsSpan().DamerauLevenshteinDistanceMetric(m_text1b, out var _, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().DamerauLevenshteinDistanceMetric(m_text2b, out var _, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().DamerauLevenshteinDistanceMetric(m_text2b, out var _, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().DamerauLevenshteinDistanceMetric(m_text3b, out var _, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text3a.AsSpan().DamerauLevenshteinDistanceMetric(m_text3b, out var _, m_comparableIgnoreNonSpace));

      Assert.AreEqual(1, m_text4a.AsSpan().DamerauLevenshteinDistanceMetric(m_text4b, out var _, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().DamerauLevenshteinDistanceMetric(m_text4b, out var _, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void HammingDistance2()
    {
      Assert.AreEqual(0, m_text2a.AsSpan().HammingDistance(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().HammingDistance(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, m_text3a.AsSpan().HammingDistance(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().HammingDistance(m_text3b, m_comparableIgnoreNonSpace));
    }

    [TestMethod]
    public void JaroWinklerDistance2()
    {
      Assert.AreEqual(0, m_text1a.AsSpan().JaroWinklerSimilarity(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(0, m_text1a.AsSpan().JaroWinklerSimilarity(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(1, m_text2a.AsSpan().JaroWinklerSimilarity(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(0.625, 1 - m_text2a.AsSpan().JaroWinklerSimilarity(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(0.362962962962963, 1 - m_text3a.AsSpan().JaroWinklerSimilarity(m_text3b, m_comparisonOrdinal), 1e15);
      Assert.AreEqual(0.222222222222222, 1 - m_text3a.AsSpan().JaroWinklerSimilarity(m_text3b, m_comparableIgnoreNonSpace), 1e15);
    }

    [TestMethod]
    public void LevenshteinDistance2()
    {
      Assert.AreEqual(2, m_text1a.AsSpan().LevenshteinDistanceMetric(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(2, m_text1a.AsSpan().LevenshteinDistanceMetric(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().LevenshteinDistanceMetric(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().LevenshteinDistanceMetric(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(6, m_text3a.AsSpan().LevenshteinDistanceMetric(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().LevenshteinDistanceMetric(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(2, m_text4a.AsSpan().LevenshteinDistanceMetric(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().LevenshteinDistanceMetric(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void LongestCommonSubsequence2()
    {
      Assert.AreEqual(1, m_text1a.AsSpan().LongestCommonSubsequenceCount(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text1a.AsSpan().LongestCommonSubsequenceCount(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, m_text2a.AsSpan().LongestCommonSubsequenceCount(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(4, m_text2a.AsSpan().LongestCommonSubsequenceCount(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().LongestCommonSubsequenceCount(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(7, m_text3a.AsSpan().LongestCommonSubsequenceCount(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(9, m_text4a.AsSpan().LongestCommonSubsequenceCount(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(9, m_text4a.AsSpan().LongestCommonSubsequenceCount(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void LongestCommonSubstring2()
    {
      Assert.AreEqual(1, m_text1a.AsSpan().LongestCommonSubstringLength(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text1a.AsSpan().LongestCommonSubstringLength(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(64, m_text2a.AsSpan().LongestCommonSubstringLength(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(1, m_text2a.AsSpan().LongestCommonSubstringLength(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(3, m_text3a.AsSpan().LongestCommonSubstringLength(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(4, m_text3a.AsSpan().LongestCommonSubstringLength(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(9, m_text4a.AsSpan().LongestCommonSubstringLength(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(9, m_text4a.AsSpan().LongestCommonSubstringLength(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void OptimalStringAlignment2()
    {
      Assert.AreEqual(3, m_text1a.AsSpan().OptimalStringAlignmentMetric(m_text1b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text1a.AsSpan().OptimalStringAlignmentMetric(m_text1b, m_comparisonOrdinalIgnoreCase));

      Assert.AreEqual(0, m_text2a.AsSpan().OptimalStringAlignmentMetric(m_text2b, m_comparableIgnoreNonSpace));
      Assert.AreEqual(60, m_text2a.AsSpan().OptimalStringAlignmentMetric(m_text2b, m_comparerDoNotIgnoreCase));

      Assert.AreEqual(5, m_text3a.AsSpan().OptimalStringAlignmentMetric(m_text3b, m_comparisonOrdinal));
      Assert.AreEqual(3, m_text3a.AsSpan().OptimalStringAlignmentMetric(m_text3b, m_comparableIgnoreNonSpace));

      Assert.AreEqual(1, m_text4a.AsSpan().OptimalStringAlignmentMetric(m_text4b, m_comparisonOrdinal));
      Assert.AreEqual(1, m_text4a.AsSpan().OptimalStringAlignmentMetric(m_text4b, m_comparisonOrdinalIgnoreCase));
    }

    [TestMethod]
    public void ShortestBalancingSubstring2()
    {
      var vocab = "ACGT";

      string[] inputs = ["GAAATAAA", "CACCGCTACCGC", "CAGCTAGC", "AAAAAAAA", "GAAAAAAA", "GATGAATAACCA", "ACGT"];
      System.ValueTuple<int, int>[] outputs = [(1, 5), (2, 7), (0, 1), (0, 6), (1, 5), (4, 4), (-1, 0)];

      Assert.AreEqual(inputs.Length, outputs.Length);

      for (var i = 0; i < inputs.Length; i++)
      {
        var vt = inputs[i].AsSpan().ShortestBalancingSubstringSearch(vocab);

        Assert.AreEqual(vt, outputs[i]);
      }
    }

    [TestMethod]
    public void ShortestCommonSubsequence()
    {
      var expectedc = 9;
      var actualc = "abcbdab".AsSpan().ShortestCommonSupersequenceCount("bdcaba", out var _);
      Assert.AreEqual(expectedc, actualc);

      var expectedv = "abdcabdab";
      var actualv = "abcbdab".AsSpan().ShortestCommonSupersequenceValues("bdcaba", out var _).AsSpan().ToString();
      Assert.AreEqual(expectedv, actualv);
    }
  }
}
