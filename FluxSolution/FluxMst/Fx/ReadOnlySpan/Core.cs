using System;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetFx.ReadOnlySpan
{
  [TestClass]
  public class Base
  {
    [TestMethod]
    public void CommonSuffixLength()
    {
      var expected = 3;
      var actual = "Robert".AsSpan().CommonSuffixLength("Rupert", null);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EndsWith()
    {
      var expected = true;
      var actual = "Robert".AsSpan().EndsWith("ert");
      Assert.AreEqual(expected, actual);

      expected = false;
      actual = "Robert".AsSpan().EndsWith("Bert");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAnyAt()
    {
      var expected = false;
      var actual = "Robert".AsSpan().EqualsAnyAt(2, 2, ["do", "re", "mi"]);
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = "Robert".AsSpan().EqualsAnyAt(2, 2, ["bo", "bitter", "be"]);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EqualsAt()
    {
      var expected = false;
      var actual = "Robert".AsSpan().EqualsAt(2, "re");
      Assert.AreEqual(expected, actual);

      expected = true;
      actual = "Robert".AsSpan().EqualsAt(2, "be");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetExtremum()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expectedIndexMin = 11;
      var expectedIndexMax = 7;

      var (actualIndexMin, actualItemMin, actualValueMin, actualIndexMax, actualItemMax, actualValueMax) = span.Extremum(n => n, null);

      Assert.AreEqual(expectedIndexMin, actualIndexMin);
      Assert.AreEqual(expectedIndexMax, actualIndexMax);
    }

    [TestMethod]
    public void GetInfimumAndSupremum()
    {
      var span = new System.ReadOnlySpan<int>([45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30]);

      var (actualIndexMin, actualItemMin, actualValueMin, actualIndexMax, actualItemMax, actualValueMax) = span.InfimumAndSupremum(60, n => n, true);
      Assert.AreEqual(6, actualIndexMin);
      Assert.AreEqual(8, actualIndexMax);

      (actualIndexMin, actualItemMin, actualValueMin, actualIndexMax, actualItemMax, actualValueMax) = span.InfimumAndSupremum(55, n => n, false);
      Assert.AreEqual(6, actualIndexMin);
      Assert.AreEqual(1, actualIndexMax);
    }

    [TestMethod]
    public void IndexOf()
    {
      var expected = 3;
      var actual = "Robert Serious".AsSpan().IndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IsIsomorphic()
    {
      var expected = true;
      var actual = new Flux.SpanMaker<char>("egg").AsReadOnlySpan().AreIsomorphic("add");
      Assert.AreEqual(expected, actual);
      expected = false;
      actual = new Flux.SpanMaker<char>("foo").AsReadOnlySpan().AreIsomorphic("bar");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void IsPalindrome()
    {
      var palindrome = "Poor Dan is in a droop".ToSpanMaker().RemoveAll(char.IsWhiteSpace).AsSpan().ToLower().AsReadOnlySpan();
      var expected = true;
      var actual = palindrome.IsPalindrome();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LastIndexOf()
    {
      var expected = 8;
      var actual = "Robert Serious".AsSpan().LastIndexOf("er");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LeftMost()
    {
      var expected = @"Rob";
      var actual = @"Rob".AsSpan().LeftMost(10);
      Assert.AreEqual(expected, actual.ToString());
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
    public void RightMost()
    {
      var expected = @"ob";
      var actual = @"Rob".AsSpan().RightMost(2);
      Assert.AreEqual(expected, actual.ToString());

      expected = @"Rob";
      actual = @"Rob".AsSpan().RightMost(10);
      Assert.AreEqual(expected, actual.ToString());
    }

    [TestMethod]
    public void SetExcept()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[] { 1, 2, 3 };
      var actual = a.Except(b).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetExcept");
    }

    [TestMethod]
    public void SetIntersect()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[] { 4, 5 };
      var actual = a.Intersect(b).Order().ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetIntersect");
    }

    [TestMethod]
    public void SetPowerSet()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[][] { [], [1], [2], [3], [4], [5], [1, 2], [1, 3], [1, 4], [1, 5], [2, 3], [2, 4], [2, 5], [3, 4], [3, 5], [4, 5], [1, 2, 3], [1, 2, 4], [1, 2, 5], [1, 3, 4], [1, 3, 5], [1, 4, 5], [2, 3, 4], [2, 3, 5], [2, 4, 5], [3, 4, 5], [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 4, 5], [1, 3, 4, 5], [2, 3, 4, 5], [1, 2, 3, 4, 5] };

      var actual = a.PowerSet().OrderBy(a => a.Length).ThenBy(a => string.Join(",", a.Order())).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetPowerSet");
    }

    [TestMethod]
    public void SetSymmetricExcept()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[] { 1, 2, 3, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.SymmetricExcept(b).ToArray(); System.Array.Sort(actual);

      CollectionAssert.AreEqual(expected, actual, "SetIntersect");
    }

    [TestMethod]
    public void SetUnion()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.Union(b).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetUnion");
    }

    [TestMethod]
    public void SetUnionAll()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsReadOnlySpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsReadOnlySpan();

      var expected = new int[] { 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.UnionAll(b);

      CollectionAssert.AreEqual(expected, actual, "SetUnionAll");
    }

    [TestMethod]
    public void StartsWith()
    {
      var expected = true;
      var actual = @"Robs boat.".AsSpan().StartsWith(@"Rob");
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SubsetSumMatrix()
    {
      var text = (ReadOnlySpan<int>)new int[] { 3, 34, 4, 12, 5, 2 }.AsSpan();
      var expected = true;
      var actual = text.IsSubsetSum(9);
      Assert.AreEqual(expected, actual);

      text = (ReadOnlySpan<int>)new int[] { 3, 34, 4, 12, 5, 2 }.AsSpan();
      expected = false;
      actual = text.IsSubsetSum(30);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ZFunction()
    {
      var text = "aaaaa".AsSpan();
      var expected = new int[] { 0, 4, 3, 2, 1 };
      var actual = text.Zfunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 1");

      text = "aaabaab".AsSpan();
      expected = new int[] { 0, 2, 1, 0, 2, 1, 0 };
      actual = text.Zfunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 2");

      text = "abacaba".AsSpan();
      expected = new int[] { 0, 0, 1, 0, 3, 0, 1 };
      actual = text.Zfunction();
      CollectionAssert.AreEqual(expected, actual, "ZFunction 3");
    }
  }
}
