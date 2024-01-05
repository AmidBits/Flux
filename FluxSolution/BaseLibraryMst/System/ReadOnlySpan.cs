using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System
{
  [TestClass]
  public class ReadOnlySpan
  {
    [TestMethod]
    public void BoothsMinimalRotation()
    {
      var text = "bbaaccaadd".AsSpan();
      var expected = 2;
      var actual = text.BoothsMinimalRotation();
      Assert.AreEqual(expected, actual);

      text = "GEEKSQUIZ".AsSpan();
      expected = 1;
      actual = text.BoothsMinimalRotation();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void BoyerMooreHorspoolAlgorithm()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = 3;
      var actual = span.BoyerMooreHorspoolFindIndex(new int[] { 10, 20, 30 });

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DamerauLevenshteinDistance()
    {
      var text1 = "CA".AsSpan();
      var text2 = "ABC".AsSpan();
      var expected = 2;
      var actual = text1.GetDamerauLevenshteinDistanceMetric(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetExtremum()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expectedIndexMin = 11;
      var expectedIndexMax = 7;

      var (actualIndexMin, actualValueMin, actualIndexMax, actualValueMax) = span.GetExtremum(n => n, null);

      Assert.AreEqual(expectedIndexMin, actualIndexMin);
      Assert.AreEqual(expectedIndexMax, actualIndexMax);
    }

    [TestMethod]
    public void GetInfimumAndSupremum()
    {
      var span = new System.ReadOnlySpan<int>([45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30]);

      var (actualIndexMin, actualValueMin, actualIndexMax, actualValueMax) = span.GetInfimumAndSupremum(60, n => n, false);
      Assert.AreEqual(6, actualIndexMin);
      Assert.AreEqual(8, actualIndexMax);

      (actualIndexMin, actualValueMin, actualIndexMax, actualValueMax) = span.GetInfimumAndSupremum(55, n => n, true);
      Assert.AreEqual(6, actualIndexMin);
      Assert.AreEqual(1, actualIndexMax);
    }

    [TestMethod]
    public void HammingDistance()
    {
      var text1 = "karolin".AsSpan();
      var text2 = "kathrin".AsSpan();
      var expected = 3;
      var actual = text1.GetHammingDistance(text2);
      Assert.AreEqual(expected, actual);

      text1 = "karolin".AsSpan();
      text2 = "kerstin".AsSpan();
      expected = 3;
      actual = text1.GetHammingDistance(text2);
      Assert.AreEqual(expected, actual);

      text1 = "kathrin".AsSpan();
      text2 = "kerstin".AsSpan();
      expected = 4;
      actual = text1.GetHammingDistance(text2);
      Assert.AreEqual(expected, actual);

      text1 = "2173896".AsSpan();
      text2 = "2233796".AsSpan();
      expected = 3;
      actual = text1.GetHammingDistance(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JackardIndex()
    {
      var collection1 = new int[] { 1, 2, 3 }.AsSpan().AsReadOnlySpan();
      var collection2 = new int[] { 2, 3, 4, 5, 6 }.AsSpan().AsReadOnlySpan();
      var expected = 0.3333333333333333;
      var actual = collection1.JackardIndex(collection2);
      Assert.AreEqual(expected, actual);

      collection1 = new int[] { 1, 2, 3, 4, 5 }.AsSpan().AsReadOnlySpan();
      collection2 = new int[] { 4, 5, 6, 7, 8, 9, 10 }.AsSpan().AsReadOnlySpan();
      expected = 0.2;
      actual = collection1.JackardIndex(collection2);
      Assert.AreEqual(expected, actual);

      collection1 = new int[] { 1, 2, 3, 4, 5 };
      collection2 = new int[] { 4, 5, 6, 7, 8 };
      expected = 0.25;
      actual = collection1.JackardIndex(collection2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void JaroWinklerSimilarity()
    {
      var text1 = "FAREMVIEL".AsSpan();
      var text2 = "FARMVILLE".AsSpan();
      var expected = 0.9189814814814814;
      var actual = text1.GetJaroWinklerSimilarity(text2);
      Assert.AreEqual(expected, actual);

      text1 = "CRATE".AsSpan();
      text2 = "TRACE".AsSpan();
      expected = 0.7333333333333334;
      actual = text1.GetJaroWinklerSimilarity(text2);
      Assert.AreEqual(expected, actual);

      text1 = "DwAyNE".AsSpan();
      text2 = "DuANE".AsSpan();
      expected = 0.8400000000000001;
      actual = text1.GetJaroWinklerSimilarity(text2);
      Assert.AreEqual(expected, actual);

      text1 = "TRATE".AsSpan();
      text2 = "TRACE".AsSpan();
      expected = 0.9066666666666667;
      actual = text1.GetJaroWinklerSimilarity(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void KnuthMorrisPrattSearch()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 3, 11 };
      var actual = span.KnuthMorrisPrattFindIndices(new int[] { 10, 20, 30 });

      Assert.AreEqual(expected.Length, actual.Count, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }

    [TestMethod]
    public void LevenshteinDistance()
    {
      var text1 = "kitten".AsSpan();
      var text2 = "sitten".AsSpan();
      var expected = 1;
      var actual = text1.GetLevenshteinDistanceMetric(text2);
      Assert.AreEqual(expected, actual);

      text1 = "sitten".AsSpan();
      text2 = "sittin".AsSpan();
      expected = 1;
      actual = text1.GetLevenshteinDistanceMetric(text2);
      Assert.AreEqual(expected, actual);

      text1 = "sittin".AsSpan();
      text2 = "sitting".AsSpan();
      expected = 1;
      actual = text1.GetLevenshteinDistanceMetric(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestAlternatingSubsequence()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 45, 60, 10, 20, 70, 80, 40, 20 }.ToList();
      var actual = span.GetLongestAlternatingSubsequenceValues(out var _).ToList();

      Assert.AreEqual(expected.Count, actual.Count, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }

    [TestMethod]
    public void LongestCommonSubsequence()
    {
      var text1 = "ABCD".AsSpan();
      var text2 = "ACBAD".AsSpan();
      var expected = 3;
      var actual = text1.GetLongestCommonSubsequenceLength(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestCommonSubstring()
    {
      var text1 = "ABABC".AsSpan();
      var text2 = "BABCA".AsSpan();
      var expected = 4;
      var actual = text1.GetLongestCommonSubstringLength(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LongestIncreasingSubsequence()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 10, 20, 30, 50, 70, 80 };
      var actual = span.GetLongestIncreasingSubsequenceValues(out var _);

      Assert.AreEqual(expected.Length, actual.Length, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }

    [TestMethod]
    public void MaximumSumSubarray()
    {
      var collection = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }.AsSpan().AsReadOnlySpan();
      var expectedStartIndex = 3;
      var expectedCount = 4;
      var expectedSum = 6;
      var actualSum = collection.MaximumSumSubarray(out var actualStartIndex, out var actualCount);
      Assert.AreEqual(expectedStartIndex, actualStartIndex);
      Assert.AreEqual(expectedCount, actualCount);
      Assert.AreEqual(expectedSum, actualSum);
    }

    [TestMethod]
    public void OptimalStringAlignment()
    {
      var text1 = "CA".AsSpan();
      var text2 = "ABC".AsSpan();
      var expected = 3;
      var actual = text1.GetOptimalStringAlignmentMetric(text2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OverlapCoefficient()
    {
      var collection1 = new int[] { 1, 2, 3 }.AsSpan().AsReadOnlySpan();
      var collection2 = new int[] { 2, 3, 4, 5, 6 }.AsSpan().AsReadOnlySpan();
      var expected = 0.6666666666666666;
      var actual = collection1.OverlapCoefficient(collection2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void PrefixFunction()
    {
      var text = "abcabcd".AsSpan();
      var expected = new int[] { 0, 0, 0, 1, 2, 3, 0 };
      var actual = text.PrefixFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 2");

      text = "aabaaab".AsSpan();
      expected = new int[] { 0, 1, 0, 1, 2, 2, 3 };
      actual = text.PrefixFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 3");

      text = "abdeca".AsSpan();
      expected = new int[] { 0, 0, 0, 0, 0, 1 };
      actual = text.PrefixFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 1");
    }

    [TestMethod]
    public void ShortestBalancingSubstring()
    {
      var collection1 = new int[] { 'G', 'A', 'A', 'A', 'T', 'A', 'A', 'A' }.AsSpan().AsReadOnlySpan();
      var collection2 = new int[] { 'A', 'C', 'T', 'G' }.AsSpan().AsReadOnlySpan();
      var expected = (1, 5);
      var actual = collection1.ShortestBalancingSubstring(collection2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ShortestCommonSupersequence()
    {
      var text1 = "abcbdab".AsSpan();
      var text2 = "bdcaba".AsSpan();
      var expectedLength = 9;
      text1.GetShortestCommonSupersequenceMatrix(text2, out var actualLength);
      Assert.AreEqual(expectedLength, actualLength);
    }

    [TestMethod]
    public void SubsetSumMatrix()
    {
      var text = new int[] { 3, 34, 4, 12, 5, 2 }.AsSpan().AsReadOnlySpan();
      var expected = true;
      var actual = text.IsSubsetSum(9);
      Assert.AreEqual(expected, actual);

      text = new int[] { 3, 34, 4, 12, 5, 2 }.AsSpan().AsReadOnlySpan();
      expected = false;
      actual = text.IsSubsetSum(30);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SørensenDiceCoefficient()
    {
      var collection1 = new int[] { 1, 2, 3 }.AsSpan().AsReadOnlySpan();
      var collection2 = new int[] { 2, 3, 4, 5, 6 }.AsSpan().AsReadOnlySpan();
      var expected = 0.5;
      var actual = collection1.SørensenDiceCoefficient(collection2);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ZFunction()
    {
      var text = "aaaaa".AsSpan();
      var expected = new int[] { 0, 4, 3, 2, 1 };
      var actual = text.ZFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 1");

      text = "aaabaab".AsSpan();
      expected = new int[] { 0, 2, 1, 0, 2, 1, 0 };
      actual = text.ZFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 2");

      text = "abacaba".AsSpan();
      expected = new int[] { 0, 0, 1, 0, 3, 0, 1 };
      actual = text.ZFunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 3");
    }
  }
}
