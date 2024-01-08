using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Radix
  {
    [TestMethod]
    public void DigitCount()
    {
      Assert.AreEqual(7, Flux.Units.Radix.DigitCount(1234567, 10));
    }

    [TestMethod]
    public void DigitSum()
    {
      Assert.AreEqual(28, Flux.Units.Radix.DigitSum(1234567, 10));
    }

    [TestMethod]
    public void DropLeastSignificantDigit()
    {
      Assert.AreEqual(123456, Flux.Units.Radix.DropLeastSignificantDigit(1234567, 10));
    }

    [TestMethod]
    public void DropLeastSignificantDigits()
    {
      Assert.AreEqual(1234, Flux.Units.Radix.DropLeastSignificantDigits(1234567, 10, 3));
    }

    [TestMethod]
    public void DropMostSignificantDigits()
    {
      Assert.AreEqual(4567, Flux.Units.Radix.DropMostSignificantDigits(1234567, 10, 3));
    }

    [TestMethod]
    public void GetDigits()
    {
      var expected = new System.Numerics.BigInteger[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0 };
      var actual = Flux.Units.Radix.GetDigits(670530.ToBigInteger(), 2).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=2"));

      expected = new System.Numerics.BigInteger[] { 6, 7, 0, 5, 3, 0 };
      actual = Flux.Units.Radix.GetDigits(670530.ToBigInteger(), 10).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=10"));

      expected = new System.Numerics.BigInteger[] { 10, 3, 11, 4, 2 };
      actual = Flux.Units.Radix.GetDigits(670530.ToBigInteger(), 16).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=16"));
    }

    [TestMethod]
    public void GetDigitsReversed()
    {
      var expected = new System.Numerics.BigInteger[] { 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 };
      var actual = Flux.Units.Radix.GetDigitsReversed(670530.ToBigInteger(), 2).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=2"));

      expected = new System.Numerics.BigInteger[] { 0, 3, 5, 0, 7, 6 };
      actual = Flux.Units.Radix.GetDigitsReversed(670530.ToBigInteger(), 10).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=10"));

      expected = new System.Numerics.BigInteger[] { 2, 4, 11, 3, 10 };
      actual = Flux.Units.Radix.GetDigitsReversed(670530.ToBigInteger(), 16).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Units.Radix.GetDigits) + ".Radix=16"));
    }

    [TestMethod]
    public void GetPlaceValues()
    {
      var expected = new int[] { 2, 10, 500 };
      var actual = Flux.Units.Radix.GetPlaceValues(512, 10);
      Assert.AreEqual(expected.Length, actual.Count());
      for (var i = expected.Length - 1; i >= 0; i--)
        Assert.AreEqual(expected[i], actual[i], $"{nameof(GetPlaceValues)} index {i}, expected {expected[i]} != actual {expected[i]}");
    }

    [TestMethod]
    public void IntegerLog()
    {
      var (logFloor, logCeiling) = Flux.Units.Radix.IntegerLog(512.ToBigInteger(), 10);

      Assert.AreEqual(2.ToBigInteger(), logFloor);
      Assert.AreEqual(3.ToBigInteger(), logCeiling);
    }

    [TestMethod]
    public void IntegerLogCeiling()
    {
      Assert.AreEqual(3, Flux.Units.Radix.IntegerLog(512.ToBigInteger() - 1, 10).ilogAwayFromZero);
    }

    [TestMethod]
    public void IntegerLogFloor()
    {
      Assert.AreEqual(2, Flux.Units.Radix.IntegerLog(512, 10).ilogTowardZero);
    }

    [TestMethod]
    public void IsIntegerPowOf()
    {
      Assert.AreEqual(false, Flux.Units.Radix.IsPowOf(511, 2));
      Assert.AreEqual(true, Flux.Units.Radix.IsPowOf(512, 2));
    }

    [TestMethod]
    public void IsJumbled()
    {
      Assert.AreEqual(false, Flux.Units.Radix.IsJumbled(512, 10));
    }

    [TestMethod]
    public void IsSingleDigit()
    {
      Assert.AreEqual(false, Flux.Units.Radix.IsSingleDigit(512, 10));
    }

    [TestMethod]
    public void KeepLeastSignificantDigit()
    {
      Assert.AreEqual(7, Flux.Units.Radix.KeepLeastSignificantDigit(1234567, 10));
    }

    [TestMethod]
    public void KeepLeastSignificantDigits()
    {
      Assert.AreEqual(567, Flux.Units.Radix.KeepLeastSignificantDigits(1234567, 10, 3));
    }

    [TestMethod]
    public void KeepMostSignificantDigits()
    {
      Assert.AreEqual(123, Flux.Units.Radix.KeepMostSignificantDigits(1234567, 10, 3));
    }

    [TestMethod]
    public void LocateIntegerPowOf()
    {
      Flux.Units.Radix.PowOf(1234567.ToBigInteger(), 10, false, RoundingMode.HalfAwayFromZero, out var nearestTowardsZero, out var nearestAwayFromZero);

      Assert.AreEqual(1000000, nearestTowardsZero);
      Assert.AreEqual(10000000, nearestAwayFromZero);
    }

    //[TestMethod]
    //public void NearestPowOf2()
    //{
    //  Assert.AreEqual(128, Flux.GenericMath.NearestPowOf(101, 2, false, RoundingMode.HalfToEven, out var _, out var _));
    //}

    //[TestMethod]
    //public void NearestPowOf8()
    //{
    //  Assert.AreEqual(64, Flux.GenericMath.NearestPowOf(101, 8, false, RoundingMode.HalfToEven, out var _, out var _));
    //}

    //[TestMethod]
    //public void NearestPowOf10()
    //{
    //  Assert.AreEqual(100, Flux.GenericMath.NearestPowOf(101, 10, false, RoundingMode.HalfToEven, out var _, out var _));
    //}

    //[TestMethod]
    //public void NearestPowOf16()
    //{
    //  Assert.AreEqual(16, Flux.GenericMath.NearestPowOf(101, 16, false, RoundingMode.HalfToEven, out var _, out var _));
    //}

    [TestMethod]
    public void ReverseDigits()
    {
      Assert.AreEqual(7654321, Flux.Units.Radix.ReverseDigits(1234567, 10));
    }

    [TestMethod]
    public void SelfNumber()
    {
      var expected = new int[] { 1, 3, 5, 7, 9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 211, 222, 233, 244, 255, 266, 277, 288, 299, 310, 312, 323, 334, 345, 356, 367, 378, 389, 400, 411, 413, 424, 435, 446, 457, 468, 479, 490 };
      var actual = System.Linq.Enumerable.Range(1, 500).Where(i => Flux.Units.Radix.IsSelfNumber(i, 10)).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToRadixString()
    {
      Assert.AreEqual("1234567", 1234567.ToBigInteger().ToRadixString(10).ToString());
    }

    [TestMethod]
    public void ToSubscriptString()
    {
      Assert.AreEqual("₁₂₃₄₅₆₇", Flux.Units.Radix.ToSubscriptString(1234567.ToBigInteger(), 10));
    }

    [TestMethod]
    public void ToSuperscriptString()
    {
      Assert.AreEqual("¹²³⁴⁵⁶⁷", Flux.Units.Radix.ToSuperscriptString(1234567.ToBigInteger(), 10));
    }
  }
}
