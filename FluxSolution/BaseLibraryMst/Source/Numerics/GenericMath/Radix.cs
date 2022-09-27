using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;
using System;

namespace GenericMath
{
  [TestClass]
  public class Radix
  {
    [TestMethod]
    public void DigitCount()
    {
      Assert.AreEqual(3.ToBigInteger(), 512.ToBigInteger().DigitCount(10));
    }

    [TestMethod]
    public void DigitSum()
    {
      Assert.AreEqual(8.ToBigInteger(), 512.ToBigInteger().DigitSum(10));
    }

    [TestMethod]
    public void DropLeadingDigit()
    {
      Assert.AreEqual(12.ToBigInteger(), 512.ToBigInteger().DropLeadingDigit(10));
    }

    [TestMethod]
    public void DropTrailingDigit()
    {
      Assert.AreEqual(51.ToBigInteger(), 512.ToBigInteger().DropTrailingDigit(10));
    }

    [TestMethod]
    public void GetDigits()
    {
      var expected = new System.Numerics.BigInteger[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0 };
      var actual = 670530.ToBigInteger().GetDigits(2).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=2");

      expected = new System.Numerics.BigInteger[] { 6, 7, 0, 5, 3, 0 };
      actual = 670530.ToBigInteger().GetDigits(10).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=10");

      expected = new System.Numerics.BigInteger[] { 10, 3, 11, 4, 2 };
      actual = 670530.ToBigInteger().GetDigits(16).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=16");
    }

    [TestMethod]
    public void GetDigitsReversed()
    {
      var expected = new System.Numerics.BigInteger[] { 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 };
      var actual = 670530.ToBigInteger().GetDigitsReversed(2).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=2");

      expected = new System.Numerics.BigInteger[] { 0, 3, 5, 0, 7, 6 };
      actual = 670530.ToBigInteger().GetDigitsReversed(10).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=10");

      expected = new System.Numerics.BigInteger[] { 2, 4, 11, 3, 10 };
      actual = 670530.ToBigInteger().GetDigitsReversed(16).ToArray();
      CollectionAssert.AreEqual(expected, actual, nameof(Flux.GenericMath.GetDigits) + ".Radix=16");
    }

    [TestMethod]
    public void GetLeastSignificantDigit()
    {
      Assert.AreEqual(2.ToBigInteger(), 512.ToBigInteger().GetLeastSignificantDigit(10));
    }

    [TestMethod]
    public void GetMostSignificantDigit()
    {
      Assert.AreEqual(5, 512.GetMostSignificantDigit(10));
    }

    [TestMethod]
    public void GetPlaceValues()
    {
      var expected = new int[] { 2, 10, 500 };
      var actual = 512.GetPlaceValues(10);
      Assert.AreEqual(expected.Length, actual.Length);
      for (var i = expected.Length - 1; i >= 0; i--)
        Assert.AreEqual(expected[i], actual[i], $"{nameof(GetPlaceValues)} index {i}, expected {expected[i]} != actual {expected[i]}");
    }

    [TestMethod]
    public void IntegerLogCeiling()
    {
      Assert.AreEqual(3, 512.IntegerLogCeiling(10));
    }

    [TestMethod]
    public void IntegerLogFloor()
    {
      Assert.AreEqual(2, 512.IntegerLogFloor(10));
    }

    [TestMethod]
    public void TryGetIntegerLog()
    {
      512.TryGetIntegerLog(10, out var logFloor, out var logCeiling);

      Assert.AreEqual(2, logFloor);
      Assert.AreEqual(3, logCeiling);
    }

    [TestMethod]
    public void IsJumbled()
    {
      Assert.AreEqual(false, 512.IsJumbled(10));
    }

    [TestMethod]
    public void IsPow()
    {
      Assert.AreEqual(true, 512.IsPow(2));
    }

    [TestMethod]
    public void IsSingleDigit()
    {
      Assert.AreEqual(false, 512.IsSingleDigit(10));
    }

    [TestMethod]
    public void ReverseDigits()
    {
      Assert.AreEqual(215, 512.ReverseDigits(10));
    }
  }
}
