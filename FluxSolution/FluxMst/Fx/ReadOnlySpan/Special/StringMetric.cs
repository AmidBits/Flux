using System;
using Flux;
using Flux.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetFx.ReadOnlySpan.Special
{
  [TestClass]
  public partial class StringMetric
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
      var actual = source.AsSpan().DamerauLevenshteinDistance(target, out var _);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreCase()
    {
      var expected = 1;
      var actual = source.AsSpan().DamerauLevenshteinDistance(target, out var _, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = source.AsSpan().DamerauLevenshteinDistance(target, out var _, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenstein_None()
    {
      var expected = 3;
      var actual = source.AsSpan().DamerauLevenshteinDistance(target, out var _, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_Default()
    {
      var expected = 3;
      var actual = source.AsSpan().HammingDistance(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = source.AsSpan().HammingDistance(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = source.AsSpan().HammingDistance(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HammingDistance_None()
    {
      var expected = 3;
      var actual = source.AsSpan().HammingDistance(target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_Default()
    {
      var expected = 0.7999999999999999;
      var actual = source.AsSpan().JaroWinklerSimilarity(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreCase()
    {
      var expected = 0.9466666666666665;
      var actual = source.AsSpan().JaroWinklerSimilarity(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_IgnoreNonSpace()
    {
      var expected = 0.8666666666666667;
      var actual = source.AsSpan().JaroWinklerSimilarity(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity_None()
    {
      var expected = 0.7999999999999999;
      var actual = source.AsSpan().JaroWinklerSimilarity(target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_Default()
    {
      var expected = 3;
      var actual = source.AsSpan().LevenshteinDistance(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreCase()
    {
      var expected = 1;
      var actual = source.AsSpan().LevenshteinDistance(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = source.AsSpan().LevenshteinDistance(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LevenshteinDistance_None()
    {
      var expected = 3;
      var actual = source.AsSpan().LevenshteinDistance(target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_Default()
    {
      var expected = 7;
      var actual = source.AsSpan().LongestCommonSubsequenceLength(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreCase()
    {
      var expected = 9;
      var actual = source.AsSpan().LongestCommonSubsequenceLength(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_IgnoreNonSpace()
    {
      var expected = 8;
      var actual = source.AsSpan().LongestCommonSubsequenceLength(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubsequence_None()
    {
      var expected = 7;
      var actual = source.AsSpan().LongestCommonSubsequenceLength(target, comparerNone);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_Default()
    {
      var expected = 3;
      var actual = source.AsSpan().LongestCommonSubstringLength(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreCase()
    {
      var expected = 7;
      var actual = source.AsSpan().LongestCommonSubstringLength(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_IgnoreNonSpace()
    {
      var expected = 5;
      var actual = source.AsSpan().LongestCommonSubstringLength(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring_None()
    {
      var expected = 3;
      var actual = source.AsSpan().LongestCommonSubstringLength(target, comparerNone);
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
      var actual = source.AsSpan().OptimalStringAlignment(target);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreCase()
    {
      var expected = 1;
      var actual = source.AsSpan().OptimalStringAlignment(target, comparerIgnoreCase);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_IgnoreNonSpace()
    {
      var expected = 2;
      var actual = source.AsSpan().OptimalStringAlignment(target, comparerIgnoreNonSpace);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OptimalStringAlignment_None()
    {
      var expected = 3;
      var actual = source.AsSpan().OptimalStringAlignment(target, comparerNone);
      Assert.AreEqual(expected, actual);
    }
  }
}
