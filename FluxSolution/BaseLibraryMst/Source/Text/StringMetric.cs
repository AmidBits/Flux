using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Text
{
  [TestClass]
  public partial class SequenceMetrics
  {
    readonly string source = "Senor Hugo";
    readonly string target = "señor hugo";

    readonly System.Collections.Generic.IEqualityComparer<char> comparerIgnoreCase = Flux.StringComparerEx.CurrentCultureIgnoreCase;
    readonly System.Collections.Generic.IEqualityComparer<char> comparerIgnoreNonSpace = Flux.StringComparerEx.CurrentCultureIgnoreNonSpace;
    readonly System.Collections.Generic.IEqualityComparer<char> comparerNone = Flux.StringComparerEx.Ordinal;

    [TestMethod]
    public void DamerauLevenstein_Default()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.DamerauLevenshteinDistance().MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreCase()
    {
      var expected = 1;
      //var actual = new Flux.Text.StringMetric.DamerauLevenshteinDistance() { Comparer = comparerIgnoreCase }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreNonSpace()
    {
      var expected = 2;
      //var actual = new Flux.Text.StringMetric.DamerauLevenshteinDistance() { Comparer = comparerIgnoreNonSpace }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_None()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.DamerauLevenshteinDistance() { Comparer = comparerNone }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.DamerauLevenshteinDistance<char>().GetMetricDistance(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_Default()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.HammingDistance().MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreCase()
    {
      var expected = 1;
      //var actual = new Flux.Text.StringMetric.HammingDistance() { Comparer = comparerIgnoreCase }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreNonSpace()
    {
      var expected = 2;
      //var actual = new Flux.Text.StringMetric.HammingDistance() { Comparer = comparerIgnoreNonSpace }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_None()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.HammingDistance() { Comparer = comparerNone }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.HammingDistance<char>().GetMetricDistance(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_Default()
    {
      var expected = 0.7999999999999999;
      //var actual = new Flux.Text.StringMetric.JaroWinklerDistance().Similarity(source, target);
      var actual = new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreCase()
    {
      var expected = 0.9466666666666665;
      //var actual = new Flux.Text.StringMetric.JaroWinklerDistance() { Comparer = comparerIgnoreCase }.Similarity(source, target);
      var actual = new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreNonSpace()
    {
      var expected = 0.8666666666666667;
      //var actual = new Flux.Text.StringMetric.JaroWinklerDistance() { Comparer = comparerIgnoreNonSpace }.Similarity(source, target);
      var actual = new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_None()
    {
      var expected = 0.7999999999999999;
      //var actual = new Flux.Text.StringMetric.JaroWinklerDistance() { Comparer = comparerNone }.Similarity(source, target);
      var actual = new Flux.SequenceMetrics.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_Default()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.LevenshteinDistance().MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreCase()
    {
      var expected = 1;
      //var actual = new Flux.Text.StringMetric.LevenshteinDistance() { Comparer = comparerIgnoreCase }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreNonSpace()
    {
      var expected = 2;
      //var actual = new Flux.Text.StringMetric.LevenshteinDistance() { Comparer = comparerIgnoreNonSpace }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_None()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.LevenshteinDistance() { Comparer = comparerNone }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.LevenshteinDistance<char>().GetMetricDistance(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_Default()
    {
      var expected = 7;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence().MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreCase()
    {
      var expected = 9;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerIgnoreCase }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreNonSpace()
    {
      var expected = 8;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerIgnoreNonSpace }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_None()
    {
      var expected = 7;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerNone }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubsequence<char>().GetMetricLength(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_Default()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence().MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreCase()
    {
      var expected = 7;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerIgnoreCase }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreNonSpace()
    {
      var expected = 5;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerIgnoreNonSpace }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_None()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.LongestCommonSubsequence() { Comparer = comparerNone }.MetricLength(source, target);
      var actual = new Flux.SequenceMetrics.LongestCommonSubstring<char>().GetMeasuredLength(source, target, comparerNone);
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
      //var actual = new Flux.Text.StringMetric.OptimalStringAlignment().MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreCase()
    {
      var expected = 1;
      //var actual = new Flux.Text.StringMetric.OptimalStringAlignment() { Comparer = comparerIgnoreCase }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(source, target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreNonSpace()
    {
      var expected = 2;
      //var actual = new Flux.Text.StringMetric.OptimalStringAlignment() { Comparer = comparerIgnoreNonSpace }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(source, target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_None()
    {
      var expected = 3;
      //var actual = new Flux.Text.StringMetric.OptimalStringAlignment() { Comparer = comparerNone }.MetricDistance(source, target);
      var actual = new Flux.SequenceMetrics.OptimalStringAlignment<char>().GetMetricDistance(source, target, comparerNone);
      Assert.AreEqual(expected, actual);
    }
  }
}
