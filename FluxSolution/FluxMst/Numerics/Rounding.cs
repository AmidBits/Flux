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
      Assert.AreEqual(-6, FloatingPoint.Envelop(-5.5));
      Assert.AreEqual(6, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.ToEven).RoundedInteger);
      Assert.AreEqual(6, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.AwayFromZero).RoundedInteger);
      Assert.AreEqual(5, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.TowardZero).RoundedInteger);
      Assert.AreEqual(5, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.TowardNegativeInfinity).RoundedInteger);
      Assert.AreEqual(6, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.TowardPositiveInfinity).RoundedInteger);
      //Assert.AreEqual(5, FloatingPoint.RoundToNearestInteger(5.5, MidpointRoundingEx.Alternating)); // First alternating state. This is also {value}.RoundHalfAlternating().
      //Assert.AreEqual(6, FloatingPoint.RoundToNearestInteger(5.5, MidpointRoundingEx.Alternating)); // Second alternating state. This is also {value}.RoundHalfAlternating().
      Assert.IsTrue(FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.Random).RoundedInteger is 5 or 6); // This is also {value}.RoundHalfToRandom(). (below)
      Assert.AreEqual(5, FloatingPoint.RoundToInteger(5.5, NearestRoundingRule.ToOdd).RoundedInteger); // This is also {value}.RoundHalfToOdd(). (below)

      //Assert.AreEqual(5, (5.5).RoundHalfToAlternating()); // Third alternating state.
      //Assert.AreEqual(6, (5.5).RoundHalfToAlternating()); // Fourth alternating state.
      //Assert.IsTrue((5.5).RoundHalfToRandom() is 5 or 6);
      //Assert.AreEqual(5, (5.5).RoundHalfToOdd());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, Number.RoundToValue(12, NearestRoundingRule.AwayFromZero, false, 7, 17), NearestRoundingRule.AwayFromZero.ToString());
      Assert.AreEqual(7, Number.RoundToValue(12, NearestRoundingRule.TowardZero, false, 7, 17), NearestRoundingRule.TowardZero.ToString());
    }

    [TestMethod]
    public void RoundToMultiple()
    {
      var n = 1.75;
      var m = 0.45;

      var (multipleTowardsZero, multipleAwayFromZero, isExactMultiple, nearestMultiple) = Number.RoundToMultiple(n, m, NearestRoundingRule.AwayFromZero, false);

      Assert.AreEqual(1.8, nearestMultiple, $"{nameof(RoundToMultiple)} {NearestRoundingRule.AwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, FloatingPoint.RoundByPrecision(99.96535789, NearestRoundingRule.ToEven, 2, 10));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, FloatingPoint.RoundByTruncatedPrecision(99.96535789, NearestRoundingRule.ToEven, 2, 10));
    }
  }
}
#endif
