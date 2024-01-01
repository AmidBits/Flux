#if NET7_0_OR_GREATER
using System;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericMath
{
  [TestClass]
  public class Rounding
  {
    [TestMethod]
    public void Round()
    {
      Assert.AreEqual(6, (5.5).RoundAwayFromZero());
      Assert.AreEqual(5, (5.5).RoundTowardZero());
      Assert.AreEqual(5, (5.5).RoundToNegativeInfinity());
      Assert.AreEqual(6, (5.5).RoundToPositiveInfinity());
      Assert.AreEqual(5, (5.5).RoundHalfToOdd());
      Assert.AreEqual(6, (5.5).RoundHalfToEven());
      Assert.AreEqual(6, (5.5).RoundHalfAwayFromZero());
      Assert.AreEqual(5, (5.5).RoundHalfTowardZero());
      Assert.AreEqual(5, (5.5).RoundHalfToNegativeInfinity());
      Assert.AreEqual(6, (5.5).RoundHalfToPositiveInfinity());
    }

    [TestMethod]
    public void RoundToBoundary()
    {
      Assert.AreEqual(17, (12.ToBigInteger()).RoundToBoundaries(RoundingMode.HalfAwayFromZero, 7, 17), RoundingMode.HalfAwayFromZero.ToString());
      Assert.AreEqual(7, (12.ToBigInteger()).RoundToBoundaries(RoundingMode.HalfTowardsZero, 7, 17), RoundingMode.HalfTowardsZero.ToString());
    }

    [TestMethod]
    public void RoundToMultipleOf()
    {
      var n = 1.75;
      var m = 0.45;

      n.RoundToMultipleOf(m, false, RoundingMode.HalfAwayFromZero, out var multipleTowardsZero, out var multipleAwayFromZero);

      Assert.AreEqual(1.8, n.RoundToBoundaries(Flux.RoundingMode.HalfAwayFromZero, multipleTowardsZero, multipleAwayFromZero), $"{nameof(RoundToMultipleOf)} {Flux.RoundingMode.HalfAwayFromZero}");
    }

    [TestMethod]
    public void RoundToPrecision()
    {
      Assert.AreEqual(99.97, (99.96535789).RoundToPrecision(Flux.RoundingMode.HalfToEven, 2));
    }

    [TestMethod]
    public void RoundToTruncatedPrecision()
    {
      Assert.AreEqual(99.96, (99.96535789).RoundToTruncatedPrecision(Flux.RoundingMode.HalfToEven, 2));
    }
  }
}
#endif
