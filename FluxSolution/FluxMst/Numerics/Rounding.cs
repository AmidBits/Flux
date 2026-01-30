#if NET7_0_OR_GREATER
using Flux;

namespace Maths
{
  [TestClass]
  public class Rounding
  {
    [TestMethod]
    public void Round()
    {
      Assert.AreEqual(-6, double.Envelop(-5.5));
      Assert.AreEqual(6, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToEven));
      Assert.AreEqual(6, IFloatingPoint.RoundHalf(5.5, HalfRounding.AwayFromZero));
      Assert.AreEqual(5, IFloatingPoint.RoundHalf(5.5, HalfRounding.TowardZero));
      Assert.AreEqual(5, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToNegativeInfinity));
      Assert.AreEqual(6, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToPositiveInfinity));
      Assert.AreEqual(5, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToAlternating)); // First alternating state. This is also {value}.RoundHalfAlternating().
      Assert.AreEqual(6, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToAlternating)); // Second alternating state. This is also {value}.RoundHalfAlternating().
      Assert.IsTrue(IFloatingPoint.RoundHalf(5.5, HalfRounding.ToRandom) is 5 or 6); // This is also {value}.RoundHalfToRandom(). (below)
      Assert.AreEqual(5, IFloatingPoint.RoundHalf(5.5, HalfRounding.ToOdd)); // This is also {value}.RoundHalfToOdd(). (below)

      //Assert.AreEqual(5, (5.5).RoundHalfToAlternating()); // Third alternating state.
      //Assert.AreEqual(6, (5.5).RoundHalfToAlternating()); // Fourth alternating state.
      //Assert.IsTrue((5.5).RoundHalfToRandom() is 5 or 6);
      //Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, INumber.RoundToNearest(12, HalfRounding.AwayFromZero, false, [7, 17]), HalfRounding.AwayFromZero.ToString());
      Assert.AreEqual(7, INumber.RoundToNearest(12, HalfRounding.TowardZero, false, [7, 17]), HalfRounding.TowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      var (multipleTowardsZero, _, multipleAwayFromZero) = INumber.MultipleOf(n, m, false, HalfRounding.AwayFromZero);
      //n.MultipleOfNearest(m, false, HalfRounding.AwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, INumber.RoundToNearest(n, HalfRounding.AwayFromZero, false, [multipleTowardsZero, multipleAwayFromZero]), $"{nameof(RoundToMultipleOf)} {HalfRounding.AwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, IFloatingPoint.RoundByPrecision(99.96535789, HalfRounding.ToEven, 2, 10));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, IFloatingPoint.RoundByTruncatedPrecision(99.96535789, HalfRounding.ToEven, 2, 10));
    }
  }
}
#endif
