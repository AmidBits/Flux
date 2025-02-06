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
      Assert.AreEqual(7, 1234567.DigitCount(10));
    }

    [TestMethod]
    public void DigitSum()
    {
      Assert.AreEqual(28, 1234567.DigitSum(10));
    }

    [TestMethod]
    public void DropLeastSignificantDigit()
    {
      Assert.AreEqual(123456, 1234567.DropLeastSignificantDigit(10));
    }

    [TestMethod]
    public void DropLeastSignificantDigits()
    {
      Assert.AreEqual(1234, 1234567.DropLeastSignificantDigits(10, 3));
    }

    [TestMethod]
    public void DropMostSignificantDigits()
    {
      Assert.AreEqual(4567, 1234567.DropMostSignificantDigits(10, 3));
    }

    [TestMethod]
    public void GetDigits()
    {
      var expected = new int[] { 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0 };
      var actual = 670530.GetDigits(2).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=2"));

      expected = new int[] { 6, 7, 0, 5, 3, 0 };
      actual = 670530.GetDigits(10).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=10"));

      expected = new int[] { 10, 3, 11, 4, 2 };
      actual = 670530.GetDigits(16).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=16"));
    }

    [TestMethod]
    public void GetDigitsReversed()
    {
      var expected = new int[] { 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1 };
      var actual = 670530.GetDigitsReversed(2).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=2"));

      expected = new int[] { 0, 3, 5, 0, 7, 6 };
      actual = 670530.GetDigitsReversed(10).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=10"));

      expected = new int[] { 2, 4, 11, 3, 10 };
      actual = 670530.GetDigitsReversed(16).ToArray();
      CollectionAssert.AreEqual(expected, actual, (string)(nameof(Flux.Fx.GetDigits) + ".Radix=16"));
    }

    [TestMethod]
    public void GetPlaceValues()
    {
      var expected = new int[] { 2, 10, 500 };
      var actual = 512.GetDigitPlaceValues(10);
      Assert.AreEqual(expected.Length, actual.Count());
      for (var i = expected.Length - 1; i >= 0; i--)
        Assert.AreEqual(expected[i], actual[i], $"{nameof(GetPlaceValues)} index {i}, expected {expected[i]} != actual {expected[i]}");
    }

    //[TestMethod]
    //public void IntegerLog()
    //{
    //  var (logFloor, logCeiling) = Flux.Quantities.Radix.IntegerLog(512.ToBigInteger(), 10);

    //  Assert.AreEqual(2.ToBigInteger(), logFloor);
    //  Assert.AreEqual(3.ToBigInteger(), logCeiling);
    //}

    [TestMethod]
    public void IntegerLogAwayFromZero()
    {
      Assert.AreEqual(3, (512 - 1).IntegerLogAwayFromZero(10));
    }

    [TestMethod]
    public void IntegerLogTowardZero()
    {
      Assert.AreEqual(2, 512.IntegerLogTowardZero(10));
    }

    [TestMethod]
    public void IsIntegerPowOf()
    {
      Assert.AreEqual(false, 511.IsIntegerPowOf(2));
      Assert.AreEqual(true, 512.IsIntegerPowOf(2));
    }

    [TestMethod]
    public void IsJumbled()
    {
      Assert.AreEqual(false, 512.IsJumbled(10));
    }

    [TestMethod]
    public void IsSingleDigit()
    {
      Assert.AreEqual(false, 512.IsSingleDigit(10));
    }

    [TestMethod]
    public void KeepLeastSignificantDigit()
    {
      Assert.AreEqual(7, 1234567.KeepLeastSignificantDigit(10));
    }

    [TestMethod]
    public void KeepLeastSignificantDigits()
    {
      Assert.AreEqual(567, 1234567.KeepLeastSignificantDigits(10, 3));
    }

    [TestMethod]
    public void KeepMostSignificantDigits()
    {
      Assert.AreEqual(123, 1234567.KeepMostSignificantDigits(10, 3));
    }

    //[TestMethod]
    //public void LocateIntegerPowOf()
    //{
    //  var value = 1234567;

    //  var pow10tz = value.PowOfTowardZero(10, false);
    //  var pow10afz = value.PowOfAwayFromZero(10, false);

    //  Assert.AreEqual(1000000, pow10tz);
    //  Assert.AreEqual(10000000, pow10afz);
    //}

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
      Assert.AreEqual(7654321, 1234567.ReverseDigits(10));
    }

    [TestMethod]
    public void SelfNumber()
    {
      var expected = new int[] { 1, 3, 5, 7, 9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 110, 121, 132, 143, 154, 165, 176, 187, 198, 209, 211, 222, 233, 244, 255, 266, 277, 288, 299, 310, 312, 323, 334, 345, 356, 367, 378, 389, 400, 411, 413, 424, 435, 446, 457, 468, 479, 490 };
      var actual = System.Linq.Enumerable.Range(1, 500).Where(i => i.IsSelfNumber(10)).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToRadixString()
    {
      Assert.AreEqual("1234567", 1234567.ToRadixString(10).ToString());
    }

    [TestMethod]
    public void ToSubscriptString()
    {
      Assert.AreEqual("₁₂₃₄₅₆₇", 1234567.ToSubscriptString(10).ToString());
    }

    [TestMethod]
    public void ToSuperscriptString()
    {
      Assert.AreEqual("¹²³⁴⁵⁶⁷", 1234567.ToSuperscriptString(10).ToString());
    }
  }
}
