using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Text
{
  [TestClass]
  public partial class SequenceMetrics
  {
    readonly string source = "Senor Hugo";
    readonly string target = "señor hugo";

    readonly Flux.StringComparerX comparerIgnoreCase = Flux.StringComparerX.CurrentCultureIgnoreCase;
    readonly Flux.StringComparerX comparerIgnoreNonSpace = Flux.StringComparerX.CurrentCultureIgnoreNonSpace;
    readonly Flux.StringComparerX comparerNone = Flux.StringComparerX.Ordinal;

    [TestMethod]
    public void DamerauLevenstein_Default()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.IndexedMetrics.DamerauLevenshteinDistance<char>(comparerIgnoreCase).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.IndexedMetrics.DamerauLevenshteinDistance<char>(comparerIgnoreNonSpace).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_None()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.DamerauLevenshteinDistance<char>(comparerNone).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_Default()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.HammingDistance<char>(comparerNone).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.IndexedMetrics.HammingDistance<char>(comparerIgnoreCase).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.IndexedMetrics.HammingDistance<char>(comparerIgnoreNonSpace).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_None()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.HammingDistance<char>(comparerNone).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_Default()
    {
      var expected = 0.7999999999999999;
      var actual = new Flux.IndexedMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreCase()
    {
      var expected = 0.9466666666666665;
      var actual = new Flux.IndexedMetrics.JaroWinklerDistance<char>(comparerIgnoreCase).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreNonSpace()
    {
      var expected = 0.8666666666666667;
      var actual = new Flux.IndexedMetrics.JaroWinklerDistance<char>(comparerIgnoreNonSpace).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_None()
    {
      var expected = 0.7999999999999999;
      var actual = new Flux.IndexedMetrics.JaroWinklerDistance<char>(comparerNone).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_Default()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.LevenshteinDistance<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.IndexedMetrics.LevenshteinDistance<char>(comparerIgnoreCase).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.IndexedMetrics.LevenshteinDistance<char>(comparerIgnoreNonSpace).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_None()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.LevenshteinDistance<char>(comparerNone).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_Default()
    {
      var expected = 7;
      var actual = new Flux.IndexedMetrics.LongestCommonSubsequence<char>().GetMetricLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreCase()
    {
      var expected = 9;
      var actual = new Flux.IndexedMetrics.LongestCommonSubsequence<char>(comparerIgnoreCase).GetMetricLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreNonSpace()
    {
      var expected = 8;
      var actual = new Flux.IndexedMetrics.LongestCommonSubsequence<char>(comparerIgnoreNonSpace).GetMetricLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_None()
    {
      var expected = 7;
      var actual = new Flux.IndexedMetrics.LongestCommonSubsequence<char>(comparerNone).GetMetricLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_Default()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.LongestCommonSubstring<char>().GetMeasuredLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreCase()
    {
      var expected = 7;
      var actual = new Flux.IndexedMetrics.LongestCommonSubstring<char>(comparerIgnoreCase).GetMeasuredLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreNonSpace()
    {
      var expected = 5;
      var actual = new Flux.IndexedMetrics.LongestCommonSubstring<char>(comparerIgnoreNonSpace).GetMeasuredLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_None()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.LongestCommonSubstring<char>(comparerNone).GetMeasuredLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void MostFreqK_Default()
    //{
    //  var expected = 7;
    //  var actual = new Flux.Text.StringMetric.MostFreqK().Distance(source, target, 2, 10);
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void MostFreqK_IgnoreCase()
    //{
    //  var expected = 7;
    //  var actual = new Flux.Text.StringMetric.MostFreqK() { Comparer = comparerIgnoreCase }.Distance(source, target, 2, 10);
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void MostFreqK_IgnoreNonSpace()
    //{
    //  var expected = 8;
    //  var actual = new Flux.Text.StringMetric.MostFreqK() { Comparer = comparerIgnoreNonSpace }.Distance(source, target, 2, 10);
    //  Assert.AreEqual(expected, actual);
    //}

    //[TestMethod]
    //public void MostFreqK_None()
    //{
    //  var expected = 8;
    //  var actual = new Flux.Text.StringMetric.MostFreqK() { Comparer = comparerNone }.Distance(source, target, 2, 10);
    //  Assert.AreEqual(expected, actual);
    //}

    [TestMethod]
    public void OptimalStringAlignment_Default()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.OptimalStringAlignment<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.IndexedMetrics.OptimalStringAlignment<char>(comparerIgnoreCase).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.IndexedMetrics.OptimalStringAlignment<char>(comparerIgnoreNonSpace).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_None()
    {
      var expected = 3;
      var actual = new Flux.IndexedMetrics.OptimalStringAlignment<char>(comparerNone).GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }
  }
}
