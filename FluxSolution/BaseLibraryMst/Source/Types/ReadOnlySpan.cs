﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
  [TestClass]
  public class ReadOnlySpan
  {
    [TestMethod]
    public void BoyerMooreHorspoolSearch()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = 3;
      var actual = span.BoyerMooreHorspoolSearch(new int[] { 10, 20, 30 });

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Extrema()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expectedIndexMinimum = 11;
      var expectedIndexMaximum = 7;
      var (actualIndexMinimum, actualIndexMaximum) = span.Extrema(n => n);

      Assert.AreEqual(expectedIndexMinimum, actualIndexMinimum);
      Assert.AreEqual(expectedIndexMaximum, actualIndexMaximum);
    }

    [TestMethod]
    public void ExtremaClosestTo()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expectedIndexMinimum = 6;
      var expectedIndexMaximum = 8;
      var (actualIndexMinimum, actualIndexMaximum) = span.ExtremaClosestTo(60, n => n);

      Assert.AreEqual(expectedIndexMinimum, actualIndexMinimum);
      Assert.AreEqual(expectedIndexMaximum, actualIndexMaximum);
    }

    [TestMethod]
    public void KnuthMorrisPrattSearch()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 3, 11 };
      var actual = span.KnuthMorrisPrattSearch(new int[] { 10, 20, 30 });

      Assert.AreEqual(expected.Length, actual.Count, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }

    [TestMethod]
    public void LongestAlternatingSubsequence()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 45, 60, 10, 20, 70, 80, 40, 20 };
      var actual = span.LongestAlternatingSubsequence();

      Assert.AreEqual(expected.Length, actual.Length, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }

    [TestMethod]
    public void LongestIncreasingSubsequence()
    {
      var span = new System.ReadOnlySpan<int>(new int[] { 45, 60, 90, 10, 20, 30, 50, 100, 70, 80, 40, 10, 20, 30 });

      var expected = new int[] { 10, 20, 30, 50, 70, 80 };
      var actual = span.LongestIncreasingSubsequence();

      Assert.AreEqual(expected.Length, actual.Length, "Element count is different.");
      CollectionAssert.AreEqual(expected, actual, "Values are different.");
    }
  }
}