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
      Assert.AreEqual(6, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToEven));
      Assert.AreEqual(6, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.AwayFromZero));
      Assert.AreEqual(5, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.TowardZero));
      Assert.AreEqual(5, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToNegativeInfinity));
      Assert.AreEqual(6, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToPositiveInfinity));
      Assert.AreEqual(5, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToAlternating)); // First alternating state. This is also {value}.RoundHalfAlternating().
      Assert.AreEqual(6, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToAlternating)); // Second alternating state. This is also {value}.RoundHalfAlternating().
      Assert.IsTrue(FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToRandom) is 5 or 6); // This is also {value}.RoundHalfToRandom(). (below)
      Assert.AreEqual(5, FloatingPoint.RoundMidpoint(5.5, MidpointRoundingEx.ToOdd)); // This is also {value}.RoundHalfToOdd(). (below)

      //Assert.AreEqual(5, (5.5).RoundHalfToAlternating()); // Third alternating state.
      //Assert.AreEqual(6, (5.5).RoundHalfToAlternating()); // Fourth alternating state.
      //Assert.IsTrue((5.5).RoundHalfToRandom() is 5 or 6);
      //Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, Number.RoundToNearest(12, MidpointRoundingEx.AwayFromZero, false, [7, 17]), MidpointRoundingEx.AwayFromZero.ToString());
      Assert.AreEqual(7, Number.RoundToNearest(12, MidpointRoundingEx.TowardZero, false, [7, 17]), MidpointRoundingEx.TowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      var (multipleTowardsZero, _, multipleAwayFromZero) = Number.MultipleOf(n, m, false, MidpointRoundingEx.AwayFromZero);
      //n.MultipleOfNearest(m, false, HalfRounding.AwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, Number.RoundToNearest(n, MidpointRoundingEx.AwayFromZero, false, [multipleTowardsZero, multipleAwayFromZero]), $"{nameof(RoundToMultipleOf)} {MidpointRoundingEx.AwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, FloatingPoint.RoundByPrecision(99.96535789, MidpointRoundingEx.ToEven, 2, 10));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, FloatingPoint.RoundByTruncatedPrecision(99.96535789, MidpointRoundingEx.ToEven, 2, 10));
    }
  }
}
#endif
