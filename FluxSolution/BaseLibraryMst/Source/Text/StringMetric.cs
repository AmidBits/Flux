using BaseLibrary.Source.Foundation.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Flux;

namespace Text
{
  [TestClass]
  public partial class SequenceMetrics
  {
    readonly string source = "Senor Hugo";
    readonly string target = "señor hugo";

    readonly StringComparerEx comparerIgnoreCase = StringComparerEx.CurrentCultureIgnoreCase;
    readonly StringComparerEx comparerIgnoreNonSpace = StringComparerEx.CurrentCultureIgnoreNonSpace;
    readonly StringComparerEx comparerNone = StringComparerEx.Ordinal;

    [TestMethod]
    public void DamerauLevenstein_Default()
    {
      var expected = 3;
      var actual = new Flux.Metrical.DamerauLevenshteinDistance<char>().GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.Metrical.DamerauLevenshteinDistance<char>(comparerIgnoreCase).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.Metrical.DamerauLevenshteinDistance<char>(comparerIgnoreNonSpace).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_None()
    {
      var expected = 3;
      var actual = new Flux.Metrical.DamerauLevenshteinDistance<char>(comparerNone).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_Default()
    {
      var expected = 3;
      var actual = new Flux.Metrical.HammingDistance<char>(comparerNone).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.Metrical.HammingDistance<char>(comparerIgnoreCase).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.Metrical.HammingDistance<char>(comparerIgnoreNonSpace).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_None()
    {
      var expected = 3;
      var actual = new Flux.Metrical.HammingDistance<char>(comparerNone).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_Default()
    {
      var expected = 0.7999999999999999;
      var actual = new Flux.Metrical.JaroWinklerDistance<char>().GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreCase()
    {
      var expected = 0.9466666666666665;
      var actual = new Flux.Metrical.JaroWinklerDistance<char>(comparerIgnoreCase).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreNonSpace()
    {
      var expected = 0.8666666666666667;
      var actual = new Flux.Metrical.JaroWinklerDistance<char>(comparerIgnoreNonSpace).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_None()
    {
      var expected = 0.7999999999999999;
      var actual = new Flux.Metrical.JaroWinklerDistance<char>(comparerNone).GetNormalizedSimilarity(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_Default()
    {
      var expected = 3;
      var actual = new Flux.Metrical.LevenshteinDistance<char>().GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.Metrical.LevenshteinDistance<char>(comparerIgnoreCase).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.Metrical.LevenshteinDistance<char>(comparerIgnoreNonSpace).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_None()
    {
      var expected = 3;
      var actual = new Flux.Metrical.LevenshteinDistance<char>(comparerNone).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_Default()
    {
      var expected = 7;
      var actual = new Flux.Metrical.LongestCommonSubsequence<char>().GetLengthMetric(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreCase()
    {
      var expected = 9;
      var actual = new Flux.Metrical.LongestCommonSubsequence<char>(comparerIgnoreCase).GetLengthMetric(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreNonSpace()
    {
      var expected = 8;
      var actual = new Flux.Metrical.LongestCommonSubsequence<char>(comparerIgnoreNonSpace).GetLengthMetric(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_None()
    {
      var expected = 7;
      var actual = new Flux.Metrical.LongestCommonSubsequence<char>(comparerNone).GetLengthMetric(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_Default()
    {
      var expected = 3;
      var actual = new Flux.Metrical.LongestCommonSubstring<char>().GetLengthMeasure(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreCase()
    {
      var expected = 7;
      var actual = new Flux.Metrical.LongestCommonSubstring<char>(comparerIgnoreCase).GetLengthMeasure(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreNonSpace()
    {
      var expected = 5;
      var actual = new Flux.Metrical.LongestCommonSubstring<char>(comparerIgnoreNonSpace).GetLengthMeasure(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_None()
    {
      var expected = 3;
      var actual = new Flux.Metrical.LongestCommonSubstring<char>(comparerNone).GetLengthMeasure(source, target);
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
      var actual = new Flux.Metrical.OptimalStringAlignment<char>().GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreCase()
    {
      var expected = 1;
      var actual = new Flux.Metrical.OptimalStringAlignment<char>(comparerIgnoreCase).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = new Flux.Metrical.OptimalStringAlignment<char>(comparerIgnoreNonSpace).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_None()
    {
      var expected = 3;
      var actual = new Flux.Metrical.OptimalStringAlignment<char>(comparerNone).GetEditDistance(source, target);
      Assert.AreEqual(expected, actual);
    }
  }
}
